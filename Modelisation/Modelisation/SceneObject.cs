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
    
        public bool notifyGameManager()
        {
            throw new System.NotImplementedException();
            return GameManager.isAuthorised();
        }
    }
}
