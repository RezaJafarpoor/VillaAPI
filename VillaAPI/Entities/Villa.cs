﻿namespace VillaAPI.Entities;

public class Villa
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Details { get; set; }
    public double Rate { get; set; }
    public int SQft { get; set; }
    public int Occupancy { get; set; }
    public string ImageUrl { get; set; }
    public string Amenity { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}