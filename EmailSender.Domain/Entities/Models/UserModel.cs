namespace EmailSender.Infrastructure.Entities.Models
{
    public class UserModel
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsRegisteredOrNot { get; set; }
    }
}
