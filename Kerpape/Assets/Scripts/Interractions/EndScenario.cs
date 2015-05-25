using System;

namespace Modelisation
{
	/// <summary>
	/// Element that defines the end of a scenario.
	/// </summary>
    public class EndScenario : ScenarioItem
    {
        public override bool isEnd()
        {
            return true;
        }
    }
}