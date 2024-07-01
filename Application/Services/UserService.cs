using api_reservas.Core.Models;
using Infrastructure.Repository;
using MongoDB.Driver;

namespace api_reservas.Services
{
    public class UserService : BaseService<Usuario>
    {
        public UserService(MyMongoRepository repository) : base(repository)
        {
        }

        public async Task<Usuario> FindByEmail(string email)
        {
            return await _collection.Find(x => x.Email == email).FirstOrDefaultAsync();
        }

    }
}
