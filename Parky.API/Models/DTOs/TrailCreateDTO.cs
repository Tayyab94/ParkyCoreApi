using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Parky.API.Models.Trail;

namespace Parky.API.Models.DTOs
{
    public class TrailCreateDTO
    {
      

        [Required]
        public string Name { get; set; }


        [Required]
        public Double Distance { get; set; }
        [Required]
        public double Elevation { get; set; }


        public DifficultyType DifficultyType { get; set; }

        [Required]
        public int NationalParkId { get; set; }

    
    }


}
