using AutoMapper;
using SudelMerck.DiscountService.Infrastructure.Context;
using SudelMerck.DiscountService.Model.Entities;

namespace SudelMerck.DiscountService.Model.Services
{
    public interface IDiscountService
    {
        DiscountDto GetDiscountByCode(string Code);
        bool UseDiscount(Guid Id);
        bool AddNewDiscount(string Code,int Amount);
    }

    public class DiscountService : IDiscountService
    {
        private readonly DiscountDataBaseContext _context;
        private readonly IMapper  _mapper;
        public DiscountService(DiscountDataBaseContext context,IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public bool AddNewDiscount(string Code, int Amount)
        {
            DiscountCode discount = new DiscountCode
            {
                Amount = Amount,
                Code = Code
            };
            _context.Discount.Add(discount);
            _context.SaveChanges();
            return true;
        }

        public DiscountDto GetDiscountByCode(string Code)
        {
            var discount = _context.Discount.SingleOrDefault(p => p.Code == Code);
            if (discount == null)
                throw new Exception("there is no Discount");
            var result = _mapper.Map<DiscountDto>(discount);
            return result;      
        }

        public bool UseDiscount(Guid Id)
        {
            var discount = _context.Discount.Find(Id);
            if (discount == null)
                throw new Exception("there is no exception");
            discount.Used = true;
            _context.SaveChanges();
            return true;
        }
    }

    public class DiscountDto
    { 
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string Code { get; set; }
        public bool Used { get; set; }
    }
}
