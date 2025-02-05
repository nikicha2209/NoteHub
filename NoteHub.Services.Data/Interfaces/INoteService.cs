using NoteHub.Web.ViewModels;

namespace NoteHub.Services.Data.Interfaces
{
    public interface INoteService
    {
        Task<IEnumerable<NoteViewModel>> GetAllNotes();
        Task<NoteViewModel?> GetNoteById(Guid id);
        Task CreateNote(NoteViewModel noteViewModel);
        Task UpdateNote(NoteViewModel noteViewModel);
        Task DeleteNote(Guid id);



    }
}
