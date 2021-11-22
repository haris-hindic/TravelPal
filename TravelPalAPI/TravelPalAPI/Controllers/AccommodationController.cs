﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelPalAPI.Context;
using TravelPalAPI.Models;
using TravelPalAPI.Models.ViewModels;

namespace TravelPalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccommodationController : ControllerBase
    {
        private readonly AppDbContext appDb;

        public AccommodationController(AppDbContext appDb)
        {
            this.appDb = appDb;
        }

        [HttpPost]
        public IActionResult Post([FromBody] AccommodationCreationVM creationVM)
        {
            var obj = new Accommodation
            {
                Name = creationVM.Name,
                Price = creationVM.Price,
                Location = new Location
                {
                    Country = creationVM.LocationCreation.Country,
                    City = creationVM.LocationCreation.City,
                    Address = creationVM.LocationCreation.Address
                },
                AccommodationDetails = new AccommodationDetails
                {
                    Parking = creationVM.AccommodationDetailsCreation.Parking,
                    Minibar= creationVM.AccommodationDetailsCreation.Minibar,
                    AirConditioning= creationVM.AccommodationDetailsCreation.AirConditioning,
                    Safe= creationVM.AccommodationDetailsCreation.Safe,
                    Dryer= creationVM.AccommodationDetailsCreation.Dryer,
                    FlatScreenTV= creationVM.AccommodationDetailsCreation.FlatScreenTV,
                    BBQ= creationVM.AccommodationDetailsCreation.BBQ,
                    Refrigerator= creationVM.AccommodationDetailsCreation.Refrigerator,
                    Balcony= creationVM.AccommodationDetailsCreation.Balcony,
                    MosquitoNet= creationVM.AccommodationDetailsCreation.MosquitoNet
                }
            };

            appDb.Accommodations.Add(obj);
            appDb.SaveChanges();
            return Ok("Succesfully created!");
        }

        [HttpGet]
        public ActionResult<IEnumerable<Accommodation>> Get()
        {
            return appDb.Accommodations.Include(l=>l.Location).Include(ad=>ad.AccommodationDetails).ToList();
        }

        [HttpGet,Route("{id}")]
        public ActionResult<Accommodation> Get(int id)
        {
            return appDb.Accommodations.Include(l => l.Location).Include(ad => ad.AccommodationDetails).ToList().FirstOrDefault(x=>x.Id==id);
        }
    }
}