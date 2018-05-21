using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspnetNote.Controllers
{
    public class UploadController : Controller
    {
        private readonly IHostingEnvironment _environment;

        public UploadController(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost, Route("api/upload")]
        public async Task<IActionResult> ImageUpload(IFormFile file)
        {
            // 이미지나 파일을 업로드 할 때 필요한 구성
            // 1. Path(경로) - 어디에 저장할지 결정

            var path = Path.Combine(_environment.WebRootPath, @"images\upload");
            // 2. Name(이름) - DateTime, GUID

            var fileFullName = file.FileName.Split('.');
            var fileName = $"{Guid.NewGuid()}.{fileFullName[1]}";
            
            // 3. Extension(확장자) - jpg, png... txt

            using (var fileStream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Ok(new { file="/images/upload/" + fileName, success=true});

            // URL 접근방식
            // ASP.NET - 호스트명/ + api/upload
            // Javascript 호스트명 + api/upload
        }
    }
}