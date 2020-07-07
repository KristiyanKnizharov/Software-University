using Microsoft.Data.SqlClient;

namespace P01.Initial_Setup
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            // Conect to master
            using SqlConnection connect = new SqlConnection(@"Server=DESKTOP-DPV1UGJ;
                                                                Database=master;
                                                                Integrated Security=true");
            connect.Open();

            // Create database Minions
            var query = @"CREATE DATABASE MinionsDB";
            using var cmd = new SqlCommand(query, connect);
            cmd.ExecuteNonQuery();

            // Conect to MinionsDatabase
            using SqlConnection newConnect = new SqlConnection(@"Server=DESKTOP-DPV1UGJ;
                                                                    Database=MinionsDB;
                                                                    Integrated Security=true;");
            newConnect.Open();

            // Create Countries table
            var queryCountriesCreate = @"CREATE TABLE Countries(
	                                        [Id] INT IDENTITY PRIMARY KEY,
	                                        [Name] NVARCHAR(50) NOT NULL";
            using var createTableCountries = new SqlCommand(queryCountriesCreate, newConnect);
            createTableCountries.ExecuteNonQuery();

            // Create Towns table
            var queryTownsCreate = @"CREATE TABLE Towns(
	                                [Id] INT IDENTITY PRIMARY KEY,
	                                [Name] NVARCHAR(50) NOT NULL,
	                                [CountryCode] INT REFERENCES Countries(Id) NOT NULL
                                )";
            using var createTableTowns = new SqlCommand(queryTownsCreate, newConnect);
            createTableTowns.ExecuteNonQuery();

            // Create Minions table
            var queryMinionsCreate = @"CREATE TABLE Minions(
	                                    [Id] INT IDENTITY PRIMARY KEY,
	                                    [Name] NVARCHAR(50) NOT NULL,
	                                    [Age] TINYINT NOT NULL,
	                                    [TownId] INT REFERENCES Towns(Id) NOT NULL
                                    )";
            using var createTableMinions = new SqlCommand(queryMinionsCreate, newConnect);
            createTableMinions.ExecuteNonQuery();

            // Create EvilnessFactors table
            var queryEvilnessFactorsCreate = @"CREATE TABLE EvilnessFactors(
	                                            [Id] INT IDENTITY PRIMARY KEY,
	                                            [Name] NVARCHAR(50) NOT NULL
                                            )";
            using var createTableEvilnessFactors = new SqlCommand(queryEvilnessFactorsCreate, newConnect);
            createTableEvilnessFactors.ExecuteNonQuery();

            // Create Villains table
            var queryVillainsCreate = @"CREATE TABLE Villains(
	                                        [Id] INT IDENTITY PRIMARY KEY,
	                                        [Name] NVARCHAR(50) NOT NULL,
	                                        [EvilnessFactorId] INT REFERENCES EvilnessFactors(Id) NOT NULL
                                        )";
            using var createTableVillains = new SqlCommand(queryVillainsCreate, newConnect);
            createTableVillains.ExecuteNonQuery();

            // Create MinionsVillains table
            var queryMinionsVillainsCreate = @"CREATE TABLE MinionsVillains(
	                                                MinionId INT REFERENCES Minions(Id),
	                                                VillainId INT REFERENCES Villains(Id),
	                                                PRIMARY KEY(MinionId, VillainId)
                                                )";
            using var createTableMinionsVillains = new SqlCommand(queryMinionsVillainsCreate, newConnect);
            createTableMinionsVillains.ExecuteNonQuery();

            // Insert into Countries
            var dataToCountries = @"INSERT INTO Countries
	                                    VALUES
	                                    ('Russia'),
	                                    ('Bulgaria'),
	                                    ('China'),
	                                    ('USA'),
	                                    ('Canada');";
            using var insertIntoCountries = new SqlCommand(dataToCountries, newConnect);
            insertIntoCountries.ExecuteNonQuery();

            // Inser into Towns
            var dataToTowns = @"INSERT INTO Towns
	                                VALUES
	                                ('Moscow', 1),
	                                ('Sofia', 2),
	                                ('Ciung Dzang', 3),
	                                ('Chicago', 4),
	                                ('Ontario', 5);
                                ";
            using var insertIntoTowns = new SqlCommand(dataToTowns, newConnect);
            insertIntoTowns.ExecuteNonQuery();

            // Inset into Minions
            var dataToMinions = @"INSERT INTO Minions
	                                VALUES
	                                ('Stuart', 15, 3),
	                                ('Josh', 7, 4),
	                                ('Vanq', 35, 1),
	                                ('Tervel', 29, 2),
	                                ('John', 47, 5);
                                ";
            using var insetIntoMinions = new SqlCommand(dataToMinions, newConnect);
            insetIntoMinions.ExecuteNonQuery();

            // Inser into EvilnessFactors
            var dataToEvilnessFactors = @"INSERT INTO EvilnessFactors
	                                        VALUES
	                                        ('Super Good'),
	                                        ('Good'),
	                                        ('Bad'),
	                                        ('Evil'),
	                                        ('Super Evil');
                                        ";
            using var insertIntoEvilnessFactors = new SqlCommand(dataToEvilnessFactors, newConnect);
            insertIntoEvilnessFactors.ExecuteNonQuery();

            // Insert into Villains
            var dataToVillains = @"INSERT INTO Villains
	                                    VALUES
	                                    ('Bad Guy', 3),
	                                    ('Evil Guy', 4),
	                                    ('Good Dude', 2),
	                                    ('Good Friend', 1),
	                                    ('Super Evil Guy', 5);
                                    ";
            using var insertIntoVillains = new SqlCommand(dataToVillains, newConnect);
            insertIntoVillains.ExecuteNonQuery();

            // Insert into MinionsVillains
            var dataToMinionsVillains = @"INSERT INTO MinionsVillains
	                                            VALUES
	                                            (1, 3),
	                                            (2, 4),
	                                            (3, 5),
	                                            (4, 1),
	                                            (5, 2),
	                                            (3, 4),
	                                            (2, 1),
	                                            (5, 3);";

            using var insertIntoMinionsVillains = new SqlCommand(dataToMinionsVillains, newConnect);
            insertIntoMinionsVillains.ExecuteNonQuery();

        }
    }
}