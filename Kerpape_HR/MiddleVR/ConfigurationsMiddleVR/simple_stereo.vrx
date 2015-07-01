<?xml version="1.0" ?>
<MiddleVR>
    <Kernel LogLevel="2" LogInSimulationFolder="0" EnableCrashHandler="0" Version="1.4.2.f1" />
    <DeviceManager>
        <Driver Type="vrDriverDirectInput" />
        <Wand Name="Wand0" Driver="0" Axis="0" HorizontalAxis="0" HorizontalAxisScale="1" VerticalAxis="1" VerticalAxisScale="1" Buttons="0" Button0="0" Button1="1" Button2="2" Button3="3" Button4="4" Button5="5" />
    </DeviceManager>
    <DisplayManager Fullscreen="0" WindowBorders="0" ShowMouseCursor="1" VSync="0" AntiAliasing="0" ForceHideTaskbar="0" SaveRenderTarget="0">
        <Node3D Name="CenterNode" Parent="VRRootNode" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <Node3D Name="HandNode" Tag="Hand" Parent="CenterNode" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <Node3D Name="HeadNode" Tag="Head" Parent="CenterNode" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
        <CameraStereo Name="CameraStereo0" Parent="HeadNode" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,0.000000,0.000000" VerticalFOV="60" Near="0.1" Far="1000" Screen="Screen0" ScreenDistance="1" UseViewportAspectRatio="0" AspectRatio="2" InterEyeDistance="0.065" LinkConvergence="1" />
        <Screen Name="Screen0" Parent="CameraStereo0" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,2.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" Width="2" Height="1" />
        <Viewport Name="Viewport0" Left="0" Top="0" Width="1024" Height="768" Camera="CameraStereo0" Stereo="1" StereoMode="0" CompressSideBySide="1" StereoInvertEyes="0" OculusRiftWarping="0" UseHomography="0" />
    </DisplayManager>
    <ClusterManager NVidiaSwapLock="0" DisableVSyncOnServer="1" ForceOpenGLConversion="0" BigBarrier="0" SimulateClusterLag="0" />
</MiddleVR>
