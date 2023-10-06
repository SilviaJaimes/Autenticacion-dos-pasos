using System.Net.Mail;

namespace API.Dtos
{
    public class EmailDTO
    {
        public string Para { get; set; } = string.Empty;
        public string Asunto { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public Attachment ArchivoAdjunto { get; set; }
    }
}