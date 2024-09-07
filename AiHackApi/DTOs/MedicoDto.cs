namespace AiHackApi.DTOs
{
    public class MedicoDto
    {
        public int IdMedico { get; set; }
        public string NomeMedico { get; set; }
        public string CRM { get; set; }
        public string Especialidade { get; set; }

        public MedicoDto(int idMedico, string nomeMedico, string crm, string especialidade)
        {
            IdMedico = idMedico;
            NomeMedico = nomeMedico ?? throw new ArgumentNullException(nameof(nomeMedico));
            CRM = crm ?? throw new ArgumentNullException(nameof(crm));
            Especialidade = especialidade ?? throw new ArgumentNullException(nameof(especialidade));
        }

        public MedicoDto()
        {
            NomeMedico = string.Empty;
            CRM = string.Empty;
            Especialidade = string.Empty;
        }
    }
}
