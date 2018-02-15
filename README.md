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
* Run `dotnet start` in the `backend/ChequeIN` directory. The following environment environment variable needs to be set for it to work: `ASPNETCORE_ENVIRONMENT=Development`

### Starting the frontend
* Navigate to the `frontend` directory
* Make sure the dependencies are installed: `npm install`
* `npm start`

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

## EditorConfig
To avoid confusing our text editors with indentation, we have an editorconfig file ! See http://editorconfig.org for more details.
