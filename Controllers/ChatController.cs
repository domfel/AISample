using CursorProjects.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace CursorProjects.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private IChatConnector _chatConnector;

    public ChatController(IChatConnector connector)
    {
        _chatConnector = connector;
    }

    [HttpPost]

    public async Task<IActionResult> Get([FromBody] ApiInputMessage message)
    {
        if (Validator.MessageIsValid(message))
        {
            var response = await _chatConnector.GetChatResponse(message);
            return Ok(response);
        }
        else
        {
            return BadRequest("Invalid message");
        }

    }
}