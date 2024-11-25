namespace Book_Commerce.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime CreatedAt { get; set; }
        public int StatusCode { get; set; }
    }
}
