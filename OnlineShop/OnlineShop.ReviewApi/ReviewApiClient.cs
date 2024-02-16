using OnlineShop.ReviewApi.Models;

namespace OnlineShop.ReviewApi;

public class ReviewApiClient
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ReviewApiClient(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<List<Review>> GetByProductIdAsync(int productId)
    {
        var httpClient = _httpClientFactory.CreateClient("ReviewApi");
        var reviews =
            await httpClient.GetFromJsonAsync<List<Review>>($"/Review/TryGetByProductId?productId={productId}") ??
            new List<Review>();
        return reviews;
    } 

    public void Add(AddReview review)
    {
    }
}