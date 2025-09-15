namespace Backend.DTOs
{
    public class UpdateTemplateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
