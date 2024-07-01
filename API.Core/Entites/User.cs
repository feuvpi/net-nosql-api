using System.ComponentModel.DataAnnotations;
using api_reservas.Core.Dtos;
using api_reservas.Core.Models.BaseModels;

namespace api_reservas.Core.Models
{
    public class User : BaseEntity
    {
        private string _password;
        public User() { }
        public User(CreateUserDTO novoUsuario)
        {
            Name = novoUsuario.Nome;
            Email = novoUsuario.Email;
            Password = novoUsuario.Password;
            PasswordSalt = novoUsuario.PasswordSalt;
        }

        [Required(ErrorMessage = "You need to have a name")]
        [MaxLength(50, ErrorMessage = "It cant exceed 50 characteres")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        //public string Cnpj { get; set; }
        //public string Cpf { get; set; }
        //public bool IsCondominio { get; set; }
    }
}

