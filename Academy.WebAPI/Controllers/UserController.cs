using Academy.Application.Interfaces;
using Academy.Application.ViewModels;
using Academy.Domain.Core.Notifications;
using Academy.WebAPI.Controllers.Base;
using Academy.WebAPI.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Academy.WebAPI.Controllers
{
    [Route("api/User")]
    [Authorize("Bearer")]
    public class UserController : BaseController
    {
        private readonly IUserAppService _userAppService;

        public UserController(IUserAppService userAppService,
                                INotificationHandler<DomainNotification> notifications) : base(notifications)
        {
            _userAppService = userAppService;
        }

        [HttpGet]
        [Route("get-all")]
        public IActionResult Get() => Response(_userAppService.GetAll());

        [HttpGet]
        [Route("get/{id:guid}")]
        public IActionResult Get(Guid id)
        {
            if (id == Guid.Empty) return NotFound();
            var user = _userAppService.Get(id);
            if (user == null) return NotFound();
            return Response(user);
        }

        [HttpPost]
        [Route("add")]
        [TokenPostFilter]
        public IActionResult Post([FromBody]UserViewModel entity)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(entity);
            }
            _userAppService.Register(entity);
            return Response(entity);
        }

        [HttpPut]
        [Route("update")]
        [TokenPutFilter]
        public IActionResult Put([FromBody]UserViewModel entity)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(entity);
            }
            _userAppService.Update(entity);
            return Response(entity);
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult Delete(Guid id)
        {
            _userAppService.Remove(id);

            return Response();
        }

        [HttpGet]
        [Route("verify-password")]
        public IActionResult VerifyPassword(string email)
        {
            var user = _userAppService.GetByEmail(email);
            if (user == null) return Response(new { message = $"Este email: {email} não está cadastrado no nosso banco de dados" });
            return Response(new { userId = user.UserId, isVerified = user.IsVerified });
        }

        [HttpPost]
        [Route("update-password")]
        public IActionResult UpdatePassword([FromBody]UserUpdatePasswordViewModel entity)
        {
            if (!ModelState.IsValid)
            {
                NotifyModelStateErrors();
                return Response(entity);
            }
            _userAppService.UpdatePassword(entity);
            return Response(entity);
        }
    }
}