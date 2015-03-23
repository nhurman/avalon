/* VRActionSample
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;

public class VRActionSample : MonoBehaviour {

    void VRAction(VRSelection iSelection)
    {
        print(name + ": VRAction.");
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    private void OnMVRWandEnter(VRSelection iSelection)
    {
        print(name + ": OnMVRWandEnter.");
   }

    private void OnMVRWandHover(VRSelection iSelection)
    {
        //print(name + ": OnMVRWandHover.");
    }

    private void OnMVRWandExit(VRSelection iSelection)
    {
        print(name + ": OnMVRWandExit.");
    }

    private void OnMVRWandButtonPressed(VRSelection iSelection)
    {
        print(name + ": OnMVRWandButtonPressed.");
    }

    private void OnMVRWandButtonReleased(VRSelection iSelection)
    {
        print(name + ": OnMVRWandButtonReleased.");
    }

    public void OnMVRTouchBegin(VRTouch iTouch)
    {
        print(name + ": OnMVRTouchBegin by, touch object=" + iTouch.TouchObject);
        this.GetComponent<Renderer>().material.color = Color.red;
    }

    public void OnMVRTouchMoved(VRTouch iTouch)
    {
        print(name + ": OnMVRTouchMoved, touch object=" + iTouch.TouchObject);
    }

    public void OnMVRTouchEnd(VRTouch iTouch)
    {
        print(name + ": OnMVRTouchBegin, touch object=" + iTouch.TouchObject);
        this.GetComponent<Renderer>().material.color = Color.white;
    }
}
