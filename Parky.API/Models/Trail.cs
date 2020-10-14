﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.API.Models
{
    public class Trail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public Double Distance { get; set; }

        [Required]
        public double Elevation { get; set; }
        public enum DifficultyType { Easy, Moderate, Difficult, Expert }
        public DifficultyType difficultyType { get; set; }

        [Required]
        public int NationalParkId { get; set; }

        [ForeignKey("NationalParkId")]
        public NationalPark NationalPark { get; set; }


        public DateTime Created { get; set; }

    }

  
}
