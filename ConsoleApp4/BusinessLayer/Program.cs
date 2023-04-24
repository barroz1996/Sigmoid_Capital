using System;
using System.IO;
using System.Linq;
using ConsoleApp4.DataAccessLayer;
using System.Threading;
using Microsoft.Extensions.Logging;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using static System.Net.WebRequestMethods;
using File = System.IO.File;
using Microsoft.Office.Interop.Excel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp4.BusinessLayer
{
    class Program
    {   
        private static DataAccessLayer.Controllers.TradeBi TradeCon = new DataAccessLayer.Controllers.TradeBi();  // control of data access layer
        static string nameOfLastCsv = "myfile.txt";  // Txt file to save the last csv file.
        static string errorsTxt = "log.txt";
        static void Main(string[] args)
        {
            checkLastCsvFile();
            var watcher = new FileSystemWatcher(@"C:\Users\Lenovo\Desktop\Sigmoid");  // Here we give the path of the directory!
            //watcher.InternalBufferSize = 64 * 1024;
            watcher.Filter = "*.csv";
            watcher.NotifyFilter = NotifyFilters.Attributes
                             | NotifyFilters.CreationTime
                             | NotifyFilters.LastWrite;
            watcher.IncludeSubdirectories = true;
            Thread.Sleep(1000);
            watcher.Created += new FileSystemEventHandler(OnCreated); 
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.EnableRaisingEvents = true;
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        // When csv created it will write it to the sql
        private static void OnCreated(object sender, FileSystemEventArgs e)  
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            writePathOfLastCsvAndInsert(nameOfLastCsv, e);
            Thread.Sleep(1000);

        }

        // When csv file changed in the directory it will write it to sql
        private static void OnChanged(object sender, FileSystemEventArgs e) 
        {
         
            writePathOfLastCsvAndInsert(nameOfLastCsv, e);
            Thread.Sleep(1000);

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
        private static async Task InsertDataAsync(string path, bool crash) {
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
                writeToTxtFile(errorsTxt, e.Message);
                sendEceptionMessageSlackAsync(e);
            }
        }


        // Send message into slack channel
        private static async Task sendEceptionMessageSlackAsync(Exception e)
        {
            string a = e.Message + "";
            var json = new
            {
                text = a
            };

            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(json);
            var httpClient = new HttpClient();
            var webhookUrl = "https://hooks.slack.com/services/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";
            // var payload = "{\"text\": \"Hello, Slack \"}";
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(webhookUrl, content);
        }

        // Insert the data we read from the csv file into the sql table
        private static void InsertDataToSql(FileSystemEventArgs e)
        {
            InsertDataAsync(e.FullPath,false);
            CleanTxt(nameOfLastCsv);   
            Console.WriteLine($"Changed: {e.FullPath}");
        }

        // Check the csv structure 
        private static void checkCsvTable(string[][] data)
        {
            if (data[0].Length != 24)
            {
                CleanTxt(nameOfLastCsv); // if we have problem with the csv size we dont want to save the csv path when we rerun the program!
                writeToTxtFile(errorsTxt, "The csv table size columns is not 24 like the format!. The size columns of the file is +" + data.GetLength(1));
            }
        }

        // Check if the file is open
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

        // When we rerun the system
        private static void checkLastCsvFile()
        {
            try
            {
                using (StreamReader reader = new StreamReader(nameOfLastCsv))  // Firstly we check here if we had crash before we run the system, if we had it will read from the txt file the last csv file that was problematic
                {
                    string fileContents = reader.ReadToEnd();
                    if (fileContents.Length > 0)
                    {
                        InsertDataAsync(fileContents, true);
                        Console.WriteLine(fileContents);
                    }

                }
                CleanTxt(nameOfLastCsv);
            }
            catch (Exception e)
            {
                writeToTxtFile(errorsTxt, e.Message);
                sendEceptionMessageSlackAsync(e);
            }
        }

        // Save the text to Txt file
        private static void writeToTxtFile(string path, string message)
        {
            StreamWriter sw = new StreamWriter(path, true);
            sw.WriteLine();
            sw.WriteLine(message);
            sw.Close();
        }

        // When systemed crashed when we rerun it will complete the mission with the last file
        private static void writePathOfLastCsvAndInsert(string path, FileSystemEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.FullPath))
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    writer.Write(e.FullPath);
                }
                InsertDataToSql(e);
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
                   sendEceptionMessageSlackAsync(e.GetException());

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
