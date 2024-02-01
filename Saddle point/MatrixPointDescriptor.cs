using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saddle_point
{
    internal class MatrixPointDescriptor
    {
        public int RowIndex { get; private set; }   
        public int ColumnIndex { get; private set; } 
        public int Value { get; private set; }

        public MatrixPointDescriptor(int rowIndex, int columnIndex, int value)
        {
            RowIndex = rowIndex;
            ColumnIndex = columnIndex;
            Value = value;
        }
        public override string ToString()
        {
            return $"Value = {Value}, Location = [{RowIndex}, {ColumnIndex}]";
        }

    }
}
