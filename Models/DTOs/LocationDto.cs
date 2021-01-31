using System;


namespace TVSeriesAPI.Models.DTOs
{
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int CharacterId { get; set; }
    }
}
