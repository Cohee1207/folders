using System.ComponentModel.DataAnnotations;

namespace Folders.DB.Models
{
    public class Folder : Entity
    {
        [Required]
        [MaxLength(ModelConstants.MaxNameLength)]
        public string Name { get; set; }
       
        public long? ParentId { get; set; }

        public Folder Parent { get; set; }
    }
}
