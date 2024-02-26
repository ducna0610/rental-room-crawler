using MySql.Data.MySqlClient;
using RentalRoomCrawler.Models;

namespace RentalRoomCrawler.Repositories;

public class RoomRepository
{

    /// <summary>
    /// 
    /// This class is used for insert data to Database
    /// 
    /// </summary>
    public async Task<bool> InsertRoomAsync(Room? data, MySqlConnection connection)
    {
        try
        {
            MySqlCommand comm = connection.CreateCommand();
            comm.CommandText = "INSERT INTO rooms (owner_name, title, description, area, phone, price, address, image, is_active, user_id)"
                            + "VALUES(@owner_name, @title, @description, @area, @phone, @price, @address, @image, @is_active, @user_id)";
            comm.Parameters.AddWithValue("@owner_name", data.OwnerName);
            comm.Parameters.AddWithValue("@title", data.Title);
            comm.Parameters.AddWithValue("@description", data.Description);
            comm.Parameters.AddWithValue("@area", data.Area);
            comm.Parameters.AddWithValue("@phone", data.Phone);
            comm.Parameters.AddWithValue("@price", data.Price);
            comm.Parameters.AddWithValue("@address", data.Address);
            comm.Parameters.AddWithValue("@image", data.Image);
            comm.Parameters.AddWithValue("@is_active", true);
            comm.Parameters.AddWithValue("@user_id", 2);

            return comm.ExecuteNonQuery() > 0;
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
