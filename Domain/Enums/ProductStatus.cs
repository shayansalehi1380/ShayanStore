﻿using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum ProductStatus
{
    [Display(Name = "موجود")]
    Available=0,
    [Display(Name = "غیر موجود")]
    NotAvailable=1,
    [Display(Name = "به زودی")]
    ComingSoon=2,
    [Display(Name = "پیش فروش")]
    OutOfDate=3
}