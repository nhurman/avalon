/* VRGUIScriptEditor
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using UnityEditor;
using System.Collections;
using MiddleVR_Unity3D;
using UnityEditor.Callbacks;

[CustomEditor(typeof(VRWebView))]
public class VRWebViewEditor : Editor
{
	//This will just be a shortcut to the target, ex: the object you clicked on.
	private VRWebView m_VRWebViewScript;

	void Awake()
	{
		m_VRWebViewScript = (VRWebView)target;
	}
	
	void Start()
	{
	}
	
	public override void OnInspectorGUI()
	{
		GUILayout.BeginVertical();

		if (GUILayout.Button("Pick html file"))
		{
			string path = EditorUtility.OpenFilePanel("Please choose a HTML file", "", "html");
			MVRTools.Log("[+] Picked " + path );
			m_VRWebViewScript.m_URL = path;
			EditorUtility.SetDirty(m_VRWebViewScript);
		}
		
		DrawDefaultInspector();
		GUILayout.EndVertical();
	}
}
