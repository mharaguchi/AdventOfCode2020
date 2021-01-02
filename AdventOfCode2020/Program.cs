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
            var answer = RunDay18();

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
        static string RunDay4()
        {
            return Day4.Run(GetInput(4)).ToString();
        }

        static string RunDay5()
        {
            return Day5.Run(GetInput(5)).ToString();
        }
        static string RunDay6()
        {
            return Day6.Run(GetInput(6)).ToString();
        }
        static string RunDay7()
        {
            return Day7.Run(GetInput(7)).ToString();
        }
        static string RunDay7Part2()
        {
            return Day7Part2.Run(GetInput(7)).ToString();
        }
        static string RunDay8()
        {
            return Day8.Run(GetInput(8)).ToString();
        }

        static string RunDay9()
        {
            return Day9.Run(GetInput(9)).ToString();
        }
        static string RunDay10()
        {
            return Day10Part2.Run(GetInput(10)).ToString();
        }
        static string RunDay11()
        {
            return Day11Part2.Run(GetInput(11)).ToString();
        }

        static string RunDay12()
        {
            return Day12Part2.Run(GetInput(12)).ToString();
        }

        static string RunDay13()
        {
            return Day13Part2.Run(GetInput(13)).ToString();
        }

        static string RunDay14()
        {
            return Day14Part2.Run(GetInput(14)).ToString();
        }
        static string RunDay15()
        {
            return Day15.Run("").ToString();
        }
        static string RunDay16()
        {
            return Day16Part2.Run(GetInput(16)).ToString();
        }
        static string RunDay17()
        {
            return Day17Part2.Run(GetInput(17)).ToString();
        }
        static string RunDay18()
        {
            return Day18Part2.Run(GetInput(18)).ToString();
        }
        static string RunDay19()
        {
            return Day19Part2.Run(GetInput(19)).ToString();
        }
        static string RunDay20()
        {
            return Day20.Run(GetInput(20)).ToString();
        }
        static string RunDay21()
        {
            return Day21.Run(GetInput(21)).ToString();
        }
        static string RunDay22()
        {
            return Day22Part2.Run(GetInput(22)).ToString();
        }
        static string RunDay23()
        {
            return Day23Part2.Run("219347865").ToString();
        }
        static string RunDay24()
        {
            return Day24Part2.Run(GetInput(24)).ToString();
        }
        static string RunDay25()
        {
            return Day25.Run(GetInput(25)).ToString();
        }
    }
}
