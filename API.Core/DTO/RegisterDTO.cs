namespace api_reservas.Core.Dtos
{
    public class RegisterDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        public string Cpf { get; set; }
        public bool isCondominio { get; set; }
        public string Password { get; set; }

    }
}

