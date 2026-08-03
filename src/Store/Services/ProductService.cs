using DataEntities;
using System.Text.Json;

namespace Store.Services;

public class ProductService
{
    private readonly HttpClient httpClient;

    public ProductService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<List<Product>> GetProducts()
    {
        var response = await httpClient.GetAsync("/api/Product");
        if (!response.IsSuccessStatusCode)
        {
            return [];
        }

        return await response.Content.ReadFromJsonAsync(ProductSerializerContext.Default.ListProduct) ?? [];
    }

    public async Task<Product?> GetProductById(int id)
    {
        var response = await httpClient.GetAsync($"/api/Product/{id}");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync(ProductSerializerContext.Default.Product)
            : null;
    }

    public async Task<bool> CreateProduct(Product product)
    {
        var response = await httpClient.PostAsJsonAsync(
            "/api/Product",
            product,
            ProductSerializerContext.Default.Product);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateProduct(int id, Product product)
    {
        var response = await httpClient.PutAsJsonAsync(
            $"/api/Product/{id}",
            product,
            ProductSerializerContext.Default.Product);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProduct(int id)
    {
        var response = await httpClient.DeleteAsync($"/api/Product/{id}");
        return response.IsSuccessStatusCode;
    }
}