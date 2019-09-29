namespace ProductCRUD.WebApi.Request
{
    public class ProductImageRequest
    {
        public int ProductId { get; set; }
        public string ImageBase64 { get; set; }
    }
}
