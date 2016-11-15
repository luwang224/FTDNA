using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using FTDNA.Models;
using System.Web.Http;

namespace FTDNA.Repository
{
    public class SampleStatus : ISampleStatus
    {
        static IEnumerable<Samples> samples = File.ReadAllLines(System.Web.HttpContext.Current.Request.MapPath("~/App_Data/Samples.txt")).Skip(1)
                .Select(line => line.Split(','))
                .Select(r => new Samples { SampleId = int.Parse(r[0]), Barcode = r[1], CreatedAt = DateTime.Parse(r[2]), CreatedBy = int.Parse(r[3]), StatusId = int.Parse(r[4]) });


        static IEnumerable<Statuses> statuses = File.ReadAllLines(System.Web.HttpContext.Current.Request.MapPath("~/App_Data/Statuses.txt")).Skip(1)
             .Select(line => line.Split(','))
             .Select(r => new Statuses { StatusId = int.Parse(r[0]), Status = r[1] });

        static IEnumerable<Users> users = File.ReadAllLines(System.Web.HttpContext.Current.Request.MapPath("~/App_Data/Users.txt")).Skip(1)
                 .Select(line => line.Split(','))
                 .Select(r => new Users { UserId = int.Parse(r[0]), FirstName = r[1], LastName = r[2] });

        public void CreateNewSample(string barcode, string createdAt, int createBy, int statusId)
        {
            int id = samples.Select(r => r.SampleId).Max() + 1;
            DateTime createdTime = Convert.ToDateTime(createdAt);
            File.AppendAllText(System.Web.HttpContext.Current.Request.MapPath("~/App_Data/Samples.txt"), id.ToString() + ',' + barcode + ',' + createdTime.ToString("yyyy-MM-dd") + ',' + createBy + ',' + statusId + Environment.NewLine);
            samples = File.ReadAllLines(System.Web.HttpContext.Current.Request.MapPath("~/App_Data/Samples.txt")).Skip(1)
                .Select(line => line.Split(','))
                .Select(r => new Samples { SampleId = int.Parse(r[0]), Barcode = r[1], CreatedAt = DateTime.Parse(r[2]), CreatedBy = int.Parse(r[3]), StatusId = int.Parse(r[4]) });
        }

        public IEnumerable<SampleInfo> ReturnAllSample()
        {
            IEnumerable<SampleInfo> sampleInfo = from a in samples
                                                 join b in statuses on a.StatusId equals b.StatusId
                                                 join c in users on a.CreatedBy equals c.UserId
                                                 select new SampleInfo { SampleId = a.SampleId, Barcode = a.Barcode, CreatedAt = a.CreatedAt, CreatedBy = c.FirstName + ' ' + c.LastName, Status = b.Status };
            return sampleInfo;
        }


        public IEnumerable<SampleInfo> SamplesContainsUser(string searchToken)
        {
            IEnumerable<SampleInfo> sampleData = from a in samples
                                                 join b in statuses on a.StatusId equals b.StatusId
                                                 join c in users on a.CreatedBy equals c.UserId
                                                 where (c.FirstName.ToLower().Contains(searchToken.ToLower()) || c.LastName.ToLower().Contains(searchToken.ToLower()))
                                                 select new SampleInfo { SampleId = a.SampleId, Barcode = a.Barcode, CreatedAt = a.CreatedAt, CreatedBy = c.FirstName + ' ' + c.LastName, Status = b.Status };

            return sampleData;
        }

        public IEnumerable<SampleInfo> SamplesWithStatus(string status)
        {
            IEnumerable<SampleInfo> sampleData = from a in samples
                                                 join b in statuses on a.StatusId equals b.StatusId
                                                 join c in users on a.CreatedBy equals c.UserId
                                                 where b.Status == status
                                                 select new SampleInfo { SampleId = a.SampleId, Barcode = a.Barcode, CreatedAt = a.CreatedAt, CreatedBy = c.FirstName + ' ' + c.LastName, Status = b.Status };
            return sampleData;
        }
        public IEnumerable<Users> getAllUser()
        {
            return users;
        }

        public IEnumerable<Statuses> getAllStatuses()
        {
            return statuses;
        }
    }
 }