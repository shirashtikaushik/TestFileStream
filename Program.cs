using System;
using System.IO;

internal class Program
{
    static void Main()
    {
        // Task 1
        string demoPath = "C://demo";
        CreateDirectoriesAndFiles(demoPath);

        Console.WriteLine("Files and directories in {0}:", demoPath);
        DisplayFilesAndDirectories(demoPath);

        Console.WriteLine("\nDo you want to delete all files and directories? (1. Yes / 2. No)");
        int deleteChoice = int.Parse(Console.ReadLine());

        if (deleteChoice == 1)
        {
            DeleteAllFilesAndDirectories(demoPath);
            Console.WriteLine("\nAll files and directories deleted.");
        }
        else
        {
            Console.WriteLine("\nDeletion skipped. Files and directories are not deleted.");
        }

        // Task 2
        FileMenu();
    }

    static void CreateDirectoriesAndFiles(string demoPath)
    {
        Directory.CreateDirectory(demoPath);
        Directory.CreateDirectory(Path.Combine(demoPath, "demo1"));
        Directory.CreateDirectory(Path.Combine(demoPath, "demo2"));

        FileInfo file = new FileInfo(Path.Combine(demoPath, "demo1", "file.txt"));
        FileInfo fileInfo = new FileInfo(Path.Combine(demoPath, "demo2", "fileinfo.txt"));
        file.Create().Close();
        fileInfo.Create().Close();

        File.WriteAllText(Path.Combine(demoPath, "demo1", "file1.txt"), "Content for file1");
        File.WriteAllText(Path.Combine(demoPath, "demo2", "file2.txt"), "Content for file2");

        File.WriteAllText(Path.Combine(demoPath, "demo1", "file3.txt"), "Content for file3");
        File.WriteAllText(Path.Combine(demoPath, "demo2", "file4.txt"), "Content for file4");

        File.Copy(Path.Combine(demoPath, "demo1", "file1.txt"), Path.Combine(demoPath, "demo2", "file1_copy.txt"), true);
    }

    static void DisplayFilesAndDirectories(string path)
    {
        foreach (var file in Directory.GetFiles(path))
        {
            Console.WriteLine($"File: {file}, Creation Time: {File.GetCreationTime(file)}");
        }

        foreach (var directory in Directory.GetDirectories(path))
        {
            Console.WriteLine($"Directory: {directory}, Creation Time: {Directory.GetCreationTime(directory)}");
        }
    }

    static void DeleteAllFilesAndDirectories(string path)
    {
        foreach (var filePath in Directory.GetFiles(path))
        {
            try
            {
                File.Delete(filePath);
                Console.WriteLine($"File deleted: {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
            }
        }

        foreach (var directoryPath in Directory.GetDirectories(path))
        {
            try
            {
                Directory.Delete(directoryPath, true);
                Console.WriteLine($"Directory deleted: {directoryPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting directory {directoryPath}: {ex.Message}");
            }
        }
    }

    static void FileMenu()
    {
        bool flag = true;
        while (flag)
        {
            Console.WriteLine("\nFile Menu:");
            Console.WriteLine("1. Create a new file");
            Console.WriteLine("2. Add contents to the file");
            Console.WriteLine("3. Append contents to the file");
            Console.WriteLine("4. Display contents one by one");
            Console.WriteLine("5. Display all contents together");
            Console.WriteLine("6. Exit");

            try
            {
                byte choice = Byte.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        CreateNewFile();
                        break;

                    case 2:
                        AddContentsToFile();
                        break;

                    case 3:
                        AppendContentsToFile();
                        break;

                    case 4:
                        DisplayContentsOneByOne();
                        break;

                    case 5:
                        DisplayAllContentsTogether();
                        break;

                    case 6:
                        flag = false;
                        break;

                    default:
                        Console.WriteLine("Enter a valid choice");
                        break;
                }
            }
            catch
            {
                Console.WriteLine("Enter a valid choice");
            }
        }
    }

    static void CreateNewFile()
    {
        Console.Write("Enter File Name: ");
        string fileName = Console.ReadLine();

        try
        {
            using (FileStream fs = File.Create(fileName))
            {
                Console.WriteLine($"{fileName} created successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating the file: {ex.Message}");
        }
    }

    static void AddContentsToFile()
    {
        Console.Write("Enter File Name: ");
        string fileName = Console.ReadLine();

        try
        {
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                Console.Write("Enter contents to add to the file: ");
                string contents = Console.ReadLine();
                sw.WriteLine(contents);
                Console.WriteLine("Contents added successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding contents to the file: {ex.Message}");
        }
    }

    static void AppendContentsToFile()
    {
        Console.Write("Enter File Name: ");
        string fileName = Console.ReadLine();

        try
        {
            using (StreamWriter sw = new StreamWriter(fileName, true))
            {
                Console.Write("Enter contents to append to the file: ");
                string contents = Console.ReadLine();
                sw.WriteLine(contents);
                Console.WriteLine("Contents appended successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error appending contents to the file: {ex.Message}");
        }
    }

    static void DisplayContentsOneByOne()
    {
        Console.Write("Enter File Name: ");
        string fileName = Console.ReadLine();

        try
        {
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Console.WriteLine(line);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error displaying contents: {ex.Message}");
        }
    }

    static void DisplayAllContentsTogether()
    {
        Console.Write("Enter File Name: ");
        string fileName = Console.ReadLine();

        try
        {
            string contents = File.ReadAllText(fileName);
            Console.WriteLine(contents);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error displaying contents: {ex.Message}");
        }
    }
}
