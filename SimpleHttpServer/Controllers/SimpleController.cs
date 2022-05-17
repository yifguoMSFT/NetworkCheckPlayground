using System;

using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;

namespace SimpleHttpServer.Controllers
{
    [ApiController]
    public class SimpleController : ControllerBase
    {
        [Route("/")]
        [HttpGet]
        public string Ok()
        {
            return Environment.CurrentDirectory;
        }

        [Route("/{*filename}")]
        [HttpGet]
        public IActionResult GetFile(string filename)
        {
            string path = Path.Join(Program.NetworkCheckPath, filename);
            if (!System.IO.File.Exists(path))
            {
                return NotFound(path + " does not exists");
            }
            else
            {
                var fileContent = System.IO.File.ReadAllText(path);
                fileContent = Regex.Replace(fileContent, "^import.*?\r{0,1}\n", "", RegexOptions.Multiline);
                fileContent = Regex.Replace(fileContent, "^export(.*?\r{0,1}\n)", "$1", RegexOptions.Multiline);
                fileContent = Regex.Replace(fileContent, "class (.*?)\\{", "var $1 = class{", RegexOptions.Multiline);
                return File(Encoding.UTF8.GetBytes(fileContent), "text/javascript");
            }
        }
    }
}
