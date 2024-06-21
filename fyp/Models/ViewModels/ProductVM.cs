﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace fyp.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> BrandList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> MetalList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> PurityList { get; set; }
    }
}
