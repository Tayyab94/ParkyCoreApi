using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Parky.Web.Models;
using Parky.Web.Models.ViewModels;
using Parky.Web.Repository.IRepository;

namespace Parky.Web.Controllers
{
    [Authorize]
    public class TrailController : Controller
    {
        private readonly INationalParkRepository nationalParkRepository;
        private readonly ITrailRepository trailRepository;

        public TrailController(INationalParkRepository nationalParkRepository, ITrailRepository trailRepository)
        {
            this.nationalParkRepository = nationalParkRepository;
            this.trailRepository = trailRepository;
        }
        public IActionResult Index()
        {
            return View(new Trail());
        }

        //get Park list

        public async Task<IActionResult> GetAllTrails()
        {
            return Json(new { data = await trailRepository.GetAllAsync(SD.TrainAPIUrl, HttpContext.Session.GetString("JWTToken")) });
        }

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Upsert(int? id)
        {

            IEnumerable<NationalPark> nationalParkList = await nationalParkRepository.GetAllAsync(SD.NationalParkAPIUrl, HttpContext.Session.GetString("JWTToken"));

            TrailVM trailVM = new TrailVM
            {
                NationalParkList = nationalParkList.Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }),
                Trail = new Trail()
                 
            };
            NationalPark nationalPark = new NationalPark();

            if(id==0)
            {
                return View(trailVM);
            }
            trailVM.Trail = await trailRepository.GetAsync(SD.TrainAPIUrl, id.GetValueOrDefault(), HttpContext.Session.GetString("JWTToken"));
            if(trailVM.Trail==null)
            {
                return NotFound();
            }
                return View(trailVM);
        }
    
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(TrailVM trailVM)
        {

            if(ModelState.IsValid)
            {
               

                if(trailVM.Trail.Id==0)
                {
                    await trailRepository.CreateAsync(SD.TrainAPIUrl, trailVM.Trail, HttpContext.Session.GetString("JWTToken"));
                }
                else
                {
                    await trailRepository.UpdateAsync(SD.TrainAPIUrl + trailVM.Trail.Id, trailVM.Trail, HttpContext.Session.GetString("JWTToken"));
                }
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<NationalPark> nationalParkList = await nationalParkRepository.GetAllAsync(SD.NationalParkAPIUrl, HttpContext.Session.GetString("JWTToken"));

            TrailVM newtrail = new TrailVM
            {
                NationalParkList = nationalParkList.Select(s => new SelectListItem()
                {
                    Text = s.Name,
                    Value = s.Id.ToString()
                }),
                Trail = trailVM.Trail 

            };
            return View(newtrail);
        }



        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await trailRepository.DeleteAsync(SD.TrainAPIUrl, id, HttpContext.Session.GetString("JWTToken"));
            if(status)
            {
                return Json(new { success = true, message = "Delete Successfuly!" });
            }

            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}
