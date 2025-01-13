# TicketSystem
 - FlowBite
 - Tailwind
	- [Tailwind](https://tailwindcss.com/docs/installation)
# Migration
 Make sure the connection string is defined in the appsettings.json file and there are no errors in the code.
 ```json
 "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TicketSystem;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
 ```
 to create a migration file
 ```bash
 dotnet ef migrations add InitialCreate
 dotnet ef database update
 ```
 # Ui
 Made using this [tutorial](https://flowbite.com/docs/getting-started/blazor/).
 To watch for changes in the css file and update the minified version
 ```bash
  npx tailwindcss -i wwwroot/app.css -o wwwroot/app.min.css --watch
 ```
 dark mode: 
 https://flowbite.com/docs/customize/dark-mode/