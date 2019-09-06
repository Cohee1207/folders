using System;
using System.Linq;
using Folders.DB;
using Folders.Web.Exceptions;
using Folders.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace Folders.Web.Services
{
    public class PathParser
    {
        private readonly FoldersContext dbContext;
        
        public PathParser(FoldersContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public FolderViewModel Parse(string url)
        {
            url = url.Trim('/');
            var segments = url.Split('/').Select(Uri.UnescapeDataString).ToArray();
            string rootName = segments.FirstOrDefault();

            if (string.IsNullOrWhiteSpace(rootName))
            {
                return new FolderViewModel
                {
                    Name = "Roots List",
                    Children = dbContext.Roots.Include(r => r.Root).Select(r => new FolderViewModel { Name = r.Root.Name, Url = Uri.EscapeDataString(r.Root.Name) }).ToList()
                };
            }

            var root = dbContext.Roots.Include(r => r.Root).SingleOrDefault(x => x.Root.Name == rootName);

            if (root == null)
            {
                throw new FolderNotFoundException($"Could not find root folder: {rootName}");
            }

            var currentFolder = root.Root;

            for (int i = 1; i < segments.Length; i++)
            {
                string currentSegment = segments[i];
                currentFolder = dbContext.Folders.SingleOrDefault(f => f.ParentId == currentFolder.Id && f.Name == currentSegment);

                if (currentFolder == null)
                {
                    throw new FolderNotFoundException($"Could not find folder: {currentSegment}");
                }
            }

            var children = dbContext.Folders.Where(f => f.ParentId == currentFolder.Id).ToList();

            return new FolderViewModel
            {
                Name = currentFolder.Name,
                Children = children.Select(f => new FolderViewModel { Name = f.Name, Url = $"/{url}/{f.Name}" }).ToList()
            };
        }
    }
}
