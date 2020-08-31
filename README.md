### ♪♫ It's D&D... D&D Beyond  ♪♫

This is a solution to the [D&D Beyond back-end developer challenge](https://github.com/DnDBeyond/ddb-back-end-developer-challenge).

## Running
You will need to have Docker installed and running first.

1. Download or clone this repo.
2. Run `docker build -t dndbeyond .`
3. Run `docker run -d -p 8080:80 --name dndbeyond-api dndbeyond`

## Notes
This solution has three parts and uses an in-memory database.

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
