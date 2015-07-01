/* VRGUIHTMLBasicSample

 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using System.Collections;

/*
 * 
 * Use with data/GUI/HTMLBasicSample/index.html in MiddleVR install directory
 * 
 */

public class VRGUIHTMLBasicSample : MonoBehaviour
{
    // Disable warning CS0414 "The private field 'XXX' is assigned but its value is never used"
    #pragma warning disable 0414

    private vrCommand m_MyCommand;

    private vrValue CommandHandler(vrValue iValue)
    {
        print("HTML Button was clicked");

        // Uncomment the following lines to have modify the HTML page in response !
        //vrWebView webView = GetComponent<VRWebView>().webView;
        //webView.ExecuteJavascript("AddResult('Button was clicked !')");

        return null;
    }

	void Start () {
        m_MyCommand = new vrCommand("MyCommand", CommandHandler);
	}
}
