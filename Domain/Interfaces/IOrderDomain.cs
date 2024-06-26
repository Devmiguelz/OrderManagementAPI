using OrderManagementAPI.Domain.Entities;

namespace OrderManagementAPI.Domain.Services.Contracts 
{ 
    public interface IOrderDomain 
    {
        public Task<Guid> SaveOrderAsync(Order order);
        public Task<bool> SaveOrderDetailAsync(List<OrderDetail> orderDetail);
        public Task<List<Order>> GetOrderAsync();
    } 
} 
