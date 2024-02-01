using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saddle_point
{
    internal class MatrixData
    {
        public int RowCount => InnerMatrix.GetLength(0);
        public int ColumnCount => InnerMatrix.GetLength(1);
        public int[,] InnerMatrix { get; private set; }

           
        public MatrixData(int[,] matrix) 
        {
            InnerMatrix = matrix;
        }

        public static MatrixData RandomMatrix(int matrixRowCount, int matrixColumnCount)
        {
            Random rand = new Random();
            int[,] matrix = new int[matrixRowCount, matrixColumnCount];
             
            HashSet<int> uniqueSet = new HashSet<int>();

            int nextVal;
            int maxValue = matrixRowCount * matrixColumnCount * 2;

            for (int i = 0; i < matrixRowCount; i++)
            {
                for (int j = 0; j < matrixColumnCount; j++)
                {
                    do
                    {
                        nextVal = rand.Next(0, maxValue);
                    } while (uniqueSet.Contains(nextVal));

                    uniqueSet.Add(nextVal);
                    matrix[i, j] = nextVal;
                }
            }

            return new MatrixData(matrix);
        }

        
        public void PrintMatrix()
        {
            for (int i = 0; i < RowCount; i++)
            {
                for (int j = 0; j < ColumnCount; j++)
                {
                    Console.Write("{0,6} ", InnerMatrix[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}
