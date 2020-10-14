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
using Parky.API.Repositories.TrailRepository;

namespace Parky.API.Controllers
{

    //// for Vertioning We have to Remove this
    //[Route("api/Trails")]


    [Route("api/v{version:apiVersion}/Trails")]


    [ApiController]

    [ProducesResponseType(StatusCodes.Status404NotFound)]  // This will be Apply at Controller Level...


    //// For the Multiple Api Documentation
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpec_Tril")]
    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepository trailRepository;
        private readonly IMapper mapper;

        public TrailsController(ITrailRepository trailRepository, IMapper mapper)
        {
            this.trailRepository = trailRepository;
            this.mapper = mapper;
        }


        /// <summary>
        /// List of All National Parks
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200,Type =typeof(List<NationalParkDTO>))]
        [ProducesResponseType(400)]
        public IActionResult GetAllTrails()
        {
            //return Ok(trailRepository.GetNationalParks());

            var modelDto = new List<Parky.API.Models.DTOs.TrailDTO>();

            var data = trailRepository.GetTrails();

            foreach (var item in data)
            {
                modelDto.Add(mapper.Map<TrailDTO>(item));
            }

            return Ok(modelDto);
        }



        /// <summary>
        /// Get Nation Park By its Id
        /// </summary>
        /// <param name="trailId"> Its a Park Id</param>
        /// <returns></returns>
        [HttpGet("{trailId:int}", Name = "GetTrailById")]
        [ProducesResponseType(200,Type =typeof(NationalParkDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        [Authorize(Roles ="test")]
        public IActionResult GetTrailById(int trailId)
        {
            var da = trailRepository.GetTrail(trailId);


            if (da == null)
                return BadRequest();

            var objdto = mapper.Map<TrailDTO>(da);

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



        /// <summary>
        /// Get Nation Park By its Id
        /// </summary>
        /// <param name="NationalParkiId"> Its a Park Id</param>
        /// <returns></returns>
        /*   [HttpGet("/GetTrailInNationalParkiId/{NationalParkiId:int}")]*/                                                          //[HttpGet("/GetTrailInNationalParkiId/{NationalParkiId:int}")]
        [HttpGet("[action]/{NationalParkiId:int}")]
        [ProducesResponseType(200, Type = typeof(List<TrailDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesDefaultResponseType]
        public IActionResult GetTrailInNationalParkiId(int NationalParkiId)
        {
            var da = trailRepository.GetTrailsInNationPark(NationalParkiId);


            if (da == null)
                return BadRequest();


            var modelList = new List<TrailDTO>();

            foreach (var item in da)
            {
                modelList.Add(mapper.Map<TrailDTO>(item));
            }


            return Ok(modelList);
        }


        [HttpPost]
        [ProducesResponseType(201, Type = typeof(TrailDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateTrail([FromBody] TrailCreateDTO model)
        {
            if (model == null)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (trailRepository.TrailsExit(model.Name))
            {
                ModelState.AddModelError("", "This Trail is already exist!");

                return StatusCode(404, ModelState);
            }


            var data = mapper.Map<Trail>(model);

            if (!trailRepository.CreateTrail(data))
            {
                ModelState.AddModelError("", $"Something is Wrong when saving the park {data.Name}");

                return StatusCode(500, ModelState);
            }

            //return Ok();

            return CreatedAtRoute("GetTrailById", new { trailId = data.Id }, data);
        }


        [HttpPatch("{trailId:int}", Name = "UpdateTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult UpdateTrail(int trailId, [FromBody] TrailUpdateDTO model)
        {
            if (model == null || trailId != model.Id)
            {
                return BadRequest(ModelState);
            }


            if (trailRepository.TrailsExit(model.Name))
            {
                ModelState.AddModelError("", "This Park is already exist!");

                return StatusCode(404, ModelState);
            }


            var data = mapper.Map<Trail>(model);

            if (!trailRepository.UpdateTrail(data))
            {
                ModelState.AddModelError("", $"Something is Wrong when Updating  the park {data.Name}");

                return StatusCode(500, ModelState);
            }


            return NoContent();
        }


        [HttpDelete("{trailId:int}", Name = "DeleteTrail")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteTrail(int trailId)
        {
            if(!trailRepository.TrailsExit(trailId))
            {
                return NotFound();
            }

            var data = trailRepository.GetTrail(trailId);

            if(!trailRepository.DeleteTrail(data))
            {
                ModelState.AddModelError("", $"Something is Wrong when Updating  the park {data.Name}");

                return StatusCode(500, ModelState);
            }

            //if (!trailRepository.DeleteNationalPark(trailRepository.GetNationalPark(parkId)))
            //{
            //    ModelState.AddModelError("", $"Something is Wrong when Updating  the park {data.Name}");

            //    return StatusCode(500, ModelState);
            //}

            return NoContent();
        }


    }
}
