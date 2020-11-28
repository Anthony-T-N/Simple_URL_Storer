using System;
using System.Collections.Generic;
using System.IO;

/*
Plan:
- Ask for name/title.
- Ask for URL.
- Optiona: Analyse category (Domain name).
- Automatically add date above record.
- Store both name and URL stored as a record in a persistent text file.
- Each record is divided by space for easy viewing.

Record Example:

====<Category>====

XX/XX/2020
<Title>
<URL>

XX/XX/2020
<Title>
<URL>

 */


namespace Simple_URL_Storer
{
    class Simple_URL_Storer
    {
        static void Main(string[] args)
        {
            Simple_URL_Storer main_program = new Simple_URL_Storer();
            while (true)
            {
                Console.WriteLine("Enter 'e' to escape");
                Console.Write("Name: ");
                string name = Console.ReadLine();
                Console.Write("URL: ");
                string url = Console.ReadLine();
                main_program.write_text(name, url);
                if (name == "e" || url == "e")
                {
                    System.Environment.Exit(0);
                }
            }
        }

        /*
        public void input_intake(string name, string url)
        {
            List<string> record = new List<string>() {
                DateTime.Now.ToString("yyyy-MM-dd"), 
                name, 
                url
            };
            for (int i = 0; i <= record.Count - 1; i++)
            {
                Console.WriteLine(record[i]);
            }

        }
        */
        public void write_text(string name, string url)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "[record].txt");
            if (!System.IO.File.Exists(path))
            {
                Console.WriteLine(" ");
                Console.WriteLine("[*] Text file does not exist. Creating a new one.");
                Console.WriteLine(" ");
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(" ");
                    sw.WriteLine("=====================================<<<< Record Text File >>>>=====================================");
                }
            }

            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string line;
            bool flag = false;
            int line_counter = 0;
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains("====<" + extract_domain(url) + ">===="))
                {
                    flag = true;
                    break;
                }
                line_counter++;
            }
            file.Close();
            if (flag == false)
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    Console.WriteLine("[*] Domain Category Unavailable");
                    Console.WriteLine("[+] Creating Domain Category");
                    sw.WriteLine(" ");
                    sw.WriteLine("====<" + extract_domain(url) + ">====");
                    Console.WriteLine(" ");
                }
            }
            if (flag == true)
            {
                var text_lines = System.IO.File.ReadAllLines(path);
                List<string> text_lines_list = new List<string>(text_lines);
                text_lines_list.Insert(line_counter + 1, " ");
                text_lines_list.Insert(line_counter + 2, DateTime.Now.ToString("dd/MM/yyyy"));
                text_lines_list.Insert(line_counter + 3, name);
                text_lines_list.Insert(line_counter + 4, url);
                File.WriteAllLines(path, text_lines_list);
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(" ");
                    sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
                    sw.WriteLine(name);
                    sw.WriteLine(url);
                }
            }

            string[] lines = System.IO.File.ReadAllLines(path);
            // Ensures maximum number of records shown back is kept at 15 lines (5 Records).
            if (lines.Length > 30)
            {
                for (int i = 30; i > 0; i--)
                {
                    string last_lines = lines[lines.Length - i];
                    System.Console.WriteLine(last_lines);
                }
            }
            else if (lines.Length < 30)
            {
                for (int i = lines.Length; i > 0; i--)
                {
                    string last_lines = lines[lines.Length - i];
                    System.Console.WriteLine(last_lines);
                }
            }
            Console.WriteLine(" ");
        }
        public string extract_domain(string url)
        {
            Uri myUri = new Uri(url);
            return myUri.Host;
        }
    }
}
