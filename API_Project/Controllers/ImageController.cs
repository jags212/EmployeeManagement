using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace API_Project.Controllers
{
    [Produces("application/json")]
    [Route("api/Image")]
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment _environment;
        public ImageController(IHostingEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        // POST: api/Image
        [HttpPost]
        public async Task<ActionResult<Photo>> Post(Photo photoForm)
        {
            if (photoForm == null)
            {
                return BadRequest();
            }

            var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            if (photoForm.photo.Length > 0)
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, photoForm.photo.FileName), FileMode.Create))
                {
                    await photoForm.photo.CopyToAsync(fileStream);
                }
            }

            return CreatedAtAction(nameof(Get), new { id = photoForm.fileName },
                    photoForm);
        }

        // GET api/values  
        [HttpGet]
        public IActionResult Get(string fileName)
        {

            try
            {
                var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                Photo photoJSON = new Photo();
                photoJSON.filePath = Path.Combine(_environment.WebRootPath, "uploads"+ "\\" + fileName + "");

                FileInfo fi = new FileInfo(uploads + "\\" + fileName + "");
                photoJSON.fileExt = fi.Extension.Replace(".", string.Empty);
                photoJSON.fileName = fileName;
                return Ok(photoJSON);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "No Image Available");
            }

            //Byte[] b;
            //var uploads = Path.Combine(_environment.WebRootPath, "uploads");
            //string extension;          
            //if (fileName != null)
            //{
            //    b = System.IO.File.ReadAllBytes(uploads + "\\" + fileName + "");
            //    FileInfo fi = new FileInfo(uploads + "\\" + fileName + "");
            //    extension = fi.Extension.Replace(".", string.Empty);
            //}
            //else
            //{
            //    return Content("No action is defined for this type value");
            //}
            //return Content(Convert.ToString(b));
        }

    }
}
