using System;
using System.Text;
using Microsoft.Data.SqlClient;

namespace P03.MinionNames
{
    public class StartUp
    {
        private const string ConnecionString = @"Server=DESKTOP-DPV1UGJ; Database=MinionsDB;Integrated Security=true;";

        static void Main(string[] args)
        {
            using SqlConnection sqlConnection = new SqlConnection(ConnecionString);

            sqlConnection.Open();

            int villianId = int.Parse(Console.ReadLine());

            string resultMethod = GetMinionsInfoAboutVillian(sqlConnection, villianId);

            Console.WriteLine(resultMethod);
        }

        private static string GetMinionsInfoAboutVillian(
            SqlConnection sqlConnection,int villianId)
        {
            StringBuilder sb = new StringBuilder();

            string villianName = GetVillainName(sqlConnection, villianId);

            if(villianName == null)
            {
                sb.AppendLine($"No villain with ID {villianId} exists in the database.");
            }
            else
            {
                sb.AppendLine($"Villain: {villianName}");

                string getMinionsInfoQueryText = @"SELECT M.[Name], M.Age FROM Minions AS M
                                     LEFT OUTER JOIN MinionsVillains AS MV
                                     ON M.Id = MV.MinionId
                                     LEFT OUTER JOIN Villains AS V
                                     ON MV.VillainId = V.Id
                                     WHERE V.[Name] = @villianName
                                     ORDER BY M.[Name]";

                SqlCommand getMinionsInfoCommand = new SqlCommand
                    (getMinionsInfoQueryText, sqlConnection);

                getMinionsInfoCommand.Parameters.AddWithValue("@villianName", villianName);

                SqlDataReader reader = getMinionsInfoCommand.ExecuteReader();

                if (reader.HasRows)
                {
                    int rowNum = 1;

                    while (reader.Read())
                    {
                        string minionName = reader["Name"]?.ToString();
                        string minionAge = reader["Age"]?.ToString();
                        sb.AppendLine($"{rowNum}. {minionName} {minionAge}");
                        rowNum++;
                    }
                }
                else
                {
                    sb.AppendLine("(no minions)");
                }
            }

            return sb.ToString().TrimEnd();
        }
        private static string GetVillainName(SqlConnection sqlConnection, int villianId)
        {
            string getVillianNameQueryText =
                @"SELECT V.[Name] 
                    FROM Villains AS V
                    WHERE Id = @villianId";

            using SqlCommand getVillianNameCmd = new SqlCommand(getVillianNameQueryText, sqlConnection);

            getVillianNameCmd.Parameters.AddWithValue("@villianId", villianId);

            string villianName = getVillianNameCmd.ExecuteScalar()?.ToString();

            return villianName;
        }
    }
}
