using Microsoft.EntityFrameworkCore;
using OrderManagementAPI.Domain.Entities;
using OrderManagementAPI.Domain.Services.Contracts;
using OrderManagementAPI.Infrastructure.DAO;

namespace OrderManagementAPI.Domain.Services 
{ 
   public class OrderDomain : IOrderDomain 
   { 
       private readonly ApplicationDbContext _dbContext; 
       public OrderDomain(ApplicationDbContext dbContext) 
       {
            _dbContext = dbContext;  
       }

        public async Task<List<Order>> GetOrderAsync()
        { 
            return await _dbContext.Order.AsNoTracking().Include(x => x.OrderDetail).ToListAsync();
        }

        public async Task<Guid> SaveOrderAsync(Order order)
        {
            order.Id = Guid.NewGuid();
            order.CreatedAt = DateTime.Now;
            order.IsDeleted = false;
            await _dbContext.Order.AddAsync(order);
            return order.Id;
        }

        public async Task<bool> SaveOrderDetailAsync(List<OrderDetail> orderDetail)
        {
            await _dbContext.OrderDetail.AddRangeAsync(orderDetail);
            return true;
        }
    } 
} 
