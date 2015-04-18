using UnityEngine;
using System.Collections;

public class Telecommande : MonoBehaviour
{
	#pragma warning disable 0414

	private vrCommand m_MyCommand;

	#pragma warning restore 0414

	private vrWebView m_webView;
	
	private vrValue CommandHandler(vrValue iValue)
	{
		print("HTML Button was clicked");

		m_webView.ExecuteJavascript("setText('Button was clicked !')");
		
		return null;
	}
	
	void Start () {
		m_MyCommand = new vrCommand("MyCommand", CommandHandler);
		m_webView = GetComponent<VRWebView>().webView;
	}
	
}

