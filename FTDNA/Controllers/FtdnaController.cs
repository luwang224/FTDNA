using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FTDNA.Models;
using FTDNA.Repository;

namespace FTDNA.Controllers
{
    public class FtdnaController : ApiController
    {
        public SampleStatus sampleRepo = new SampleStatus();

        [HttpGet]
        public IEnumerable<SampleInfo> ReturnAllSample()
        {
            return sampleRepo.ReturnAllSample();
        }

        [HttpGet]
        public IEnumerable<SampleInfo> SamplesWithStatus(string status)
        {
            return sampleRepo.SamplesWithStatus(status);
        }

        [HttpGet]
        public IEnumerable<SampleInfo> SamplesContainsUser(string searchToken)
        {
            return sampleRepo.SamplesContainsUser(searchToken);
        }

        [HttpPost]
        public HttpResponseMessage CreateNewSample(string barcode, string createdAt, int createBy, int statusId)
        {
            try
            {
                sampleRepo.CreateNewSample(barcode, createdAt, createBy, statusId);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex) // we can log the exception
            {
                return Request.CreateResponse(HttpStatusCode.NotAcceptable);
            }
        }

        [HttpGet]
        public IEnumerable<Users> getAllUser()
        {
            return sampleRepo.getAllUser();
        }
        [HttpGet]
        public IEnumerable<Statuses> getAllStatuses()
        {
            return sampleRepo.getAllStatuses();
        }

    }
}
