# YattleAPIExample
Example REST API code for Yattle job vacancy project. This is written in C# .NET 10, 
at time of writing you'll need an up to date version of Visual Studio Code or Visual Studio Insiders to build & run.
Use YattleAPI.http file to test the calls. Line 18 in Program.cs - app.UseHttpsRedirection() is commented out to get this .http file 
to run in Visual Studio, this is a workaround for a VS bug, you can comment it back in and Postman etc will hit the endpoints without issue.

When you build the project the package manager will automatically update the dependancy packages