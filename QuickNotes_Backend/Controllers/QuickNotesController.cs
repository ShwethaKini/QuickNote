using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using QuickNotes.DataAccess.Entities;
using QuickNotes.Models;
using QuickNotes.Services;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace QuickNotes.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class QuickNotesController : ControllerBase
    {

        private readonly ILogger<QuickNotesController> _logger;
        private readonly IQuickNoteService _quickNoteService;
        private string _userName;

        public QuickNotesController(ILogger<QuickNotesController> logger, IQuickNoteService quickNoteService)
        {
            _logger = logger;
            _quickNoteService = quickNoteService;
            

        }

        /// <summary>
        /// Gets all the quicknotes
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuickNoteResponseModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                _userName = User.Identity?.Name;
                List<QuickNote> notes = await _quickNoteService.GetAllNotes(_userName);
                if (!notes.Any())
                {
                    return NoContent();
                }
                QuickNoteResponseModel model = new QuickNoteResponseModel()
                {
                    Count = notes.Count(),
                    Items = notes
                };
                return new  OkObjectResult(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.InnerException);
                throw;
            }
        }

        /// <summary>
        /// Creates the quicknote
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(QuickNote))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddNote([FromBody] QuickNoteRequestModel request)
        {
            try
            {
                _userName = User.Identity?.Name;
                if (string.IsNullOrWhiteSpace(request.Message))
                {
                    return BadRequest(new ProblemDetails()
                    {
                        Detail = "Supplied message is empty or null. Please try with valid message.",
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Invalid request model"
                    });
                }
                QuickNote note = await _quickNoteService.AddNote(_userName,request.Message);

                return new ObjectResult(note) { StatusCode = StatusCodes.Status201Created };
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ProblemDetails()
                {
                    Detail = "Supplied message is null or empty. Please try with valid message.",
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid request model"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Modifies the quicknote of given id
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuickNote))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> EditNote([FromRoute,Required] string id, [FromBody] QuickNoteRequestModel request)
        {
            try
            {
                _userName = User.Identity?.Name;
                if (string.IsNullOrWhiteSpace(request.Message) || string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest(new ProblemDetails()
                    {
                        Detail = "Message and id can not be empty or null. Please try with valid data",
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Invalid data model"
                    });
                }

                QuickNote note = await _quickNoteService.EditNote(new Guid(id),_userName, request.Message);

                return new ObjectResult(note);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ProblemDetails()
                {
                    Detail = "Quick note with id :{id} does not exist. Please try with valid id",
                    Status = StatusCodes.Status404NotFound,
                    Title = "Id not found"
                });
            }
            catch (InvalidDataException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ProblemDetails()
                {
                    Detail = "Message and id can not be empty or null. Please try with valid data",
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid data model"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }

        /// <summary>
        /// Delets the quicknote of given id
        /// </summary>
        [HttpDelete ("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteNote([FromRoute] string id)
        {
            try
            {
                _userName = User.Identity?.Name;
                if (string.IsNullOrWhiteSpace(id))
                {
                    return BadRequest(new ProblemDetails()
                    {
                        Detail = "Id can not be null or empty. Please try with some valid id.",
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Invalid Id"
                    });
                }

                bool result = await _quickNoteService.DeleteNote(new Guid(id), _userName);

                return new ObjectResult(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex.Message);
                return NotFound(new ProblemDetails()
                {
                    Detail = $"Message with supplied id :{id} could not be found. Please check Id.",
                    Status = StatusCodes.Status404NotFound,
                    Title = "Id not found"
                });
            }
            catch (InvalidDataException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(new ProblemDetails()
                {
                    Detail = "Id can not be null or empty. Please try with some valid id.",
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid Id"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}