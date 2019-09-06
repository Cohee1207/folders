namespace Folders.DB.Models
{
    public class RootFolder : Entity
    {
        public long RootId { get; set; }

        public Folder Root { get; set; }
    }
}
