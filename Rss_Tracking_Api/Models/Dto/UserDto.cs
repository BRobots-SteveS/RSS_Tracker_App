namespace Rss_Tracking_Api.Models.Dto
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
    }
}
