namespace StatusStream.Domain.Entities
{
    public class Status
    {
        public int Id { get; private set; }
        public string User { get; private set; } = string.Empty;
        public string Message { get; private set; } = string.Empty;
        public DateTime Timestamp { get; private set; } = DateTime.UtcNow;

        public Status(int id,string user , string message ) {
            Id = id;
            User = user;
            Message = message;
            Timestamp = DateTime.UtcNow;

        }
    }
}
