using QuickNotes.DataAccess.Entities;

namespace QuickNotes.Models
{
    public class QuickNoteResponseModel
    {
        public int Count { get; set; }
        public IEnumerable<QuickNote> Items { get; set; }
    }
}
