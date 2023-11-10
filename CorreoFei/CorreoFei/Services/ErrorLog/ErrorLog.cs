﻿using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace CorreoFei.Services.ErrorLog
{
    public class ErrorLog : IErrorLog
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ErrorLog(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor htttpCOntextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = htttpCOntextAccessor;
        }

        [HttpPost]
        public async Task ErrorLogAsync(string Mensaje)
        {
            try
            {
                string webRootPath = _webHostEnvironment.WebRootPath;
                string path = "";
                path = Path.Combine(webRootPath, "log");
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var feature = _httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>();
                string localIPAddr = feature?.LocalIpAddress.ToString();

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(path,"log.txt"),true))
                {
                    await outputFile.WriteLineAsync(Mensaje + " - " +
                       _httpContextAccessor.HttpContext.User.Identity.Name +
                       " - " + localIPAddr + " - " + DateTime.Now.ToString());
                }
            }
            catch
            {

            }
        }
    }
}