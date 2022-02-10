using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Models;
using TravelPalAPI.ViewModels.Accommodation;
using TravelPalAPI.ViewModels.AccommodationDetails;
using TravelPalAPI.ViewModels.AccommodationImage;
using TravelPalAPI.ViewModels.Event;
using TravelPalAPI.ViewModels.EventImages;
using TravelPalAPI.ViewModels.Identity;
using TravelPalAPI.ViewModels.Location;
using TravelPalAPI.ViewModels.Message;
using TravelPalAPI.ViewModels.PaymentInfo;
using TravelPalAPI.ViewModels.Reservation;

namespace TravelPalAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
    
        public AutoMapperProfiles( )
        {
            //AccommodationImage
            CreateMap<AccommodationImage, AccommodationImageVM>()
                .ForMember(x => x.Id, opts => opts.MapFrom(x => x.Id))
                .ForMember(x => x.ImagePath, opts => opts.MapFrom(x => x.ImagePath));
            //AcommodationDetails
            CreateMap<AccommodationDetailsCreationVM, AccommodationDetails>().ReverseMap();
            CreateMap<AccommodationDetailsVM, AccommodationDetails>().ReverseMap();
            //Location
            CreateMap<LocationCreationVM, Location>();
            CreateMap<LocationVM, Location>().ReverseMap()
                .ForMember(x=>x.City,opts=>opts.MapFrom(x=>x.City.Name))
                .ForMember(x=>x.Country,opts=>opts.MapFrom(x=>x.City.Country.Name))
                .ForMember(x=>x.CountryId,opts=>opts.MapFrom(x=>x.City.Country.Id));
            //CreateMap<Location, LocationVM>().ReverseMap();
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
                vm => vm.MapFrom(x => x.AccommodationDetails))
                 .ForMember(X => X.User,
                vm => vm.MapFrom(x => x.Host))
                 .ForMember(X => X.Images,
                vm => vm.MapFrom(x => x.AccommodationImages));
            //Event
            CreateMap<EventCreationVM, Event>()
                .ForMember(x => x.Location,
                vm => vm.MapFrom(x => x.LocationVM));

            CreateMap<Event, EventVM>()
                .ForMember(x => x.LocationVM,
                vm => vm.MapFrom(x => x.Location))
                .ForMember(x => x.User, 
                vm => vm.MapFrom(x => x.Host))
                .ForMember(x => x.Images,
                vm => vm.MapFrom(x => x.Images));

            CreateMap<EventImages, EventImagesVM>()
                .ForMember(x => x.Id, 
                vm => vm.MapFrom(x => x.Id))
               .ForMember(x => x.ImagePath,
                vm => vm.MapFrom(x => x.ImagePath));

            //User
            CreateMap<UserAccount, UserVM>()
                .ForMember(x => x.Id, user => user.MapFrom(x => x.Id))
                .ForMember(x => x.UserName, user => user.MapFrom(x => x.UserName));

            //Reservation
            CreateMap<ReservationCreationVM, Reservation>()
                .ForMember(x=>x.PaymentInfo,opts=>opts.MapFrom(x=>x.PaymentInfo));
            CreateMap<Reservation, ReservationVM>()
                .ForMember(x => x.Status, opts => opts.MapFrom(x => x.Status.Description))
                .ForMember(x => x.Accommodation, opts => opts.MapFrom(x => x.Accommodation.Name));

            //PaymentInfo
            CreateMap<PaymentInfoCreationVM, PaymentInfo>();


            CreateMap<Message, MessageVM>()
                .ForMember(x => x.SenderPhotoUrl, 
                opt => opt.MapFrom(src => src.Sender.Picture.FirstOrDefault()))
                .ForMember(x => x.RecipientPhotoUrl, 
                opt => opt.MapFrom(src => src.Recipient.Picture.FirstOrDefault()));
        }
    }
}
