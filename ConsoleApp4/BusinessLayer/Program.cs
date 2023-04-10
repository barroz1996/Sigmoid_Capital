using System;
using System.IO;
using System.Linq;
using ConsoleApp4.DataAccessLayer;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.Net.WebRequestMethods;
using File = System.IO.File;

namespace ConsoleApp4.BusinessLayer


{
    class Program
    {   
        private static DataAccessLayer.Controllers.TradeBi TradeCon = new DataAccessLayer.Controllers.TradeBi();  // control of data access layer
        static string nameOfLastCsv = "myfile.txt";  // Txt file to save the last csv file.
        static void Main(string[] args)
        {
            using (StreamReader reader = new StreamReader(nameOfLastCsv))  // Firstly we check here if we had crash before we run the system, if we had it will read from the txt file the last csv file that was problematic
            {
           
                string fileContents = reader.ReadToEnd();
                if (fileContents.Length > 0)
                    InsertData(fileContents,true);
                Console.WriteLine(fileContents);

            }
            CleanTxt(nameOfLastCsv);   // After we read the problematic csv file we clean the txt file
     
            var watcher = new FileSystemWatcher(@"C:\Users\Lenovo\Desktop\Sigmoid");  // Here we give the path of the directory!
            watcher.Filter = "*.csv";
            watcher.NotifyFilter = NotifyFilters.Attributes
                             | NotifyFilters.CreationTime
                             | NotifyFilters.LastWrite;
            watcher.IncludeSubdirectories = true;
            watcher.Created += new FileSystemEventHandler(OnCreated); 
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        // When csv created it will write it to the sql
        private static void OnCreated(object sender, FileSystemEventArgs e)  
        {
            string fileName = "myfile.txt";
            if (!string.IsNullOrEmpty(e.FullPath))
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.Write(e.FullPath);
                }
                InsertDataToSql(e);
            }
        }

        // When csv file changed in the directory it will write it to sql
        private static void OnChanged(object sender, FileSystemEventArgs e) 
        {           
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
               return;
            }
            string fileName = "myfile.txt";
            if (!string.IsNullOrEmpty(e.FullPath))
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.Write(e.FullPath);
                }
                InsertDataToSql(e);
            }
        }
        
        // Clean saved csv name file
        private static void CleanTxt(string filePath)  
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write("");
            }
        }

    
        // Read the csv file and insert the data to sql
        private static void InsertData(string path, bool crash) {
            while (IsFileLocked(path))
            { }
            try
            {
                string[] lines = File.ReadAllLines(path);
                string[][] data = lines.Select(l => l.Split(',')).ToArray();
                checkCsvTable(data);
                int i = 0;
                foreach (String[] row in data)
                {
                    if (i == 1)
                        TradeCon.InsertAsync(new DataAccessLayer.DTOs.TradeBiDTO(row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10], row[11], row[12], row[13], row[14], row[15], row[16], row[17], row[18], row[19], row[20], row[21], row[22], row[23]), crash); //inserts all of the new columns of the new board to the database.
                    if (i != 1)
                        i = 1;
                }
            }
            catch(Exception e)
            {
                StreamWriter sw = new StreamWriter("log.txt", true);
                sw.WriteLine();
                sw.WriteLine(e.Message);
                sw.Close();
            }
        }

        private static void InsertDataToSql(FileSystemEventArgs e)
        {
            InsertData(e.FullPath,false);
            CleanTxt(nameOfLastCsv);   
            Console.WriteLine($"Changed: {e.FullPath}");
        }


        private static void checkCsvTable(string[][] data)
        {
            if (data[0].Length != 24)
            {
                CleanTxt(nameOfLastCsv); // if we have problem with the csv size we dont want to save the csv path when we rerun the program!
                StreamWriter sw = new StreamWriter("log.txt", true);
                sw.WriteLine();
                sw.WriteLine("The csv table size columns is not 24 like the format!. The size columns of the file is +" + data.GetLength(1));
                sw.Close();
            }
        }

        private static bool IsFileLocked(string filePath)
        {
            try
            {
                // Try to open the file with read/write access
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                {
                    // File is not locked, so close the stream and return false
                    stream.Close();
                    return false;
                }
            }
            catch (IOException)
            {
                // File is locked, so return true
                return true;
            }
        }


        // print the csv file data
        private static void printData(string[][] data)
        {
            foreach (string[] row in data)
            {
                foreach (string col in row)
                {
                    Console.Write(col + " ");
                }
                Console.WriteLine();
            }
        }

      

        /*
  private static void OnError(object sender, ErrorEventArgs e) =>
      PrintException(e.GetException());

  private static void PrintException(Exception ex)
  {
      //if (ex != null)
      //{
         // Console.WriteLine($"Message: {ex.Message}");
         // StreamWriter sw = new StreamWriter("log.txt", true);
         // sw.WriteLine($"Message: {ex.Message}");
         // sw.Close();
      //}
  }

  */
    }
}

       

     
       
   

