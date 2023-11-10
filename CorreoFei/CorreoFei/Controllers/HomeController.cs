using CorreoFei.Models;
using CorreoFei.Services.Email;
using CorreoFei.Services.ErrorLog;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CorreoFei.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmail _email;
        private readonly IErrorLog _errorLog;

        public HomeController(IErrorLog errorlog, IEmail email)
        {
            _errorLog = errorlog;
            _email = email;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> IndexAsync(ContactViewModel contacto)
        {
            string eror = "";
            if (!ModelState.IsValid)
            {
                try
                {
                    await _email.EnviaCOrreoAsync("Correo electronico desde FEI", contacto.Correo, null, null, CuerpoCorreo(contacto.Nombre));
                    return RedirectToAction(nameof(Success));
                }catch (Exception ex) {
                    await _errorLog.ErrorLogAsync(ex.Message);
                    eror = ex.Message;
                }
            }
            ModelState.AddModelError("","No ha sido posible enviar el correo, Intentelo nuevamente." + eror);
            return View();
        }

        public string CuerpoCorreo(string nombre)
        {
            string Mensaje = $"<p> Estimado/Estimada usuario, {nombre}</p>";
            Mensaje += "<p>Por este medio le informamos que su participacion ha sido recibida</p>";
            Mensaje += "<p>Agradecemos su particion.</p>";
            Mensaje += "<br /><br /><br /><div>------------------------------------------------";
            return Mensaje;
        
        }

        public IActionResult Success()
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
    }
}