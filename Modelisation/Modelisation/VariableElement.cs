using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public abstract class VariableElement : Element
    {
        public int Min
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Max
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Step
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Property
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public void setValue()
        {
            throw new System.NotImplementedException();
        }
    }
}
