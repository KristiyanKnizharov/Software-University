﻿namespace CarDealer.DTO
{
    public class CarDTO
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public long TravellDistance { get; set; }

        public int[] PartsId { get; set; }
    }
}
