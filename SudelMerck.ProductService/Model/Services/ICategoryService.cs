using SudelMerck.ProductService.Infrastructure.Context;
using SudelMerck.ProductService.Model.Entity;

namespace SudelMerck.ProductService.Model.Services
{
    public interface ICategoryService
    {
        List<CategotyDto> GetCategories();
        void AddCategory(CategotyDto category);

    }
    public class CetegoryService : ICategoryService
    {
        private readonly ProductDataBaseContext _context;
        public CetegoryService(ProductDataBaseContext context)
        {
            _context = context; 
        }
        public void AddCategory(CategotyDto category)
        {
            Category newCategory = new Category
            {
                Description = category.Name,
                Name = category.Name
            };
            _context.Categories.Add(newCategory);
            _context.SaveChanges();
        }

        public List<CategotyDto> GetCategories()
        {
            var data = _context.Categories.OrderBy(p => p.Name)
                .Select(p => new CategotyDto
                {
                    Name = p.Name,
                    Description = p.Description,
                    Id = p.Id
                }).ToList();
            return data;
        }
    }

    public class CategotyDto
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
