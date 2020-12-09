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
            var answer = RunDay9();
            //var answer = Day5.GetRow("FBFBBF").ToString();
            //var answer = Day5.GetCol("RLR").ToString();
            //var answer = Day5.GetSeat("BBFFBBFRLL").ToString();
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
    }
}
