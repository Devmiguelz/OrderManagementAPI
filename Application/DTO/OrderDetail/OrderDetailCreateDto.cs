namespace OrderManagementAPI.Application.DTO.OrderDetail
{
    public class OrderDetailCreateDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
