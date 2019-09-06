using System.Collections.Generic;

namespace Folders.Web.Models
{
    public class FolderViewModel
    {
        public string Url { get; set; }

        public string Name { get; set; }

        public List<FolderViewModel> Children { get; set; }
    }
}