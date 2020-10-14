using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Parky.Web.Models;
using Parky.Web.Repository.IRepository;

namespace Parky.Web.Controllers
{
    [Authorize]
    public class NationalParkController : Controller
    {
        private readonly INationalParkRepository nationalParkRepository;

        public NationalParkController(INationalParkRepository nationalParkRepository)
        {
            this.nationalParkRepository = nationalParkRepository;
        }
        public IActionResult Index()
        {
            return View(new NationalPark());
        }

        //get Park list

        public async Task<IActionResult> GetAllNationalParks()
        {
            return Json(new { data = await nationalParkRepository.GetAllAsync(SD.NationalParkAPIUrl,HttpContext.Session.GetString("JWTToken")) });
        }

        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            NationalPark nationalPark = new NationalPark();

            if(id==0)
            {
                return View(nationalPark);
            }
            nationalPark = await nationalParkRepository.GetAsync(SD.NationalParkAPIUrl, id.GetValueOrDefault(), HttpContext.Session.GetString("JWTToken"));
            if(nationalPark==null)
            {
                return NotFound();
            }
                return View(nationalPark);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Upsert(NationalPark nationalPark)
        {
            if(ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;

                if (files.Count > 0)
                {
                    byte[] p1 = null;

                    using var fs1 = files[0].OpenReadStream();
                    using var ms1 = new MemoryStream();
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();

                    nationalPark.Picture = p1;
                }
                else
                {
                    var objFromdb = await nationalParkRepository.GetAsync(SD.NationalParkAPIUrl, nationalPark.Id, HttpContext.Session.GetString("JWTToken"));
                    nationalPark.Picture = objFromdb.Picture;
                }

                if(nationalPark.Id==0)
                {
                    await nationalParkRepository.CreateAsync(SD.NationalParkAPIUrl, nationalPark, HttpContext.Session.GetString("JWTToken"));
                }
                else
                {
                    await nationalParkRepository.UpdateAsync(SD.NationalParkAPIUrl + nationalPark.Id, nationalPark, HttpContext.Session.GetString("JWTToken"));
                }
            }
            return View(nationalPark);
        }



        [HttpDelete]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> Delete(int id)
        {
            var status = await nationalParkRepository.DeleteAsync(SD.NationalParkAPIUrl, id, HttpContext.Session.GetString("JWTToken"));
            if(status)
            {
                return Json(new { success = true, message = "Delete Successfuly!" });
            }

            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}
