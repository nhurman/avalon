using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace Modelisation
{
	/// <summary>
	/// Scenario item that play a sound as long as it is active.
	/// </summary>
	public class AudioScenarioItem : ScenarioItem
	{
		private AudioSource audio;


		/// <summary>
		/// Allow creation of an audioscenario item; the sound is played until the next scenarioItem become current.
		/// </summary>
		/// <param name="name">The name of the object in the unity scene.</param>
		/// <param name="descrition">Desciption of what the action is supposed to achieve.</param>
		/// <param name="audioName">Name of the audio that should be played.</param>
		/// <param name="name">Allow the execution of other actions.</param>
		/// <param name="name">Allow this action to be executed in a different mode than the current one.</param>
		public AudioScenarioItem (string name, string description, string audioName, bool auth = true, Mode? mode = null)  :  base(name, description, auth, mode)
		{
			audio = GameObject.Find ("/GameManager/" + audioName).GetComponent<AudioSource>();
		}

		public virtual void startAction() {
			audio.Play ();
		}
		public virtual void stopAction() {
			audio.Stop ();
		}
		
	}
}

