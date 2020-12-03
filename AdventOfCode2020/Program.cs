using System;
using System.Text;
using System.Windows.Forms;

namespace AdventOfCode2020
{
    class Program
    {
        static string GetInput(int day)
        {
            //F:\Projects\Software\AdventOfCode2020\AdventOfCode2020\input
            string text = System.IO.File.ReadAllText(@"F:\Projects\Software\AdventOfCode2020\AdventOfCode2020\input\day" + day.ToString() + ".txt", Encoding.Default);
            return text;
        }

        [STAThread]
        static void Main(string[] args)
        {
            var answer = RunDay3();
            Console.WriteLine(answer);
            Clipboard.SetText(answer);
            Console.ReadLine();
        }

        static string RunDay1()
        {
            return Day1.Run(GetInput(1)).ToString();
        }

        static string RunDay2()
        {
            //return Day2.Run(Day2.input).ToString();
            return Day2.RunPart2(GetInput(2)).ToString();
        }

        static string RunDay3()
        {
            return Day3.Run(GetInput(3)).ToString();
        }
    }
}
