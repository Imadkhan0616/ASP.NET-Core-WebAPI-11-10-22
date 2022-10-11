using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_Core_Web_Api_CRUD_Run.Model.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ASP.NET_Core_Web_Api_CRUD_Run.Model.DomainModel;
using ASP.NET_Core_Web_Api_CRUD_Run.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ASP.NET_Core_Web_Api_CRUD_Run.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly NotesDbContext notesDbContext;

        public NoteController(NotesDbContext notesDbContext)
        {
            this.notesDbContext = notesDbContext;
        }
        [HttpPost]
        public IActionResult AddNote(AddNoteReq addNoteRequest)
        {
            //Convert the DTO to Domain Model
            var note = new Model.DomainModel.Note
            {
                Title = addNoteRequest.Title,
                Description = addNoteRequest.Description,
                Colorhex = addNoteRequest.Colorhex,
                CreatedDate = DateTime.Now
            };

            notesDbContext.Notes.Add(note);
            notesDbContext.SaveChanges();

            return Ok(note);

        }

        [HttpGet]
        public IActionResult GetAllNotes()
        {
            var notes = notesDbContext.Notes.ToList();

            var notesDTO = new List<Model.DTO.Note>();

            foreach (var note in notes)
            {
                notesDTO.Add(new Model.DTO.Note
                {
                    Id=note.Id,
                    Title = note.Title,
                    Description=note.Description,
                    Colorhex=note.Colorhex,
                    CreatedDate=note.CreatedDate
                });
            }
            return Ok();
        }

        [HttpGet]
        [Route("{Id:Guid}")]
        public IActionResult GetNoteById(Guid Id)
        {
            var noteDomainObject = notesDbContext.Notes.Find(Id);

            if (noteDomainObject != null)
            {
                var noteDTO = new Model.DTO.Note
                {
                    Id = noteDomainObject.Id,
                    Title = noteDomainObject.Title,
                    Description = noteDomainObject.Description,
                    Colorhex = noteDomainObject.Colorhex,
                    CreatedDate = noteDomainObject.CreatedDate
                };
                return Ok(noteDTO);

            }
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateNote(Guid Id, UpdateNoteReq updateNoteReq) 
        {
            var existingNote = notesDbContext.Notes.Find(Id);

            if (existingNote !=null)
            {
                existingNote.Title = updateNoteReq.Title;
                existingNote.Description = updateNoteReq.Description;
                existingNote.Colorhex = updateNoteReq.Colorhex;

                notesDbContext.SaveChanges();
                return Ok(existingNote);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("{Id:Guid}")]
        public IActionResult DeleteNote(Guid Id)
        {
            var existingNote = notesDbContext.Notes.Find(Id);
            if (existingNote != null)
            {
                notesDbContext.Notes.Remove(existingNote);
                notesDbContext.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}
