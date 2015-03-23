/* VRRaySelection
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;


public class VRRaySelection : VRInteraction {

    public Color HoverColor = Color.green;

    private VRSelection    m_LastSelection = new VRSelection();

    private VRSelectionManager m_SelectionMgr = null;
    private VRWand m_Wand = null;


    private void Start()
    {
        // Make sure the base interaction is started and create interaction
        InitializeBaseInteraction();
        CreateInteraction("VRRaySelection");
        base.GetInteraction().AddTag("ContinuousSelection");
        base.Activate();

        m_SelectionMgr = this.GetComponent<VRSelectionManager>();
        if (m_SelectionMgr == null)
        {
            MVRTools.Log(1, "[X] VRRaySelection: impossible to retrieve VRSelectionManager.");
        }

        m_Wand = this.GetComponent<VRWand>();
        if (m_Wand == null)
        {
            MVRTools.Log(1, "[X] VRRaySelection: impossible to retrieve VRWand.");
        }
    }

    private void Update ()
    {
        if (IsActive())
        {
            _RaySelection();
            _RefreshRayMesh();
            _SendWandEvents();
        }
    }

    private void _SendWandEvents()
    {
        if (!m_Wand.SendWandEvents)
        {
            return;
        }

        VRSelection selection = m_SelectionMgr.GetSelection();

        // Enter/exit events
        if ( !VRSelection.Compare(m_LastSelection, selection) )
        {
            // Selection changed

            // Exit last
            if (m_LastSelection != null)
            {
                m_LastSelection.SelectedObject.SendMessage("OnMVRWandExit", m_LastSelection, SendMessageOptions.DontRequireReceiver);
            }

            // Enter new
            if (selection != null)
            {
                selection.SelectedObject.SendMessage("OnMVRWandEnter", selection, SendMessageOptions.DontRequireReceiver);
            }
        }
        else
        {
            // Hover current
            if (selection != null)
            {
                selection.SelectedObject.SendMessage("OnMVRWandHover", selection, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void _RaySelection()
    {
        // Ray picking
        RaycastHit[] hits;
        Vector3 dir = transform.localToWorldMatrix * Vector3.forward;

        hits = Physics.RaycastAll(transform.position, dir, m_Wand.GetDefaultRayLength());

        bool foundActor = false;
        int currentHitId = 0;
        int foundHitId = 0;
        float distance = Mathf.Infinity;

        while (currentHitId < hits.Length)
        {
            RaycastHit hit = hits[currentHitId];

            if (hit.distance < distance && hit.collider.name != "VRWand")
            {
                if (hit.collider.GetComponent<VRActor>() == null)
                {
                    currentHitId++;
                    continue;
                }

                // Pass through empty/transparent GUI pixels
                VRWebView webView = hit.collider.GetComponent<VRWebView>();
                if (webView != null)
                {
                    if (!webView.GetComponent<Renderer>().enabled || webView.IsPixelEmpty(hit.textureCoord))
                    {
                        currentHitId++;
                        continue;
                    }
                }

                foundActor = true;
                foundHitId = currentHitId;
                distance = hit.distance;
            }

            currentHitId++;
        }

        m_LastSelection = m_SelectionMgr.GetSelection();

        // If something found, select
        if (foundActor)
        {
            RaycastHit selectionHit = hits[foundHitId];
            VRSelection newSelection = new VRSelection();
            newSelection.SourceWand = m_Wand;
            newSelection.SelectedObject = selectionHit.collider.gameObject;
            newSelection.TextureCoordinate = selectionHit.textureCoord;
            newSelection.SelectionDistance = selectionHit.distance;
            newSelection.SelectionContact = selectionHit.point;
            newSelection.SelectionNormal = selectionHit.normal;
            m_SelectionMgr.SetSelection(newSelection);
        }
        else
        {
            m_SelectionMgr.SetSelection(null);
        }
    }

    private void _RefreshRayMesh()
    {
        VRSelection selection = m_SelectionMgr.GetSelection();

        if (selection != null)
        {
            m_Wand.SetRayColor(HoverColor);
            m_Wand.SetRayLength(selection.SelectionDistance);
        }
        else
        {
            m_Wand.SetRayColor(m_Wand.DefaultRayColor);
            m_Wand.SetRayLength(m_Wand.DefaultRayLength);
        }
    }
}
