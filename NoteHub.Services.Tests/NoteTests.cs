using Microsoft.EntityFrameworkCore;
using NoteHub.Data;
using NoteHub.Data.Models;
using NoteHub.Services.Data;
using NoteHub.Web.ViewModels;

namespace NoteHub.Services.Tests
{
    [TestFixture]
    public class NoteServiceTests
    {
        private NotesDbContext _context;
        private NoteService _noteService;

        [SetUp]
        public async Task Setup() 
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new NotesDbContext(options);
            _noteService = new NoteService(_context);

            // Добавяне на тестови данни
            await _context.Notes.AddRangeAsync(new List<Note>
            {
                new Note { Id = Guid.NewGuid(), Title = "Note 1", Content = "Content 1" },
                new Note { Id = Guid.NewGuid(), Title = "Note 2", Content = "Content 2" }
            });

            await _context.SaveChangesAsync();
        }

        [TearDown]
        public async Task TearDown() 
        {
            await _context.Database.EnsureDeletedAsync();
            await _context.DisposeAsync();
        }

        [Test]
        public async Task GetAllNotes_ShouldReturnAllNotes()
        {
            var result = await _noteService.GetAllNotes();
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetNoteById_ShouldReturnCorrectNote()
        {
            var note = await _context.Notes.FirstAsync();
            var result = await _noteService.GetNoteById(note.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual(note.Id, result.Id);
            Assert.AreEqual(note.Title, result.Title);
        }

        [Test]
        public async Task CreateNote_ShouldAddNote()
        {
            var newNote = new NoteViewModel { Title = "New Note", Content = "New Content" };

            await _noteService.CreateNote(newNote);
            var result = await _context.Notes.FirstOrDefaultAsync(n => n.Title == "New Note");

            Assert.IsNotNull(result);
            Assert.AreEqual("New Note", result.Title);
            Assert.AreEqual("New Content", result.Content);
        }

        [Test]
        public async Task UpdateNote_ShouldModifyExistingNote()
        {
            var existingNote = await _context.Notes.FirstAsync();
            var updatedNote = new NoteViewModel { Id = existingNote.Id, Title = "Updated", Content = "Updated Content" };

            await _noteService.UpdateNote(updatedNote);
            var result = await _context.Notes.FindAsync(existingNote.Id);

            Assert.IsNotNull(result);
            Assert.AreEqual("Updated", result.Title);
            Assert.AreEqual("Updated Content", result.Content);
        }

        [Test]
        public async Task DeleteNote_ShouldRemoveNote()
        {
            var noteToDelete = await _context.Notes.FirstAsync();

            await _noteService.DeleteNote(noteToDelete.Id);
            var result = await _context.Notes.FindAsync(noteToDelete.Id);

            Assert.IsNull(result);
        }
    }
}
