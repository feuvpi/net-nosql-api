using api_reservas.Core.Models;
using System.Text.Json.Serialization;
using BCryptNet = BCrypt.Net.BCrypt;
namespace api_reservas.Core.Dtos
{
    public class CreateUserDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string? Cnpj { get; set; }
        public string? Cpf { get; set; }
        public bool isCondominio { get; set; }
        private string _password;
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


