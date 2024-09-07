namespace AiHackApi.DTOs
{
    public class BairroDto
    {
        public int IdBairro { get; set; }
        public string NomeBairro { get; set; }

        public BairroDto(int idBairro, string nomeBairro)
        {
            IdBairro = idBairro;
            NomeBairro = nomeBairro ?? throw new ArgumentNullException(nameof(nomeBairro));
        }

        public BairroDto()
        {
            NomeBairro = string.Empty;
        }
    }
}
