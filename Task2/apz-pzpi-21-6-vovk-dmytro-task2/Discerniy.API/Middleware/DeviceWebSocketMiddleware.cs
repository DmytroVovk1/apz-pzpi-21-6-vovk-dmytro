using Discerniy.Domain.Entity.SubEntity;
using Discerniy.Domain.Interface.Services;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Discerniy.API.Middleware
{
    public class DeviceWebSocketMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<DeviceWebSocketMiddleware> logger;

        public DeviceWebSocketMiddleware(RequestDelegate next, ILogger<DeviceWebSocketMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            if(context.Request.Path != "/connect/device")
            {
                await next(context);
                return;
            }

            if (context.WebSockets.IsWebSocketRequest)
            {
                IDeviceWebSocketCommandHandler deviceWebSocketCommandHandler = context.RequestServices.GetRequiredService<IDeviceWebSocketCommandHandler>();

                string userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

                var socket = await context.WebSockets.AcceptWebSocketAsync();
                while (socket.State != WebSocketState.Closed && !context.RequestAborted.IsCancellationRequested)
                {
                    var receiveBuffer = new byte[1024];
                    var receiveResult = await socket.ReceiveAsync(receiveBuffer, context.RequestAborted);
                    if (receiveResult.MessageType != WebSocketMessageType.Text || receiveResult.EndOfMessage == false)
                    {
                        logger.LogDebug("Received invalid message type");
                        continue;
                    }
                    var message = Encoding.UTF8.GetString(receiveBuffer, 0, receiveResult.Count);
                    await deviceWebSocketCommandHandler.HandleCommand(userId, socket, context, message, context.RequestAborted);
                }
            }
            else
            {
                context.Response.StatusCode = 405;
            }
        }
    }
}
