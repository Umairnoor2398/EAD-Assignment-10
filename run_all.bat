@echo off

cd /d "D:\Semester6\EAD\Assignments\Assignment10\dotnetservice\dotnetservice"
start dotnet run

cd /d "D:\Semester6\EAD\Assignments\Assignment10\nodeservice"
start npm run dev

cd /d "D:\Semester6\EAD\Assignments\Assignment10\djangoservice"
start python manage.py runserver

cd /d "D:\Semester6\EAD\Assignments\Assignment10\dotnetapigateway\dotnetapigateway"
start dotnet run

:: Wait for the gateway project to start (adjust the delay as needed)
timeout /t 10

:: Open Swagger UI URL
start msedge.exe "https://localhost:7147/swagger/index.html"