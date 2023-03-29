using System;
using System.Data.SQLite;
using System.IO;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
namespace ConsoleApp4.DataAccessLayer
{
    abstract class DalController
    {
        protected readonly string _connectionString;
        protected readonly string _tableName;
        public DalController(string tableName)
        {
            var path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "TradeBIDataBase.db"));
            _connectionString = $"Data Source={path}; Version=3;";
            _tableName = tableName;
            Create(path);
        }
        public void Create(string path) // create the Data Base
        {
            if (!File.Exists(path)) //if the file does not exist, it will create a new db file with all the tables.
            {
                SQLiteConnection.CreateFile("TradeBIDataBase.db");
    
                using (var connection = new SQLiteConnection(_connectionString))
                {
                  
                    var TradeBITable = "CREATE TABLE \"TradeBI\"(\"TradeID\"    TEXT PRIMARY KEY NOT NULL, \"Trader\" TEXT , \"Broker\" TEXT, \"Symbol\" TEXTL, \"AccountID\" TEXT, \"AccountSize\" TEXT, \"StrategyName\" TEXT , \"Profile\" TEXT, \"EntryType\" TEXT , \"ExitType\" TEXT, \"StartDate\" TEXT, \"EndDate\" TEXT, \"TradeDuartion_Minute\" TEXT, \"StartPrice\" TEXT, \"EndPrice\" TEXT, \"Contracts\" TEXT, \"PositionSize\" TEXT,  \"Margin\" TEXT, \"Commission\" TEXT, \"Profit\" TEXT,\"DrawDown\" TEXT,\"DrawDown_Percent\" TEXT,\"Run_up\" TEXT,\"Run_up_Percent\" TEXT )";

                   // var TaskTable = "CREATE TABLE \"TradeBI\"(\"posTradeID\"    TEXT PRIMARY KEY NOT NULL, \"traderName\" TEXT NOT NULL)";
                    var command = new SQLiteCommand(null, connection);

                    try //Creates all tables.
                    {
                        connection.Open();
                        command.CommandText = TradeBITable;
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        command.Dispose();
                        connection.Close();
                    }
                }

            }
        }

        public bool DeleteTable() //Deletes all tasks from the database.
        {
            int res = -1;

            using (var connection = new SQLiteConnection(_connectionString))
            {
                var command = new SQLiteCommand
                {
                    Connection = connection,
                    CommandText = $"DELETE FROM {_tableName} "
                };
                try
                {
                    connection.Open();
                    res = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
                finally
                {
                    command.Dispose();
                    connection.Close();
                }

            }
            return res > 0;
        }
    }
}
