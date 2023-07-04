# ShippingService
Shipping Service API

Pull code and run docker-compose up --build

http port is forwarded to port 8080
https port is forwarder to port 8081

--------- Docker ---------
Two containers are configured. 

- PostgreSQL DB
- .Net Core Web API

DB-Container is forwarded to port 5432 (change it if you have active postgreSQL on your PC)
App-Container is forwarded to port 8080 (http) and port 8081 (https)

--------------------------

Logic

- Every webshop needs an account. You can create one by calling api/auth/register with the following content
{
  "email": "string",
  "username": "string",
  "password": "string"
}
- Then, we can request a JWT by calling api/auth/login with following content
{
  "email": "string",
  "password": "string"
}
- The response we get contains the token
{
  "username": "string",
  "email": "string",
  "token": "string"
}
- With the token we can call following endpoints
  - api/Shipping/GetShippingCost
  - api/Shipping/GetShipmentsForClient
 
- The content for GetShippingCost needs the following items
  - transactionGuid: Unique transaction identifier
  - clientGuid: Unique client identifier
  - itemGuid: Unique item identifier for this particular webshop
 
- The GetShippingCost returns the following response
{
  "account": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "product": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "access": "string"
}
- The GetShipmentsForClient takes the clientGuid as a parameter
  - This returns all the shipments for the client provided for this particular webshop for further analysis
