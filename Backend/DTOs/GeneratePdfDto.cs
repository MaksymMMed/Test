namespace Backend.DTOs
{
    public class GeneratePdfDto
    {
        public Guid TemplateId { get; set; }
        public Dictionary<string, string> PlaceholderValues { get; set; }
    }
}
