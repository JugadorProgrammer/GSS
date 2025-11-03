using GSS.Domain.DomainModelServices;
using GSS.Domain.ResultObjects.OperationResults.Connection;
using GSS.Domain.ResultObjects.OperationResults.Group;
using GSS.Domain.ResultObjects.OperationResults.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace GSS.Application
{
    [Authorize]
    public class ConnectionHub : Hub
    {
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        private readonly IConnectionService _connectionService;

        public ConnectionHub(IUserService userService, IGroupService groupService, IConnectionService connectionService) 
        {
            _userService = userService;
            _groupService = groupService;
            _connectionService = connectionService;
        }

        public async Task SendHello(Guid groupId)
        {
            var result = await _userService.GetAuthorizeUserId();
            if (result.OperationResult == AuthorizeUserOperationResult.Unauthorized)
            {
                await Clients.Caller.SendAsync("User Unauthorized");
                await Context.GetHttpContext()!.SignOutAsync();
                return;
            }

            var resultGetGroup = await _groupService.GetGroup(groupId);
            if (resultGetGroup.OperationResult == GroupGetOperationResult.NotFound)
            {
                await Clients.Caller.SendAsync("GroupNotFound");
            }

            var group = resultGetGroup.Model;
            if (group is null)
            {
                await SendFailedToCaller();
                return;
            }

            var userId = result.Model;
            var connectionResult = await _connectionService.CreateConnection(Context.ConnectionId, userId, groupId);
            if (connectionResult == ConnetionOperationResult.Failed)
            {
                await SendFailedToCaller();
                return;
            }

            // TODO: подумать, что будет, если пользователь уже подключён, просто отвалился от хаба.
            await Groups.AddToGroupAsync(Context.ConnectionId, group.Name);
            await Clients.Group(group.Name).SendAsync("NewUser");
            await Clients.Caller.SendAsync("SendOffer");
        }

        public async Task ForwardOffer(object offer)
        {
            await Clients.All.SendAsync("ReceiveOffer", offer);
        }

        public async Task ForwardAnswer(object asnwer)
        {
            await Clients.All.SendAsync("ReceiveAnswer", asnwer);
        }


        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception is null)
            {

            }

            var result = await _connectionService.GetConnection(Context.ConnectionId);
            if (result.OperationResult == ConnetionOperationResult.Failed || result.Model is null)
            {
                // TODO: придумать, как обработать этот случай
                return;
            }

            var connection = result.Model;
            var userName = connection.User.Login;
            var groupName = connection.Group.Name;

            await Clients.Group(groupName).SendAsync("ExitUser", userName);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            await _connectionService.RemoveConnection(Context.ConnectionId);
        }

        private async Task SendFailedToCaller()
        {
            await Clients.Caller.SendAsync("Failed");
        }
    }
}
