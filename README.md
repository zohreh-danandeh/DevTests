## Senior Developer Excerise
Introduction
You are working for ABC Inc. on their new scheduling system. To help their clients save time ABC Inc. wants to build a new feature to schedule employees into available shifts automatically.

The product manager has identified that the new feature must account for each employee's stated availability and only schedule employees on the days they are available to work. Furthermore, the scheduling system should fairly schedule shifts for each employee so that one doesn't end up with all the shifts.

### Instructions
Write an API that can perform the following scenarios (order of operation should not matter):
1. Ingest the included employee.json files and store the data in an appropriately designed SQL table called employee.
2. Ingest the included shifts json files (one at a time) and stores the data in an appropriately designed SQL table called shifts.
3. provide an endpoint that creates a schedule based on the data stored in the employee and shifts tables and stores it in a new table called schedule.
4. Provide an endpoint that returns back the schedule

> NOTE : the "create schedule" and "schedule" calls should be able to be called multiple times without negative affects

### Guidelines
* This is a take home test that should take you approximatley 2hrs (or less)
* The use of Entity Framework is not permitted (ADO.Net please)
* The test should be written in C# and .Net Core
* The response form the API should be in JSON format

### Submission
In order to submit your results the create a private fork of this repo and share it with the hiring manager. For the Database work please provide the appropriate scripts to create the DB tables for testing. 
