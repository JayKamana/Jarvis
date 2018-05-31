using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Jarvis.Models;
using Jarvis.Dtos;
using AutoMapper;
using System.Web;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Web.Http.Filters;
using PushSharp.Google;
using System.IO;

namespace Jarvis.Controllers.Api
{
    //[Authorize]
    public class FrdsController : ApiController
    {
        private ApplicationDbContext _context;

        public FrdsController()
        {
            _context = new ApplicationDbContext();
        }

        //[HttpGet]
        //public IHttpActionResult GetFRD()
        //{
        //    return Ok(_context.FRDS.ToList());
        //}

        //[Throttle(Name = "TestThrottle", Message = "You must wait {n} seconds before accessing this url again.", Seconds = 5)]
        public IHttpActionResult GetFRD()
        {
            var frdDtos = _context.FRDS.Include(c => c.User).ToList().Select(Mapper.Map<FRD, FRDDtos>);

            string result = SendNotificationFromFirebaseCloud();

            return Ok(frdDtos);
        }

        //GET /api/frds
        //   [HttpGet]
        //   public IHttpActionResult GetFRDS()
        //   {
        //       var userid = User.Identity.GetUserId();
        //      var frd = _context.FRDS.SingleOrDefault(f => f.UserId == userid);
        // var frdDtos = _context.FRDS.ToList().Select(Mapper.Map<FRD, FRDDtos>);

        //      return Ok(frd);
        //     }


        //  GET /api/frds/1
        [HttpGet]
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
            frd.UserId = User.Identity.GetUserId();
            _context.FRDS.Add(frd);
            _context.SaveChanges();

            frdDto.Id = frd.Id;
            return Created(new Uri(Request.RequestUri + "/" + frd.Id), frdDto);
        }

        //PUT  /api/frds/1
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

        //DELETE    /api/frds/1
        [Authorize(Roles = RoleName.CanManageFRD)]
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

        public static string SendNotificationFromFirebaseCloud()
        {
            var result = "-1";
            var webAddr = "https://fcm.googleapis.com/fcm/send";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Headers.Add(HttpRequestHeader.Authorization, "key=AAAAaSw0DgA:APA91bHWxzRlmcQJTFDkHPeVoSS_8G8PuN_WiXXslGrFEtPtx6x10tBXNlTP2UFBqEsfmyFDsf_OGScMmglX6hj9SjmnT1d_j329er8AOvuv9vx1huSpKgQVu-7fFXVeYOF37ElVGGc3");
            httpWebRequest.Method = "POST";
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string strNJson = @"{
                    ""to"": ""/topics/Turkcell"",
                    ""notification"": {
                    ""title"": ""From Firebase"",
                    ""text"": ""This is Notification"",
                    ""sound"":""default""
                    }
                }";

                streamWriter.Write(strNJson);
                streamWriter.Flush();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
            }
            return result;

        }
    }
}
