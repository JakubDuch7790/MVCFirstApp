﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFirstApp.Models;

public class OrderHeader
{
    public int Id { get; set; }
    public string ApplicationUserId { get; set; }
    [ForeignKey("ApplicationUserId")]
    [ValidateNever]
    public ApplicationUser ApplicationUser { get; set; }

    public DateTime OrderDate { get; set; }
    public DateTime ShippingDay { get; set; }
    public double OrderTotal { get; set; }

    public string? OrderStatus { get; set; }
    public string? PaymentStatus { get; set; }
    public string? TrackingNumber { get; set; }
    public string? Carrier { get; set; }

    public DateTime PaymentDate { get; set; }
    public DateTime PaymentDueDate { get; set; }

    public string? PaymentIntentId { get; set; }

    [Required]
    public string Name { get; set; }
    [Required]
    public string? StreetAdress { get; set; }
    [Required]
    public string? City { get; set; }
    [Required]
    public string? Country { get; set; }
    [Required]
    public string? PostalCode { get; set; }


}
