# Payment Gateway v1
A demo payment gateway API, allowing payments to be created and retrieved. 

This project was built using ASP.NET Core 3 and utilises the following concepts:

* Containerisation with Docker and Docker Compose
* Data storage/retrieval with MongoDB and Mongo Express
* CQRS with Mediatr
* Object mapping with AutoMapper
* Authorisation via API key
* Unit testing with XUnit, Moq, AutoFixture, and FluentAssertions
* OpenAPI with Swashbuckle

## Credentials
* API key - _demo123_
* MongoDB username/password - _root/password_
* Mongo Express username/password - _root/password_

## Using the application
Before starting the application with Visual Studio, ensure that __docker-compose__ is set as the startup project.

Once the application has started, the browser will be launched at the _/swagger_ page. From here you can test the two payment gatewawy endpoints.

Note that you will need to input the API key as an authorization value (located in the top right of the page).
