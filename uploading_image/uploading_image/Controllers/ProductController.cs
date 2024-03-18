
using Microsoft.AspNetCore.Mvc;
using uploading_image.models;

namespace uploading_image.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPut("UploadImage")]
        public async Task<IActionResult>UploadImage(IFormFile formFile,string productcode)
        {
            APIResponseFormat response = new ApiResponse();
            try
            {
                string Filepath = GetFilepath(productcode);
               if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                string imagepath = Filepath + "\\" + productcode + ".png";
               if(System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
               using(FileStream stream=System.IO.File.Create(imagepath)) 
                {
                 await stream.CopyToAsync(stream);
                    response.ResponseCode = 200;
                    response.Result = "pass";
                }
            }
            catch (Exception ex)
            {
                response.Errormessage=ex.Message;
            }
            
            return Ok(response);
        }

        [NonAction]
        private string GetFilepath(string productcode)
        {
            return this._webHostEnvironment.WebRootPath + "\\Upload\\product\\" + productcode;
        }
    }
}
