# ChequeIN

![ChequeIN logo](chicken.png)

## The 10 golden rules of the Chicken
1. Do not talk about fight club
2. Create a new branch for every user story
3. Masta branch is solely used for production code
4. When an user story is done, make a pull request to `dev` branch
5. Every commit will be scrupulously validated by Lord Travis CI the Grey
6. A pull request will not be accepted unless 2 contributors approve
7. All hail to C#
8. Stay away from typescript
9. Small people are the last ones to know it's raining
10. Java is to JavaScript as ham is to hamster - Jeremy Keith

## Running the project
### Requirements
* The .NET Core framework 2.0+
* Node 8+

### Starting the backend
* Run `dotnet run` in the `backend/ChequeIN` directory. The following environment environment variable needs to be set for it to work: `ASPNETCORE_ENVIRONMENT=Development`

*Note:* if you get an unhandled exception, delete `backend/ChequeIN/chequein_db.sqlite` and run the project again.

### Starting the frontend
* Navigate to the `frontend` directory
* Make sure the dependencies are installed: `npm install`
* `npm start`

### Login with a test user
To test the app, use the following account:
```
email: user@test.com
password: ChequeIn1234
```

## Authentication
### How to protect an API call (in the backend)
Add the `[Authorize]` decorator on the method of a call.

```
// Example
[HttpGet]
[Authorize]
public IEnumerable<string> Get()
{
    return new string[] { "value1", "value2"};
}
```
### How to make an authenticated API call in the frontend
Use the ApiService located in frontend/app/api.service.ts. Add your calls to it, just like the example call and then use the service from your component.

### How to enforce authentication in development
Set `"DisableAuthentication": false` in `appsettings.Development.json`.

## Database
Now the database system uses migrations. Migrations is a way to version-control the schema of a database by keeping track of the changes (named migrations) applied to a database over time. This ensures that the database in production is always up to date because migrations are applied on deployment.
### What's up me?
### If you have a weird Exception when starting the service
* Delete the `chequein_db.sqlite` file.
* Run the project again.

### If you made changes to the data models
* Run `dotnet ef migrations add [MigrationName]` in the `backend/ChequeIn` folder. Replace `[MigrationName]` with a meaningful name for the changes you made. **No need to delete the database.**
* A file will be created in the `backend/ChequeIn/Migrations` folder. You need to commit this file along with your domain model changes.
* Run the project (this will update your `chequein_db.sqlite` to the new models).

### If you made changes to the seeding data
* Delete the `chequein_db.sqlite` file.
* Run the project (the database will be recreated with the seeding data into it).

## EditorConfig
To avoid confusing our text editors with indentation, we have an editorconfig file ! See http://editorconfig.org for more details.
