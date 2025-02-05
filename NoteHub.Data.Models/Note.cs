namespace NoteHub.Data.Models
{
    public class Note
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
}
