@echo off
title Windows Updates Cache Remove
c:
del /Q /s C:\Windows\SoftwareDistribution\Download\*.*
pushd "C:\Windows\SoftwareDistribution\Download" && (rd /s /q "C:\Windows\SoftwareDistribution\Download" 2>nul & popd)
cls
exit