# DiceRollingMicroservices

Practical assignment from Nemetschek (ProductLab team) for the role of .NET Developer

A dice-rolling game implemented with a microservices architecture, using RabbitMQ as a message bus to synchronize databases

![Alt Text](assets/Microservice_Architecture.png)

## Technologies

* Microservice Architecture
* ASP.NET Core Web API
* SQL Server
* Entity Framework Core
* MongoDB
* RabbitMQ message bus with Fanout Exchange
* Docker Compose
* CQRS
* MediatR
* Automapper
* JWT Authentication
* Nginx API Gateway + Reverse Proxy + Rate Limiter (Token bucket algorithm, 10 req/1 sec)

### Docker Images: 

https://hub.docker.com/r/itplamen/operativeservice/tags <br />
https://hub.docker.com/r/itplamen/userdataservice/tags

```bash
docker pull itplamen/operativeservice:1.0.0
docker pull itplamen/userdataservice:1.0.0
```

```bash
docker-compose up -d --build

# Check the status
dicerollingmicroservices-userdataservice          Built  
dicerollingmicroservices-operativeservice         Built  
Network dicerollingmicroservices_backend          Created
Volume "dicerollingmicroservices_sqlserver_data"  Created
Volume "dicerollingmicroservices_mongodb_data"    Created
Container rabbitmq                                Healthy
Container sqlserver                               Started
Container mongodb                                 Healthy
Container userdataservice                         Started
Container operativeservice                        Started
Container apigateway                              Started
```

```bash
docker-compose down -v

# Check the status
Container apigateway                              Removed
Container userdataservice                         Removed
Container operativeservice                        Removed
Container sqlserver                               Removed
Container mongodb                                 Removed
Container rabbitmq                                Removed
Volume dicerollingmicroservices_sqlserver_data    Removed
Volume dicerollingmicroservices_mongodb_data      Removed
Network dicerollingmicroservices_backend          Removed
```

## URLs:
RabbitMQ: http://localhost:15672/ <br />
UserDataAccessService API: http://localhost/users/swagger/index.html <br />
OperativeService API: http://localhost/operations/swagger/index.html

## Examples
UserDataAccessService: http://localhost/users/api/Auth/ <br />
- `POST /Register` - Ccreates a new user. The data is stored in the SQL database. A RabbitMQ message is published to the users.notifications Fanout Exchange. MongoDB is updated asynchronously with the new user data. <br />
- `POST /Login` - Authenticates a user. Issues a JWT access token + refresh token <br />
- `POST /RefreshToken` - Invalidates the current refresh token and generates a new one <br />
- `POST /Logout` - Invalidates the current refresh token, effectively logging out the user <br />

OperativeService (JWT Bearer {access_token} authentication with the token generated during /Login): http://localhost/operations/api/Games/ <br />
- `/Create` - Creates a new game with different number of dice, rounds to be played, list of players participating and number of dice assigned to each player <br />
```
POST: http://localhost/operations/api/Games/Create Bearer {access_token}
{
  "name": "GameName",
  "dieType": "D4",
  "maxUsers": 4,
  "maxRounds": 5,
  "dicePerUser": 3
}

Response 
{
  "gameId": "68d1eabb021e564b2d93c40c"
}
```

- `/Join` - Allows another player to join the specified GameId <br />
```
POST: http://localhost/operations/api/Games/Join Bearer {access_token}
{
  "gameId": "68d1eabb021e564b2d93c40c"
}
```

- `/Play` - Rolls the dice for the specified GameId. <br />
```
POST: http://localhost/operations/api/Games/Play Bearer {access_token}
{
  "gameId": "68d1eabb021e564b2d93c40c"
}
```

OperativeService (JWT Bearer {access_token} authentication with the token generated during /Login): http://localhost/operations/api/Profile/
- `/Get` - Returns user info and played games/rounds for the logged-in player, with sorting giving higher priority to SumOfDice
```
GET http://localhost/operations/api/Profile/Get?year=2025&sort=SumOfDice&sort=Datetime&desc=true&pagenumber=2&pagesize=4
```
