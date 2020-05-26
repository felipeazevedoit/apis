using System.Linq;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TServices.Comum.Model;
using TServices.Comum.Patterns.DomainNotification.Handler;

namespace TServices.Comum.Patterns.DomainNotification.Api
{
    public class ApiController : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private readonly NotifyHandler _messageHandler;

        protected ApiController(INotificationHandler<DomainNotifications> notification)
        {
            _messageHandler = (NotifyHandler)notification;
        }

        protected IActionResult CreatedHasNotification(object result = null)
        {
            if (!HasNotifications())
            {
                if (result != null)
                    return StatusCode(HttpStatusCode.OK.GetHashCode(), new
                    {
                        success = true,
                        data = result
                    });

                return StatusCode(HttpStatusCode.OK.GetHashCode(), new
                {
                    success = true
                });
            }

            return NoticationsEntity();
        }

        protected bool HasNotifications()
        {
            return _messageHandler.HasNotifications();
        }

        protected IActionResult NoticationsEntity()
        {
            var notifications =
                _messageHandler.GetNotifications();

            if (notifications.Any())
            {
                return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), notifications.ToList());
            }

            return StatusCode(HttpStatusCode.BadRequest.GetHashCode(), notifications.ToList());
        }
    }
}