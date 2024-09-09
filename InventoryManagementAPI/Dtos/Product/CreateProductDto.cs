﻿namespace InventoryManagementAPI.Dtos.Product
{
    public class CreateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
        public int SupplierId { get; set; }
    }
}
