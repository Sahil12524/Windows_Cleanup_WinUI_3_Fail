@echo off
color 0a
title DISM Restore Health
cls
DISM /Online /Cleanup-Image /RestoreHealth
pause