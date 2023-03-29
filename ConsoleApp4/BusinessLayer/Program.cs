using System;
using System.IO;
using System.Linq;
using ConsoleApp4.DataAccessLayer;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace ConsoleApp4.BusinessLayer


{
    class Program
    {   
        // int k=0;
        private static DataAccessLayer.Controllers.TradeBi TradeCon = new DataAccessLayer.Controllers.TradeBi();
        static void Main(string[] args)
        {
           
            var watcher = new FileSystemWatcher(@"C:\Users\Lenovo\Desktop\Sigmoid");

            watcher.Changed += new FileSystemEventHandler(OnChanged);
           // watcher.Created += new FileSystemEventHandler(OnChanged);
           // watcher.Error += new FileSystemEventHandler(OnError);
         
            
            watcher.EnableRaisingEvents = true;

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            while (IsFileLocked(e.FullPath))
            {}

            string[] lines = File.ReadAllLines(e.FullPath);

            // Parse the lines into a 2D array of string values
            string[][] data = lines.Select(l => l.Split(',')).ToArray(); // all the data from the csv
            printData(data);
            int i=0;
            int k = 0;
            foreach (String[] row in data)
            {
                if(i==1)
                    TradeCon.Insert(new DataAccessLayer.DTOs.TradeBiDTO(row[0],row[1],row[2],row[3],row[4],row[5],row[6],row[7],row[8],row[9],row[10],row[11],row[12],row[13],row[14],row[15],row[16],row[17],row[18],row[19],row[20],row[21],row[22],row[23])); //inserts all of the new columns of the new board to the database.
                if(i!=1)
                    i=1;
                
                StreamWriter sw = new StreamWriter("log.txt", true);
                sw.WriteLine("Log message 1");
                sw.Close();
            }
            
            
            //  k=k+1;
            // Console.WriteLine(k);
            Console.WriteLine($"Changed: {e.FullPath}");
        }

        private static void OnError(object sender, ErrorEventArgs e) =>
            PrintException(e.GetException());

        private static void PrintException(Exception ex)
        {
            if (ex != null)
            {
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine("Stacktrace:");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine();
                PrintException(ex.InnerException);
            }
        }

        public static void printData(string[][] data)
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

        private static void checkData(string[] data)
        {

        }
    }
 }

       

     
       
   

