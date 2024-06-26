using System;
using System.Collections.Generic;

namespace OrderManagementAPI.Domain.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetail = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}
