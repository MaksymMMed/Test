namespace Backend.Entity
{
    public class Template
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public List<string> Placeholders { get; set; }
    }
}
