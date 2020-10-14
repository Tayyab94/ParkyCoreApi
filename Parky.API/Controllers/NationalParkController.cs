using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Parky.API.Models;
using Parky.API.Models.DTOs;
using Parky.API.Repositories.NationalParkRepository;

namespace Parky.API.Controllers
{

    // for Vertioning We have to Remove this 
    //[Route("api/[controller]")]

   [Route("api/v{version:apiVersion}/NationalPark")]

    [ApiController]

    [ProducesResponseType(StatusCodes.Status404NotFound)]  // This will be Apply at Controller Level...
    
    
    //// For the Multiple Api Documentation
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpec_NP")]
    public class NationalParkController : ControllerBase
    {
        private readonly INationalParkRepository nationalParkRepository;
        private readonly IMapper mapper;

        public NationalParkController(INationalParkRepository nationalParkRepository, IMapper mapper)
        {
            this.nationalParkRepository = nationalParkRepository;
            this.mapper = mapper;
        }


        /// <summary>
        /// List of All National Parks
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200,Type =typeof(List<NationalParkDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            //return Ok(nationalParkRepository.GetNationalParks());

            var modelDto = new List<Parky.API.Models.DTOs.NationalParkDTO>();

            var data = nationalParkRepository.GetNationalParks();

            foreach (var item in data)
            {
                modelDto.Add(mapper.Map<NationalParkDTO>(item));
            }

            return Ok(modelDto);
        }



        /// <summary>
        /// Get Nation Park By its Id
        /// </summary>
        /// <param name="parkId"> Its a Park Id</param>
        /// <returns></returns>
        [HttpGet("{parkId:int}", Name = "GetNationPark")]
        [ProducesResponseType(200,Type =typeof(NationalParkDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize]
        public IActionResult GetNationPark(int parkId)
        {
            var da = nationalParkRepository.GetNationalPark(parkId);


            if (da == null)
                return BadRequest();

            var objdto = mapper.Map<NationalParkDTO>(da);

            //var d = new NationalPark
            //{
            //    Created = da.Created,
            //    Established = da.Established,
            //    Id = da.Id,
            //    Name = da.Name,
            //    State = da.State
            //};

            return Ok(objdto);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(NationalParkDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateNationalPart([FromBody] NationalParkDTO model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (nationalParkRepository.NationalparkExit(model.Name))
            {
                ModelState.AddModelError("", "This Park is already exist!");

                return StatusCode(404, ModelState);
            }


            var data = mapper.Map<NationalPark>(model);

            if (!nationalParkRepository.CreateNationalPark(data))
            {
                ModelState.AddModelError("", $"Something is Wrong when saving the park {data.Name}");

                return StatusCode(500, ModelState);
            }

            //return Ok();

            //return CreatedAtRoute("GetNationPark", new { parkId = data.Id }, data);

            // If we have Specify the APi VErsion then 
            return CreatedAtRoute("GetNationPark", new {version=HttpContext.GetRequestedApiVersion().ToString(),parkId = data.Id }, data);
        }


        [HttpPatch("{parkId:int}", Name = "UpdateNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateNationalPark(int parkId, [FromBody] NationalParkDTO model)
        {
            if (model == null || parkId != model.Id)
            {
                return BadRequest(ModelState);
            }


            if (nationalParkRepository.NationalparkExit(model.Name))
            {
                ModelState.AddModelError("", "This Park is already exist!");

                return StatusCode(404, ModelState);
            }


            var data = mapper.Map<NationalPark>(model);

            if (!nationalParkRepository.UpdateNationalPark(data))
            {
                ModelState.AddModelError("", $"Something is Wrong when Updating  the park {data.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }


        [HttpDelete("{parkId:int}",Name ="DeleteNationalPark")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteNationalPark(int parkId)
        {
            if(!nationalParkRepository.NationalparkExit(parkId))
            {
                return NotFound();
            }

            var data = nationalParkRepository.GetNationalPark(parkId);

            if(!nationalParkRepository.DeleteNationalPark(data))
            {
                ModelState.AddModelError("", $"Something is Wrong when Updating  the park {data.Name}");

                return StatusCode(500, ModelState);
            }

            //if (!nationalParkRepository.DeleteNationalPark(nationalParkRepository.GetNationalPark(parkId)))
            //{
            //    ModelState.AddModelError("", $"Something is Wrong when Updating  the park {data.Name}");

            //    return StatusCode(500, ModelState);
            //}

            return NoContent();
        }


    }
}
