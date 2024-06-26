using FluentValidation;
using OrderManagementAPI.Application.DTO.Order;
using OrderManagementAPI.Application.DTO.OrderDetail;

namespace OrderManagementAPI.Application.Validators
{
    public class OrderCreateDtoValidator: AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("CustomerName is required.");
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("OrderDate is required.");

            RuleForEach(x => x.OrderDetails).SetValidator(new OrderDetailCreateDtoValidator());
        }
    }

    public class OrderDetailCreateDtoValidator: AbstractValidator<OrderDetailCreateDto>
    {
        public OrderDetailCreateDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("ProductId is required.")
                .Must(id => id != Guid.Empty).WithMessage("ProductId must be a valid non-empty GUID.");
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0).WithMessage("Quantity must be greater than or equal to 0.");
        }
    }
}
