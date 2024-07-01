using api_reservas.Core.Models;
using api_reservas.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_reservas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : BaseController<Listing>
    {
        public CustomersController(BaseService<Listing> service) : base(service)
        {
        }
    }
}
