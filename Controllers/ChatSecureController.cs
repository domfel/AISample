using CursorProjects.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CursorProjects.Controllers;

[ApiController]
[Route("[controller]")]
public class ChatSecureController : ControllerBase
{
    private IChatConnector _chatConnector;

    public ChatSecureController(IChatConnector connector)
    {
        _chatConnector = connector;
    }

    [HttpPost]
    public async Task<IActionResult> Get2([FromBody] ApiInputMessage message)
    {
        if (Validator.MessageIsValid(message))
        {
            var response = await _chatConnector.GetChatSecureResponse(message.Messages);
            return Ok(response);
        }
        else
        {
            return BadRequest("Invalid message");
        }
    }
}