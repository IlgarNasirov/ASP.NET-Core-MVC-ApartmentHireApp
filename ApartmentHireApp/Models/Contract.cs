using System;
using System.Collections.Generic;

namespace ApartmentHireApp.Models;

public partial class Contract
{
    public int Id { get; set; }

    public int Apartmentid { get; set; }

    public DateTime Startdate { get; set; }

    public DateTime Enddate { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public double Cost { get; set; }

    public bool? Status { get; set; }

    public virtual Apartment Apartment { get; set; } = null!;
}
