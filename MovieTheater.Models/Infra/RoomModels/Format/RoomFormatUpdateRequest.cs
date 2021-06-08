﻿using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.RoomModels.Format
{
    public class RoomFormatUpdateRequest
    {
        public int Id { get; set; }
        [Display(Name = "Tên")]
        public string Name { get; set; }
        [Display(Name = "Giá")]
        public int Price { get; set; }
    }
}