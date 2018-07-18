call "C:\Program Files (x86)\Microsoft Visual Studio\2017\Enterprise\Common7\Tools\VsDevCmd.bat"
cd Math
msbuild /t:pack /v:m /p:Configuration=Release
cd ..\Math.Tools.TrackReaders
msbuild /t:pack /v:m /p:Configuration=Release
cd ..
pause
