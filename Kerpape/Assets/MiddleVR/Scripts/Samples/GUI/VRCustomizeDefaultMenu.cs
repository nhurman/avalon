/* VRCustomizeDefaultMenu

 * MiddleVR
 * (c) i'm in VR
 */


using UnityEngine;
using System.Collections;

public class VRCustomizeDefaultMenu : MonoBehaviour
{
    // Start waits on VRMenu creation with a coroutine
    IEnumerator Start()
    {
        VRMenu MiddleVRMenu = null;
        while (MiddleVRMenu == null || MiddleVRMenu.menu == null)
        {
            // Wait for VRMenu to be created
            yield return null;
            MiddleVRMenu = FindObjectOfType(typeof(VRMenu)) as VRMenu;
        }

        AddButton(MiddleVRMenu);
        RemoveItem(MiddleVRMenu);
        MoveItems(MiddleVRMenu);

        // End coroutine
        yield break;
    }

    private vrCommand m_MyItemCommand;
    vrValue MyItemCommandHandler(vrValue iValue)
    {
        print("My menu item has been clicked");
        return null;
    }

    private void AddButton(VRMenu vrmenu)
    {
        // Add a button at the start of the menu
        m_MyItemCommand = new vrCommand("VRMenu.MyCustomButtonCommand", MyItemCommandHandler);

        vrWidgetButton button = new vrWidgetButton("VRMenu.MyCustomButton", vrmenu.menu, "My Menu Item", m_MyItemCommand);
        vrmenu.menu.SetChildIndex(button, 0);

        // Add a separator below it
        vrWidgetSeparator separator = new vrWidgetSeparator("VRMenu.MyCustomSeparator", vrmenu.menu);
        vrmenu.menu.SetChildIndex(separator, 1);
    }

    private void RemoveItem(VRMenu vrmenu)
    {
        // Remove "Reset" submenu
        for (uint i = 0; i < vrmenu.menu.GetChildrenNb(); ++i)
        {
            vrWidget widget = vrmenu.menu.GetChild(i);
            if( widget.GetLabel().Contains("Reset"))
            {
                vrmenu.menu.RemoveChild(widget);
                break;
            }
        }   
    }

    private void MoveItems(VRMenu vrmenu)
    {
        // Move every menu item under a sub menu
        vrWidgetMenu subMenu = new vrWidgetMenu("VRMenu.MyNewSubMenu", null, "MiddleVR Menu");

        while (vrmenu.menu.GetChildrenNb() > 0)
        {
            vrWidget widget = vrmenu.menu.GetChild(0);
            widget.SetParent(subMenu);
        }

        subMenu.SetParent(vrmenu.menu);
    }
}
