using Parky.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.API.Repositories.NationalParkRepository
{
    public interface INationalParkRepository
    {

        ICollection<NationalPark> GetNationalParks();

        NationalPark GetNationalPark(int NationalParkId);


        bool NationalparkExit(int NationalParkId);


        bool NationalparkExit(string Name);


        bool CreateNationalPark(NationalPark nationalPark);

        bool UpdateNationalPark(NationalPark nationalPark);
        bool DeleteNationalPark(NationalPark nationalPark);
        bool Save();
    }
}
