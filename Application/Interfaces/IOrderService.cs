using OrderManagementAPI.Application.DTO.Order;

namespace OrderManagementAPI.Application.Contracts 
{ 
    public interface IOrderService 
    {
        public Task<bool> SaveOrderAsync(OrderCreateDto orderCreate);
        public Task<IEnumerable<OrderListDto>> GetOrderAsync();
    } 
}  
