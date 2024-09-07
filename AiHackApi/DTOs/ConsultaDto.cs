namespace AiHackApi.DTOs
{
    public class ConsultaDto
    {
        public int IdConsulta { get; set; }
        public DateTime DataConsulta { get; set; }
        public string Diagnostico { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }

        public ConsultaDto(int idConsulta, DateTime dataConsulta, string diagnostico, int idPaciente, int idMedico)
        {
            IdConsulta = idConsulta;
            DataConsulta = dataConsulta;
            Diagnostico = diagnostico ?? throw new ArgumentNullException(nameof(diagnostico));
            IdPaciente = idPaciente;
            IdMedico = idMedico;
        }

        public ConsultaDto()
        {
            DataConsulta = DateTime.MinValue;
            Diagnostico = string.Empty;
        }
    }
}
