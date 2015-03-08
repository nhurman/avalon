using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Modelisation
{
    public class VariableLight : VariableElement
    {
        // In Unity, light.intensity go from 0 to 8
        public VariableLight() : base(0, 8)
        {
            //ObjectProperty = &light.Intensity;
            ObjectProperty = GetComponent<Light>().Intensity;
        }
        
    }
}
