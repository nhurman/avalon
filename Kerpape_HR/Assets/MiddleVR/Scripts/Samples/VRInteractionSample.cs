/* VRInteractionSample
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;

public class VRInteractionSample : VRInteraction {

    private void Start()
    {
        // Make sure the base interaction is started
        InitializeBaseInteraction();

        // Must tell base class about our interaction
        CreateInteraction("MyInteraction");
        base.GetInteraction().AddTag("ContinuousNavigation");

        base.Activate();
    }

    void Update () {
        if (IsActive() )
        {
            print("interacting");
        }
    }

    public override void OnActivate()
    {
        print("Activating MyInteraction");
    }

    public override void OnDeactivate()
    {
        print("Deactivating MyInteraction");
    }
}
