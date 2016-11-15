using FTDNA.Models;
using System;
using System.Collections.Generic;

namespace FTDNA.Repository
{
   public interface ISampleStatus
    {
        IEnumerable<SampleInfo> ReturnAllSample();
        IEnumerable<SampleInfo> SamplesWithStatus(string status);
        IEnumerable<SampleInfo> SamplesContainsUser(string searchToken);
        void CreateNewSample(string barcode, string createdAt, int createBy, int statusId);

        IEnumerable<Users> getAllUser();
        IEnumerable<Statuses> getAllStatuses();

    }
}
