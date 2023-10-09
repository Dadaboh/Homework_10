using System.Text;

namespace Homework_10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Start();                    
        }

        private static void Start()
        {
            var drives = DriveInfo.GetDrives();
            var userChoices = "";

            Console.WriteLine("Оберіть диск:");

            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine(i + " - " + drives[i].Name);
            }

            do
            {
                userChoices = Console.ReadLine();
            }
            while (!CheckUserChoices(userChoices, drives.Length) || userChoices == "return" || userChoices == "addDirect" || userChoices == "addFile");

            GetInfo(drives[Convert.ToInt32(userChoices)].Name);
        }

        private static void GetInfo(string path)
        {
            Console.Clear();

            var directories = Directory.GetDirectories(path);
            var files = Directory.GetFiles(path);
            var userChoices = "";

            Console.WriteLine($"Доступні папки та файли ({path}): \n");

            for (int i = 0; i < directories.Length; i++)
            {
                Console.WriteLine(i + " - " + directories[i]);
            }

            Console.WriteLine();

            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine(files[i]);
            }

            Console.WriteLine("\nВведіть номер папки, або 'return' щоб повернутись в попередню, або 'addDirect' щоб створити нову папку,\nабо 'addFile' для створення нового файлу:");

            do
            {
                userChoices = Console.ReadLine();
            }
            while (!CheckUserChoices(userChoices, directories.Length));

            if(userChoices == "return")
            {
                var drives = DriveInfo.GetDrives();
                var drivesList = new List<string>();

                foreach (var drive in drives)
                {
                    drivesList.Add(drive.Name);
                }

                if (drivesList.Contains(path))
                {
                    Console.WriteLine();
                    Start();
                }
                else
                {
                    GetInfo(Directory.GetParent(path).ToString());
                }
            }
            else if(userChoices == "addDirect")
            {

                Console.WriteLine("\nВведіть ім'я папки:");
                var folderName = Console.ReadLine();

                if(Directory.Exists(path + $"\\{folderName}"))
                {
                    Console.WriteLine("Папка з такою назвою вже існує.");

                    do
                    {
                        Console.WriteLine("Введіть інше ім'я:");
                        folderName = Console.ReadLine();
                    }
                    while (Directory.Exists(path + $"\\{folderName}"));
                }

                Directory.CreateDirectory(path + $"\\{folderName}");

                GetInfo(path + $"\\{folderName}");
            }
            else if(userChoices == "addFile")
            {
                Console.WriteLine("\nВведіть ім'я файлу:");
                var fileName = Console.ReadLine();

                Console.WriteLine("Введіть розширення файлу:");
                var fileType = Console.ReadLine();

                if (File.Exists(path + $"\\{fileName}.{fileType}"))
                {
                    Console.WriteLine("Файл з такою назвою вже існує.");

                    do
                    {
                        Console.WriteLine("Введіть інше ім'я:");
                        fileName = Console.ReadLine();
                    }
                    while (File.Exists(path + $"\\{fileName}"));
                }

                File.Create(path + $"\\{fileName}.{fileType}");
                GetInfo(path);
            }

            GetInfo(directories[Convert.ToInt32(userChoices)]);
        }

        private static bool CheckUserChoices(string checkStr, int arrayLength)
        {
            int checkInt;

            if(checkStr == "return" || checkStr == "addDirect" || checkStr == "addFile")
            {
                return true;
            }
            else if(!Int32.TryParse(checkStr, out checkInt))
            {
                return false;
            }
            else if(checkInt < 0 || checkInt >= arrayLength)
            {
                return false;
            }

            return true;
        }
    }
}