using ActorRepositoryLib;
using Microsoft.AspNetCore.Mvc;
using RestApiDemo.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {

        private readonly IActorsRepository _repo;


        public ActorsController(IActorsRepository repo)
        {
            _repo = repo;
        }





        // GET: api/<ActorsController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.GetAll());
        }

        // GET api/<ActorsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_repo.GetById(id));
        }


        // Get Api
        [HttpGet]
        [Route("byName/{name}")]
        public IActionResult Get(string name)
        {
            List<IActor> actors = _repo.GetAll();
            IActor actor = actors.Find(x => x.Name.Contains(name));

            return (actor is null) ? NotFound() : Ok(actor);
        }

        // Get Api
        [HttpGet]
        [Route("Sort")]
        public IActionResult Sort()
        {
            List<IActor> actors = _repo.GetSortByName();

            return actors.Count > 0 ? Ok(actors) : NotFound();
        }

        // filter
        [HttpGet]
        [Route("Serve")]
        public IActionResult GetFilter([FromQuery] FilterDTO filter)
        {
            List<IActor> actors = _repo.GetByFilter(lowYear: filter.lowYear, highYear: filter.highYear);
            return actors.Count == 0 ? NoContent() : Ok(actors);
        }



        // POST api/<ActorsController>
        [HttpPost]
        public IActionResult Post([FromBody] ActorDTO actorDTO)
        {
            IActor newActor = ActorConverter.ActorDTOtoActor(actorDTO);
            return Ok(_repo.Add(newActor));

        }

        // PUT api/<ActorsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ActorDTO dto)
        {
            IActor realActor = ActorConverter.ActorDTOtoActor(dto);
            return Ok(_repo.Update(id, realActor));

        }

        // DELETE api/<ActorsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                return Ok(_repo.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
