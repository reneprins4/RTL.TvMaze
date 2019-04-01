# RTL.TvMaze

- .NET Core 2.1
- MSSQL Server for data storage
- EF over MS SQL for persistence storage
- Swagger for documentation
- Unit testing with NUnit
- InMemory DB for testing

For a new metadata ingester we need a service that provides the cast of all the tv shows in the
TVMaze database, so we can enrich our metadata system with this information. The TVMaze
database provides a public REST API that you can query for this data.
http://www.tvmaze.com/api

This API requires no authentication but it is rate limited, so keep that in mind

Example response:

[
{
"id": 1,
 "name": "Game of Thrones",
"cast": [
{
"id": 7,
"name": "Mike Vogel",
"birthday": "1979-07-17"
},
{
"id": 9,
"name": "Dean Norris",
"birthday": "1963-04-08"
}
]
},
{
"id": 4,
 "name": "Big Bang Theory",
"cast": [
{
"id": 6,
"name": "Michael Emerson",
"birthday": "1950-01-01"
}
]
}
]
