sing System;
using System.Collections.Generic;
using System.IO;

/*
Planning / Design:
- Ask user for name/title.
- Ask for URL.
- Optional: Analyse category (Domain name).
- Automatically add date above record.
- Store both name and URL stored as a record in a persistent text file.
- Each record is divided by space for easy viewing.

Record Example:

====<Domain Category>====

XX/XX/2020
<Recent Title>
<URL>

XX/XX/2020
<Old Title>
<URL>

====<Domain Category - 2>====

XX/XX/2020
<Recent Title>
<URL>

XX/XX/2020
<Old Title>
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
                bool i_flag = false;
                Console.WriteLine("'e' to escape | 'i' to flag importance");
                Console.Write("Desc: ");
                string desc = Console.ReadLine();
                if (desc == "e")
                {
                    System.Environment.Exit(0);
                }
                if (desc == "i")
                {
                    i_flag = true;
                    Console.WriteLine("[*] Important Flag On");
                    Console.Write("Desc: ");
                    desc = Console.ReadLine();
                }
                Console.Write("URL: ");
                string url = Console.ReadLine();
                main_program.write_text(desc, url, i_flag);
            }
        }
        public void write_text(string desc, string url, bool i_flag)
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
            string read_lines;
            bool domain_exist = false;
            int line_counter = 0;
            string domain_name;
            if (i_flag == false)
            {
                domain_name = extract_domain(url);
            }
            else
            {
                domain_name = "Important";
            }
            
            while ((read_lines = file.ReadLine()) != null)
            {
                if (read_lines.Contains("====<" + domain_name + ">===="))
                {
                    domain_exist = true;
                    break;
                }
                line_counter++;
            }
            file.Close();
            if (domain_exist == false)
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    Console.WriteLine("[*] Domain Category Unavailable");
                    Console.WriteLine("[+] Creating Domain Category");
                    sw.WriteLine(" ");
                    sw.WriteLine("====<" + domain_name + ">====");
                }
            }
            if (domain_exist == true)
            {
                string[] text_lines = System.IO.File.ReadAllLines(path);
                List<string> text_lines_list = new List<string>(text_lines);
                text_lines_list.Insert(line_counter + 1, " ");
                text_lines_list.Insert(line_counter + 2, DateTime.Now.ToString("dd/MM/yyyy"));
                text_lines_list.Insert(line_counter + 3, desc);
                text_lines_list.Insert(line_counter + 4, url);
                File.WriteAllLines(path, text_lines_list);
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(" ");
                    sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy"));
                    sw.WriteLine(desc);
                    sw.WriteLine(url);
                }
            }
            read_back_text(path, domain_name, desc, url);
        }
        public void read_back_text(string path, string domain_name, string desc, string url)
        {
            string[] lines_array = System.IO.File.ReadAllLines(path);
            Console.WriteLine(" ");
            Console.WriteLine("=====================================<<<< Record Extract >>>>=====================================");
            int line_counter = 0;
            for (int i = 0; i < lines_array.Length - 1; i++)
            {
                if (lines_array[i].Contains("====<" + domain_name + ">====") && lines_array[i + 2].Contains(DateTime.Now.ToString("dd/MM/yyyy")) && lines_array[i + 3].Contains(desc) && lines_array[i + 4].Contains(url))
                {
                    break;
                }
                line_counter++;
            }
            if ((line_counter + 20) > lines_array.Length)
            {
                int line_difference = lines_array.Length - line_counter;
                for (int i = 0; i < line_difference; i++)
                {
                    Console.WriteLine(lines_array[line_counter + i]);
                }
            }
            else
            {
                for (int i = 0; i <= 20; i++)
                {
                    Console.WriteLine(lines_array[line_counter + i]);
                }
            }
            Console.WriteLine(" ");
        }
        public string extract_domain(string url)
        {
            try
            {
                Uri myUri = new Uri(url);
                return myUri.Host;
            }
            catch
            {
                Console.WriteLine("Invalid URL: Placing in the 'Other' domain category");
                return "Other";
            }
        }
    }
}
