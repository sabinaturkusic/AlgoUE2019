using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace needlemanwunsch
{
    class Program
    {
        public static void Main(string[] argv)
        {
            //string seqa = Console.ReadLine();
            //string seqb = Console.ReadLine();
            //int gap = Convert.ToInt32(Console.ReadLine());  
            //int match = Convert.ToInt32(Console.ReadLine());
            //int mismatch = Convert.ToInt32(Console.ReadLine());
            
            int gap = -2;
            int match = 1;
            int mismatch = -1;

            if (argv.Length > 1)
            {
                
                for (int i = 0; i < argv.Length; i++)
                {
                    if (argv[i] == "--match")
                    {
                        if(string.IsNullOrWhiteSpace(argv[i+1]))
                        {
                            Console.WriteLine("Fehler beim Aufruf."); 
                        }
                        match = Convert.ToInt32(argv[i + 1]); 
                    }
                    if (argv[i] == "--mismatch")
                    {
                        if (string.IsNullOrWhiteSpace(argv[i + 1]))
                        {
                            Console.WriteLine("Fehler beim Aufruf.");
                        }
                        mismatch = Convert.ToInt32(argv[i + 1]);
                    }
                    if(argv[i] == "--gap")
                    {
                        if (string.IsNullOrWhiteSpace(argv[i + 1]))
                        {
                            Console.WriteLine("Fehler beim Aufruf.");
                        }
                        gap = Convert.ToInt32(argv[i + 1]); 
                    }
                }
            }

            //string seqa = "AGT";
            //string seqb = "AAAT";
            string line;
            string seqaheader = "";
            string seqbheader = ""; 
            string seqa = "";
            string seqb = ""; 
            int counter = 0; 
            while ((line = Console.ReadLine()) != null)
            {
                if (line.StartsWith(">"))
                {
                    counter++; 
                }
                if(counter == 1 && line.StartsWith(">"))
                {
                    seqaheader = line; 
                }
                if (counter == 2 && line.StartsWith(">"))
                {
                    seqbheader = line;
                }
                if (counter == 1 && !(line.StartsWith(">")))
                {
                    seqa += line; 
                }
                if (counter == 2 && !(line.StartsWith(">")))
                {
                    seqb += line;
                }
            }

            seqaheader = seqaheader.Substring(1, seqaheader.IndexOf(" "));
            seqbheader = seqbheader.Substring(1, seqbheader.IndexOf(" "));

            /*Console.WriteLine("seqaheader = " + seqaheader);
            Console.WriteLine("seqbheader = " + seqbheader);
            Console.WriteLine("seqa = " + seqa);
            Console.WriteLine("seqb = " + seqb);*/

            var result = Align(seqa, seqb, gap, match, mismatch);

            PrintResult(seqaheader, seqbheader, seqa, result[0]); 
        }

        public static void PrintResult(string seqaheader, string seqbheader, string seqa, string nwseq)
        {
            
            string[] matchresult = new string[seqa.Length]; 
            for (int i = 0; i < seqa.Length; i++)
            {
                if (seqa[i] == nwseq[i])
                {
                    matchresult[i] = "*"; 
                }
                else
                {
                    matchresult[i] = " ";
                }
            }
           

            Console.Write(seqaheader);
            Console.Write("     ");
            Console.WriteLine(seqa);
            Console.Write(seqbheader);
            Console.Write("     ");
            Console.WriteLine(nwseq);

            
            var resultheader = new StringBuilder();
            resultheader.Append(' ', (seqaheader.Length + 5));
            Console.Write(resultheader); 

            for (int i = 0; i < seqa.Length; i++)
            {
                if (seqa[i] == nwseq[i])
                {
                    Console.Write("*"); 
                }
                else
                {
                    Console.Write(" ");
                }
            }
        }
        
        
        public static int ScoreFunction(char a, char b, int matchScore, int mismatchScore)
        {
            
            return a == b ? matchScore : mismatchScore;
        }


        
        public static string[] Align(string sequenceA, string sequenceB, int gapPenalty, int matchScore, int mismatchScore)
        {
            
            #region Initialize
            int[,] matrix = new int[sequenceA.Length + 1, sequenceB.Length + 1];
            char[,] tracebackMatrix = new char[sequenceA.Length + 1, sequenceB.Length + 1];
            matrix[0, 0] = 0;

            
            for (int i = 1; i < sequenceA.Length + 1; i++)
            {
                matrix[i, 0] = matrix[i - 1, 0] + gapPenalty;
                tracebackMatrix[i, 0] = 'L';
            }

            
            for (int i = 1; i < sequenceB.Length + 1; i++)
            {
                matrix[0, i] = matrix[0, i - 1] + gapPenalty;
                tracebackMatrix[0, i] = 'U';
            }
            #endregion

            
            #region Scoring
            for (int i = 1; i < sequenceA.Length + 1; i++)
            {
                for (int j = 1; j < sequenceB.Length + 1; j++)
                {
                    
                    int diagonal = matrix[i - 1, j - 1] + ScoreFunction(sequenceA[i - 1], sequenceB[j - 1], matchScore, mismatchScore);
                    int links = matrix[i - 1, j] + gapPenalty;
                    int oben = matrix[i, j - 1] + gapPenalty;

                    
                    matrix[i, j] = Math.Max(oben, Math.Max(links, diagonal));

                    
                    if (matrix[i, j] == diagonal && i > 0 && j > 0)
                    {
                        tracebackMatrix[i, j] = 'D';
                    }
                    else if (matrix[i, j] == links)
                    {
                        tracebackMatrix[i, j] = 'L';
                    }
                    else if (matrix[i, j] == oben)
                    {
                        tracebackMatrix[i, j] = 'U';
                    }
                }
            }
            #endregion

            int rowLength = matrix.GetLength(0);
            int colLength = matrix.GetLength(1);
            
            
            Console.Error.Write(matrix[sequenceA.Length, sequenceB.Length]);
            Console.Error.Write(Environment.NewLine); 

            
            #region Traceback
            return TraceBack(tracebackMatrix, sequenceA, sequenceB);
            #endregion

        }

        
        public static string[] TraceBack(char[,] tracebackMatrix, string sequenzA, string sequenzB)
        {

            int i = tracebackMatrix.GetLength(0) - 1;
            int j = tracebackMatrix.GetLength(1) - 1;

            StringBuilder alignedSeqA = new StringBuilder();
            StringBuilder alignedSeqB = new StringBuilder();

            
            while (tracebackMatrix[i, j] != 0)
            {
                switch (tracebackMatrix[i, j])
                {
                    case 'D':
                        alignedSeqA.Append(sequenzA[i - 1]);
                        alignedSeqB.Append(sequenzB[j - 1]);
                        i--;
                        j--;
                        break;
                    case 'U':
                        alignedSeqA.Append("-");
                        alignedSeqB.Append(sequenzB[j - 1]);
                        j--;
                        break;
                    case 'L':
                        alignedSeqA.Append(sequenzA[i - 1]);
                        alignedSeqB.Append("-");
                        i--;
                        break;

                }
            }

            string[] alignments = new string[2];
            
            alignments[0] = new string(alignedSeqA.ToString().Reverse().ToArray());
            alignments[1] = new string(alignedSeqB.ToString().Reverse().ToArray());

            
            return alignments;

        }
    }
}
