using Jarvis.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jarvis.Controllers.Api
{
    [Authorize]
    public class SMSDemandsController : ApiController
    {
        private ApplicationDbContext _context;

        public SMSDemandsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<SMSDemand> GetSMSDemands()
        {
            return _context.SMSDemands.ToList();
        }

        public SMSDemand GetSMSDemand(int id)
        {
            var smsDemand = _context.SMSDemands.SingleOrDefault(s => s.ID == id);
            if (smsDemand == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return smsDemand;
        }

        [HttpPost]
        public SMSDemand CreateSMSDemand(SMSDemand smsDemand)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            smsDemand.UpdateDate = DateTime.Now;
            smsDemand.UserId = User.Identity.GetUserId();
            _context.SMSDemands.Add(smsDemand);
            _context.SaveChanges();

            return smsDemand;
        }

        [HttpPut]
        public void UpdateSMSDemand(int id, SMSDemand smsDemand)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var smsDemandInDb = _context.SMSDemands.SingleOrDefault(s => s.ID == id);

            if (smsDemandInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            smsDemandInDb.Sender = smsDemand.Sender;
            smsDemandInDb.Content_en = smsDemand.Content_en;
            smsDemandInDb.Content_tr = smsDemand.Content_tr;
            smsDemandInDb.SMSCodeId = smsDemand.SMSCodeId;
            smsDemandInDb.UpdateDate = DateTime.Now;
            smsDemandInDb.UserId = User.Identity.GetUserId();

            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteSMSDemand(int id)
        {
            var smsDemandInDb = _context.SMSDemands.SingleOrDefault(s => s.ID == id);

            if (smsDemandInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.SMSDemands.Remove(smsDemandInDb);
            _context.SaveChanges();
        }
    }
}
