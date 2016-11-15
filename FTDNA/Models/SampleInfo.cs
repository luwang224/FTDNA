using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTDNA.Models
{
        public class SampleInfo
        {
            public int SampleId { set; get; }
            public string Barcode { set; get; }
            public DateTime? CreatedAt { set; get; }
            public string Status { set; get; }
            public string CreatedBy { set; get; }
        }
}