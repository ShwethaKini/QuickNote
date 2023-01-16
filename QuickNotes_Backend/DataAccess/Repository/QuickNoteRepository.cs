using QuickNotes.DataAccess.Entities;

namespace QuickNotes.DataAccess.Repository
{
    public class QuickNoteRepository : IQuickNoteRepository
    {
        private IDictionary<string, QuickNote> _items = new Dictionary<string, QuickNote>();

        public QuickNoteRepository()
        {
            var id1 = Guid.NewGuid();
            QuickNote note = new QuickNote()
            {
                Id = id1,
                Message = "Default Note2",
                CreatedOn = DateTime.Now,
                
            };
            _items.Add(id1 + "_" + "admin", note);
            var id2 = Guid.NewGuid();
            QuickNote note1 = new QuickNote()
            {
                Id = id2,
                Message = "Default Note1",
                CreatedOn = DateTime.Now,

            };
            _items.Add(id2 + "_" + "user", note1);
        }
       
        public async Task<QuickNote> AddNote(string userName, QuickNote newNote)
        {
           _items.Add(newNote.Id + "_" + userName, newNote);
             return await Task.FromResult(newNote);
        }

        public async Task<bool> DeleteNote(string id)
        {
            if (!_items.ContainsKey(id))
            {
                throw new KeyNotFoundException("Key does not exist");
            }
            else
            {
                _items.Remove(id);
                return await Task.FromResult(true);
            }
        }

        public async Task<QuickNote> EditNote(string id, string message)
        {
            if (!_items.ContainsKey(id))
            {
                throw new KeyNotFoundException("Key does not exist");
            }
            else
            {
                _items[id].Message= message;
                return await Task.FromResult(_items[id]);
            }
        }

        public async Task<List<QuickNote>> GetAllNotes(string userName)
        {
           
            return await Task.FromResult(_items.Where(kvp => kvp.Key.Contains(userName)).Select(kvp => kvp.Value).ToList());
        }
    }
}
