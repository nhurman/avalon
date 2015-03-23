/* VRMenuMaximizeButton
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;

public class VRMenuMaximizeButton : MonoBehaviour
{
    public VRMenuManager MenuManager;
    public GameObject    ToggleIcon;

    public Texture ClosedTexture;
    public Texture OpenTexture;


    void VRAction()
    {
        if (MenuManager != null)
        {
            MenuManager.ToggleMenuGUI();
        }
    }

    public void Toggle(bool iOpen)
    {
        if (iOpen)
        {
            ToggleIcon.GetComponent<Renderer>().material.SetTexture("_MainTex", OpenTexture);
        }
        else
        {
            ToggleIcon.GetComponent<Renderer>().material.SetTexture("_MainTex", ClosedTexture);
        }
    }
}
