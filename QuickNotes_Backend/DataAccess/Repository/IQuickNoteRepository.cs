using QuickNotes.DataAccess.Entities;

namespace QuickNotes.DataAccess.Repository
{
    public interface IQuickNoteRepository
    {
        Task<QuickNote> AddNote(string userName, QuickNote newNote);
        Task<List<QuickNote>> GetAllNotes(string userName);
        Task<QuickNote> EditNote(string id, string message);
        Task<bool> DeleteNote(string id);
    }
}
