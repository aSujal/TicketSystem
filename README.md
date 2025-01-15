TicketSystem Documentation
==========================

Libraries and Tools
-------------------

*   **FlowBite**
*   **Tailwind CSS**
    *   [Tailwind Documentation](https://tailwindcss.com/docs/installation)

* * *

Migration
---------

### Connection String

Ensure the connection string is defined in `appsettings.json` and there are no errors in the code:

json

Code kopieren

`"ConnectionStrings": {   "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=TicketSystem;Trusted_Connection=True;MultipleActiveResultSets=true" }`

### Creating and Applying Migrations

1.  Create a migration file:
    
    bash
    
    Code kopieren
    
    `dotnet ef migrations add InitialCreate`
    
2.  Apply the migration:
    
    bash
    
    Code kopieren
    
    `dotnet ef database update`
    

* * *

UI Development
--------------

### Tailwind CSS

*   UI components are styled using Tailwind CSS.
*   For changes in the CSS file, update the minified version by running:
    ```bash
    npx tailwindcss -i wwwroot/app.css -o wwwroot/app.min.css --watch
    ```

### Dark Mode
*   Refer to the [FlowBite Dark Mode Guide](https://flowbite.com/docs/customize/dark-mode/).

### Tutorial
*   UI implementation is based on this [FlowBite Blazor tutorial](https://flowbite.com/docs/getting-started/blazor/).

* * *
TODO List
---------
[x] Delete Ticket: Implement ticket deletion functionality.
[x] Create Ticket without Sprint ID: Ensure tickets can be created without associating them with a sprint.
[x] Add User ID: Include the user ID when creating a ticket.
[]  Emoji Changer: Add functionality for changing the ticket emoji.
[]  Ticket Update: Implement ticket update functionality.