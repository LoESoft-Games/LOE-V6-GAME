FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :5555') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im appengine.exe
FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :5555') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im appengine.exe
FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :5555') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im appengine.exe
FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :5555') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im appengine.exe
FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :3000') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im appengine.exe
FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :3000') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im appengine.exe
FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :3000') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im appengine.exe
FOR /F "tokens=4 delims= " %%P IN ('netstat -a -n -o ^| findstr :3000') DO @ECHO TaskKill.exe /PID %%P
taskkill /f /im appengine.exe
start appengine.exe
exit