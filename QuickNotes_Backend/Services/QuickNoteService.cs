using QuickNotes.DataAccess.Entities;
using QuickNotes.DataAccess.Repository;

namespace QuickNotes.Services
{
    public class QuickNoteService : IQuickNoteService
    {
        private readonly IQuickNoteRepository _quickNoteRepository;
        public QuickNoteService(IQuickNoteRepository quickNoteRepository)
        {
            _quickNoteRepository = quickNoteRepository;
        }

        public async Task<QuickNote> AddNote(string userName,string message)
        {
           if(string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException("message");
            }
            
            QuickNote note = new QuickNote()
                {
                    Id = Guid.NewGuid(),
                    Message = message,
                    CreatedOn = DateTime.Now
                };
                await _quickNoteRepository.AddNote(userName,note);
                return note;

            
        }

        public async Task<bool> DeleteNote(Guid id, string userName)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    throw new InvalidDataException("The key is not valid");
                }
                return await _quickNoteRepository.DeleteNote(id +"_"+userName);
                
            } catch(KeyNotFoundException ex) 
            {
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<QuickNote> EditNote(Guid id,string userName, string message)
        {
            try
            {
                if (id == Guid.Empty || string.IsNullOrWhiteSpace(message))
                {
                    throw new InvalidDataException("The data is not valid");
                }
                return await _quickNoteRepository.EditNote(id +"_"+ userName, message);

            }
            catch (KeyNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<QuickNote>> GetAllNotes(string userName)
        {
            return await _quickNoteRepository.GetAllNotes(userName);
          
        }
    }
}
