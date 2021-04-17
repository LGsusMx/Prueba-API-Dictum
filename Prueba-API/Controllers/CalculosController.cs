using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Prueba_API.Auth;
using Prueba_API.Models;
using System;

namespace Prueba_API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("calculos")]
    public class CalculosController : ControllerBase
    {
        private readonly ILogger<CalculosController> _logger;
        private readonly IJwtAuthenticationService _authService;
        public CalculosController(ILogger<CalculosController> logger, IJwtAuthenticationService authService)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _authService = authService;
        }

        [HttpGet]
        public float VNA()
        {

            spreadsheet HojaDeCalculo = new spreadsheet("EjercicioDictum2");
            HojaDeCalculo.SetCell("A2", 100);
            HojaDeCalculo.Calculate();
            return HojaDeCalculo.GetCell("A4");
        }
        //Metodo demostrativo para simular el logeo 
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(string username, string password)
        {
            var token = _authService.Authenticate(username, password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }
    }
}
