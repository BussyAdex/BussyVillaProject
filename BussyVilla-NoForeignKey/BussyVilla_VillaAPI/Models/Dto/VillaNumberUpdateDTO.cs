﻿using System.ComponentModel.DataAnnotations;

namespace BussyVilla_VillaAPI.Models.Dto
{
    public class VillaNumberUpdateDTO
    {
        [Required]
        public int VillaNo { get; set; }
        public string SpecialDetails { get; set; }

    }
}
