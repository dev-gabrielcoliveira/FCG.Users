namespace FCG.Users.Application.DTOs
{
    public class UsuarioAtualizarInput
    {
        public int Id { get; set; }

        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string Senha { get; set; }
    }
}
