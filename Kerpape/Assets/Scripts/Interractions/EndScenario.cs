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

		public override void startAction() {

		}
		public override void stopAction() {

		}
		
		public override void inst() {
		
		}
    }
}