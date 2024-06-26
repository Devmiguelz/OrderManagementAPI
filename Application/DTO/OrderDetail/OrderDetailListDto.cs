using OrderManagementAPI.Application.DTO.Product;

namespace OrderManagementAPI.Application.DTO.OrderDetail
{
    public class OrderDetailListDto
    {
        public Guid ID { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public ProductListDto Product { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
