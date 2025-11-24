using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Shwetha.Services_LifetimeDemo;

namespace WebApi_Shwetha.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LifetimeController : ControllerBase
    {
        private readonly TransientService _transient1;
        private readonly TransientService _transient2;
        private readonly ScopedService _scoped1;
        private readonly ScopedService _scoped2;
        private readonly SingletonService _singleton1;
        private readonly SingletonService _singleton2;

        public LifetimeController(
            TransientService transient1,
            TransientService transient2,
            ScopedService scoped1,
            ScopedService scoped2,
            SingletonService singleton1,
            SingletonService singleton2)
        {
            _transient1 = transient1;
            _transient2 = transient2;
            _scoped1 = scoped1;
            _scoped2 = scoped2;
            _singleton1 = singleton1;
            _singleton2 = singleton2;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Transient1 = _transient1.GetGuid(),
                Transient2 = _transient2.GetGuid(),
                Scoped1 = _scoped1.GetGuid(),
                Scoped2 = _scoped2.GetGuid(),
                Singleton1 = _singleton1.GetGuid(),
                Singleton2 = _singleton2.GetGuid()
            });
        }
    }
}