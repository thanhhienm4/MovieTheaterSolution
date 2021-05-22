﻿using System.ComponentModel.DataAnnotations;

namespace MovieTheater.Models.Infra.RoomModels
{
    public class RoomVMD
    {
        [Display(Name = "Mã phòng")]
        public int Id { get; set; }

        [Display(Name = "Tên phòng")]
        public string Name { get; set; }

        [Display(Name = "Loại  phòng")]
        public string Format { get; set; }
    }
}