﻿using Academy.Application.ViewModels;
using Academy.Domain.Entities;
using AutoMapper;

namespace Academy.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<User, UserViewModel>();
        }
    }
}
