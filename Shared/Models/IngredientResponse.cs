﻿namespace Shared.Models
{
    internal class IngredientResponse
    {
        public string Aisle { get; set; }
        public float Amount { get; set; }
        public int Id { get; set; }
        public string Image { get; set; }
        public string[] Meta { get; set; }
        public string Name { get; set; }
        public string Original { get; set; }
        public string OriginalName { get; set; }
        public string Unit { get; set; }
        public string UnitLong { get; set; }
        public string UnitShort { get; set; }
    }
}
