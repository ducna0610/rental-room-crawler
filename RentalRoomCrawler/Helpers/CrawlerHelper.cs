using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;

namespace RentalRoomCrawler.Helpers;

public static class CrawlerHelper
{
    public static HtmlNode GetSingleNode(this HtmlDocument document, string cssSelector)
    {
        var note = document
            .DocumentNode
            .QuerySelector(cssSelector);

        return note;
    }

    public static List<HtmlNode> GetMultiNode(this HtmlDocument document, string cssSelector)
    {
        var nodes = document
            .DocumentNode
            .QuerySelectorAll(cssSelector)
            .ToList();

        return nodes;
    }

    public static string GetInnerText(this HtmlNode? node)
    {
        return node == null ? "Error" : node.InnerText.RemoveBreakLineTab();
    }
}