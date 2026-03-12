namespace SurvayBasketApi.Controllers;

[Route("api/[controller]")] //the url will be "api/name of the controller"
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
   private readonly IPollService _pollService = pollService;

    [HttpGet("")] // Action Or Endpoint
    //[Route("getAll")]
    public IActionResult GetAll() // IActionResult allows you to return many type of things (data, status code, ...)
    {
        var polls = _pollService.GetAll();
        return Ok(polls);
    }

    [HttpGet("{id}")] //Will map the id to the function --> in future.
    public IActionResult Get([FromRoute] int id)
    {
        var poll = _pollService.Get(id);
        
        if(poll is null)
            return NotFound();

        var config = new TypeAdapterConfig();

        config.NewConfig<Poll, PollResponse>()
            .Map(dest => dest.Notes, src => src.Description);

        var respose = poll.Adapt<PollResponse>();
        return Ok(respose);
    }

    [HttpPost("")]
    public IActionResult Add([FromBody] CreatePollRequest request) //Here we binding the requset object to the inner function
    {
        //var newPoll = _pollService.Add(request.MapToPoll());
        //return CreatedAtAction(nameof(Get), new {id = newPoll.Id},newPoll);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] CreatePollRequest request)
    {
        //var isUpdated = _pollService.Update(id, request.MapToPoll());

        //return !isUpdated ? NotFound() : NoContent();
        return NoContent();

    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var isDeleted = _pollService.Delete(id);

        return !isDeleted ? NotFound() : NoContent();

    }


}
