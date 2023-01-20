namespace Robot.VOA.Models;


public class VOA
{
    public string title { get; set; }
    public string url { get; set; }
    public Content[] content { get; set; }
    public int dueTime { get; set; }
    public int period { get; set; }
}

public class Content
{
    public string category { get; set; }
    public Html html { get; set; }
}

public class Html
{
    public string newsPath { get; set; }
    public string titleAttribute { get; set; }
    public string srcAttribute { get; set; }
    public Title title { get; set; }
}

public class Title
{
    public string relativeXPath { get; set; }
    public string relativeAttribute { get; set; }
    public object absoluteXPath { get; set; }
    public object absoluteattribute { get; set; }
    public object absoluteAttribute { get; set; }
}
