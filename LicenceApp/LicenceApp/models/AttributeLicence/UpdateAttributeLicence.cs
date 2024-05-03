﻿using LicenceApp.Enums;

namespace LicenceApp.models.AttributeLicence
{
    public class UpdateAttributeLicence
    {
        public int Id { get; set; }
        public required string Intutile { get; set; }
        public required string Description { get; set; }
        public required Types Type { get; set; }
    }
}