using Microsoft.AspNetCore.Mvc;
using reactSyncfusionApp.Models;
using Syncfusion.DocIO.DLS;
using System.Diagnostics;

namespace reactSyncfusionApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult TestWordDocument()
        {
            // Creates an empty Word document
            WordDocument document = new WordDocument();

            // Add a section and paragraph
            document.EnsureMinimal();

            // Add text
            document.LastParagraph.AppendText("Hello World");

            // Save the Word document
            string documentsPath = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments);

            string filePath = Path.Combine(documentsPath, "Result.docx");

            document.Save(filePath);

            // Close the document
            document.Close();

            return Content("Word document created.");

        }

        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok(new
            {
                message = "Hello from ASP.NET Core!"
            });
        }
    }
}
