using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

    [Route("api/v{version:apiVersion}/NationalParkVer")]
    [ApiVersion("2.0")]


    [ApiController]

    [ProducesResponseType(StatusCodes.Status404NotFound)]  // This will be Apply at Controller Level...
    
    
    //// For the Multiple Api Documentation
    //[ApiExplorerSettings(GroupName = "ParkyOpenAPISpec_NP")]     if you want to add the versioning in your APi then you have to comment the group name
    public class NationalParkVerController : ControllerBase
    {
        private readonly INationalParkRepository nationalParkRepository;
        private readonly IMapper mapper;

        public NationalParkVerController(INationalParkRepository nationalParkRepository, IMapper mapper)
        {
            this.nationalParkRepository = nationalParkRepository;
            this.mapper = mapper;
        }


        /// <summary>
        /// List of All National Parks
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(200,Type =typeof(NationalParkDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetNationalParks()
        {
            //return Ok(nationalParkRepository.GetNationalParks());

            var modelDto = new List<Parky.API.Models.DTOs.NationalParkDTO>();

            var data = nationalParkRepository.GetNationalParks().FirstOrDefault();

            var model = mapper.Map<NationalParkDTO>(data);

            return Ok(model);
        }



    }
}
