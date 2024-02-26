using System.ComponentModel;

namespace RentalRoomCrawler.Models;

public class Room
{
    [Description("Tên chủ trọ")]
    public string OwnerName { get; set; }

    [Description("Tiêu đề")]
    public string Title { get; set; }

    [Description("Mô tả")]
    public string Description { get; set; }

    [Description("Diện tích")]
    public string Area { get; set; }

    [Description("Số điện thoại")]
    public string Phone { get; set; }

    [Description("Giá cho thuê")]
    public string Price { get; set; }

    [Description("Địa chỉ")]
    public string Address { get; set; }

    [Description("Hình ảnh")]
    public string Image { get; set; }
}
