namespace OrderManagementAPI.Application.DTO.Product
{
    public class ProductImageListDto
    {
        public Guid id { get; set; }
        public string fileName { get; set; } = null!;
        public string imageURL { get; set; } = null!;
        public DateTime createdAt { get; set; }
    }
}
