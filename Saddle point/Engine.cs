using System;
using TextCopy;

namespace Saddle_point
{
    internal class Engine
    {
       
        private MatrixPointDescriptor GetMaxValueInRow(MatrixData data, int rowIndex)
        {
            int maxValue = data.InnerMatrix[rowIndex, 0];
            int colIndex = 0;

            for(int j=1; j< data.ColumnCount; j++)
            {
                if (data.InnerMatrix[rowIndex, j] > maxValue) 
                {
                    maxValue = data.InnerMatrix[rowIndex, j];
                    colIndex = j;
                }
            }

            return new MatrixPointDescriptor(rowIndex, colIndex, maxValue);
        }

        private MatrixPointDescriptor GetMinValueInColumn(MatrixData data, int columnIndex)
        {
            int minValue = data.InnerMatrix[0, columnIndex];
            int rowIndex = 0;

            for (int i = 1; i < data.RowCount; i++)
            {
                if (data.InnerMatrix[i,columnIndex] < minValue)
                {
                    minValue = data.InnerMatrix[i, columnIndex];
                    rowIndex = i;
                }
            }

            return new MatrixPointDescriptor(rowIndex, columnIndex, minValue);
        }

        private MatrixPointDescriptor FindSaddlePoint(MatrixData data)
        {
            MatrixPointDescriptor currentPointByRow;
            MatrixPointDescriptor currentPointByColumn;

            for (int i=0; i< data.RowCount; i++)
            {
                currentPointByRow = GetMaxValueInRow(data, i);
                currentPointByColumn = GetMinValueInColumn(data, currentPointByRow.ColumnIndex);

                if (currentPointByRow.RowIndex == currentPointByColumn.RowIndex)
                    return currentPointByRow;
            }

            return null;

                      
        }

        private MatrixData GetUserInput()
        {
            Console.WriteLine("Select data input mode (matrix data source): [C]-copy matrix from Clipboard or [R]-generate random matrix.");
            string userInput = null;
             
            do
            {
                if (userInput != null)
                {
                    Console.WriteLine($"Value '{userInput}' is not a valid input, please enter one of 'C' or 'R' symbols.");
                }

                userInput = Console.ReadKey().KeyChar.ToString().ToUpper();                        

            } while (!(userInput == "C" || userInput == "R"));

                           

            if (userInput == "C")
            {
                return GetUserInputMatrixFromClipboard();
            }

            // userInput == "R"
            return GetUserInputForRandomMatrix();
        }

        private MatrixData GetUserInputMatrixFromClipboard()
        {
            MatrixParser parser = new MatrixParser();
            MatrixData data = null;

            do
            {
                Console.WriteLine("\nPlease copy matrix data to clipboard and press enter when ready:");
                Console.ReadLine();
                data = parser.ParseMatrixFromText(ClipboardService.GetText());
                if (data == null)
                {
                    Console.WriteLine("Bad matrix data. Please try again.");
                    continue;
                }

            } while (data == null);

            return data;
        }
        private MatrixData GetUserInputForRandomMatrix()
        {
            Console.WriteLine("\nEnter dimentions of MxN matrix");
            int rowCount = -1;
            int columnCount = -1;
            string inputStr= null;

            do
            {
                Console.Write("M: ");     
                inputStr= Console.ReadLine();
            } while ( ! ( int.TryParse(inputStr, out rowCount) && rowCount > 0 && rowCount <= 32768 ));

            do
            {
                Console.Write("N: ");
                inputStr = Console.ReadLine();
            } while (!(int.TryParse(inputStr, out columnCount) && columnCount > 0 && columnCount <= 32768));

            
            return MatrixData.RandomMatrix(rowCount,columnCount);

        }
        public void RunLookup()
        {
           
            MatrixData data = GetUserInput();

            data.PrintMatrix();

           
            MatrixPointDescriptor saddlePoint = FindSaddlePoint(data);

            
            if(saddlePoint != null)
            {
                Console.WriteLine($"Matrix Saddle point found: {saddlePoint}.");
              
            }
            else
            {
                Console.WriteLine("Matrix does not contain Saddle point.(\"No\")");
            }
            
        }
    }
}
