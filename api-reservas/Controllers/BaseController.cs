//using api_reservas.Models;
//using api_reservas.Models.Interfaces;
//using api_reservas.Repositories;
//using api_reservas.Services;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Options;

//namespace api_reservas.Controllers.BaseController
//{
//    [Controller]
//    public class BaseController<T> : ControllerBase where T : class, IBaseEntity, new()
//    {
//        public readonly BaseService<T> _service;
//        //public readonly MyMongoRepository _myMongoRepository;
//        private readonly IOptions<DatabaseSettings> _options;

//        public BaseController(BaseService<T> service)
//        {
//            _service = service;
//            //_myMongoRepository = repo;
//        }

//        [HttpGet]
//        public async Task<List<T>> GetAsync() => await _service.GetAsync();

//        [HttpGet("{id:length(24)}")]
//        public async Task<ActionResult<T>> GetAsync(string id)
//        {
//            var book = await _service.GetAsync(id);

//            if (book is null)
//            {
//                return NotFound();
//            }

//            return book;
//        }

//        [HttpPost]
//        public virtual async Task<IActionResult> Post(T entity2)
//        {
//            await _service.CreateAsync(entity2);

//            return NoContent();
//        }

//        [HttpPut]
//        public async Task<IActionResult> Update(string id, T entity)
//        {
//            var result = await _service.GetAsync(id);

//            if (result is null)
//            {
//                return NotFound();
//            }

//            entity.Id = result.Id;

//            await _service.UpdateAsync(id, entity);

//            return NoContent();
//        }

//        [HttpDelete("{id:length(24)}")]
//        public async Task<IActionResult> Delete(string id)
//        {
//            var result = await _service.GetAsync(id);

//            if (result is null)
//            {
//                return NotFound();
//            }

//            await _service.RemoveAsync(id);

//            return NoContent();
//        }
//    }
//}
