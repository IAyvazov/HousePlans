﻿namespace HousePlans.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Room : BaseModel<int>
    {
        [Required]
        public string Name { get; set; }

        public double Area { get; set; }
    }
}
