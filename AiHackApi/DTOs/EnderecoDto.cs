namespace AiHackApi.DTOs
{
    public class EnderecoDto
    {
        public int IdEndereco { get; set; }
        public string Rua { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }

        public EnderecoDto(int idEndereco, string rua, string cidade, string estado, string cep)
        {
            IdEndereco = idEndereco;
            Rua = rua ?? throw new ArgumentNullException(nameof(rua));
            Cidade = cidade ?? throw new ArgumentNullException(nameof(cidade));
            Estado = estado ?? throw new ArgumentNullException(nameof(estado));
            CEP = cep ?? throw new ArgumentNullException(nameof(cep));
        }

        public EnderecoDto()
        {
            Rua = string.Empty;
            Cidade = string.Empty;
            Estado = string.Empty;
            CEP = string.Empty;
        }
    }
}
