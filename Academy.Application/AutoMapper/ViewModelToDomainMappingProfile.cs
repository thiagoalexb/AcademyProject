using Academy.Application.ViewModels;
using Academy.Domain.Commands;
using Academy.Domain.Entities;
using AutoMapper;
using System.Collections.Generic;

namespace Academy.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<UserViewModel, RegisterNewUserCommand>()
                .ConstructUsing(c => new RegisterNewUserCommand(c.FirstName, c.LastName, c.Email, c.Password, c.DateOfBirth,
                                                c.CreationDate, c.CreatorUserId, c.LastUpdateDate, c.LastUpdatedUserId));

            CreateMap<UserViewModel, UpdateUserCommand>()
                .ConstructUsing(c => new UpdateUserCommand(c.UserId, c.FirstName, c.LastName, c.Email, c.Password, c.DateOfBirth,
                                                c.CreationDate, c.CreatorUserId, c.LastUpdateDate, c.LastUpdatedUserId));

            CreateMap<UserViewModel, User>()
                .ConstructUsing(c => new User(c.UserId, c.FirstName, c.LastName, c.Email, c.Password, c.DateOfBirth,
                                                c.CreationDate, c.CreatorUserId, c.LastUpdateDate, c.LastUpdatedUserId));
        }
    }
}
