using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Parky.Web.Models.ViewModels
{
    public class IndexVM
    {
        public IEnumerable<NationalPark> NationalParksList { get; set; }


        public IEnumerable<Trail> Trails { get; set; }
    }
}
