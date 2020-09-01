### ♪♫ It's D&D... D&D Beyond  ♪♫

This is a solution to the [D&D Beyond back-end developer challenge](https://github.com/DnDBeyond/ddb-back-end-developer-challenge).


## Running

### https setup
You will need to have a certificate for running the app over https. If you don't have one already, you can generate one from the command line.

    dotnet dev-certs https -ep ~/.aspnet/https/DnDBeyond.pfx -p changeit
    dotnet dev-certs https —-trust
    dotnet user-secrets -p DnDBeyond/DnDBeyond.csproj set Kestrel:Certificates:Default:Password "changeit"

Alternatively, you can disable https :)

    ### Startup.cs
    # 105 | ...
    # 106 | app.UseHttpsRedirection(); // comment out this line
    # 107 | ...

### Docker
You will need to have Docker installed and running first.

1. Download or clone this repo.
2. Run `docker-compose up`


## Notes
If you run locally, the app will an in-memory database. If you run with Docker, it will use a postgres database.

This solution has three parts.

The first is a standard REST API. Even though this is a small app, you will still find services, repositories, and more. You can use the included Postman collection to run sample API requests against [https://localhost:8080/api](https://localhost:8080/api)

The second part is that you can find Swagger documentation at [https://localhost:8080/swagger](https://localhost:8080/swagger)

The third (bonus) part is an implementation of GraphQL. You can play around with this at [https://localhost:8080/graphiql](https://localhost:8080/graphiql). Some sample queries and mutations can be found in `DnDBeyond.sample_graphql.txt`. Please be sure to include the query variables found in `DnDBeyond.sample_graphql_variables.txt`. It can work in conjunction with the standard REST API as well.


            ,     \    /      ,
           / \    )\__/(     / \
          /   \  (_\  /_)   /   \
     ____/_____\__\@  @/___/_____\____
    |             |\../|              |
    |              \VV/               |
    |        ----------------         |
    |           Thank you !           |
    |_________________________________|
    |    /\ /      \\       \ /\    |
    |  /   V        ))       V   \  |
    |/     `       //        '     \|
    `              V                '
