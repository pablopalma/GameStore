GameStore Catalog Management System
Welcome to the GameStore Catalog Management System repository!

About
This project is a small-scale Application built on .NET 8 and Blazor, serving as an intuitive CRUD (Create, Read, Update, Delete) interface for managing a catalog of video games. Whether you're a gaming enthusiast or a developer looking to explore Blazor applications, this project provides a practical example of how to implement such a system using modern technologies.

Features
CRUD Operations: Easily manage your video game catalog with full CRUD functionality.
Blazor: Leverage the power of Blazor to create interactive and dynamic web interfaces.
.NET 8: Built on the latest version of .NET, ensuring compatibility and performance enhancements.
MSSQL Server 2018: Utilize MSSQL Server 2018 as the backend database, offering reliability and scalability for your data storage needs.
Getting Started
To get started with the GameStore Catalog Management System, follow these steps:

Clone this repository to your local machine.
Ensure you have .NET 8 SDK installed.
Set up a MSSQL Server 2018 database for storing your video game catalog data.
Update the connection string in the project configuration to point to your database.
Build and run the project.
Start managing your video game catalog effortlessly!
Contributing
Contributions are welcome! Whether you want to fix bugs, implement new features, or improve documentation, feel free to fork this repository, make your changes, and submit a pull request. Together, we can make this project even better!

License
This project is licensed under the MIT License.

# Game Store

## Starting SQL Server
```powershell
$sa_password = "[SA PASSWORD HERE]"
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=$sa_password" -p 1433:1433 -d -v sqlvolume:/var/opt/mssql --rm --name mssql mcr.microsoft.com/mssql/server:2022-latest
```
