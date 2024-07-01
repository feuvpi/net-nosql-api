using System.Text.Json.Serialization;
using BCryptNet = BCrypt.Net.BCrypt;
namespace api_reservas.Core.Dtos
{
    public class CreateUserDTO
    {
        private string _password;
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Password
        {
            set
            {
                var salt = BCryptNet.GenerateSalt();
                PasswordSalt = salt;
                _password = BCryptNet.HashPassword(value, salt);
            }
            get { return _password; }
        }
        [JsonIgnore]
        public string PasswordSalt { get; set; }
    }
}


