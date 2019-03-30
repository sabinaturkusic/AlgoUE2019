using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHTP
{
    class Program
    {
        public static double ManhattanProblem(double[,] RightMatrix, double[,] DownMatrix)
        {
            int n = RightMatrix.GetLength(0) ;
            int m = DownMatrix.GetLength(1) ;
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

            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < m; j++)
                {
                    ManhattanMatrix[i, j] =
                     Math.Max(ManhattanMatrix[i - 1, j] + DownMatrix[i - 1, j],
                     ManhattanMatrix[i, j - 1] + RightMatrix[i, j - 1]);
                }
            }

            return ManhattanMatrix.Cast<double>().Max();
        }

        static void Main(string[] args)
        {
            String line;
            bool flag = false;

            //cleanup
            try
            {
                File.Delete(@"right.txt");
                File.Delete(@"down.txt");
            }
            catch(Exception e)
            {

            }


            while ((line = Console.ReadLine()) != null )
             {
                 if (line.Contains("---"))
                 {
                     flag = true; 
                 }
                 if (flag == false)
                 {
                     using (StreamWriter w1 = File.AppendText("down.txt"))
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
                 else
                 {
                     using (StreamWriter w2 = File.AppendText("right.txt"))
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
             }
             

            // right
            var lines = File.ReadAllLines(@"right.txt")
               .Where(x => !string.IsNullOrWhiteSpace(x))
               .Select(x => x.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(double.Parse)
                            .ToList())
               .ToList();

            var h = lines.Count();
            var w = lines.Max(x => x.Count);
            var multiArray = new double[h, w];

            for (var i = 0; i < lines.Count; i++)
                for (var j = 0; j < lines[i].Count; j++)
                    multiArray[i, j] = lines[i][j];

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

            double resultManhattan = ManhattanProblem(multiArray, multiArrayDown);
            Console.WriteLine(resultManhattan); 

        }
    }
}
