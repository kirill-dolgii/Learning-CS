using System;
using System.Collections.Generic;
using System.Linq;

namespace _1275._Find_Winner_on_a_Tic_Tac_Toe_Game
{
    class Program
    {
        public class Solution
        {
            public string Tictactoe(int[][] moves)
            {
                if (moves.Length < 5)
                    return "Pending";

                List<List<int>> o = new List<List<int>>();
                List<List<int>> x = new List<List<int>>();

                for (int i = 0; i != moves.Length; i++)
                {
                    if (i % 2 == 0)
                        x.Add(moves[i].ToList());
                    else
                        o.Add(moves[i].ToList());
                }

                if (x.GroupBy(val => val[0]).Select(gr => gr.ToList().Capacity).ToList().Any(n => n == 3) ||
                    x.GroupBy(val => val[1]).Select(gr => gr.ToList().Capacity).ToList().Any(n => n == 3) ||
                    x.Count(val => val[0] == val[1]) == 3 ||
                    (x.Any(l => l[0] == 2 && l[1] == 0) && x.Any(l => l[0] == 1 && l[1] == 1) && x.Any(l => l[0] == 0 && l[1] == 2)))
                    return "A";
                else if (o.GroupBy(val => val[0]).Select(gr => gr.ToList().Capacity).ToList().Any(n => n == 3) ||
                         o.GroupBy(val => val[1]).Select(gr => gr.ToList().Capacity).ToList().Any(n => n == 3) ||
                        o.Count(val => val[0] == val[1]) == 3 ||
                        (o.Any(l => l[0] == 2 && l[1] == 0) && o.Any(l => l[0] == 1 && l[1] == 1) && o.Any(l => l[0] == 0 && l[1] == 2)))
                    return "B";
                else if (moves.Length >= 5 && moves.Length != 9)
                    return "Pending";

                return "Draw";
            }
        }

        static void Main(string[] args)
        {
            
        }
    }
}
