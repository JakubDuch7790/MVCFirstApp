﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFirstApp.Models.ViewModels;

public class ShoppingCartVM
{
    //public ShoppingCart ShoppingCart { get; set; }

    [ValidateNever]
    public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
    public double OrderTotal { get; set; }
}
