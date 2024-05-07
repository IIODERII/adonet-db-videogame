using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    public class Videogame
    {
        public string Name { get; set; }
        public string Overview { get; set; }
        public DateTime Release_date { get; set; }
        public long Software_house_id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime Updated_at { get; set; }

        public Videogame(string name, string overview, long soft_id, DateTime release, DateTime created, DateTime updated) {
            Name = name;
            Overview = overview;
            Release_date = release;
            Created_at = created;
            Updated_at = updated;
            Software_house_id = soft_id;
        }
    }

    public static class VideogameManager
    {
        public static void InserisciVideogame (string name, string overview, long soft_id, DateTime release, DateTime created, DateTime updated)
        {
            string conn = "Data Source=localhost;Initial Catalog=videogamesDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            string query = "INSERT INTO videogames (name, overview, release_date, created_at, updated_at, software_house_id) VALUES (@name, @overview, @release_date, @created_at, @updated_at, @software_house_id)";

            Videogame nuovoGioco = new Videogame(name, overview, soft_id, release, created, updated);

            SqlConnection connessione = new SqlConnection(conn);

            try
            {
                connessione.Open();

                SqlCommand cmd = new SqlCommand(query, connessione);
                cmd.Parameters.Add(new SqlParameter("@name", name));
                cmd.Parameters.Add(new SqlParameter("@overview", overview));
                cmd.Parameters.Add(new SqlParameter("@release_date", release));
                cmd.Parameters.Add(new SqlParameter("@created_at", created));
                cmd.Parameters.Add(new SqlParameter("@updated_at", updated));
                cmd.Parameters.Add(new SqlParameter("@software_house_id", soft_id));

                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                connessione.Close();
            }
        }
        public static Videogame GetVideogameById(int id)
        {
            string conn = "Data Source=localhost;Initial Catalog=videogamesDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            string query = $"SELECT * FROM videogames WHERE id = {id}";
            SqlConnection connessione = new SqlConnection(conn);

            connessione.Open();

            SqlCommand cmd = new SqlCommand(query, connessione);
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    Videogame videogame = new Videogame(
                        reader.GetString(reader.GetOrdinal("name")),
                        reader.GetString(reader.GetOrdinal("overview")),
                        reader.GetInt64(reader.GetOrdinal("software_house_id")),
                        reader.GetDateTime(reader.GetOrdinal("release_date")),
                        reader.GetDateTime(reader.GetOrdinal("created_at")),
                        reader.GetDateTime(reader.GetOrdinal("updated_at"))
                    );

                    return videogame;
                }
                return new Videogame(null, null, 0, DateTime.Now, DateTime.Now, DateTime.Now);
            }
        }

        public static List<Videogame> GetGamesBySearch(string search)
        {
            List<Videogame> videogames = new List<Videogame>();

            string conn = "Data Source=localhost;Initial Catalog=videogamesDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            string query = $"SELECT * FROM videogames WHERE name LIKE '%{search}%'";
            using (SqlConnection connessione = new SqlConnection(conn))
            {
                connessione.Open();

                SqlCommand cmd = new SqlCommand(query, connessione);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Videogame videogame = new Videogame(
                        reader.GetString(reader.GetOrdinal("name")),
                        reader.GetString(reader.GetOrdinal("overview")),
                        reader.GetInt64(reader.GetOrdinal("software_house_id")),
                        reader.GetDateTime(reader.GetOrdinal("release_date")),
                        reader.GetDateTime(reader.GetOrdinal("created_at")),
                        reader.GetDateTime(reader.GetOrdinal("updated_at"))
                    );

                        videogames.Add(videogame);
                    }
                }
            connessione.Close();
            }
            return videogames;
        }

        public static void DeleteGame(int id)
        {
            string conn = "Data Source=localhost;Initial Catalog=videogamesDB;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            string query = $"DELETE FROM videogames WHERE id=@id";
            SqlConnection connessione = new SqlConnection(conn);
            connessione.Open();
            SqlCommand cmd = new SqlCommand(query, connessione);
            cmd.Parameters.Add(new SqlParameter("@id", $"{id}"));

            int rows = cmd.ExecuteNonQuery();
        }
    }
}
