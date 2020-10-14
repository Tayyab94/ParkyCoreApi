﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.Web.Models
{
    public class Trail
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }


        [Required]
        public Double Distance { get; set; }

        [Required]
        public double Elevation { get; set; }

        public enum DifficultyType { Easy, Moderate, Difficult, Expert }
        public DifficultyType DifficultyTypes { get; set; }

        [Required]
        public int NationalParkId { get; set; }


        public NationalPark NationalPark { get; set; }
    }
}
