using System.ComponentModel.DataAnnotations;
using api_reservas.Core.Dtos;
using api_reservas.Core.Models.BaseModels;


namespace api_reservas.Core.Models
{
    public class Usuario : BaseEntity
    {
        private string _password;
        public Usuario() { }
        public Usuario(CreateUserDTO novoUsuario)
        {
            Name = novoUsuario.Nome;
            Email = novoUsuario.Email;
            Password = novoUsuario.Password;
            PasswordSalt = novoUsuario.PasswordSalt;
        }

        [Required(ErrorMessage = "O Nome é Obrigatório")]
        [MaxLength(50, ErrorMessage = "O tamanho não pode exceder 50 caracteres")]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        //public string Cnpj { get; set; }
        //public string Cpf { get; set; }
        //public bool IsCondominio { get; set; }
    }
}

