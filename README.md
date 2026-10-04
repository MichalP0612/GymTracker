1. To run the application firstly run start up docker then in your terminal run the following command:

  docker compose up -d
  
  check the sql server is running by typing docker ps you should see something like gymtracker-sqlserver and Up next to it

2. To start the .NET API run the following command:

  dotnet run --project backend/GymTracker.Api

  then to check that is running look out for the url like this: Now listening on: http://localhost:5163

3. Now in a separate terminal run the following to start React:

   cd into the frontend folder then run: rpm run dev

The react frontend runs on port 1573, while the .NET API runs on port 5163. However I have added a CORS policy in Program.cs which allows
both to have the same origin and be able to run. 
