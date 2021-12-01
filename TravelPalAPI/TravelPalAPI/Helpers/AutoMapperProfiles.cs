using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.AccommodationDetails;
using TravelPalAPI.ViewModels.Event;
using TravelPalAPI.ViewModels.Location;

namespace TravelPalAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
    
        public AutoMapperProfiles( )
        {
            //AcommodationDetails
            CreateMap<AccommodationDetailsCreationVM, AccommodationDetails>().ReverseMap();
            CreateMap<AccommodationDetailsVM, AccommodationDetails>().ReverseMap();
            //Location
            CreateMap<LocationCreationVM, Location>().ReverseMap();
            CreateMap<LocationVM, Location>().ReverseMap();
            CreateMap<Location, LocationVM>().ReverseMap();
            //Accomodation
            CreateMap<AccommodationCreationVM, Accommodation>()
                .ForMember(X => X.Location,
                vm => vm.MapFrom(x => x.Location))
                .ForMember(X => X.AccommodationDetails,
                vm => vm.MapFrom(x => x.AccommodationDetails));

            CreateMap<Accommodation, AccommodationVM>()
                .ForMember(X => X.Location,
                vm => vm.MapFrom(x => x.Location))
                .ForMember(X => X.AccommodationDetails,
                vm => vm.MapFrom(x => x.AccommodationDetails));
            //Event
            CreateMap<EventCreationVM, Event>()
                .ForMember(x => x.Location,
                vm => vm.MapFrom(x => x.LocationVM));
            CreateMap<Event, EventVM>()
                .ForMember(x => x.LocationVM,
                vm => vm.MapFrom(x => x.Location));

        }
    }
}
