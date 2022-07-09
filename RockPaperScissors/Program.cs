using RockPaperScissors.Models;
using RockPaperScissors.Service;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    internal class Program
    {
        private static readonly string A = ConfigurationManager.AppSettings["A"];
        private static readonly string shape = ConfigurationManager.AppSettings["shape"];

        static void Main(string[] args)
        {
            Player playerA = new Player
            {
                Name = A,
                Shape = shape
            };

            RockPaperScissor rps = new RockPaperScissor();

            Task.Run(async delegate
            {
                Console.WriteLine(await rps.Play(playerA, 100));
            }).Wait();

            Console.ReadLine();
        }
    }

   

   
}
