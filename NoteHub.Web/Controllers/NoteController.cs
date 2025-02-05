using Microsoft.AspNetCore.Mvc;
using NoteHub.Services.Data.Interfaces;
using NoteHub.Web.ViewModels;

namespace NoteHub.Web.Controllers
{
    public class NoteController : Controller
    {
        private readonly INoteService _noteService;

        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var notes = await _noteService.GetAllNotes();
            return View(notes); 
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NoteViewModel noteViewModel)
        {
            if (ModelState.IsValid)
            {
                await _noteService.CreateNote(noteViewModel); 
                return RedirectToAction(nameof(Index));
            }

            return View(noteViewModel); 
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var note = await _noteService.GetNoteById(id);
            if (note == null)
            {
                return NotFound(); 
            }

            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, NoteViewModel noteViewModel)
        {
            if (id != noteViewModel.Id)
            {
                return NotFound(); 
            }

            if (ModelState.IsValid)
            {
                await _noteService.UpdateNote(noteViewModel);
                return RedirectToAction(nameof(Index)); 
            }

            return View(noteViewModel); 
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var note = await _noteService.GetNoteById(id);
            if (note == null)
            {
                return NotFound(); 
            }

            return View(note); 
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _noteService.DeleteNote(id); 
            return RedirectToAction(nameof(Index)); 
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var note = await _noteService.GetNoteById(id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note); 
        }
    }
}
