Welcome to my Test task from VebTech company. In this list I was talking about, how you can start this project:
1. In VebTechTestTask Web-project you need to open appsettings.json file and changed DB connection string with your data
(Host=_yourData_;Port=_yourData_;Database=_yourData_;Username=v;Password=_yourData_;) 
2. You need to open Package Manager Console (or Terminal) and write this command:
Update-Database (for package manager) or cd /path/to/project and dotnet ef database update (for terminal)
3. That's all. You configured project!!!! Launch it!! Enjoy using it!!
