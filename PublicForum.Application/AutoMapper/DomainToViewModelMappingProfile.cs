using AutoMapper;
using PublicForum.Application.ViewModels;
using PublicForum.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Topic, TopicViewModel>();
        }
    }
}
