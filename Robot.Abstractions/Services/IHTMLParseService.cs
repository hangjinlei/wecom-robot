using HtmlAgilityPack;

namespace Robot.Abstractions.Services;

public interface IHTMLParseService
{
    public HtmlDocument Parse(string url);
}
