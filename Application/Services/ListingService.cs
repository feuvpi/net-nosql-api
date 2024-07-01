using api_reservas.Core.Models;
using Infrastructure.Repository;

namespace api_reservas.Services
{
    public class ReservaService : BaseService<Reserva>
    {
        public ReservaService(MyMongoRepository repository) : base(repository)
        {

        }
    }
}
