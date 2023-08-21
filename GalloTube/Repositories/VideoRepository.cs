using System.Data;
using GalloTube.Interfaces;
using GalloTube.Models;
using MySql.Data.MySqlClient;

namespace GalloTube.Repositories;

public class MovieRepository : IMovieRepository
{
    readonly string connectionString = "server=localhost;port=3306;database=GalloTubedb;uid=root;pwd=''";

    public void Create(Video model)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "insert into Video(Name, Description, UploadDate, Duration, Thumbnail, VideoFile) "
              + "values (@Name, @Description, @UploadDate, @Duration, @Thumbnail, @VideoFile)";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Name", model.Name);
        command.Parameters.AddWithValue("@Description", model.Description);
        command.Parameters.AddWithValue("@UploadDate", model.UploadDate);
        command.Parameters.AddWithValue("@Duration", model.Duration);
        command.Parameters.AddWithValue("@Thumbnail", model.Thumbnail);
        command.Parameters.AddWithValue("@VideoFile", model.VideoFile);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void Delete(int? id)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "delete from Video where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", id);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }

    public List<Video> ReadAll()
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Video";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        
        List<Video> videos = new();
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            Video video = new()
            {
                Id = reader.GetInt32("id"),
                Name = reader.GetString("Name"),
                Description = reader.GetString("Description"),
                UploadDate = reader.GetDateTime("UploadDate"),
                Duration = reader.GetInt16("Duration"),
                Thumbnail = reader.GetString("Thumbnail"),
                VideoFile = reader.GetString("VideoFile")
            };
            videos.Add(video);
        }
        connection.Close();
        return movies;
    }

    public Movie ReadById(int? id)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "select * from Movie where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", id);
        
        connection.Open();
        MySqlDataReader reader = command.ExecuteReader();
        reader.Read();
        if (reader.HasRows)
        {
            Movie movie = new()
            {
                Id = reader.GetInt32("id"),
                Title = reader.GetString("title"),
                OriginalTitle = reader.GetString("originalTitle"),
                Synopsis = reader.GetString("synopsis"),
                MovieYear = reader.GetInt16("movieYear"),
                Duration = reader.GetInt16("duration"),
                AgeRating = reader.GetByte("ageRating"),
                Image = reader.GetString("image")
            };
            connection.Close();
            return movie;
        }
        connection.Close();
        return null;
    }

    public void Update(Movie model)
    {
        MySqlConnection connection = new(connectionString);
        string sql = "update Movie set "
                        + "Title = @Title, "
                        + "OriginalTitle = @OriginalTitle, "
                        + "Synopsis = @Synopsis, "
                        + "MovieYear = @MovieYear, "
                        + "Duration = @Duration, "
                        + "AgeRating = @AgeRating, "
                        + "Image = @Image "
                    + "where Id = @Id";
        MySqlCommand command = new(sql, connection)
        {
            CommandType = CommandType.Text
        };
        command.Parameters.AddWithValue("@Id", model.Id);
        command.Parameters.AddWithValue("@Title", model.Title);
        command.Parameters.AddWithValue("@OriginalTitle", model.OriginalTitle);
        command.Parameters.AddWithValue("@Synopsis", model.Synopsis);
        command.Parameters.AddWithValue("@MovieYear", model.MovieYear);
        command.Parameters.AddWithValue("@Duration", model.Duration);
        command.Parameters.AddWithValue("@AgeRating", model.AgeRating);
        command.Parameters.AddWithValue("@Image", model.Image);
        
        connection.Open();
        command.ExecuteNonQuery();
        connection.Close();
    }
}
