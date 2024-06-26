using AutoMapper;
using OrderManagementAPI.Application.DTO.Order;
using OrderManagementAPI.Application.DTO.OrderDetail;
using OrderManagementAPI.Domain.Entities;

namespace OrderManagementAPI.Application.Mapper
{
    public class GlobalMapperProfile: Profile
    {
        public GlobalMapperProfile()
        {
            CreateMap<OrderCreateDto, Order>();
            CreateMap<Order, OrderListDto>();
            CreateMap<OrderDetailCreateDto, OrderDetail>();
        }
    }
}
