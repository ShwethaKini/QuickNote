namespace QuickNotes.DataAccess.Entities
{
    public class QuickNote
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
