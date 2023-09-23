﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using ZeusMed.Core.Entities;

namespace ZeusMed.UI.Areas.ZeusMedAdmin.ViewModels.DoctorViewModel
{
    public class DoctorPostVM
    {
        [Required, StringLength(70)]
        public string? Fullname { get; set; } = null!;

        [Required, StringLength(255)]
        public string? ImagePath { get; set; } = null!;

        [Display(Name = "Associated Service")]
        public int AssociatedServiceId { get; set; }

        public List<Service> Services { get; set; }

        public DoctorDetail DoctorDetail { get; set; }
    }
}
