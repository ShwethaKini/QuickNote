using QuickNotes.DataAccess.Entities;

namespace QuickNotes.Services
{
    public interface IQuickNoteService
    {
        Task<QuickNote> AddNote(string userName, string message);
        Task<List<QuickNote>> GetAllNotes(string userName);
        Task<QuickNote> EditNote(Guid id,string userName, string message);
        Task<bool> DeleteNote(Guid id, string userName);
    }
}
