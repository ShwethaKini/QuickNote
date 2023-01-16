using System.ComponentModel.DataAnnotations;

namespace QuickNotes.Models
{
    public class QuickNoteRequestModel
    {
        [MaxLength(1000)]
        public string Message { get; set; }
    }
}
