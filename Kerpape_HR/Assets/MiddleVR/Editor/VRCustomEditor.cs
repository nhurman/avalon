/* VRCustomEditor
 * MiddleVR
 * (c) i'm in VR
 */

using UnityEngine;
using UnityEditor;
using System.Collections;
using MiddleVR_Unity3D;
using UnityEditor.Callbacks;

[CustomEditor(typeof(VRManagerScript))]
public class VRCustomEditor : Editor
{
    //This will just be a shortcut to the target, ex: the object you clicked on.
    private VRManagerScript mgr;

    static private bool m_SettingsApplied = false;

    void Awake()
    {
        mgr = (VRManagerScript)target;

        if( !m_SettingsApplied )
        {
            ApplyVRSettings();
            m_SettingsApplied = true;
        }
    }

    void Start()
    {
        Debug.Log("MT: " + PlayerSettings.MTRendering);
    }

    public void ApplyVRSettings()
    {
        PlayerSettings.defaultIsFullScreen = false;
        PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Disabled;
        PlayerSettings.runInBackground = true;
        PlayerSettings.captureSingleScreen = false;
        PlayerSettings.MTRendering = false;
        //PlayerSettings.usePlayerLog = false;
        PlayerSettings.useDirect3D11 = false;

        MVRTools.Log("VR Player settings changed:");
        MVRTools.Log("- DefaultIsFullScreen = false");
        MVRTools.Log("- DisplayResolutionDialog = Disabled");
        MVRTools.Log("- RunInBackground = true");
        MVRTools.Log("- CaptureSingleScreen = false");
        //MVRTools.Log("- UsePlayerLog = false");

        string[] names = QualitySettings.names;
        int qualityLevel = QualitySettings.GetQualityLevel();

        // Disable VSync on all quality levels
        for( int i=0 ; i<names.Length ; ++i )
        {
            QualitySettings.SetQualityLevel( i );
            QualitySettings.vSyncCount = 0;
        }

        QualitySettings.SetQualityLevel( qualityLevel );

        MVRTools.Log("Quality settings changed for all quality levels:");
        MVRTools.Log("- VSyncCount = 0");
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();

        if (GUILayout.Button("Re-apply VR player settings"))
        {
            ApplyVRSettings();
        }

        if (GUILayout.Button("Pick configuration file"))
        {
            string path = EditorUtility.OpenFilePanel("Please choose MiddleVR configuration file", "C:/Program Files (x86)/MiddleVR/data/Config", "vrx");
            MVRTools.Log("[+] Picked " + path );
            mgr.ConfigFile = path;
            EditorUtility.SetDirty(mgr);
        }

        DrawDefaultInspector();
        GUILayout.EndVertical();
    }

    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) 
    {
        string renderingPlugin32Path = pathToBuiltProject.Replace(".exe","_Data/Plugins/MiddleVR_UnityRendering.dll");
        string renderingPlugin64Path = pathToBuiltProject.Replace(".exe","_Data/Plugins/MiddleVR_UnityRendering_x64.dll");

        switch( target )
        {
            case BuildTarget.StandaloneWindows :
            {
                Debug.Log( "[ ] 32-bit build : delete " + renderingPlugin64Path );
                
                // Delete x64 version
                if( System.IO.File.Exists( renderingPlugin64Path ) )
                {
                    System.IO.File.Delete( renderingPlugin64Path );
                }

                break;
            }
            case BuildTarget.StandaloneWindows64 :
            {
                Debug.Log( "[ ] 64-bit build : delete " + renderingPlugin32Path + " and rename " + renderingPlugin64Path );
                
                // Delete 32b version...
                if( System.IO.File.Exists( renderingPlugin32Path ) )
                {
                    System.IO.File.Delete( renderingPlugin32Path );
                }
                
                // ...and rename x64 version
                if( System.IO.File.Exists( renderingPlugin64Path ) )
                {
                    System.IO.File.Move( renderingPlugin64Path, renderingPlugin32Path );
                }

                break;
            }
        }

        // Copy web assets for HTML5 default GUI
        string webAssetsPathSource = Application.dataPath + System.IO.Path.DirectorySeparatorChar + "/MiddleVR/.WebAssets";
        string webAssetsPathDestination = pathToBuiltProject.Replace(".exe", "_Data/MiddleVR/.WebAssets");
        FileSystemTools.DirectoryCopy(webAssetsPathSource, webAssetsPathDestination, true, true);

        // Sign Application
        MVRTools.SignApplication( pathToBuiltProject );
    }
}

public class AdditionnalImports : AssetPostprocessor
{
    public static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        // If this is not a MiddleVR package import, skip
        bool importingMvrDll = false;
        foreach (string s in importedAssets)
        {
            // MiddleVR_Unity3D.dll should be imported only at package import
            if (s.Contains("MiddleVR_Unity3D.dll"))
            {
                importingMvrDll = true;
                break;
            }
        }

        if (!importingMvrDll)
        {
            return;
        }

        Debug.Log("[ ] Package import post process: import GUI web assets from Program Files...");

        // Copy web assets from MiddleVR installation folders

        // Find MiddleVR installation folder from Path environment variable
        string mvrDllPath = FileSystemTools.FindFileInPath("MiddleVR.dll");
        if (mvrDllPath == "")
        {
            EditorUtility.DisplayDialog("MiddleVR package import error", "MiddleVR installation folder was not found\nDid you install MiddleVR on this computer and did you restart after MiddleVR installation?\nYou should restart and re-import the MiddleVR Unity package.", "Ok");
            return;
        }

        // Replace the dll path by the menu path
        char separator = System.IO.Path.DirectorySeparatorChar;
        int lastPathSeparatorPos = mvrDllPath.LastIndexOf(separator);
        mvrDllPath = mvrDllPath.Remove(lastPathSeparatorPos);
        lastPathSeparatorPos = mvrDllPath.LastIndexOf(separator);
        mvrDllPath = mvrDllPath.Remove(lastPathSeparatorPos);

        string webAssetsPathSource = mvrDllPath + separator + "data" + separator + "GUI" + separator + "Menu";
        string webAssetsPathDestination = Application.dataPath + "/MiddleVR/.WebAssets/VRMenu";
        Debug.Log("[ ] Trying to copy folder '" + webAssetsPathSource + "' to " + webAssetsPathDestination + "'...");

        // If destination already exists, update it
        if (System.IO.Directory.Exists(webAssetsPathDestination))
        {
            Debug.Log("[ ] Destination folder '" + webAssetsPathDestination + "' already exists, updating it...");
        }

        if (System.IO.Directory.Exists(webAssetsPathSource))
        {
            FileSystemTools.DirectoryCopy(webAssetsPathSource, webAssetsPathDestination, true, true);
        }
        else
        {
            EditorUtility.DisplayDialog("MiddleVR package import error", "Web assets folder was not found:\n'" + webAssetsPathSource + "'\nYou should manually copy the content of the folder '/data/GUI/Menu' from your MiddleVR installation location to:\n'" + webAssetsPathDestination + "'", "Ok");
        }

        if (!System.IO.File.Exists(Application.dataPath + "/MiddleVR_Source_Project.txt"))
        {
            // Clean old deprecated files from previous MiddleVR versions
            string[] filesToDelete = {  "/Editor/VRCustomEditor.cs",
                                        "/Resources/OVRLensCorrectionMat.mat",
                                        "/MiddleVR/Scripts/Internal/VRCameraCB.cs",
                                        "/MiddleVR/Assets/Materials/WandRayMaterial.mat" };

            foreach (string fileToDelete in filesToDelete)
            {
                string filePath = Application.dataPath + fileToDelete;
                if (System.IO.File.Exists(filePath))
                {
                    Debug.Log("[ ] Package import post process: clean deprecated MiddleVR files. Deleting file '" + filePath + "'.");
                    System.IO.File.Delete(filePath);
                }
            }
        }

        Debug.Log("[ ] Package import post process: End.");
    }
}

public class FileSystemTools
{
    public static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs, bool overwrite)
    {
        // Get the subdirectories for the specified directory
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(sourceDirName);
        System.IO.DirectoryInfo[] dirs = dir.GetDirectories();

        if (!dir.Exists)
        {
            throw new System.IO.DirectoryNotFoundException(
                "Source directory does not exist or could not be found: "
                + sourceDirName);
        }

        // If the destination directory doesn't exist, create it
        if (!System.IO.Directory.Exists(destDirName))
        {
            System.IO.Directory.CreateDirectory(destDirName);
        }

        // Get the files in the directory and copy them to the new location
        System.IO.FileInfo[] files = dir.GetFiles();
        foreach (System.IO.FileInfo file in files)
        {
            string temppath = System.IO.Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, overwrite);
        }

        // If copying subdirectories, copy them and their contents to new location
        if (copySubDirs)
        {
            foreach (System.IO.DirectoryInfo subdir in dirs)
            {
                string temppath = System.IO.Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, copySubDirs, overwrite);
            }
        }
    }

    public static string FindFileInPath(string iFileName)
    {
        string pathFullString = System.Environment.GetEnvironmentVariable("PATH");
        if (pathFullString != null)
        {
            string[] pathValues = pathFullString.Split(';');

            foreach (string pathValue in pathValues)
            {
                string filePath = pathValue.Trim();
                filePath = System.IO.Path.Combine(filePath, iFileName);

                if (!string.IsNullOrEmpty(filePath) && System.IO.File.Exists(filePath))
                {
                    return System.IO.Path.GetFullPath(filePath);
                }
            }
        }

        return "";
    }

    public static string ProgramFilesx86()
    {
        if (8 == System.IntPtr.Size
            || (!System.String.IsNullOrEmpty(System.Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
        {
            return System.Environment.GetEnvironmentVariable("ProgramFiles(x86)");
        }

        return System.Environment.GetEnvironmentVariable("ProgramFiles");
    }
}
