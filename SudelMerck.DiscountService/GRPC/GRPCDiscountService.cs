using DiscountService.Proto;
using Grpc.Core;
using SudelMerck.DiscountService.Model.Services;

namespace SudelMerck.DiscountService.GRPC
{
    public class GRPCDiscountService:DiscountServiceProto.DiscountServiceProtoBase
    {
        private readonly IDiscountService _discount;
        public GRPCDiscountService(IDiscountService discount)
        {
            this._discount = discount;
        }
        public override Task<ResultGetDiscountByCode> GetDiscountCode(RequestgetDiscountByCode request, ServerCallContext context)
        {
            var data = _discount.GetDiscountByCode(request.Code);

            return Task.FromResult(new ResultGetDiscountByCode
            {
                Amount = data.Amount.ToString(),
                Code = data.Code,
                Id = data.Id.ToString(),
                Used = data.Used
            });
        }

        public override Task<ResultAddNewDiscount> AddNewDiscount(RequestAddNewDiscount request, ServerCallContext context)
        {
            var result = _discount.AddNewDiscount(request.Code, request.Amount);
            return Task.FromResult(new ResultAddNewDiscount
            {
                IsSuccess = true
            });
        }

        public override Task<ResultUseDiscount> UseDiscount(RequestUseDiscount request, ServerCallContext context)
        {
            var result = _discount.UseDiscount(Guid.Parse(request.Id.ToString()));
            return Task.FromResult(new ResultUseDiscount
            {
                IsSuccess= true
            });
        }
    }
}
