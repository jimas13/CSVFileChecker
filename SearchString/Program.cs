using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace SearchString
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Give directory");
                string path = Console.ReadLine();
                Console.WriteLine("Search for");
                string searchValue = Console.ReadLine();

                var allFiles = from file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                               .Where(s => s.EndsWith(".txt") || s.EndsWith(".csv"))
                               from line in File.ReadLines(file)
                               where line.Contains(searchValue)
                               select new
                               {
                                   File = file,
                                   Line = line
                               };

                
                List<string> listOfFiles = new List<string>();
                List<string> listOfLines = new List<string>();

                foreach (var file in allFiles)
                {
                    listOfLines.Clear();
                    if (!listOfFiles.Contains(file.File))
                    {
                        if (listOfFiles.Any())
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                        }
                        listOfFiles.Add(file.File);
                        Console.WriteLine();
                        Console.WriteLine($"{file.File}");
                        Console.WriteLine($"{Path.GetFileName(file.File)}");
                        Console.WriteLine();
                    }
                    
                    if (!listOfLines.Contains(file.Line))
                    {
                        listOfLines.Add(file.Line);
                        Console.WriteLine($"{file.Line}");
                    }
                    
                }

            }
            catch (UnauthorizedAccessException uAEx)
            {
                Console.WriteLine(uAEx.Message);
            }
            catch (PathTooLongException pathEx)
            {
                Console.WriteLine(pathEx.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }

            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);
        }
    }
}
