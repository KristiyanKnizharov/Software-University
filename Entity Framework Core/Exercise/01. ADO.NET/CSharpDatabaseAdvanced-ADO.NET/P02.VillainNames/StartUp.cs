using System;
using System.Text;
using Microsoft.Data.SqlClient;

namespace P02.Villain_Names
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using SqlConnection connect = new SqlConnection
                (@"Server = DESKTOP-DPV1UGJ;
                    Database = MinionsDB;
                    Integrated Security = true");
            connect.Open();

            var queryText = @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                            FROM Villains AS v 
                            JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                            GROUP BY v.Id, v.Name 
                            HAVING COUNT(mv.VillainId) > 3 
                            ORDER BY COUNT(mv.VillainId)";

            using var command = new SqlCommand(queryText, connect);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"{reader["Name"]} - {reader["MinionsCount"]}");
            }
        }
    }
}