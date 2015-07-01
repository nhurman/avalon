<?xml version="1.0" encoding="UTF-8"?>
<MiddleVR>
	<Kernel LogLevel="2" LogInSimulationFolder="0" EnableCrashHandler="0" Version="1.6.1.b3" />
	<DeviceManager>
		<Driver Type="vrDriverDirectInput" />
		<Driver Type="vrDriverSpaceMouse" TrackerTranslationSpeed="1" TrackerRotationSpeed="1" TrackerMovementsInLocalSpace="1" />
		<Driver Type="vrDriverOculusRift" />
		<Wand Name="Wand0" Driver="0" Axis="SpaceMouse.Axis" HorizontalAxis="0" HorizontalAxisScale="-0.001" VerticalAxis="1" VerticalAxisScale="-0.001" Buttons="Mouse.Buttons" Button0="0" Button1="1" Button2="2" Button3="3" Button4="4" Button5="5" />
	</DeviceManager>
	<DisplayManager Fullscreen="1" AlwaysOnTop="1" WindowBorders="1" ShowMouseCursor="1" VSync="0" GraphicsRenderer="1" AntiAliasing="0" ForceHideTaskbar="0" SaveRenderTarget="0" ChangeWorldScale="0" WorldScale="1">
		<Node3D Name="VRSystemCenterNode" Tag="VRSystemCenter" Parent="None" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
		<Node3D Name="HeadNode" Tag="Head" Parent="VRSystemCenterNode" Tracker="OculusRift.Tracker" IsFiltered="0" Filter="0" PositionLocal="0.000000,0.000000,1.200000" UseTrackerX="0" UseTrackerY="0" UseTrackerZ="0" UseTrackerYaw="1" UseTrackerPitch="1" UseTrackerRoll="1" />
		<Camera Name="Camera0" Parent="HeadNode" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,0.000000,0.000000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" VerticalFOV="60" Near="0.1" Far="1000" Screen="0" ScreenDistance="1" UseViewportAspectRatio="1" AspectRatio="1.33333" />
		<Node3D Name="WandNode" Parent="VRSystemCenterNode" Tracker="0" IsFiltered="0" Filter="0" PositionLocal="0.000000,-0.700000,1.200000" OrientationLocal="0.000000,0.000000,0.000000,1.000000" />
		<Viewport Name="Viewport0" Left="0" Top="0" Width="1280" Height="800" Camera="Camera0" Stereo="0" StereoMode="3" CompressSideBySide="0" StereoInvertEyes="0" OculusRiftWarping="0" UseHomography="0" />
	</DisplayManager>
	<ClusterManager NVidiaSwapLock="0" DisableVSyncOnServer="1" ForceOpenGLConversion="0" BigBarrier="0" SimulateClusterLag="0" MultiGPUEnabled="0" ImageDistributionMaxPacketSize="8000" />
</MiddleVR>
