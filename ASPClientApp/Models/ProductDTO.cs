using System.Text.Json.Serialization;

namespace ASPClientApp.Models
{
    public class ProductDTO
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }
        [JsonPropertyName("productName")]

        public string ProductName { get; set; } = null!;
        [JsonPropertyName("productPrice")]

        public decimal Price { get; set; }
    }
}