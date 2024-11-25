namespace Book_Commerce.Models
{
    public class CartItemViewModel : BaseViewModel
    {
       // public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
