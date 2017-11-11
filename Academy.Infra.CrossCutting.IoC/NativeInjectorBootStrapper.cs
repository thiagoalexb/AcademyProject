using Academy.Application.Interfaces;
using Academy.Application.Services;
using Academy.Domain.CommandHandlers;
using Academy.Domain.Commands;
using Academy.Domain.Core.Bus;
using Academy.Domain.Core.Events;
using Academy.Domain.Core.Notifications;
using Academy.Domain.Entities;
using Academy.Domain.EvemtHandlers;
using Academy.Domain.Events;
using Academy.Domain.Interfaces;
using Academy.Domain.Interfaces.Core;
using Academy.Domain.Services;
using Academy.Domain.Services.Interfaces;
using Academy.Infra.CrossCutting.Bus;
using Academy.Infra.Data.Context;
using Academy.Infra.Data.Repositories;
using Academy.Infra.Data.Repositories.EventSourcing;
using Academy.Infra.Data.UnitOfWork;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Academy.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IMapper mapper)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Domain Bus (Mediator)
            services.AddScoped<IMediatorHandler, InMemoryBus>();

            // Application
            services.AddSingleton(mapper);
            services.AddScoped<IUserAppService, UserAppService>();
            services.AddScoped<IErrorLogAppService, ErrorLogAppService>();

            // Domain - Events
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<UserRegisteredEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<UserUpdatedEvent>, UserEventHandler>();
            services.AddScoped<INotificationHandler<UserRemovedEvent>, UserEventHandler>();

            // Domain - Commands
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<INotificationHandler<RegisterNewUserCommand>, UserCommandHandler>();
            services.AddScoped<INotificationHandler<UpdateUserCommand>, UserCommandHandler>();
            services.AddScoped<INotificationHandler<RemoveUserCommand>, UserCommandHandler>();

            // Infra - Data
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IErrorLogRepository, ErrorLogRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbContext, AcademyContext>();

            // Infra - Data EventSourcing
            services.AddScoped<IEventStoreRepository, EventStoreSQLRepository>();
            services.AddScoped<IEventStore, SqlEventStore>();

            // Infra - User
            services.AddScoped<IUser, User>();

            //Services
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
