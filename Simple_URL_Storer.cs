using System;
using System.Collections.Generic;

/*
Plan:
- Ask for name/title.
- Ask for URL.
- Optiona: Analyse category (Domain name).
- Automatically add date above record.
- Store both name and URL stored as a record in a persistent text file.
- Each record is divided by space for easy viewing.

Record Example:

===<Category>===

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
            main_program.input_intake();
        }
        public void input_intake()
        {
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("URL: ");
            string url = Console.ReadLine();

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
    }
}
