using OrderManagementAPI.Application.DTO.OrderDetail;

namespace OrderManagementAPI.Application.DTO.Order
{
    public class OrderListDto
    {
        public Guid ID { get; set; }
        public string CustomerName { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderDetailListDto> OrderDetails { get; set; } = null!;
    }
}
