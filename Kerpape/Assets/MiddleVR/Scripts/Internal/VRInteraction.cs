/* VRInteraction
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class VRInteraction : MonoBehaviour
{
    private vrInteraction   m_Interaction = null;
    private vrEventListener m_Listener;

    private bool m_Active = false;


    private bool EventListener(vrEvent iEvent)
    {
        vrInteractionEvent evt = vrInteractionEvent.Cast(iEvent);
        if (evt == null) return false;

        vrInteraction evtInteraction = evt.GetInteraction();

        if(m_Interaction != null && evtInteraction != null && evt != null && evtInteraction.GetId() == m_Interaction.GetId())
        {
            if (evt.GetEventType() == (int)VRInteractionEventEnum.VRInteractionEvent_Activated)
            {
                Activate();
            }

            if (evt.GetEventType() == (int)VRInteractionEventEnum.VRInteractionEvent_Deactivated)
            {
                Deactivate();
            }
        }

        return true;
    }

    public void Activate()
    {
        if (m_Active == false)
        {
            m_Active = true;
            MiddleVR.VRInteractionMgr.Activate(m_Interaction);

            OnActivate();
        }
    }

    public void Deactivate()
    {
        if (m_Active == true)
        {
            m_Active = false;
            MiddleVR.VRInteractionMgr.Deactivate(m_Interaction);

            OnDeactivate();
        }
    }

    public virtual void OnActivate()
    {
        MVRTools.Log(3, "[ ] VRInteraction: Activating '" + m_Interaction.GetName() + "'.");
    }

    public virtual void OnDeactivate()
    {
        MVRTools.Log(3, "[ ] VRInteraction: Deactivating '" + m_Interaction.GetName() + "'.");
    }

    public bool IsActive()
    {
        return m_Active;
    }

    public void InitializeBaseInteraction ()
    {
        m_Listener = new vrEventListener(EventListener);
        MiddleVR.VRKernel.AddEventListener(m_Listener);
    }

    public vrInteraction CreateInteraction(string iName)
    {
        if (m_Interaction == null)
        {
            // Create the requested interaction
            m_Interaction = new vrInteraction(iName);
            MiddleVR.VRInteractionMgr.AddInteraction(m_Interaction);
        }
        else
        {
            // Interaction already existing, rename it
            m_Interaction.SetName(iName);
        }

        return m_Interaction;
    }

    public void SetInteraction(vrInteraction iInteraction)
    {
        m_Interaction = iInteraction;
    }

    public vrInteraction GetInteraction()
    {
        return m_Interaction;
    }
}
