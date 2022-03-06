using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelPalAPI.Database;
using TravelPalAPI.Models;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestDataController : ControllerBase
    {
        private readonly AppDbContext appDb;
        private readonly UserManager<UserAccount> userManager;

        public TestDataController(AppDbContext appDb, UserManager<UserAccount> userManager)
        {
            this.appDb = appDb;
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult Count()
        {
            Dictionary<string, int> data = new Dictionary<string, int>();
            data.Add("Users", appDb.UserAccounts.Count());
            data.Add("Countries", appDb.Countries.Count());
            data.Add("Cities", appDb.Cities.Count());
            data.Add("Statuese", appDb.Statuses.Count());
            data.Add("Accommodations", appDb.Accommodations.Count());
            data.Add("Events", appDb.Events.Count());

            return Ok(data);
        }

        [HttpPost("location-status")]
        public ActionResult GenerateLocationsStatueses()
        {
            var countries = new List<Country>();
            var cities = new List<City>();
            var statuses = new List<Status>
            {
                new Status { Description = "Active" },
                new Status { Description = "Cancelled" },
                new Status { Description = "Completed" }
            };

            countries.Add(new Country { Name = "Bosnia and Herzegovina" });
            countries.Add(new Country { Name = "Croatia" });
            countries.Add(new Country { Name = "Serbia" });
            countries.Add(new Country { Name = "Montenegro" });

            cities.Add(new City { Country = countries[0], Name = "Sarajevo" });
            cities.Add(new City { Country = countries[0], Name = "Banja Luka" });
            cities.Add(new City { Country = countries[0], Name = "Mostar" });
            cities.Add(new City { Country = countries[0], Name = "Tuzla" });
            cities.Add(new City { Country = countries[0], Name = "Konjic" });
            cities.Add(new City { Country = countries[0], Name = "Jablanica" });
            cities.Add(new City { Country = countries[0], Name = "Neum" });

            cities.Add(new City { Country = countries[1], Name = "Zagreb" });
            cities.Add(new City { Country = countries[1], Name = "Dubrovnik" });
            cities.Add(new City { Country = countries[1], Name = "Split" });
            cities.Add(new City { Country = countries[1], Name = "Makarska" });
            cities.Add(new City { Country = countries[1], Name = "Hvar" });
            cities.Add(new City { Country = countries[1], Name = "Zadar" });

            cities.Add(new City { Country = countries[2], Name = "Beograd" });
            cities.Add(new City { Country = countries[2], Name = "Novi Sad" });
            cities.Add(new City { Country = countries[2], Name = "Smederevo" });
            cities.Add(new City { Country = countries[2], Name = "Sombor" });
            cities.Add(new City { Country = countries[2], Name = "Subotica" });
            cities.Add(new City { Country = countries[2], Name = "Niš" });

            cities.Add(new City { Country = countries[3], Name = "Podgorica" });
            cities.Add(new City { Country = countries[3], Name = "Bijelo Polje" });
            cities.Add(new City { Country = countries[3], Name = "Nikšić" });
            cities.Add(new City { Country = countries[3], Name = "Herceg Novi" });
            cities.Add(new City { Country = countries[3], Name = "Kotor" });
            cities.Add(new City { Country = countries[3], Name = "Budva" });



            appDb.AddRange(statuses);
            appDb.AddRange(countries);
            appDb.AddRange(cities);
            appDb.SaveChanges();
            return Count();
        }

        [HttpPost("users")]
        public async Task<ActionResult> GenerateUsers()
        {
            var users = new List<UserAccount>();
            string password = "Password123*";
            var verified = new Claim("status", "verified");
            var admin = new Claim("role", "admin");

            users.Add(new UserAccount
            {
                UserName = "admin",
                Email = "admin@user.com",
                EmailConfirmed = true,
                FirstName = "Neal",
                LastName = "Harrison",
                PhoneNumber = "+225-883",
                PhoneNumberConfirmed = true,
                Picture = "https://res.cloudinary.com/travelpal/image/upload/v1646346560/users/default_ordioo.jpg"
            });
            users.Add(new UserAccount
            {
                UserName = "host1",
                Email = "host1@user.com",
                EmailConfirmed = true,
                FirstName = "Darius",
                LastName = "Stone",
                PhoneNumber = "+225-883",
                PhoneNumberConfirmed = true,
                Picture = "https://res.cloudinary.com/travelpal/image/upload/v1646427466/users/user2_hvmyfc.jpg"
            });
            users.Add(new UserAccount
            {
                UserName = "host2",
                Email = "host2@user.com",
                EmailConfirmed = true,
                FirstName = "Thomas",
                LastName = "Shelby",
                PhoneNumber = "+225-883",
                PhoneNumberConfirmed = true,
                Picture = "https://res.cloudinary.com/travelpal/image/upload/v1646427385/users/user1_n1gexi.png"
            });

            foreach (var u in users)
            {
                await userManager.CreateAsync(u, password);
            }

            await userManager.AddClaimAsync(users[0], admin);

            foreach (var u in users)
            {
                await userManager.AddClaimAsync(u, verified);
            }

            return Count();
        }

        [HttpPost("accommodations")]
        public IActionResult GenerateAccommodations()
        {
            var users = appDb.UserAccounts.Take(3).ToList();
            var cities = appDb.Cities.OrderBy(x => x.Name).Take(10).ToList();

            var accommodations = new List<Accommodation>();

            var images1 = new List<AccommodationImage>()
            {
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646346650/accommodations/stay1/1_linufi.webp"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646346649/accommodations/stay1/2_lexz8a.webp"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646346656/accommodations/stay1/3_rsvmgt.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646346656/accommodations/stay1/4_okxf6x.webp"},
            };

            var images2 = new List<AccommodationImage>()
            {
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646346696/accommodations/stay2/4_mloewu.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646346695/accommodations/stay2/1_xpm1i5.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646346695/accommodations/stay2/3_svwxvm.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646346694/accommodations/stay2/2_hzb24p.jpg"},
            };

            var images3 = new List<AccommodationImage>()
            {
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417745/accommodations/stay3/2_zrcs6t.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417741/accommodations/stay3/1_siig0z.webp"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417740/accommodations/stay3/3_nuaxth.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417739/accommodations/stay3/4_tdmcqd.webp"},
            };

            var images4 = new List<AccommodationImage>()
            {
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417817/accommodations/stay4/1_keyd0g.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417815/accommodations/stay4/2_gfp3jq.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417818/accommodations/stay4/3_niznu0.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417817/accommodations/stay4/4_dxtnjc.jpg"},
            };

            var images5 = new List<AccommodationImage>()
            {
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417889/accommodations/stay5/1_fkn2in.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417887/accommodations/stay5/2_qpgebm.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417879/accommodations/stay5/3_e6rv9t.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417885/accommodations/stay5/4_uarlzo.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417885/accommodations/stay5/5_tmqmq3.jpg"},
            };

            var images6 = new List<AccommodationImage>()
            {
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417924/accommodations/stay6/1_fymtiz.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417927/accommodations/stay6/2_ezxuel.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417924/accommodations/stay6/3_duzfic.jpg"},
            };

            var images7 = new List<AccommodationImage>()
            {
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417943/accommodations/stay7/1_cozsyg.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417945/accommodations/stay7/2_axx7c5.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417944/accommodations/stay7/3_prv7es.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417942/accommodations/stay7/4_ewnwp2.jpg"},
            };

            var images8 = new List<AccommodationImage>()
            {
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417961/accommodations/stay8/1_l8vnur.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417962/accommodations/stay8/2_jn3zwv.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417962/accommodations/stay8/4_t63ttl.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417962/accommodations/stay8/3_pv0tzu.jpg"},
            };

            var images9 = new List<AccommodationImage>()
            {
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417985/accommodations/stay9/2_og4bbl.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417987/accommodations/stay9/3_g7vemo.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417987/accommodations/stay9/4_rloxzo.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646417985/accommodations/stay9/2_og4bbl.jpg"},
            };

            var images10 = new List<AccommodationImage>()
            {
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646418003/accommodations/stay10/1_u5oj37.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646418002/accommodations/stay10/3_evimcy.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646418002/accommodations/stay10/4_y9uqwg.jpg"},
                new AccommodationImage{ImagePath="https://res.cloudinary.com/travelpal/image/upload/v1646418002/accommodations/stay10/2_vgvqnc.jpg"},
            };

            accommodations.Add(new Accommodation
            {
                Name = "Amalthia Luxury Suite",
                Price = 261,
                Description = "A 39 m² lovely studio with private swimming pool and a hot tub, spa for 3, magnificent sea view, large furnished outdoor area around the pool, 1 bedroom, living room and a fully equipped kitchen. Bedroom features a queen size bed with anatomic mattress.The living room features a large sofa and cushions filled with goose feathers, can accommodate a child or an adult.SMART TV 43 and free NETFLIX.Bluetooth HiFi sound system.5 stars Relaxing and Prestigious accommodation with privacy!",
                Rooms = 2,
                Capacity = 3,
                Location = new Location() { CityId = cities[0].Id, Address = "First street", Latitude = 44.75736409758204, Longitude = 17.1991744642128 },
                AccommodationDetails = new AccommodationDetails()
                {
                    AirConditioning = true,
                    Balcony = true,
                    Minibar = true,
                    MosquitoNet = true,
                    BBQ = true,
                    Parking = true,
                    Cancellation = "Non-refundable: Cancel before check-in and get back only the cleaning fee, if you paid one.",
                    HouseRules = "Check-in: After 3:00 PM Checkout: 12:00 PM Self check-in with building staff No smoking No pets No parties or events"
                },
                AccommodationImages = images1,
                HostId = users[0].Id,
            });

            accommodations.Add(new Accommodation
            {
                Name = "Apollo Hotel",
                Price = 196,
                Description = "All rooms are air-conditioned and include satellite TV with international and premium channels, a minibar, and complimentary bottled water and coffee and tea making facilities. Pay TV channels are free of charge. Private parking is possible in front of the hotel for a surcharge.A wide range of coffee specialties and mixed drinks is served in lobby bar and summer terrace.The St.Hubert Restaurant is famous for its international kitchen and its traditional game specialties. The hotel's salt cave and fitness centre can be visited free of charge. There is also a Finnish sauna which is available at additional cost.Hotel is easy accessible from all highways directions.The city centre can be reached within 10 minutes by trolley bus and within 20 minutes on foot.The Mlynské Nivy Bus Station is situated 800 m away, and the Main Railway Station is 2.5 km away, Bratislava's M.R. Štefánika Airport is reachable within 8 km, and Vienna Airport can be reached within 50 km.",
                Rooms = 2,
                Capacity = 4,
                Location = new Location()
                {
                    CityId = cities[1].Id,
                    Address = "Second street",
                    Latitude = 44.80790633236112,
                    Longitude = 20.457444811368063
                },
                AccommodationDetails = new AccommodationDetails()
                {
                    AirConditioning = true,
                    Balcony = true,
                    Minibar = true,
                    MosquitoNet = true,
                    Shower = true,
                    WiFi = true,
                    Cancellation = "Cancellation and prepayment policies vary according to accommodation type. Please check what conditions may apply to each option when making your selection.",
                    HouseRules = "Check-in:14.00-00.00,Check-out:until 11.00.Guests are required to show a photo identification and credit card upon check-in.Children of any age are welcome." +
                    "All cots and extra beds are subject to availability. The minimum age for check-in is 18. Pets are allowed on request. Charges may be applicable."
                },
                AccommodationImages = images2,
                HostId = users[0].Id,
            });

            accommodations.Add(new Accommodation
            {
                Name = "Floating vacation home made from wood — Ufogel",
                Price = 338,
                Description = "The space he architecturally stunning, floating vacation home Ufogel is located between Nußdorf and Lienz and can accommodate 5 people. It includes a living room with kitchenette, 2 bedrooms—one of which features a custom-made bed made from Swiss pine—and a bathroom. The facilities in this house, constructed entirely out of wood, also include Wi-Fi, air conditioning, a high chair, a cot and a parking area. The breathtaking view of the mountains from the panorama window and the cosy warmth from the fireplace and underfloor heating complete this stunning house.",
                Rooms = 3,
                Capacity = 5,
                Location = new Location()
                {
                    CityId = cities[2].Id,
                    Address = "Third street",
                    Latitude = 43.06532474316451,
                    Longitude = 19.75112099200611
                },
                AccommodationDetails = new AccommodationDetails()
                {
                    AirConditioning = true,
                    Balcony = true,
                    Minibar = true,
                    MosquitoNet = true,
                    BBQ = true,
                    Parking = true,
                    Cancellation = "Cancelling 28 days or more before check-in no fee applies.",
                    HouseRules = "Check-in: 3:00 PM - 6:00 PM Checkout: 11:00 AM No smoking No pets No parties or events"
                },
                AccommodationImages = images3,
                HostId = users[0].Id,
            });

            accommodations.Add(new Accommodation
            {
                Name = "Holiday Inn Express - North Riverside, an IHG Hotel",
                Price = 236,
                Description = "Located in Amsterdam, 1.9 km from A'DAM Lookout, Holiday Inn Express Amsterdam - North Riverside, an IHG Hotel provides accommodation with a restaurant, private parking, a bar and a shared lounge. With free WiFi, this 3-star hotel offers a 24-hour front desk and luggage storage space. The hotel features family rooms.At the hotel, every room is equipped with a wardrobe, a flat-screen TV, a private bathroom, bed linen and towels. All units at Holiday Inn Express Amsterdam - are equipped with a seating area. The accommodation offers a continental or buffet breakfast. Holiday Inn Express Amsterdam - North Riverside, an IHG Hotel offers a terrace. Guests at the hotel will be able to enjoy activities in and around Amsterdam, like hiking and cycling. Beurs van Berlage is 2.5 km from Holiday Inn Express Amsterdam -, while Museum Ons' Lieve Heer op Solder is 2.5 km away. The nearest airport is Schiphol Airport, 22 km from the accommodation.",
                Rooms = 2,
                Capacity = 3,
                Location = new Location()
                {
                    CityId = cities[3].Id,
                    Address = "Fourth street",
                    Latitude = 42.305360027513515,
                    Longitude = 18.838157141582418
                },
                AccommodationDetails = new AccommodationDetails()
                {
                    AirConditioning = true,
                    Balcony = true,
                    Minibar = true,
                    Dryer = true,
                    Shower = true,
                    WiFi = true,
                    Cancellation = "Cancelling 5 days before check-in is free,else customer is charged 50% of full price.",
                    HouseRules = "Check-in from 15.00,check-out by 12.00. A damage deposit of EUR 50 is required on arrival. That's about 97.79BAM. This will be collected by credit card. You should be reimbursed on check-out. Your deposit will be refunded in full via credit card, subject to an inspection of the property."
                },
                AccommodationImages = images4,
                HostId = users[0].Id,
            });

            accommodations.Add(new Accommodation
            {
                Name = "Lovely Bungalow Apartmen",
                Price = 80,
                Description = "This charming bungalow house truly offers the perfect blend of modern amenities while still enjoying a rural lifestyle in the picturesque seaside of Croatia. Here you will be able to rest and enjoy a peaceful gateway from the noisy urban life and discover the charm of it's very romantic Dalmatian surroundings. Our bungalow is located in Zablaće, 6 km from Šibenik.",
                Rooms = 2,
                Capacity = 5,
                Location = new Location()
                {
                    CityId = cities[4].Id,
                    Address = "Fifth street",
                    Latitude = 42.643266197038294,
                    Longitude = 18.117454033346224
                },
                AccommodationDetails = new AccommodationDetails()
                {
                    AirConditioning = true,
                    Balcony = true,
                    Dryer = true,
                    Shower = true,
                    WiFi = true,
                    Cancellation = "- Cancelling 28 days or more before departure: no fee applies. - Cancelling between 27 and 18 days before departure: the customer is charged a service fee of €40.",
                    HouseRules = "Any resident who drinks excessively, uses premises for illegal activity or commits a nuisance will be subject to eviction. No unnecessary noise due to loud talking, radios, televisions, stereos or musical instruments is permitted.No rollerskating, skateboarding or riding bikes on the premises."
                },
                AccommodationImages = images5,
                HostId = users[1].Id,
            });

            accommodations.Add(new Accommodation
            {
                Name = "Novotel Wien City",
                Price = 309,
                Description = "The spacious, air-conditioned rooms provide a flat-screen TV, a minibar, and tea and coffee-making facilities. Austrian and international dishes are served at the modern restaurant. The rich breakfast buffet is served until 11:30 on weekends and public holidays. Guests benefit from a 24-hour front desk. On Sundays, check-out is possible until 17:00, subject to availability. A private parking garage is available for an extra charge. The Nestroyplatz and Schwedenplatz Underground Stations (lines U1 and U4) are within a 5-minute walk, providing quick connections to all major sights, as well as the Messe Wien fairgrounds. The Prater amusement park can be reached in a short walk.",
                Rooms = 3,
                Capacity = 4,
                Location = new Location()
                {
                    CityId = cities[5].Id,
                    Address = "Main road",
                    Latitude = 42.48304898522179,
                    Longitude = 18.47341108136003
                },
                AccommodationDetails = new AccommodationDetails()
                {
                    MosquitoNet = true,
                    BBQ = true,
                    Parking = true,
                    Dryer = true,
                    Shower = true,
                    WiFi = true,
                    Cancellation = "Cancellation is not possible",
                    HouseRules = "Check-in:15.00-00.00,Check-out:07.00-12.00.There is no age requirement for check-in. Pets are allowed. Charges may be applicable. When booking more than 7 rooms, different policies and additional supplements may apply."
                },
                AccommodationImages = images6,
                HostId = users[1].Id,
            });

            accommodations.Add(new Accommodation
            {
                Name = "Signature Townhouse Hyde Park",
                Price = 312,
                Description = "Signature Townhouse London Hyde Park is a beautifully restored 19th-century townhouse overlooking Hyde Park. It is a 5-minute walk from Lancaster Gate Tube Station and just 2 Tube stops from Oxford Circus' shops. The Grade II-listed Signature Townhouse London Hyde Park has elegant period features, including high ceilings and traditional sash windows. Looking onto the park, rooms feature espresso coffee makers, bathrobes and slippers. Signature Townhouse London Hyde Park has an on-site restaurant and a lounge area for drinks. The restaurant serves a continental and English breakfast at an extra cost. Light snacks are available for room service and there is a 24-hour reception and concierge services are available.",
                Rooms = 1,
                Capacity = 2,
                Location = new Location()
                {
                    CityId = cities[6].Id,
                    Address = "8 mile",
                    Latitude = 43.1655363173692,
                    Longitude = 16.467307582498837
                },
                AccommodationDetails = new AccommodationDetails()
                {
                    AirConditioning = true,
                    Balcony = true,
                    Minibar = true,
                    MosquitoNet = true,
                    BBQ = true,
                    Cancellation = "Free cancellation 3 days before check-in",
                    HouseRules = "All cots and extra beds are subject to availability. There is no age requirement for check-in. Pets are not allowed. When booking more than 3 rooms, different policies and additional supplements may apply."
                },
                AccommodationImages = images7,
                HostId = users[1].Id,
            });

            accommodations.Add(new Accommodation
            {
                Name = "The Chilworth Paddington",
                Price = 224,
                Description = "Less than a 5-minute walk from London Paddington Station and Hyde Park, this boutique hotel offers elegant rooms with free internet and satellite TV. Oxford Street’s shops are a 10-minute Tube ride away, while Buckingham Palace can be reached in 15 minutes. Rooms benefit from an en-suite bathroom with underfloor heating, free toiletries, a hairdryer and bathrobes. A daily newspaper and teacoffee facilities are provided in all rooms.Guests can choose between a full English or Continental breakfast at Shaftesbury Premier’s restaurant. An American-style dinner menu is available in the bar, which also serves drinks and snacks.The 24-hour reception staff can arrange car rentals. Upon request, guests can make use of the fitness centre at Park Grand Paddington Court Hotel and Suites, a 5-minute walk away.",
                Rooms = 2,
                Capacity = 4,
                Location = new Location()
                {
                    CityId = cities[7].Id,
                    Address = "Road 66",
                    Latitude = 43.67670049339411,
                    Longitude = 17.77698553692002
                },
                AccommodationDetails = new AccommodationDetails()
                {
                    AirConditioning = true,
                    Balcony = true,
                    Minibar = true,
                    Dryer = true,
                    Shower = true,
                    WiFi = true,
                    Cancellation = "Cancellation and prepayment policies vary according to accommodation type. Please check what conditions may apply to each option when making your selection.",
                    HouseRules = "Check-in is from 14.00 to 00.00.Guests are required to show a photo identification and credit card upon check-in.Children of any age are welcome. There is no age requirement for check-in.Pets are not allowed.When booking more than 5 rooms, different policies and additional supplements may apply."
                },
                AccommodationImages = images8,
                HostId = users[2].Id,
            });

            accommodations.Add(new Accommodation
            {
                Name = "The original Spice Bus from 1997 movie Spice World",
                Price = 188,
                Description = "This is the actual and original Spice Bus that fans will know and love from the movie Spice World - but with an interior makeover. Working with some incredible designers, we have turned it into accommodation, so that you can stay the night and live out every fan’s wildest dream. The bus has the bonus of being situated in a stunning location overlooking Island Harbour Marina. There is a Bar, Restaurant, and a small shop on site.",
                Rooms = 3,
                Capacity = 6,
                Location = new Location()
                {
                    CityId = cities[8].Id,
                    Address = "Victory street",
                    Latitude = 43.65975275793134,
                    Longitude = 17.96673298298538
                },
                AccommodationDetails = new AccommodationDetails()
                {
                    AirConditioning = true,
                    Balcony = true,
                    BBQ = true,
                    Parking = true,
                    Dryer = true,
                    Shower = true,
                    WiFi = true,
                    Cancellation = "For changing or cancelling the orders, please contact us.",
                    HouseRules = "All personal items of all occupants and/or guests, including but not limited to, recreation, health, sports and/or hobby  equipment, tools, brooms, cleaning supplies, recyclables, etc. shall be kept out of view. No rugs, towels, articles of clothing and/or linens, or any such items shall be hung on the exterior of the building, on balconies or in hallways."
                },
                AccommodationImages = images9,
                HostId = users[2].Id,
            });

            accommodations.Add(new Accommodation
            {
                Name = "The Silo House At Laughing Llama Farm",
                Price = 401,
                Description = "Enjoy The Silo House At Laughing Llama farm, secluded and private, conveniently located within minutes to everything Waco, Temple & Belton have to offer.",
                Rooms = 2,
                Capacity = 3,
                Location = new Location()
                {
                    CityId = cities[9].Id,
                    Address = "Second boulevar",
                    Latitude = 42.41576478158465,
                    Longitude = 18.781107522420424
                },
                AccommodationDetails = new AccommodationDetails()
                {
                    AirConditioning = true,
                    Balcony = true,
                    Minibar = true,
                    MosquitoNet = true,
                    BBQ = true,
                    Parking = true,
                    Cancellation = "Free cancellation for 48 hours",
                    HouseRules = "Your rental shall be kept in good and clean condition and free from any objectionable odors. "
                },
                AccommodationImages = images10,
                HostId = users[2].Id,
            });


            appDb.AddRange(accommodations);
            appDb.SaveChanges();
            return Count();
        }

        [HttpPost("events")]
        public IActionResult GenerateEvents()
        {
            var users = appDb.UserAccounts.Take(3).ToList();
            var cities = appDb.Cities.Take(25).ToList();

            var events = new List<Event>();

            var images1 = new List<EventImages>()
            {
                new EventImages()
                {
                    ImagePath="https://i.imgur.com/ME1bLvr.jpg"
                }
            };

            var images2 = new List<EventImages>()
            {
                new EventImages()
                {
                    ImagePath="https://i.imgur.com/qzRlhGi.png"
                }
            };

            var images3 = new List<EventImages>()
            {
                new EventImages()
                {
                    ImagePath = "https://i.imgur.com/5DPfCN4.jpg",
                },
                new EventImages()
                {
                    ImagePath = "https://i.imgur.com/mHeGdFS.jpg",
                }
            };

            var images4 = new List<EventImages>()
            {
                new EventImages()
                {
                    ImagePath = "https://i.imgur.com/HfhRKvP.jpg"
                },
                new EventImages()
                {
                    ImagePath = "https://i.imgur.com/iktNc9n.jpg"
                }
            };

            var images5 = new List<EventImages>()
            {
                new EventImages()
                {
                    ImagePath="https://i.imgur.com/19910sK.jpg"
                },
                new EventImages()
                {
                    ImagePath="https://i.imgur.com/5l3gQPR.jpg"
                }
            };

            var images6 = new List<EventImages>()
            {
               new EventImages()
                {
                    ImagePath="https://i.imgur.com/mTCoXtX.jpg"
                },

               new EventImages()
                {
                    ImagePath="https://i.imgur.com/SNAv4yG.jpg"
                }
            };

            //zvijezde
            var images7 = new List<EventImages>
            {
                new EventImages()
                {
                    ImagePath="https://i.imgur.com/1aLyJeG.jpg"
                }
            };

            var images8 = new List<EventImages>()
            {
                new EventImages()
                {
                    ImagePath="https://i.imgur.com/PR1c7nV.jpg"
                }
            };

            //ng
            var images9 = new List<EventImages>()
            {
                new EventImages()
                {
                    ImagePath="https://i.imgur.com/XI22e3v.jpg"
                }
            };

            //vegan webinar
            var images10 = new List<EventImages>()
            {
                new EventImages()
                {
                    ImagePath="https://i.imgur.com/nENJTY1.jpg"
                }
            };

            events.Add(
                new Event()
                {
                    Name = "Digital Marketing and Social Media for Intermediates Webinar",
                    Date = new DateTime(2022, 4, 4),
                    Duration = 2,
                    EventDescription = "How you treat people with your marketing is a reflection of your brand. More than ever, you need to get to know your customer Give your customers results - oriented content Provide quick insights.",
                    HostId = users[0].Id,
                    Images = images1,
                    Location = new Location()
                    {
                        CityId = cities[0].Id,
                        Address = "Grbavicka",
                        Latitude = 43.84943793477401,
                        Longitude = 18.394755614767593
                    },
                    Price = 0,
                });

            events.Add(
                new Event()
                {
                    Name = "Webinar: Education for a successful career",
                    Date = new DateTime(2022, 5, 8),
                    Duration = 2,
                    EventDescription = "In this session, learn about how you can pursue further education to help advance your career in Canada. We will also talk about other programs and strategies to support your career development",
                    HostId = users[0].Id,
                    Images = images2,
                    Location = new Location()
                    {
                        CityId = cities[1].Id,
                        Address = "Banjalucka",
                        Latitude = 42.45563439845189,
                        Longitude = 18.531050876982565
                    },
                    Price = 5,
                });

            events.Add(
               new Event()
               {
                   Name = "Vegan For Beginners",
                   Date = new DateTime(2022, 10, 28),
                   Duration = 2,
                   EventDescription = "More and more people are interested in vegan/plant-based eating. Some are curious, some want to get their feet wet, and others are ready to come to the V-side! I am here for you all!I offer information to introduce you to what eating vegan is like.",
                   HostId = users[0].Id,
                   Images = images10,
                   Location = new Location()
                   {
                       CityId = cities[2].Id,
                       Address = "Siroka ulica",
                       Latitude = 42.78464831481979,
                       Longitude = 18.948937271457957
                   },
                   Price = 8,
               });

            events.Add(
              new Event()
              {
                  Name = "Makarska Party",
                  Date = new DateTime(2022, 6, 7),
                  Duration = 3,
                  EventDescription = "You can enjoy good music and crazy parties in a unique and natural environment.",
                  HostId = users[1].Id,
                  Images = images3,
                  Location = new Location()
                  {
                      CityId = cities[13].Id,
                      Address = "Zagorska ulica",
                      Latitude = 43.29443760591283,
                      Longitude = 17.020820964986502
                  },
                  Price = 15,
              });

            events.Add(
              new Event()
              {
                  Name = "Masters",
                  Date = new DateTime(2022, 9, 9),
                  Duration = 2,
                  EventDescription = "<strong>You can enjoy good music and crazy parties in a unique and natural environment.</strong>",
                  HostId = users[1].Id,
                  Images = images5,
                  Location = new Location()
                  {
                      CityId = cities[16].Id,
                      Address = "Augusta Senoe",
                      Latitude = 45.80644706510394,
                      Longitude = 15.981578190253773

                  },
                  Price = 10,
              });

            events.Add(
             new Event()
             {
                 Name = "Beach party",
                 Date = new DateTime(2022, 8, 9),
                 Duration = 2,
                 EventDescription = "Live music with famous singers",
                 HostId = users[1].Id,
                 Images = images6,
                 Location = new Location()
                 {
                     CityId = cities[19].Id,
                     Address = "Luka",
                     Latitude = 43.657698977097965,
                     Longitude = 17.951923416626858

                 },
                 Price = 7,
             });

            events.Add(
            new Event()
            {
                Name = "Dancing night",
                Date = new DateTime(2022, 5, 3),
                Duration = 2,
                EventDescription = "Loud music, lots of dancing, strobe lighting and all the rest, this is that famous Balkan nightlife.",
                HostId = users[2].Id,
                Images = images7,
                Location = new Location()
                {
                    CityId = cities[21].Id,
                    Address = "Musala",
                    Latitude = 43.344672277421544,
                    Longitude = 17.81100981112521

                },
                Price = 7,
            });

            events.Add(
            new Event()
            {
                Name = "Carpe Diem Bar Hvar",
                Date = new DateTime(2022, 6, 7),
                Duration = 6,
                EventDescription = "Lively waterfront spot providing cocktails, snacks & evening parties in a loungey, open-air space.",
                HostId = users[2].Id,
                Images = images8,
                Location = new Location()
                {
                    CityId = cities[23].Id,
                    Address = "Hvar",
                    Latitude = 43.17230129867638,
                    Longitude = 16.44076422820159

                },
                Price = 7,
            });

            events.Add(
            new Event()
            {
                Name = "New year 2023!",
                Date = new DateTime(2022, 12, 31),
                Duration = 10,
                EventDescription = "Celebrate new year with live music",
                HostId = users[2].Id,
                Images = images9,
                Location = new Location()
                {
                    CityId = cities[24].Id,
                    Address = "Obilaznica",
                    Latitude = 42.28803849263764,
                    Longitude = 18.834474386028184

                },
                Price = 40,
            });

            events.Add(
           new Event()
           {
               Name = "An evening of poetry",
               Date = new DateTime(2022, 6, 7),
               Duration = 1,
               EventDescription = "Listen to beautiful poetry in Nan-tes",
               HostId = users[2].Id,
               Images = images4,
               Location = new Location()
               {
                   CityId = cities[18].Id,
                   Address = "Zlatnih ljiljana",
                   Latitude = 43.66435978842122,
                   Longitude = 17.759383604555197

               },
               Price = 0,
           });

            appDb.AddRange(events);
            appDb.SaveChanges();
            return Count();


            return Ok();
        }

    }
        
}
