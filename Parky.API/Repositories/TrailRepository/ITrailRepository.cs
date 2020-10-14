using Parky.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.API.Repositories.TrailRepository
{
    public interface ITrailRepository
    {

        ICollection<Trail> GetTrails();
        ICollection<Trail> GetTrailsInNationPark(int nationParkId);
        Trail GetTrail(int TrailId);


        bool TrailsExit(int TrailId);


        bool TrailsExit(string Name);


        bool CreateTrail(Trail nationalPark);

        bool UpdateTrail(Trail nationalPark);
        bool DeleteTrail(Trail nationalPark);
        bool Save();
    }
}
