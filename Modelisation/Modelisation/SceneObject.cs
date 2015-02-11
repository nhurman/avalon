using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelisation
{
    public abstract class SceneObject : MonoBehavior
    {
        public GameManager GameManager
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    
        public void notifyGameManager()
        {
            throw new System.NotImplementedException();
        }

        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }
}
