using RockPaperScissors.Models;
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Service
{
    public class RockPaperScissor
    {
        private static readonly string B = ConfigurationManager.AppSettings["B"];
        private readonly string[] shapes = { "rock", "paper", "scissors" };
        Player playerB = new Player
        {
            Name = B
        };
        public async Task<string> Play(Player playerA, int times)
        {
            var strShapes = String.Join(",", shapes);
            if (!strShapes.Contains(playerA.Shape))
            {
                return $"{playerA.Name}\'s shape {playerA.Shape} is not valid";
            }

            /* rock > scissors, paper > rock, scissors > paper
            A player who decides to play rock will beat another player who has chosen scissors ("rock crushes scissors" or "breaks scissors" or sometimes "blunts scissors"[4]),
           but will lose to one who has played paper ("paper covers rock"); 
           a play of paper will lose to a play of scissors ("scissors cuts paper"). 
           If both players choose the same shape, the game is tied and is usually immediately replayed to break the tie.
            */

            int AWins = 0;
            int BWins = 0;
            int Draws = 0;
            StringBuilder str = new StringBuilder();
            int i = 0;

            while (i < times)
            {
                string result;

                playerB.Shape = await GetRandomShape();

                // A generic check for all conditions. It is possible the inserted shape might not always. be rock
                if (playerA.Shape.Equals(shapes[0]) && playerB.Shape.Equals(shapes[2]))
                {
                    result = PlayerAWins(playerA, str);
                    AWins++;
                }
                else if (playerA.Shape.Equals(shapes[2]) && playerB.Shape.Equals(shapes[0]))
                {
                    result = PlayerBWins(playerA, str);
                    BWins++;
                }
                else if (playerA.Shape.Equals(shapes[0]) && playerB.Shape.Equals(shapes[1]))
                {
                    result = PlayerBWins(playerA, str);
                    BWins++;
                }
                else if (playerA.Shape.Equals(shapes[1]) && playerB.Shape.Equals(shapes[0]))
                {
                    result = PlayerAWins(playerA, str);
                    AWins++;
                }
                else if (playerA.Shape.Equals(shapes[1]) && playerB.Shape.Equals(shapes[2]))
                {
                    result = PlayerAWins(playerA, str);
                    BWins++;
                }
                else if (playerA.Shape.Equals(shapes[2]) && playerB.Shape.Equals(shapes[1]))
                {
                    result = PlayerAWins(playerA, str);

                    AWins++;
                }
                else
                {
                    result = $"{playerA.Name} draws {playerB.Name} \n";
                    str.AppendLine(result);
                    Draws++;
                    //There must always be a winner. So play again
                    continue;
                }
                i++;
            }

            return $"{str} \nA won {AWins} times and B won {BWins} times.\n Draws and Replays : {Draws} \n";
        }

        private string PlayerBWins(Player playerA, StringBuilder str)
        {
            string result = $"{playerA.Name} {playerA.Shape} vs {playerB.Name } {playerB.Shape}. {playerB.Name} wins {playerA.Name} \n";
            str.AppendLine(result);
            return result;
        }

        private string PlayerAWins(Player playerA, StringBuilder str)
        {
            string result = $"{playerA.Name} {playerA.Shape} vs {playerB.Name } {playerB.Shape}. {playerA.Name} wins {playerB.Name} \n";
            str.AppendLine(result);
            return result;
        }

        private async Task<string> GetRandomShape()
        {
            //delay by 1ms to generated a more different random shape
            await Task.Delay(1);
            var randomShapeIndex = new Random().Next(0, shapes.Length);
            return shapes[randomShapeIndex];
        }

    }
}
