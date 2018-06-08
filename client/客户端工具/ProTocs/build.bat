@echo off  
cd proto
for /f "delims=" %%a in ('dir /b ".\*.proto"') do copy "%%a" ..
cd ..
for /f "delims=" %%i in ('dir /b/a "*.proto"') do protogen -i:%%i -o:%%~ni.cs  -ns:gprotocol
del *.proto
cd cs 
del *.cs
cd ..
for /f "delims=" %%a in ('dir /b ".\*.cs"') do move "%%a" cs
pause 