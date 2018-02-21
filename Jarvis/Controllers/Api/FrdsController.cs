using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jarvis.Models;
using Jarvis.Dtos;
using AutoMapper;

namespace Jarvis.Controllers.Api
{
    public class FrdsController : ApiController
    {
        private ApplicationDbContext _context;

        public FrdsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/frds
        public IHttpActionResult GetFRDS()
        {
            var frdDtos = _context.FRDS.ToList().Select(Mapper.Map<FRD, FRDDtos>);

            return Ok(frdDtos);
        }

        //GET /api/frds/1
        public IHttpActionResult GetFRD(int id)
        {
            var frd = _context.FRDS.SingleOrDefault(c => c.Id == id);

            if (frd == null)
                return NotFound();

            return Ok(Mapper.Map<FRD, FRDDtos>(frd));
        }

        //POST /api/frds
        [HttpPost]
        public IHttpActionResult CreateFRD(FRDDtos frdDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var frd = Mapper.Map<FRDDtos, FRD>(frdDto);
            _context.FRDS.Add(frd);
            _context.SaveChanges();

            frdDto.Id = frd.Id;
            return Created(new Uri(Request.RequestUri + "/" + frd.Id), frdDto);
        }

        //PUT  /api/frd/1
        [HttpPut]
        public IHttpActionResult UpdateCustomer(int id, FRDDtos frdDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var frdInDb = _context.FRDS.SingleOrDefault(c => c.Id == id);

            if (frdInDb == null)
                return NotFound();

            Mapper.Map(frdDto, frdInDb);

            _context.SaveChanges();

            return Ok();

        }

        //DELETE    /api/frd/1
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var frdInDb = _context.FRDS.SingleOrDefault(c => c.Id == id);

            if (frdInDb == null)
                return NotFound();

            _context.FRDS.Remove(frdInDb);
            _context.SaveChanges();

            return Ok();
        }

    }
}
