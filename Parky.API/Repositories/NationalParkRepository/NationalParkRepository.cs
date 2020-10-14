using Parky.API.Data;
using Parky.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Parky.API.Repositories.NationalParkRepository
{
    public class NationalParkRepository : INationalParkRepository
    {
        private readonly ApplicationDbContext context;

        public NationalParkRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public bool CreateNationalPark(NationalPark nationalPark)
        {
            context.NationalParks.Add(nationalPark);

            return Save();
        }

        public bool DeleteNationalPark(NationalPark nationalPark)
        {
            context.NationalParks.Remove(nationalPark);
            return Save();
        }

        public NationalPark GetNationalPark(int NationalParkId)
        {
            return context.NationalParks.FirstOrDefault(s => s.Id == NationalParkId);
        }

        public ICollection<NationalPark> GetNationalParks()
        {
            return context.NationalParks.OrderBy(s => s.Name).ToList();
        }

        public bool NationalparkExit(int NationalParkId)
        {
            return context.NationalParks.Any(s => s.Id == NationalParkId);
        }

        public bool NationalparkExit(string Name)
        {
            return context.NationalParks.Any(s => s.Name.ToLower().Equals(Name.ToLower()));
        }

        public bool Save()
        {
            return context.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateNationalPark(NationalPark nationalPark)
        {
             context.NationalParks.Update(nationalPark);

            return Save();
            
        }
    }
}
