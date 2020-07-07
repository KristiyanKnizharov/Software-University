using System;
using System.Text;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace P04.AddMinion
{
    public class StartUp
    {
        static void Main(string[] args)
        {

            var minionInput = Console.ReadLine().Split(": ");
            var minionInfo = minionInput[1].Split(" ");
            var villainInfo = Console.ReadLine().Split(": ");

            //private const string connect ="@Server=DESKTOP-DPV1UGJ;Database=[MinionsDB];Integrated Security=true;";

            using var connect = new SqlConnection
                ("@Server=DESKTOP-DPV1UGJ;Database=[MinionsDB];Integrated Security=true;");
           
            connect.Open();

            var searchVillianQuery = @"SELECT Id FROM Villains WHERE Name = @Name";
            var searchMinionQuery = @"SELECT Id FROM Minions WHERE Name = @Name";
            var searchTownQuery = @"SELECT Id FROM Towns WHERE Name = @townName";

            using var commandTown = new SqlCommand(searchTownQuery, connect);
            commandTown.Parameters.AddWithValue("@townName", minionInfo[2]);
            var curentTown = commandTown.ExecuteScalar();

            if (curentTown == null)
            {
                var addTowenQuery = @"INSERT INTO Towns (Name) VALUES (@townName)";
                using var addTown = new SqlCommand(addTowenQuery, connect);
                addTown.Parameters.AddWithValue("@townName", minionInfo[2]);
                addTown.ExecuteNonQuery();
                Console.WriteLine($"Town {minionInfo[2]} was added to the database.");
            }

            using var commandVillain = new SqlCommand(searchVillianQuery, connect);
            commandVillain.Parameters.AddWithValue("@Name", villainInfo[1]);
            var curentVillain = commandVillain.ExecuteScalar();

            if (curentVillain == null)
            {
                var addVillainQuery = @"INSERT INTO Villains (Name, EvilnessFactorId)
                                         VALUES (@villainName, 4)";
                using var addVillain = new SqlCommand(addVillainQuery, connect);

                addVillain.Parameters.AddWithValue("@villainName", villainInfo[1]);
                addVillain.ExecuteNonQuery();

                Console.WriteLine($"Villain {villainInfo[1]} was added to the database.");
            }

            using var commandMinion = new SqlCommand(searchMinionQuery, connect);
            commandMinion.Parameters.AddWithValue("@Name", minionInfo[0]);
            var curentMinion = commandMinion.ExecuteScalar();

            if (curentMinion == null)
            {
                using var commandTownId = new SqlCommand(searchTownQuery, connect);
                commandTownId.Parameters.AddWithValue("@townName", minionInfo[2]);
                var townId = (int)commandTownId.ExecuteScalar();

                var addMinionQuery = @"INSERT INTO Minions (Name, Age, TownId)
                                        VALUES (@name, @age, @townId)";
                using var addMinion = new SqlCommand(addMinionQuery, connect);

                addMinion.Parameters.AddWithValue("@name", minionInfo[0]);
                addMinion.Parameters.AddWithValue("@age", minionInfo[1]);
                addMinion.Parameters.AddWithValue("@townId", townId);

                addMinion.ExecuteNonQuery();
            }
            else
            {
                using var commandVillainId = new SqlCommand(searchVillianQuery, connect);
                commandVillainId.Parameters.AddWithValue("@Name", villainInfo[1]);
                var villainId = (int)commandVillainId.ExecuteScalar();

                using var commandMinionId = new SqlCommand(searchMinionQuery, connect);
                commandMinionId.Parameters.AddWithValue("@Name", minionInfo[0]);
                var minionId = (int)commandMinionId.ExecuteScalar();

                var addMinionToVillainQuery = @"INSERT INTO MinionsVillains 
                        (MinionId, VillainId) VALUES (@villainId, @minionId)";
                using var commandAddMinionToVillain = 
                                        new SqlCommand(addMinionToVillainQuery, connect);

                commandAddMinionToVillain.Parameters.AddWithValue("@villainId", villainId);
                commandAddMinionToVillain.Parameters.AddWithValue("@minionId", minionId);
                commandAddMinionToVillain.ExecuteNonQuery();

                Console.WriteLine($"Successfully added {minionInfo[0]}" +
                    $" to be minion of {villainInfo[1]}.");
            }
        }
    }
}
