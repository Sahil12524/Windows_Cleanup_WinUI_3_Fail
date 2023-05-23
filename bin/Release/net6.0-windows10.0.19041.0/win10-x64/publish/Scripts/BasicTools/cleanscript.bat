@echo off
color 0a
title PC Cleaner
c:
cls
del /Q /s C:\Windows\Prefetch\*.*
pushd "C:\Windows\Prefetch" && (rd /s /q "C:\Windows\Prefetch" 2>nul & popd)
del /Q /s C:\Windows\Temp\*.*
pushd "C:\Windows\Temp" && (rd /s /q "C:\Windows\Temp" 2>nul & popd)
del /Q /s C:\Users\%USERNAME%\AppData\Local\Temp\*.*
pushd "C:\Users\%USERNAME%\AppData\Local\Temp" && (rd /s /q "C:\Users\%USERNAME%\AppData\Local\Temp" 2>nul & popd)
pause
cls
exit