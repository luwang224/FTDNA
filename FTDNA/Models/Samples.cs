using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FTDNA.Models
{
    public class Samples
    {
        public int SampleId { set; get; }
        public string Barcode { set; get; }
        public DateTime? CreatedAt { set; get; }
        public int CreatedBy { set; get; }
        public int StatusId { set; get; }
    }
}