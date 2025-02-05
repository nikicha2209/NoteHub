using Microsoft.EntityFrameworkCore;
using NoteHub.Data;
using NoteHub.Data.Models;
using NoteHub.Services.Data.Interfaces;
using NoteHub.Web.ViewModels;

namespace NoteHub.Services.Data
{
    public class NoteService : INoteService
    {
        private readonly NotesDbContext _context;

        public NoteService(NotesDbContext context)
        {
            _context = context;
        }

        //Get all notes
        public async Task<IEnumerable<NoteViewModel>> GetAllNotes()
        {
            var notes = await _context.Notes.ToListAsync(); 

            var noteViewModels = notes.Select(n => new NoteViewModel
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content
            });

            return noteViewModels;
        }

        //Get a note by ID
        public async Task<NoteViewModel?> GetNoteById(Guid id)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == id);

            if (note == null)
            {
                return null;
            }

            var noteViewModel = new NoteViewModel
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content
            };

            return noteViewModel;
        }

        //Create a new note
        public async Task CreateNote(NoteViewModel noteViewModel)
        {
            var note = new Note
            {
                Id = Guid.NewGuid(),
                Title = noteViewModel.Title,
                Content = noteViewModel.Content
            };

            await _context.AddAsync(note); 
            await _context.SaveChangesAsync(); 
        }

        // Update a note
        public async Task UpdateNote(NoteViewModel noteViewModel)
        {
            var note = await _context.Notes.FirstOrDefaultAsync(n => n.Id == noteViewModel.Id);

            if (note == null)
            {
                return;
            } 

            note.Title = noteViewModel.Title;
            note.Content = noteViewModel.Content;

            _context.Update(note); 
            await _context.SaveChangesAsync(); 
        }

        // Delete a note
        public async Task DeleteNote(Guid id)
        {
            var note = await _context.Notes.FindAsync(id); 

            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
        }
    }
}
