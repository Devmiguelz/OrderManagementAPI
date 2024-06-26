namespace OrderManagementAPI.Application.DTO.Product
{
    public class ProductListDto
    {
        public Guid id { get; set; }
        public string name { get; set; } = null!;
        public string? description { get; set; }
        public decimal price { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime? updatedAt { get; set; }
        public DateTime? deletedAt { get; set; }
        public List<ProductImageListDto> images { get; set; } = null!;
    }
}
