using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saddle_point
{
    internal class MatrixParser
    {
               
        public  MatrixData ParseMatrixFromText(string matrixText) 
        {
            string line;
          
            int[] matrixRow = null;
            int expectedColumnCount = 0;
            List<int[]> list = new List<int[]>();
      
            using (TextReader reader = new StreamReader(new MemoryStream(UTF8Encoding.UTF8.GetBytes(matrixText))))
            {

                while ((line = reader.ReadLine()) != null)
                {
                    matrixRow = ParseLine(line, list.Count, expectedColumnCount);

                    if (matrixRow == null) return null;
                   // Console.WriteLine(line);
                   if(expectedColumnCount == 0) { expectedColumnCount = matrixRow.Length; } 
                   list.Add(matrixRow);
                }
            }

            int[,] matrix = new int[list.Count, expectedColumnCount];

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < expectedColumnCount; j++)
                {
                    matrix[i,j] = list[i][j];
                }
                
            }

            return new MatrixData(matrix);

        }



        private int[] ParseLine(string line, int lineIndex, int expectedColumnCount)
        {
            string[] parts = line.Trim().Split(new char[] { ' ', '\t', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            if(parts == null|| parts.Length == 0)
            {
                Console.WriteLine($"Bad matrix row in input line #{lineIndex}. Empty row.");
                return null;
            }

            if(expectedColumnCount > 0 && parts.Length != expectedColumnCount)
            {
                Console.WriteLine($"Bad matrix row in input line #{lineIndex}. Expected columns count={expectedColumnCount}, Actual columns count={parts.Length}");
                return null;
            }

            int[] result = new int[parts.Length];
            int value = 0;
            string cellText = null;

            for (int j = 0; j < parts.Length; j++)
            {
                cellText = parts[j];
                if (!int.TryParse(cellText, out value))
                {
                    Console.WriteLine($"Bad matrix row in input line #{lineIndex}, column #{j} text '{value}' is not a valid int input.");
                    return null;
                }

                result[j] = value;
            }

            return result;

        }

    }
}
