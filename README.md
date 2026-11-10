A full-stack exercise tracking application for logging workouts and getting statistical insights into training: total volume lifted, personal records per exercise, per-day progress, and estimated one-rep-max (1RM) using the Epley Formula.

<img width="681" height="146" alt="image" src="https://github.com/user-attachments/assets/1f315435-af10-490f-a908-cd5c44acad4b" />

## Tech Stack:

Backend: C#, ASP.NET Core Web API (controller/service architecture, dependency injection, DTO validation), Entity Framework Core (code-first migrations)

Database: SQL Server 2022 running in Docker

Frontend: React, Typescript, Vite, Recharts (dashboard is still in progress)

Here are the API endpoints:

| Method | Route | Description |
| --- | --- | --- |
| GET | `/api/exercises` | List exercises |
| POST | `/api/exercises` | Create an exercise |
| GET | `/api/workout` | List workouts with their sets |
| GET | `/api/workout/{id}` | Get a single workout |
| POST | `/api/workout` | Create a workout with sets |
| PUT | `/api/workout/{id}` | Update a workout and its sets |
| DELETE | `/api/workout/{id}` | Delete a workout |
| GET | `/api/statistics` | Total workouts, total volume (kg) and personal records |
| GET | `/api/statistics/exercises/{exerciseId}` | Best set, total volume and 1RM progress for one exercise |

## Running locally:

*Prerequisites: Docker, .NET SDK and Node.js

1. Choose a password for the database, Copy '.env.example to '.env and set 'MSSQL_SA_PASSWORD ('.env' is git-ignored).
   
2. Start SQL Server: docker compose up -d

   Run 'docker ps' to check that 'gymtracker-sqlserver' is showing as up

3. Give the API the connection string using the .NET user secrets, no password is commited:

   dotnet user-secrets set "ConnectionStrings:GymTracker" "Server=localhost,1433;Database=GymTrackerDb;User Id=sa;Password=<yourpassword>;TrustServerCertificate=True;"
   --project backend/GymTracker.Api

4. Apply the migrations and start the API

   dotnet ef database update --project backend/GymTracker.Api
   dotnet run --project backend/GymTracker.Api

The API listens on 'http://localhost:5163'

5. Start the React app in a second terminal

   cd frontend
   npm install
   npm run dev

The frontend runs on 'http://localhost:5173'. A CORS policy in Program.cs allows it to call the API on port 5163.
