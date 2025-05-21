namespace Products.Dto
{
    public class GetProductResponse
    {
        public ProductDto? Product { get; set; }

        public GetProductResponse(ProductDto productDto)
        {
            Product = productDto;
        }
    }
}
