@echo off
set cdir=%cd%
pushd .
cd /d "C:\Program Files\Sublime Text 2\"
set file=%*
set cfile="%cdir%\%*"
if [%file%]==[] start sublime_text.exe&popd&goto :EOF
if [%file:~1,1%]==[:] (start sublime_text.exe %file%)&popd&goto :EOF

if exist %cfile% (
    start sublime_text.exe "%cdir%\%*"
) else (
    echo %cfile% doesn't exist
    popd
    goto :end
)

popd

:end
exit /B %ERRORLEVEL%s
