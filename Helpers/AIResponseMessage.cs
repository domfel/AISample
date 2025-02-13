
using CursorProjects;
using System.Collections.Generic;

public class AIResponseMessage
{  
    public string Text { get; set; }
    public bool HasOrder { get; set; }
    public IEnumerable<AIGeneratedOrder> Orders { get; set; }

}