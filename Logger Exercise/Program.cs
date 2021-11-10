using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Logger_Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Log File System
            string logFilePath = @"C:\Users\ManLikeCliff\source\repos\Logger Exercise\Logger Exercise\bin\Debug\logFile.txt";
            string newFilePath = @"C:\Users\ManLikeCliff\source\repos\Logger Exercise\Logger Exercise\bin\Debug\newFile.txt";
            LogFile logger = new LogFile();

            // STEP 1: Create File
            //logger.CreateLogFile(logFilePath, newFilePath); // Comment out line to run subsequent lines within Region

            // STEP 2: Start logger
            uint counter = 1;
            Console.WriteLine("Initializing log process! Please wait...");

            while (counter <= 5)
            {
                if (!(File.Exists(logFilePath) || File.Exists(newFilePath)))
                {
                    Console.WriteLine("No file created yet! Run the CreateLogFile method first!");
                    return;
                }

                logger.ProcessTimeLog(logFilePath, newFilePath);
                counter++;
                Thread.Sleep(5000);
            }

            // After 25 seconds, entries will be written to new file
            logger.GetLoggedTime(newFilePath);
            #endregion
        }
    }

    public class LogFile
    {
        #region Fields And Properties
        private string _time;
        private Stack<string> sortedLog = new Stack<string>();

        public string Time
        {
            get
            {
                return _time;
            }

            set
            {
                _time = value;
            }
        }
        #endregion

        #region Initialize Logger By Creating File
        public void CreateLogFile(string oldPath, string newPath)
        {
            // Check if file name already exists in directory
            if (File.Exists(oldPath) || File.Exists(newPath))
            {
                Console.WriteLine("File already created!");
                return;
            }

            // Create file
            File.Create(oldPath);
            File.Create(newPath);

            Console.WriteLine("Files created successfully!");
        }
        #endregion

        #region Initialize Time Logger To Store Time To File
        public void ProcessTimeLog(string path, string newPath)
        {
            Time = (DateTime.Now.ToLongTimeString() + " ::: " + DateTime.Now.ToLongDateString());

            File.WriteAllText(path, "");
            File.WriteAllText(newPath, "");

            File.AppendAllLines(path, new string[] { Time });
            sortedLog.Push(Time);
        }
        #endregion

        #region Get Logged Time
        public void GetLoggedTime(string newPath)
        {
            while (sortedLog.Count > 0)
            {
                File.AppendAllLines(newPath, new string[] { sortedLog.Pop().ToString() });
            }
            Console.WriteLine("Time Logging done! Check newFile.txt for reversed time log sequence.");
        }
        #endregion
    }
}
