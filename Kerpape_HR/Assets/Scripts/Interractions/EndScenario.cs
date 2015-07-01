using System;

namespace Modelisation
{
	/// <summary>
	/// Element that define the end of a scenario.
	/// </summary>
    public class EndScenario : ScenarioItem
    {

        public override bool isEnd()
        {
            return true;
        }
    }
}