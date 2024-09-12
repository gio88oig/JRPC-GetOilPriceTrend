# A web application that exposes a JSON RPC API for obtaining the historical oil price trend, built with .NET 8

## Technologies used

- .NET 8
- ASP.NET Core
- JsonRpc.Router (https://github.com/edjCase/JsonRpc)
- XUnit
- Moq

## Project Structure

    .
    ├── WebApplication_GetOilPriceTrend.API                   # Compiled files (alternatively `dist`)
    ├── docs                    # Documentation files (alternatively `doc`)
    ├── src                     # Source files (alternatively `lib` or `app`)
    ├── test                    # Automated tests (alternatively `spec` or `tests`)
    ├── tools                   # Tools and utilities
    ├── LICENSE
    └── README.md

## Usage with Docker

#### Prerequisites

- Docket Desktop
- Postman (or any other API testing tool)

After cloning the repository, navigate to the root directory of the project and run the following command to build the Docker image:
`docker build . -t getoilapi`

Then run the image running the following command:
`docker run -p 5000:8080 -t getoilapi`

Call the API using Postman or any other API testing tool in this way:
- Endpoint: `http://localhost:5000`
- Method: `POST`
- Headers: `Content-Type: application/json`
- Body: 
```json
{
	"jsonrpc": "2.0",
	"method": "GetOilPriceTrend",
	"params": {
		"startDateISO8601": "2020-01-01",
		"endDateISO8601": "2020-01-05"
	},
	"id": 1
}
```

## Usage with dotnet

#### Prerequisites

- .NET 8
- dotnet CLI
- Postman (or any other API testing tool)

After cloning the repository, navigate to the root directory of the project and run the following command to build the project:
`dotnet run --project WebApplication_GetOilPriceTrend.API`

Call the API using Postman or any other API testing tool in this way:
- Endpoint: `http://localhost:5245`
- Method: `POST`
- Headers: `Content-Type: application/json`
- Body: 
```json
{
	"jsonrpc": "2.0",
	"method": "GetOilPriceTrend",
	"params": {
		"startDateISO8601": "2020-01-01",
		"endDateISO8601": "2020-01-05"
	},
	"id": 1
}
```

#### Run Tests

Navigate to the root directory of the project and run the following command to run the tests:
`dotnet test`