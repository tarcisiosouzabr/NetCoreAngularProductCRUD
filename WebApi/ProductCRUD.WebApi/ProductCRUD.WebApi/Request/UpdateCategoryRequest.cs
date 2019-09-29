namespace ProductCRUD.WebApi.Request
{
    public class UpdateCategoryRequest
    {
        public int ProductId { get; set; }
        public int[] Categories { get; set; }
    }
}
