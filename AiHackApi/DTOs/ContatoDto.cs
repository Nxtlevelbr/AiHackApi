namespace AiHackApi.DTOs
{
    public class ContatoDto
    {
        public int IdContato { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }

        public ContatoDto(int idContato, string telefone, string email)
        {
            IdContato = idContato;
            Telefone = telefone ?? throw new ArgumentNullException(nameof(telefone));
            Email = email ?? throw new ArgumentNullException(nameof(email));
        }

        public ContatoDto()
        {
            Telefone = string.Empty;
            Email = string.Empty;
        }
    }
}
