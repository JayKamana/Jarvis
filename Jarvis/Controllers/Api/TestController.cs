using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jarvis.Controllers.Api
{

    public class TestController : ApiController
    {
      //  [Route("HelloWorld")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage HelloWorld()
        {
            return Request.CreateResponse(HttpStatusCode.OK, User.Identity.GetUserId());
        }


    }
}
