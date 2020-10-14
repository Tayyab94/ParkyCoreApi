using Microsoft.EntityFrameworkCore;
using Parky.API.Data;
using Parky.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.API.Repositories.TrailRepository
{
    public class TrailRepository:ITrailRepository
    {
        private readonly ApplicationDbContext context;

        public TrailRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public bool CreateTrail(Trail trail)
        {
            context.Trails.Add(trail);

            return Save();
        }

        public bool DeleteTrail(Trail trail)
        {
            context.Trails.Remove(trail);
            return Save();
        }

        public Trail GetTrail(int TrailId)
        {
            return context.Trails.Include(s => s.NationalPark).FirstOrDefault(s => s.Id == TrailId);
        }

        public ICollection<Trail> GetTrails()
        {
            return context.Trails.Include(s=>s.NationalPark).OrderBy(s => s.Name).ToList();
        }

        public ICollection<Trail> GetTrailsInNationPark(int nationParkId)
        {
            return context.Trails.Include(s => s.NationalPark).Where(s => s.NationalParkId == nationParkId).ToList();
        }

        public bool Save()
        {
            return context.SaveChanges() >= 0 ? true : false;
        }

        public bool TrailsExit(int TrailId)
        {
            return context.Trails.Any(s => s.Id == TrailId);
        }

        public bool TrailsExit(string Name)
        {
            return context.Trails.Any(s => s.Name.ToLower().Equals(Name.ToLower()));
        }

        public bool UpdateTrail(Trail trail)
        {
            context.Trails.Update(trail);

            return Save();

        }
    }
}






