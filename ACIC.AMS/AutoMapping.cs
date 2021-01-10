using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ACIC.AMS.Web
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Dto.User, Domain.Models.User>().ForMember(x => x.DateCreated, act => act.Ignore());
            CreateMap<Domain.Models.User, Dto.User>().ForMember(x => x.DateCreated, act => act.Ignore());
            CreateMap<Domain.Models.Account, Dto.Account>();
            CreateMap<Dto.Account, Domain.Models.Account>();
            CreateMap<Dto.Agent, Domain.Models.Agent>();
            CreateMap<Domain.Models.Agent, Dto.Agent>();
            CreateMap<Domain.Models.DdUsstate, Dto.UsState>();
            CreateMap<Dto.UsState, Domain.Models.DdUsstate >();
            CreateMap<Dto.Driver, Domain.Models.Driver>();
            CreateMap<Domain.Models.Driver, Dto.Driver>();
            CreateMap<Domain.Models.DdContactsTitle, Dto.ContactsTitle>();
            CreateMap<Domain.Models.Contact, Dto.Contact>();
            CreateMap<Dto.Contact, Domain.Models.Contact>();
            CreateMap<Domain.Models.DdVehicleMake, Dto.VehicleMake>();
            CreateMap<Domain.Models.Bank, Dto.Bank>();
            CreateMap<Dto.Vehicle, Domain.Models.Vehicle>();
            CreateMap<Dto.Policy, Domain.Models.Policy>();
            CreateMap<Domain.Models.Carrier, Dto.Carrier>();
            CreateMap<Domain.Models.Mga, Dto.Mga>();
        }
    }
}
