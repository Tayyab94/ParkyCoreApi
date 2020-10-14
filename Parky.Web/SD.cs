using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.Web
{
    public static class SD
    {

        public static string APIBaseUrl { get; set; } = "https://localhost:44304/";

        public static string NationalParkAPIUrl { get; set; } = APIBaseUrl + "api/v1/NationalPark/";

        public static string TrainAPIUrl { get; set; } = APIBaseUrl + "api/v1/Trails/";

        public static string UserAccountAPIUrl { get; set; } = APIBaseUrl + "api/v1/Users/";
    }
}
