using DrMusicRepo;
using Microsoft.AspNetCore.Mvc;

namespace RestDRMusic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : Controller
    {
        private readonly RecordRepoDb _repo;

        public RecordsController(RecordRepoDb repo)
        {
            _repo = repo;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public ActionResult<IEnumerable<Record>> Get([FromQuery] string? title, [FromQuery] string? artist, [FromQuery] int? durationBot, [FromQuery] int? durationTop, [FromQuery] int? publicationYearBot, [FromQuery] int? publicationYearTop) 
        {
            return Ok(_repo.GetAll(titleIncludes: title, artistIncludes: artist, durationMin: durationBot, durationMax: durationTop, publicationYearMin: publicationYearBot, publicationYearMax: publicationYearTop));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Record> Get(int id)
        {
            Record? record = _repo.GetById(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Record> Post([FromBody] Record record)
        {
            try { _repo.Add(record); }
            catch (ArgumentNullException e) { return BadRequest(e.Message); }
            catch (ArgumentOutOfRangeException e) { return BadRequest(e.Message); }
            return Created($"Record called {record.Title} has been created", record);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Record> Delete(int id)
        {
            Record? record = _repo.Remove(id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("{id}")]
        public ActionResult<Record> Put(int id, Record record)
        {
            try
            {
                Record? recordToUpdate = _repo.Update(id, record);
                if (recordToUpdate == null)
                {
                    return NotFound();
                }
                return Ok(recordToUpdate);
            }
            catch(ArgumentNullException e) { return BadRequest(e.Message); }
            catch(ArgumentOutOfRangeException e) { return BadRequest(e.Message);}
        }

    }
}
