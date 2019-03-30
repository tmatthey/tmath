call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\VsDevCmd.bat"
xsd.exe /c gpx.xsd  TrackPointExtensionv1.xsd /l:CS /n:Math.Tools.TrackReaders.Gpx
del gpx.cs
rename gpx_TrackPointExtensionv1.cs gpx.cs
pause

