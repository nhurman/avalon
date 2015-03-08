using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public class Table : VariableElement
    {
        public static double minHeight;
        public static double maxHeight;
        public Table() : base(minHeight, maxHeight)
        {

        }
    }
}
