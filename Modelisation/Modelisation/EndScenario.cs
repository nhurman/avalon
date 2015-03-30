using System;

namespace Modelisation
{
    //This class is not actually needed on one line scenario
    public class EndScenario : ScenarioItem
    {
        public override bool isEnd()
        {
            return true;
        }
    }
}