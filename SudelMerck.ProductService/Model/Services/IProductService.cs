using SudelMerck.ProductService.Infrastructure.Context;
using SudelMerck.ProductService.Model.Entity;

namespace SudelMerck.ProductService.Model.Services
{
    public interface IProductService
    {
        List<ProductDto> GetProduct();
        ProductDto GetProductById(Guid id);
        void AddProduct(ProductDto product);

        void AddNewProductDto(AddNewProductDto addNewProductDto);
    }

    public class ProductService : IProductService
    {
        private readonly ProductDataBaseContext _context;
        public ProductService(ProductDataBaseContext context)
        {
            _context = context;
        }

        public void AddNewProductDto(AddNewProductDto addNewProductDto)
        {
            var category = _context.Categories.Find(addNewProductDto.CategoryId);
            if (category == null)
                throw new Exception("Category not find ...!!!");
            Product product = new Product
            {
                CategoryId = addNewProductDto.CategoryId,
                Category = category,
                Description = addNewProductDto.Description,
                Image = addNewProductDto.Image,
                Name = addNewProductDto.Name,
                Price = addNewProductDto.Price
            };
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void AddProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }

        public List<ProductDto> GetProduct()
        {
            var data = _context.Products.Select(p => new ProductDto
            {
                Description = p.Description,
                Id = p.Id,
                Image = p.Image,
                Name = p.Name,
                Price = p.Price,
                ProductCategoryDto = new ProductCategoryDto
                {
                    CategoryId = p.Category.Id,
                    Category = p.Category.Name
                }
            }).ToList();
            return data;
        }

        public ProductDto GetProductById(Guid id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);
            if (product == null)
                throw new Exception("product Not find");
            else
                return new ProductDto
                {
                    Description = product.Description,
                    Id = product.Id,
                    Image = product.Image,
                    Name = product.Name,
                    Price = product.Price,
                    ProductCategoryDto = new ProductCategoryDto
                    {
                        Category = product.Category.Name,
                        CategoryId = product.Category.Id
                    }
                };
        }
    }

    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public ProductCategoryDto ProductCategoryDto { get; set; } 
    }

    public class ProductCategoryDto
    {
        public Guid CategoryId{ get; set; }
        public string Category { get; set; }
    }

    public class AddNewProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public Guid CategoryId { get; set; }
    }

}
