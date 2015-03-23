/* VRManagerPostFrame
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;
using System.IO;

public class VRManagerPostFrame : MonoBehaviour {
    public vrKernel kernel = null;
    public vrDeviceManager dmgr = null;
    public vrClusterManager cmgr = null;

    private bool LoggedNoKeyboard = false;

    IEnumerator EndOfFrame()
    {
        yield return new WaitForEndOfFrame();

        VRManagerScript mgr = GetComponent<VRManagerScript>();
        if (mgr != null && MiddleVR.VRKernel == null )
        {
            Debug.LogWarning("[ ] If you have an error mentionning 'DLLNotFoundException: MiddleVR_CSharp', please restart Unity. If this does not fix the problem, please make sure MiddleVR is in the PATH environment variable.");
            mgr.GetComponent<GUIText>().text = "[ ] Check the console window to check if you have an error mentionning 'DLLNotFoundException: MiddleVR_CSharp', please restart Unity. If this does not fix the problem, please make sure MiddleVR is in the PATH environment variable.";
        }

        MVRTools.Log(4, "[>] Unity: Starting VR End of Frame.");

        if( kernel == null )
        {
            kernel = MiddleVR.VRKernel;
        }

        if (dmgr == null)
        {
            dmgr = MiddleVR.VRDeviceMgr;
        }

        if(cmgr == null)
        {
            cmgr = MiddleVR.VRClusterMgr;
        }

        if (dmgr != null )
        {
            vrKeyboard keyb = dmgr.GetKeyboard();
            if (keyb != null)
            {
                VRManagerScript vrmgr = GetComponent<VRManagerScript>();

                if (vrmgr != null && vrmgr.QuitOnEsc && keyb.IsKeyPressed((uint)MiddleVR.VRK_ESCAPE))
                {
                    vrmgr.QuitApplication();
                }
            }
            else
            {
                if (!LoggedNoKeyboard)
                {
                    MVRTools.Log("[X] No VR keyboard");
                    LoggedNoKeyboard = true;
                }
            }
        }

        if (kernel != null)
        {
            /*
            MVRTools.Log("SavingRT"); ;
            SaveRT();
             */
            kernel.PostFrameUpdate();
        }

        MVRTools.Log(4, "[<] Unity: End of VR End of Frame.");

        if( kernel != null && kernel.GetFrame() == 2 && !Application.isEditor )
        {
            MVRTools.Log(2, "[ ] If the application is stuck here and you're using Quad-buffer active stereoscopy, make sure that in the Player Settings of Unity, the option 'Run in Background' is checked.");
        }
    }

    void Update () {
        MVRTools.Log(4, "[>] Unity: VR PostFrame Update!");

        MiddleVR.VRClusterMgr.EndFrameUpdate();

        MVRTools.Log(4, "[ ] Unity: StartCoRoutine EndOfFrame!");
        StartCoroutine(EndOfFrame());

        MVRTools.Log(4, "[<] Unity: End of VR PostFrame Update!");
    }
}
