using PeopleHubAPI.Model;
using PeopleHubAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PeopleHubAPI.Controllers
{
    [ApiController]
    [Route("api/v1/people")]
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;
        private readonly ILogger<PeopleController> _logger;

        public PeopleController(IPeopleRepository peopleRepository, ILogger<PeopleController> logger)
        {
            _peopleRepository = peopleRepository ?? throw new ArgumentNullException(nameof(peopleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); ;
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] PeopleViewModel peopleView)
        {
            var filePath = Path.Combine("Storage", peopleView.Photo.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            peopleView.Photo.CopyTo(fileStream);

            var people = new People(peopleView.Id, peopleView.Name, peopleView.Age, filePath);

            _peopleRepository.Add(people);

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            try
            {
                var people = _peopleRepository.Get(id);
                if (people == null)
                {
                    _logger.LogWarning($"Id {id} não encontrado.");
                    return NotFound($"Id {id} não encontrado.");
                }

                var dataBytes = System.IO.File.ReadAllBytes(people.photo);
                if (dataBytes == null || dataBytes.Length == 0)
                {
                    _logger.LogWarning($"Foto para o id {id} não encontrada.");
                    return NotFound($"Foto para o id {id} não encontrada.");
                }

                return File(dataBytes, "image/png");
            }
            catch (FileNotFoundException ex)
            {
                _logger.LogError(ex, $"Arquivo não encontrado para o id {id}.");
                return NotFound($"Arquivo não encontrado para o id {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocorreu um erro ao baixar a foto do id {id}.");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            //_logger.Log(LogLevel.Error, "Houve um Erro TESTE");

            //throw new Exception("Erro de TESTE");

            var peoples = _peopleRepository.Get(pageNumber, pageQuantity);

            //_logger.LogInformation("Teste");

            return Ok(peoples);
        }
    }
}