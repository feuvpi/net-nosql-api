using System.Text.Json.Serialization;
using BCryptNet = BCrypt.Net.BCrypt;

namespace api_reservas.Core.Dtos
{
    public class LoginDto
    {
        private string _password;
        public string Email { get; set; }
        public string Password
        {
            get { return Salt != null ? HashPassword(_password) : _password; }
            set { _password = value; }
        }
        [JsonIgnore]
        public string? Salt { get; set; }
        private string HashPassword(string password)
        {
                return BCryptNet.HashPassword(password, Salt);
        }

    }
}
