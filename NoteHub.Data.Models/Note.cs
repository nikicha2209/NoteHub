using System.ComponentModel.DataAnnotations;

using static NoteHub.Common.EntityValidationConstants.Note;

namespace NoteHub.Data.Models
{
    public class Note
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(NoteTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MaxLength(NoteContentMaxLength)]
        public string Content { get; set; } = null!;
    }
}
