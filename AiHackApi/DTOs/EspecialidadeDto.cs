namespace AiHackApi.DTOs
{
    public class EspecialidadeDto
    {
        public int IdEspecialidade { get; set; }
        public string NomeEspecialidade { get; set; }

        public EspecialidadeDto(int idEspecialidade, string nomeEspecialidade)
        {
            IdEspecialidade = idEspecialidade;
            NomeEspecialidade = nomeEspecialidade ?? throw new ArgumentNullException(nameof(nomeEspecialidade));
        }

        public EspecialidadeDto()
        {
            NomeEspecialidade = string.Empty;
        }
    }
}
