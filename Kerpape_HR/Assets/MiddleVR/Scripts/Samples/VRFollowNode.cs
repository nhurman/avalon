/* VRFollowNode
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class VRFollowNode : MonoBehaviour {
    public string VRNodeName = "HeadNode";
    private vrNode3D m_Node = null;

    void Update () {
        if (m_Node == null && MiddleVR.VRDisplayMgr != null)
        {
            m_Node = MiddleVR.VRDisplayMgr.GetNode(VRNodeName);
        }

        if (m_Node != null)
        {
            transform.position = MVRTools.ToUnity(m_Node.GetPositionVirtualWorld());
            transform.rotation = MVRTools.ToUnity(m_Node.GetOrientationVirtualWorld());
        }
    }
}
