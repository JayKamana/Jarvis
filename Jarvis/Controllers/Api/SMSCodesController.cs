using Jarvis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace Jarvis.Controllers.Api
{
    [Authorize]
    public class SMSCodesController : ApiController
    {
        private ApplicationDbContext _context;

        public SMSCodesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IHttpActionResult GetSMSCode()
        {
            return Ok(_context.SMSCodes.ToList());
        }

        public SMSCode GetSMSCode(int id)
        {
            var smsCode = _context.SMSCodes.SingleOrDefault(s => s.Id == id);
            if (smsCode == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            return smsCode;
        }

        [HttpPost]
        public SMSCode CreateSMSCode(SMSCode smsCode)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.SMSCodes.Add(smsCode);
            _context.SaveChanges();

            return smsCode;
        }

        [HttpPut]
        public void UpdateSMSCode(int id, SMSCode smsCode)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var smsCodeInDb = _context.SMSCodes.SingleOrDefault(s => s.Id == id);

            if (smsCodeInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            smsCodeInDb.CodeDescription = smsCode.CodeDescription;
            smsCodeInDb.ReferenceCode = smsCode.ReferenceCode;

            _context.SaveChanges();
        }

        [HttpDelete]
        public void DeleteSMSCode(int id)
        {
            var smsCodeInDb = _context.SMSCodes.SingleOrDefault(s => s.Id == id);

            if (smsCodeInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.SMSCodes.Remove(smsCodeInDb);
            _context.SaveChanges();
        }
    }
}
