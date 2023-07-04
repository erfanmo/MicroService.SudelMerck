using RestSharp;
using System.Text.Json;

namespace SudelMerck.Webfrontend.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProduct();
        ProductDto GetProductById(Guid id);
    }

    public class ProductService : IProductService
    {
        private readonly RestClient restClient;
        public ProductService(RestClient restClient)
        {
            this.restClient = restClient;
            restClient.Timeout = -1;
        }
    
        public IEnumerable<ProductDto> GetAllProduct()
        {
            var request = new RestRequest("/api/product",Method.GET);
            IRestResponse response = restClient.Execute(request);
            Console.WriteLine(response.Content);
            var product = JsonSerializer.Deserialize<List<ProductDto>>(response.Content);
            return product;

        }

        public ProductDto GetProductById(Guid id)
        {
            var request = new RestRequest($"/api/Product/{id}", Method.GET);
            IRestResponse response = restClient.Execute(request);
            var product = JsonSerializer.Deserialize<ProductDto>(response.Content);
            return product;
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class ProductCategoryDto
    {
        public string CategoryId { get; set; }
        public string Category { get; set; }
    }

    public class ProductDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public ProductCategoryDto ProductCategoryDto { get; set; }
    }
}
