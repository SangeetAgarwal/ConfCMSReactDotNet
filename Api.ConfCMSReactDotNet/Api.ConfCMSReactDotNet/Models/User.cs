namespace Api.ConfCMSReactDotNet.Models
{
    public class User
    {
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
