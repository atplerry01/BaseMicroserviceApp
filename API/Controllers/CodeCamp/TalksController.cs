using API.Data.CodeCamp.Services;
using API.Models.CodeCamp;
using LoggerClassLib.Attributes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers.CodeCamp
{
    [ApiController]
    [Route("api/camps/{moniker}/talks")]
    [ApiVersion("1.0")]
    public class TalksController : ControllerBase
    {
        private readonly ITalkService _talkService;   
        private readonly LinkGenerator _linkGenerator;

        public TalksController(ITalkService talkService, LinkGenerator linkGenerator)
        {
            _talkService = talkService;            
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        [LogUsage("View all Talks")]
        public async Task<ActionResult<TalkModel[]>> Get(string moniker)
        {
            var res = await _talkService.GetTalksByMonikerAsync(moniker, true);
            if (res.statusCode == 500)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, res.message);
            }

            return res.talkModel;          
        }

        [HttpGet("{id:int}")]
        [LogUsage("View a Talk")]
        public async Task<ActionResult<TalkModel>> Get(string moniker, int id)
        {
                var res = await _talkService.GetTalkByMonikerAsync(moniker, id);
                switch (res.statusCode)
                {
                    case 404:
                        return NotFound(res.message);
                    case 500:
                        return StatusCode(StatusCodes.Status500InternalServerError, res.message);
                    default:
                    return res.talkModel;
                }              
        }

        [HttpPost]
        [LogUsage("Create new Talk")]
        public async Task<ActionResult<TalkModel>> Post(string moniker, TalkModel model)
        {          
            var res = await _talkService.CreateTalk(moniker, model);
            switch (res.statusCode)
            {
                case 400:
                    return BadRequest(res.message);
                case 500:
                    return StatusCode(StatusCodes.Status500InternalServerError, res.message);
                default:
                    {
                        var url = _linkGenerator.GetPathByAction(HttpContext, "Get", values: new { moniker, id = res.talkModel.TalkId });
                        return Created(url, res.talkModel);
                    }
            }
        }

        [HttpPut("{id:int}")]
        [LogUsage("Update Talk")]
        public async Task<ActionResult<TalkModel>> Put(string moniker, int id, TalkModel model)
        {

            var res = await _talkService.UpdateTalk(moniker, id, model);
            switch (res.statusCode)
            {
                case 404:
                    return NotFound(res.message);
                case 400:
                    return BadRequest(res.message);
                case 500:
                    return StatusCode(StatusCodes.Status500InternalServerError, res.message);
                default:
                    return res.talkModel;
            }
        }

        [HttpDelete("{id:int}")]
        [LogUsage("Delete Talk")]
        public async Task<IActionResult> Delete(string moniker, int id)
        {
            var res = await _talkService.RemoveTalk(moniker, id);
            switch (res.statusCode)
            {
                case 404:
                    return NotFound(res.message);
                case 400:
                    return BadRequest(res.message);
                case 500:
                    return StatusCode(StatusCodes.Status500InternalServerError, res.message);
                default:
                    return Ok();
            }
        }
    }
}
