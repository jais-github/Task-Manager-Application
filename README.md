# Task-Manager-Application(Backend heavy System)

## 📌 Overview

Task Manager is a web-based task tracking system built with **ASP.NET Core Web API** and **Blazor WebAssembly**. It allows users to authenticate, create and manage tasks and jobs. The backend is powered by Entity Framework Core using the **Code-First** approach, and the application emphasizes performance, modularity, and clean architecture.

---

## Tech Stack

| Layer           | Technology                         |
|----------------|-------------------------------------|
| Frontend        | Blazor WebAssembly                  |
| Backend         | ASP.NET Core Web API (.NET 7)       |
| ORM             | Entity Framework Core (Code-First)  |
| Database        | Microsoft SQL Server                |
| Authentication  | JWT Bearer Token                    |
| API Format      | REST (JSON)                         |

##  Project Structure
TaskManager(backend)
│
├──  Data
│   └── AppDbContext.cs
│
├──  Features              
│   ├── Auth
│   ├── Tasks
│   ├── Tags
│   ├── Users
│
├──  Helpers               
│   └── JwtHelper.cs
│
├──  Middleware 
│   └── ExceptionMiddleware.cs
│
├──  Validators            
│   ├── TaskRequestValidator.cs
│   
│
├── Program.cs
├── appsettings.json
├── launchSettings.json
└── 

TaskManager.UI/
├── Pages/
│   ├── Login.razor
│   ├── Register.razor
│   ├── Tasks.razor
│   ├── Tags.razor
│   └── Profile.razor
├── Services/
│   ├── AuthService.cs
│   ├── TaskService.cs
│   ├── TagService.cs
├── Models/
│   ├── AuthRequest.cs
│   ├── AuthResponse.cs
│   ├── TaskRequest.cs
│   ├── TaskResponse.cs
│   ├── Tag.cs
│   └── UserProfile.cs
├── Program.cs

##  Getting Started

### 1. Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
- Visual Studio 2022 or Visual Studio Code
- SQL Server

### 2. Running the Application

Update the connection string in `appsettings.json` inside `TaskManager`.

Then run both backend and frontend:

```bash
dotnet dev-certs https --trust

# Run Backend
cd TaskManager.API
dotnet run

# Run Frontend
cd ../TaskManager.UI
dotnet run
Ensure ports match between Blazor and API. CORS and HTTPS must be configured correctly.

 # API Endpoints
Method	  Route	                    Description
POST    /api/auth/login	            Login endpoint, returns JWT token
GET     /api/tasks	                Fetch all tasks
POST    /api/tasks	                Create new task
PUT     /api/tasks/{id}	            Update task
GET     /api/jobs/{jobId}/logs	    Get logs related to a job
POST    /api/jobs	                  Add/update job data
POST    /api/customeranalytics	    Bulk insert analytics records


Challenges Faced & How They Were Addressed
1) Challenge: Initial issues with the frontend (Blazor WebAssembly) failing to call backend APIs due to SSL and CORS errors.
Solution: Fixed incorrect API URLs, configured correct HTTPS endpoints, and resolved ERR_CONNECTION_REFUSED and ERR_SSL_PROTOCOL_ERROR by ensuring both frontend and backend projects were running correctly with consistent ports.

2) Challenge: Faced a TimeoutException when launching the WebAssembly debug proxy due to insufficient timeout (default 10s).
Solution: Resolved temporarily by disabling WebAssembly debugging. Plan to increase timeout via custom middleware or settings if needed during development.

3) Challenge: Encountered System.Net.Http.HttpRequestException: TypeError: Failed to fetch in Blazor on failed login attempts.
Solution: Implemented proper error handling on the client side and verified backend service availability to ensure robust and user-friendly error messages.

