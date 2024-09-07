namespace AiHackApi.DTOs
{
    public class PacienteDto
    {
        public int IdPaciente { get; set; }

        public string NomePaciente { get; set; }

        public string CPF { get; set; }

        // Construtor para inicializar os valores
        public PacienteDto(int idPaciente, string nomePaciente, string cpf)
        {
            IdPaciente = idPaciente;
            NomePaciente = nomePaciente ?? throw new ArgumentNullException(nameof(nomePaciente));
            CPF = cpf ?? throw new ArgumentNullException(nameof(cpf));
        }

        // Construtor padrão
        public PacienteDto()
        {
            NomePaciente = string.Empty;
            CPF = string.Empty;
        }
    }
}
