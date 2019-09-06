using System;

namespace Folders.Web.Exceptions
{
    public class FolderNotFoundException : Exception
    {
        public FolderNotFoundException(string message) : base(message)
        {
        }
    }
}
