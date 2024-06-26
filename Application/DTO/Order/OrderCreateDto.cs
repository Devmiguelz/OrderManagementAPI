using OrderManagementAPI.Application.DTO.OrderDetail;

namespace OrderManagementAPI.Application.DTO.Order
{
    public class OrderCreateDto
    {
        public string CustomerName { get; set; } = null!;
        public DateTime OrderDate { get; set; }
        public List<OrderDetailCreateDto> OrderDetails { get; set; } = null!;
    }
}
