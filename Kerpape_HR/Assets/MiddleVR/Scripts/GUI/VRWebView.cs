/* VRWebView
 * MiddleVR
 * (c) MiddleVR
 */

// UNITY_PRO_LICENSE exists starting from Unity 4.5
// For Unity 4.2 and 4.3 we always assume it's a Pro edition
#if !(UNITY_PRO_LICENSE || UNITY_4_2 || UNITY_4_3)
#define VRWEBVIEW_UNITY_FREE
#endif

using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public class VRWebView : MonoBehaviour {

    // Public attributes
    public int m_Width  = 1024;
    public int m_Height = 768;
    public string m_URL = "http://www.middlevr.com/";
    public float m_Zoom = 1.0f;

    // View management
    private vrWebView m_WebView = null;
    private vrImage m_Image     = null;
    private Texture2D m_Texture = null;

    // Virtual mouse management
    private List<Camera> m_Cameras = null;
    private Vector2      m_VirtualMousePosition;
    private bool         m_MouseButtonState = false;
    private bool         m_IgnorePhysicalMouseInput = false;

    // Interaction
    private bool m_WandRayWasVisible = true;
    private static byte ALPHA_LIMIT = 50;

#if VRWEBVIEW_UNITY_FREE
    // Unity Free texture management
    private Color32[] m_Pixels;
    private GCHandle m_PixelsHandle;
#endif

    public vrImage image
    {
        get { return m_Image; }
    }

    public vrWebView webView
    {
        get { return m_WebView; }
    }

    protected void SetVirtualMouseButtonPressed()
    {
        if (m_WebView != null)
        {
            m_WebView.SendMouseButtonPressed((int)m_VirtualMousePosition.x, (int)m_VirtualMousePosition.y);
        }
    }

    protected void SetVirtualMouseButtonReleased()
    {
        if (m_WebView != null)
        {
            m_WebView.SendMouseButtonReleased((int)m_VirtualMousePosition.x, (int)m_VirtualMousePosition.y);
        }
    }

    // pos: texture coordinate of raycast hit
    protected void SetVirtualMousePosition(Vector2 pos)
    {
        if (m_WebView != null)
        {
            m_VirtualMousePosition = new Vector2(pos.x * m_Texture.width, (float)m_WebView.GetHeight() - (pos.y * m_Texture.height));
            m_WebView.SendMouseMove((int)m_VirtualMousePosition.x, (int)m_VirtualMousePosition.y);
        }
    }

    public void IgnorePhysicalMouseInput()
    {
        m_IgnorePhysicalMouseInput = true;
    }

    public bool IsPixelEmpty(Vector2 iTextureCoord)
    {
        byte alpha = image.GetAlphaAtPoint((int)(iTextureCoord.x * m_Width), (int)(iTextureCoord.y * m_Height));
        return alpha < ALPHA_LIMIT;
    }

    protected void Start ()
    {
        // Check if we are running MiddleVR
        if(MiddleVR.VRKernel == null)
        {
            Debug.Log("[X] VRManager is missing from the scene !");
            enabled = false;
            return;
        }

        m_VirtualMousePosition = new Vector2(0, 0);

        if (Application.isEditor)
        {
            // Get the vrCameras corresponding Cameras
            m_Cameras = new List<Camera>();

            uint camerasNb = MiddleVR.VRDisplayMgr.GetCamerasNb();
            for (uint i = 0; i < camerasNb; ++i)
            {
                vrCamera vrcamera = MiddleVR.VRDisplayMgr.GetCameraByIndex(i);
                GameObject cameraObj = GameObject.Find(vrcamera.GetName());
                Camera camera = cameraObj.GetComponent<Camera>();
                if (camera != null)
                {
                    m_Cameras.Add(camera);
                }
            }
        }

        m_Texture = new Texture2D(m_Width, m_Height, TextureFormat.ARGB32, false);
        m_Texture.wrapMode = TextureWrapMode.Clamp;

        // Create vrImage and Texture2D
#if VRWEBVIEW_UNITY_FREE
        // Texture2D.SetPixels takes RGBA.
        m_Image = new vrImage("", (uint)m_Width, (uint)m_Height, VRImagePixelFormat.VRImagePixelFormat_RGBA);
        m_Pixels = m_Texture.GetPixels32 (0);
        m_PixelsHandle = GCHandle.Alloc(m_Pixels, GCHandleType.Pinned);
#else
        // OpenGL and Direct3D 9: Memory order for texture upload is BGRA (little-endian representation of ARGB32)
        // Direct3D 11: Unity seems to ignore TextureFormat.ARGB32 and always creates an RGBA texture.
        // We let vrImage do the pixel format conversion because this operation is done in another thread.
        if (SystemInfo.graphicsDeviceVersion.Contains("Direct3D 11"))
        {
            m_Image = new vrImage("", (uint)m_Width, (uint)m_Height, VRImagePixelFormat.VRImagePixelFormat_RGBA);
        }
        else
        {
            m_Image = new vrImage("", (uint)m_Width, (uint)m_Height, VRImagePixelFormat.VRImagePixelFormat_BGRA);
        }
#endif

        // Fill texture
        Color32[] colors = new Color32[(m_Width * m_Height)];
        
        for (int i = 0; i < (m_Width * m_Height); i++)
        {
            colors[i].r = 0;
            colors[i].g = 0;
            colors[i].b = 0;
            colors[i].a = 0;
        }
        
        m_Texture.SetPixels32(colors);
        m_Texture.Apply(false, false);

        // Attach texture
        if (gameObject != null && gameObject.GetComponent<GUITexture>() == null && gameObject.GetComponent<Renderer>() != null)
        {
            var renderer = gameObject.GetComponent<Renderer>();

            // Assign the material/shader to the object attached
            renderer.material = Resources.Load("VRWebViewMaterial", typeof(Material)) as Material;
            renderer.material.mainTexture = this.m_Texture;
        }
        else if (gameObject != null && gameObject.GetComponent<GUITexture>() != null )
        {
            gameObject.GetComponent<GUITexture>().texture = this.m_Texture;
        }
        else
        {
            MiddleVR.VRLog(2, "VRWebView must be attached to a GameObject with a renderer or a GUITexture !");
            enabled = false;
            return;
        }
        
        // Handle Cluster
        if ( MiddleVR.VRClusterMgr.IsServer() && ! MiddleVR.VRKernel.GetEditorMode() )
        {
            MiddleVR.VRClusterMgr.AddSynchronizedObject( m_Image );
        }
        
        if( ! MiddleVR.VRClusterMgr.IsClient() )
        {
            m_WebView = new vrWebView("", GetAbsoluteURL( m_URL ) , (uint)m_Width, (uint)m_Height, m_Image );
            m_WebView.SetZoom( m_Zoom );
        }
    }

#if !VRWEBVIEW_UNITY_FREE
    [DllImport("MiddleVR_UnityRendering")]
    private static extern void MiddleVR_CopyBufferToUnityNativeTexture(IntPtr iBuffer, IntPtr iNativeTexturePtr, uint iWidth, uint iHeight);
#endif

    protected void Update ()
    {
        // Handle mouse input
        if (!MiddleVR.VRClusterMgr.IsClient())
        {
            if (!m_IgnorePhysicalMouseInput)
            {
                Vector2 mouseHit = new Vector2(0, 0);
                bool hasMouseHit = false;

                if (gameObject.GetComponent<GUITexture>() != null)
                {
                    // GUITexture mouse input
                    Rect r = gameObject.GetComponent<GUITexture>().GetScreenRect();
                
                    if( Input.mousePosition.x >= r.x && Input.mousePosition.x < (r.x + r.width) &&
                        Input.mousePosition.y >= r.y && Input.mousePosition.y < (r.y + r.height) )
                    {

                        float x = (Input.mousePosition.x - r.x) / r.width;
                        float y = (Input.mousePosition.y - r.y) / r.height;
                    
                        mouseHit = new Vector2(x, y);
                        hasMouseHit = true;
                    }
                }
                else if( gameObject.GetComponent<Renderer>() != null && Application.isEditor )
                {
                    // 3D object mouse input
                    mouseHit = GetClosestMouseHit();

                    if (mouseHit.x != -1 && mouseHit.y != -1)
                    {
                        hasMouseHit = true;
                    }
                }

                if (hasMouseHit)
                {
                    bool newMouseButtonState = Input.GetMouseButton(0);

                    if (m_MouseButtonState == false && newMouseButtonState == true)
                    {
                        SetVirtualMousePosition(mouseHit);
                        SetVirtualMouseButtonPressed();
                    }
                    else if (m_MouseButtonState == true && newMouseButtonState == false)
                    {
                        SetVirtualMouseButtonReleased();
                        SetVirtualMousePosition(mouseHit);
                    }
                    else
                    {
                        SetVirtualMousePosition(mouseHit);
                    }

                    m_MouseButtonState = newMouseButtonState;
                }
            }

            m_IgnorePhysicalMouseInput = false;
        }

        // Handle texture update
        if ( m_Image.HasChanged() )
        {
            using (vrImageFormat format = m_Image.GetImageFormat())
            {
                if ((uint)m_Texture.width != format.GetWidth() || (uint)m_Texture.height != format.GetHeight())
                {
#if VRWEBVIEW_UNITY_FREE
                    m_PixelsHandle.Free();
#endif
                    m_Texture.Resize((int)format.GetWidth(), (int)format.GetHeight());
                    m_Texture.Apply(false, false);
#if VRWEBVIEW_UNITY_FREE
                    m_PixelsHandle.Free();
                    m_Pixels = m_Texture.GetPixels32 (0);
                    m_PixelsHandle = GCHandle.Alloc(m_Pixels, GCHandleType.Pinned);
#endif
                }

                if (format.GetWidth() > 0 && format.GetHeight() > 0)
                {
#if VRWEBVIEW_UNITY_FREE
                    m_Image.GetReadBufferData( m_PixelsHandle.AddrOfPinnedObject() );
                    m_Texture.SetPixels32(m_Pixels, 0);
                    m_Texture.Apply(false, false);
#else
                    MiddleVR_CopyBufferToUnityNativeTexture(m_Image.GetReadBuffer(), m_Texture.GetNativeTexturePtr(), format.GetWidth(), format.GetHeight());
#endif
                }
            }
        }
    }

    protected void OnDestroy ()
    {
#if VRWEBVIEW_UNITY_FREE
        m_PixelsHandle.Free();
#endif
    }

    private Vector2 GetClosestMouseHit()
    {
        foreach( Camera camera in m_Cameras )
        {
            RaycastHit[] hits = Physics.RaycastAll( camera.ScreenPointToRay(Input.mousePosition));

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    return hit.textureCoord;
                }
            }
        }

        return new Vector2(-1,-1);
    }

    private string GetAbsoluteURL( string iUrl )
    {
        string url = iUrl;
        
        // If url does not start with http/https we assume it's a file
        if( !url.StartsWith( "http://" ) && !url.StartsWith( "https://" ) )
        {
            if( url.StartsWith( "file://" ) )
            {
                url = url.Substring(7, url.Length-7 );
                
                if( Application.platform == RuntimePlatform.WindowsPlayer && url.StartsWith( "/" ) )
                {
                    url = url.Substring(1, url.Length-1);
                }
            }
            
            if( ! System.IO.Path.IsPathRooted( url ) )
            {
                url = Application.dataPath + System.IO.Path.DirectorySeparatorChar + url;
            }
            
            if( Application.platform == RuntimePlatform.WindowsPlayer )
            {
                url = "/" + url;
            }
            
            url = "file://" + url;
        }
        
        return url;
    }

    protected void OnMVRWandEnter(VRSelection iSelection)
    {
        // Force show ray and save state
        m_WandRayWasVisible = iSelection.SourceWand.IsRayVisible();
        iSelection.SourceWand.ShowRay(true);
    }

    protected void OnMVRWandHover(VRSelection iSelection)
    {
        SetVirtualMousePosition(iSelection.TextureCoordinate);
    }

    protected void OnMVRWandButtonPressed(VRSelection iSelection)
    {
        SetVirtualMousePosition(iSelection.TextureCoordinate);
        SetVirtualMouseButtonPressed();
    }

    protected void OnMVRWandButtonReleased(VRSelection iSelection)
    {
        SetVirtualMouseButtonReleased();
        SetVirtualMousePosition(iSelection.TextureCoordinate);
    }

    protected void OnMVRWandExit(VRSelection iSelection)
    {
        // Unforce show ray
        iSelection.SourceWand.ShowRay(m_WandRayWasVisible);
    }

    protected void OnMVRTouchEnd(VRTouch iTouch)
    {
        SetVirtualMouseButtonPressed();
        SetVirtualMouseButtonReleased();
    }

    protected void OnMVRTouchMoved(VRTouch iTouch)
    {
        Vector3 fwd = -transform.TransformDirection(Vector3.up);
        Ray ray = new Ray(iTouch.TouchObject.transform.position, fwd);

        var collider = GetComponent<Collider>();

        RaycastHit hit;
        if (collider != null && collider.Raycast(ray, out hit, 1.0F))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Vector2 mouseCursor = hit.textureCoord;
                SetVirtualMousePosition(mouseCursor);
            }
        }
    }

    protected void OnMVRTouchBegin(VRTouch iTouch)
    {
        Vector3 fwd = -transform.TransformDirection(Vector3.up);
        Ray ray = new Ray(iTouch.TouchObject.transform.position, fwd);

        var collider = GetComponent<Collider>();

        RaycastHit hit;
        if (collider != null && collider.Raycast(ray, out hit, 1.0F))
        {
            if (hit.collider.gameObject == gameObject)
            {
                Vector2 mouseCursor = hit.textureCoord;
                SetVirtualMousePosition(mouseCursor);
            }
        }
    }
}
