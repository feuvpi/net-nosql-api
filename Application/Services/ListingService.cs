using api_reservas.Core.Models;
using Infrastructure.Repository;

namespace api_reservas.Services
{
    public class ListingService : BaseService<Listing>
    {
        public ListingService(MyMongoRepository repository) : base(repository)
        {

        }
    }
}
