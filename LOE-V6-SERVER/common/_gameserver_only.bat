FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :2050') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im gameserver.exe
FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :2050') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im gameserver.exe
FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :2050') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im gameserver.exe
FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :2050') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im gameserver.exe
start gameserver.exe
exit