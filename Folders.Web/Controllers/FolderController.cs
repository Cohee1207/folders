using System;
using Folders.Web.Exceptions;
using Folders.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Folders.Web.Controllers
{
    public class FolderController : Controller
    {
        private readonly PathParser parser;

        public FolderController(PathParser parser)
        {
            this.parser = parser;
        }

        [HttpGet("{*url}")]
        public IActionResult Index(string url)
        {
            try
            {
                var viewModel = parser.Parse(url ?? string.Empty);
                return View(viewModel);
            }
            catch (FolderNotFoundException exception)
            {
                return NotFound(exception.Message);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

    }
}
