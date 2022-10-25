// See https://aka.ms/new-console-template for more information
using System;

namespace stage0
{
    partial class Progrem
    {
        static void Main(string[] args)
        {
            welcome7796();
            welcome6941();
            Console.ReadKey();
        }

        static partial void welcome6941();
        private static void welcome7796()
        {
            Console.WriteLine("enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application", name);
        }
    }
}