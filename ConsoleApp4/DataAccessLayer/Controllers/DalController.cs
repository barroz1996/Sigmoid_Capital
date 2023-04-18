using System;

using System.IO;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;
namespace ConsoleApp4.DataAccessLayer
{
    abstract class DalController
    {
        protected readonly string _connectionString;
        protected readonly string _tableName;
        public DalController(string tableName)
        { 
           // string connectionString = @"Data Source=DESKTOP-0G3N8AU;Initial Catalog=TableBIDataBase;Integrated Security=True";
            _tableName = tableName;        
        }
    
    
    }
}
