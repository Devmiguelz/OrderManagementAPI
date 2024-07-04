using AutoMapper;
using OrderManagementAPI.Application.Contracts;
using OrderManagementAPI.Application.DTO.Order;
using OrderManagementAPI.Application.DTO.OrderDetail;
using OrderManagementAPI.Application.DTO.Product;
using OrderManagementAPI.Application.Exceptions;
using OrderManagementAPI.Domain.Entities;
using OrderManagementAPI.Domain.Services.Contracts;
using OrderManagementAPI.Infrastructure.DAO;
using System.Text.Json;

namespace OrderManagementAPI.Application 
{ 
    public class OrderService : IOrderService 
    {
        private const string RequestProduct = "http://localhost:5001/api/Product/GetProductList";
        private readonly IOrderDomain _orderDomain; 
        private readonly IMapper _mapper; 
        private readonly ApplicationDbContext _dataContext;
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderService(IOrderDomain orderDomain, IMapper mapper, IHttpClientFactory httpClientFactory, ApplicationDbContext dataContext) 
        { 
            _orderDomain = orderDomain; 
            _mapper = mapper;
            _dataContext = dataContext;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<OrderListDto>> GetOrderAsync()
        {
            try
            {
                var orders = await _orderDomain.GetOrderAsync();

                var guids = orders.SelectMany(x => x.OrderDetail).Select(x => x.ProductId).Distinct().ToList();
                var productList = await GetProductListDtoAsync(guids);

                List<OrderListDto> orderDtos = orders.Select(order =>
                {
                    var orderDto = new OrderListDto
                    {
                        ID = order.Id,
                        CustomerName = order.CustomerName,
                        TotalAmount = order.TotalAmount,
                        CreatedAt = order.CreatedAt,
                        OrderDetails = order.OrderDetail.Select(detail => {

                            var product = productList.Find(x => x.id.Equals(detail.ProductId));

                            return new OrderDetailListDto
                            {
                                ID = detail.Id,
                                OrderId = detail.OrderId,
                                ProductId = detail.ProductId,
                                Product = product!,
                                Quantity = detail.Quantity,
                                UnitPrice = detail.UnitPrice,
                                TotalPrice = detail.TotalPrice,
                            };

                        }).ToList()

                    };

                    return orderDto;

                }).ToList();

                return orderDtos;
            }
            catch (Exception ex)
            {
                    throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }

        public async Task<bool> SaveOrderAsync(OrderCreateDto orderCreate)
        {
            try
            {
                var guids = orderCreate.OrderDetails.Select(x => x.ProductId).ToList();
                var productList = await GetProductListDtoAsync(guids);
                if(productList.Count != guids.Count)
                {
                    return false;
                }

                var order = _mapper.Map<Order>(orderCreate);
                order.TotalAmount = orderCreate.OrderDetails.Select(x => productList.Find(p => p.id.Equals(x.ProductId)).price * x.Quantity).Sum();
                var orderId = await _orderDomain.SaveOrderAsync(order);

                List<OrderDetail> orderDetail = new();
                foreach (var item in orderCreate.OrderDetails)
                {
                    var detailOrder = _mapper.Map<OrderDetail>(item);
                    var product = productList.Find(x => x.id.Equals(item.ProductId));

                    detailOrder.Id = Guid.NewGuid();
                    detailOrder.OrderId = orderId;
                    detailOrder.UnitPrice = product!.price;

                    orderDetail.Add(detailOrder);
                }
                await _orderDomain.SaveOrderDetailAsync(orderDetail);
                await _dataContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                throw new CustomException(ErrorMessages.GenericError, ex);
            }
        }

        #region Private Method

        private async Task<List<ProductListDto>> GetProductListDtoAsync(List<Guid> guids)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsJsonAsync(RequestProduct, guids);

            if (!response.IsSuccessStatusCode)
            {
                return new List<ProductListDto>();
            }

            var responseStream = await response.Content.ReadAsStreamAsync();
            var products = await JsonSerializer.DeserializeAsync<List<ProductListDto>>(responseStream);

            return products ?? new List<ProductListDto>();
        }

        #endregion
    }
} 
