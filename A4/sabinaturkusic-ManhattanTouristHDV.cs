using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//bsp 20.68
namespace MHTP
{
    class Program
    {
        public static double ManhattanProblem(double[,] RightMatrix, double[,] DownMatrix, double[,] DiagonalMatrix)
        {
            int n = RightMatrix.GetLength(0);
            int m = DownMatrix.GetLength(1);
            int d = DiagonalMatrix.GetLength(1);
            double[,] ManhattanMatrix = new double[n, m];

            ManhattanMatrix[0, 0] = 0;

            for (int i = 1; i < n; i++)
            {
                ManhattanMatrix[i, 0] = ManhattanMatrix[i - 1, 0] + DownMatrix[i - 1, 0];
            }

            for (int j = 1; j < m; j++)
            {
                ManhattanMatrix[0, j] = ManhattanMatrix[0, j - 1] + RightMatrix[0, j - 1];
            }

            /*for (int k = 1; k < d; k++)
            {
                ManhattanMatrix[k, 0] = ManhattanMatrix[k - 1, 0] + DiagonalMatrix[k - 1, 0];
            }*/

            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < m; j++)
                {
                    ManhattanMatrix[i, j] =
                    Math.Max(ManhattanMatrix[i - 1, j] + DownMatrix[i - 1, j],
                    Math.Max(ManhattanMatrix[i, j - 1] + RightMatrix[i, j - 1],
                    ManhattanMatrix[i - 1, j - 1] + DiagonalMatrix[i - 1, j - 1]));
                }
            }

            return ManhattanMatrix.Cast<double>().Max();
        }

        static void Main(string[] args)
        {
            String line;
            int flag = 0;

            //cleanup
            try
            {
                File.Delete(@"right.txt");
                File.Delete(@"down.txt");
                File.Delete(@"diagonal.txt");
            }
            catch (Exception e)
            {
                //Exception Handling
            }


            while ((line = Console.ReadLine()) != null)
            {
                if (line.Contains("---"))
                {
                    flag++;
                }
                if (flag == 0)
                {
                    using (StreamWriter w2 = File.AppendText("down.txt"))
                    {
                        if (line.Contains("-") || line.Contains("G_"))
                        {
                            continue;
                        }
                        else
                        {
                            System.Text.RegularExpressions.Regex.Replace(line, @"\s+", " ");
                            w2.WriteLine(line);
                        }
                    }
                }
                if (flag == 1)
                {
                    using (StreamWriter w1 = File.AppendText("right.txt"))
                    {
                        if (line.Contains("-") || line.Contains("G_"))
                        {
                            continue;
                        }
                        else
                        {
                            System.Text.RegularExpressions.Regex.Replace(line, @"\s+", " ");
                            w1.WriteLine(line);
                        }
                    }
                }
                if (flag == 2)
                {
                    using (StreamWriter w3 = File.AppendText("diagonal.txt"))
                    {
                        if (line.Contains("-") || line.Contains("G_"))
                        {
                            continue;
                        }
                        else
                        {
                            System.Text.RegularExpressions.Regex.Replace(line, @"\s+", " ");
                            w3.WriteLine(line);
                        }
                    }
                }
            }

            // right
            var linesRight = File.ReadAllLines(@"right.txt")
               .Where(x => !string.IsNullOrWhiteSpace(x))
               .Select(x => x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(double.Parse)
                            .ToList())
               .ToList();

            var hRight = linesRight.Count();
            var wRight = linesRight.Max(x => x.Count);
            var multiArrayRight = new double[hRight, wRight];

            for (var i = 0; i < linesRight.Count; i++)
                for (var j = 0; j < linesRight[i].Count; j++)
                    multiArrayRight[i, j] = linesRight[i][j];

            // down
            var linesDown = File.ReadAllLines(@"down.txt")
               .Where(x => !string.IsNullOrWhiteSpace(x))
               .Select(x => x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(double.Parse)
                            .ToList())
               .ToList();

            var hDown = linesDown.Count();
            var wDown = linesDown.Max(x => x.Count);
            var multiArrayDown = new double[hDown, wDown];

            for (var i = 0; i < linesDown.Count; i++)
                for (var j = 0; j < linesDown[i].Count; j++)
                    multiArrayDown[i, j] = (linesDown[i][j]);

            // diagonal
            var linesDiagonal = File.ReadAllLines(@"diagonal.txt")
               .Where(x => !string.IsNullOrWhiteSpace(x))
               .Select(x => x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(double.Parse)
                            .ToList())
               .ToList();

            var hDiagonal = linesDiagonal.Count();
            var wDiagonal = linesDiagonal.Max(x => x.Count);
            var multiArrayDiagonal = new double[hDiagonal, wDiagonal];

            for (var i = 0; i < linesDiagonal.Count; i++)
                for (var j = 0; j < linesDiagonal[i].Count; j++)
                    multiArrayDiagonal[i, j] = linesDiagonal[i][j];

            double resultManhattan = ManhattanProblem(multiArrayRight, multiArrayDown, multiArrayDiagonal);
            Console.WriteLine(resultManhattan);

        }
    }
}