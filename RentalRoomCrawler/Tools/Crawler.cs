using HtmlAgilityPack;
using RentalRoomCrawler.Helpers;
using RentalRoomCrawler.Models;
using System.Text;

namespace RentalRoomCrawler.Tools;

public class Crawler
{
    private const string BaseUrl = "https://phongtro123.com";

    private readonly string _savePathImage;

    public Crawler(string savePathImage)
    {
        _savePathImage = savePathImage;
    }

    public List<Room> Crawling(int stopRoom = 1)
    {
        var dataCrawl = new List<Room>();
        var count = 0;

        var web = new HtmlWeb()
        {
            AutoDetectEncoding = false,
            OverrideEncoding = Encoding.UTF8
        };

        for (int i = 1; i <= 5000; i++)
        {
            var requestPerPage = BaseUrl + $"?page={i}";
            var documentForListItem = web.Load(requestPerPage);

            var listNodeRoomItem = documentForListItem
                .GetMultiNode("h3.post-title > a");

            foreach (var href in listNodeRoomItem)
            {
                if (dataCrawl.Count == stopRoom) return dataCrawl;
                var detailLink = href.Attributes["href"].Value;

                if (!string.IsNullOrEmpty(detailLink))
                {
                    var documentForDetail = web.Load(BaseUrl + detailLink);

                    if (documentForDetail != null)
                    {
                        var roomOwnerName = documentForDetail.GetSingleNode("#aside > div > span.author-name").GetInnerText();
                        var roomTitle = documentForDetail.GetSingleNode("h1.page-h1 > a").GetInnerText();
                        var roomDescription = documentForDetail.GetSingleNode("section.post-main-content > div.section-content").GetInnerText();
                        var roomArea = documentForDetail.GetSingleNode("div.acreage > span").GetInnerText().ReplaceMultiToEmpty(new List<string>() { "m2" });
                        var roomPhone = documentForDetail.GetSingleNode("#aside > div > a.btn.author-phone")?.GetInnerText();
                        var roomPrice = documentForDetail.GetSingleNode("div.price > span").GetInnerText().ReplaceMultiToEmpty(new List<string>() { " triệu/tháng", " đồng/tháng" });
                        var roomAddress = documentForDetail.GetSingleNode("span.post-fix-bar-address").GetInnerText();

                        var roomImageNode = documentForDetail.GetSingleNode("div.swiper-slide > img");
                        var src = roomImageNode.Attributes["src"].Value;
                        var roomImage = DateTime.Now.Ticks + ".png";
                        var pathReturn = DownloadHelper.DownloadImageFromUri(src, _savePathImage + roomImage);

                        var room = new Room()
                        {
                            OwnerName = roomOwnerName,
                            Title = roomTitle,
                            Description = roomDescription,
                            Area = roomArea,
                            Phone = roomPhone,
                            Price = roomPrice,
                            Address = roomAddress,
                            Image = roomImage,
                        };
                        dataCrawl.Add(room);
                    }
                }
                count++;
                Console.WriteLine($"Crawling {count} Room(s)");
            }
        }

        return null;
    }
}
