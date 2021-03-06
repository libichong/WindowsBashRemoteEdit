@echo off
set cdir=%cd%
pushd .
cd /d "C:\Program Files (x86)\Notepad++\"
set file=%*
set cfile="%cdir%\%*"
if [%file%]==[] start notepad++.exe&popd&goto :EOF
if [%file:~1,1%]==[:] (start notepad++.exe %file%)&popd&goto :EOF

if exist %cfile% (
    start notepad++.exe "%cdir%\%*"
) else (
    echo %cfile% doesn't exist
    popd
    goto :end
)

popd

:end
exit /B %ERRORLEVEL%s
