using System.ComponentModel.DataAnnotations;

using static NoteHub.Common.EntityValidationMessages.Note;
using static NoteHub.Common.EntityValidationConstants.Note;

namespace NoteHub.Web.ViewModels
{
    public class NoteViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = NoteTitleRequiredMessage)]
        [StringLength(NoteTitleMaxLength,
            MinimumLength = NoteTitleMinLength,
            ErrorMessage = NoteTitleLengthMessage)]
        public string Title { get; set; } = null!;


        [Required(ErrorMessage = NoteContentRequiredMessage)]
        [StringLength(NoteContentMaxLength,
            MinimumLength = NoteContentMinLength,
            ErrorMessage = NoteContentLengthMessage)]
        public string Content { get; set; } = null!;
    }
}
