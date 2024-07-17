namespace DatingApp.Api.Contracts.Common
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public DateTime TimeStamp { get; set; }
    }
}
