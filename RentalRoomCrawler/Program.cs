using MySql.Data.MySqlClient;
using RentalRoomCrawler.Helpers;
using RentalRoomCrawler.Models;
using RentalRoomCrawler.Repositories;
using RentalRoomCrawler.Tools;

const string connectionString = "server=localhost;uid=root;pwd=mysql;database=rental_room";

var currentPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) ?? "";
var savePathImage = currentPath.Split("bin")[0] + @"Image\";
var savePathExcel = currentPath.Split("bin")[0] + @"Excel\";

Console.WriteLine("Please do not turn off the app while crawling!");

try
{
    await using var connection = new MySqlConnection(connectionString);
    connection.Open();

    var tool = new Crawler(savePathImage);
    var roomRepository = new RoomRepository();

    // Crawl data 
    var dataCrawl = tool.Crawling(2);

    var fileName = DateTime.Now.Ticks + "_list-room.xlsx";
    ExportToExcel<Room>.GenerateExcel(dataCrawl, savePathExcel + fileName);

    foreach (var data in dataCrawl)
    {
        await roomRepository.InsertRoomAsync(data, connection);
    }

    connection.Close();
    Console.WriteLine($"Crawled {dataCrawl.Count} Room(s)");
    Console.WriteLine($"File excel: " + fileName);
    Console.WriteLine("DONE !!!");
}
catch (Exception e)
{
    throw;
}