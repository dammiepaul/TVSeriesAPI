# TVSeriesAPI
A .NET Core 5 Web API with basic swagger documentation, some TV series endpoints, and sample data seeded as part of the migrations...

The project uses EF Core 5 for data access and a library Microsoft.EntityFrameworkCore.UnitOfWork (https://github.com/mingxiaoyu/UnitOfWork) to eliminate the need for boilerplate repository and unit of work patterns code.

`Install-Package Microsoft.EntityFrameworkCore.UnitOfWork`

A prerequisite for running the API is [Docker](https://www.docker.com/).

## Ways to Run the API
1. Go to my Docker Hub page to see the steps to run the API as a container.
[The TVSeriesAPI image on Docker Hub](https://hub.docker.com/r/dammiepaul/tv-series-api)

OR

2. From Visual Studio

- Clone this repo: git clone https://github.com/dammiepaul/TVSeriesAPI.git
- Load solution in Visual Studio and rebuild
- Load Terminal using Ctrl + ` i.e. control key plus backtick
- Run command: docker-compose build
- Run Command: docker-compose up
- In your browser, navigate to the swagger page: http://localhost:8080/swagger/index.html

I recommend going the Docker Hub way, because it ensures the DB container would be fully running and ready for the API container to access. I noticed that method #2 sometimes fails on my local machine because the DB container would not have been fully spun up for the API container to start connecting to and running DB migrations...
