# Dotnet Microservice Development Practice

This is a repository containing a Dotnet Microservice that I developed following the tutorial developed by [Les Jackson](https://youtu.be/DgVjEo3OGBI?si=_aLh0LMOW2HiRV8b)

## Architecture:
This project is comprised of two Microservices which communicate using a mix of sync and async messaging via gRPC and events on a RabbitMQ message bus deployed on the K8s cluster.
* Platform Service
   * Handles the creation and storage of "Platforms" in a SQL Server deployed in the K8s cluster and attached to a PV
   * New Platform creation events are generated and sent over the wire via the RabbitMQ Message Bus

* Command Service
   *  Handles the creation and storage of new Commands (and associated usage instructions) in an in-memory storage solution
   *  Newly created Platforms are mapped in-memory via a event listener configured on the RabbitMQ Message Bus channel
   *  On-Startup, persisted data in the SQL Server attached to Platform Service is retrieved via gRPC and seeded into the in-memory storage of Command Service


![Dotnet Microservice Practice](https://github.com/absandell/dotnet-microservice/assets/58280054/c6918fe3-3bf2-4a50-9519-048112814641)

## Requests for Testing Functionality in Local Environment
### Commands Service
---
Inbound Connection Test: `POST http://localhost:6201/api/c/platforms`

Create Command for Platform: `POST http://localhost:6201/api/c/platforms/{Platform ID}/commands`
* Request Body: `{"HowTo": "Describe command use-case", "CommandLine": "Describe command syntax"}` 

Get All Platforms: `GET http://localhost:6201/api/c/platforms`

Get all Commands for Platform: `GET http://localhost:6201/api/c/platforms/{Platform ID}/commands`

Get Command for Platform: `GET http://localhost:6201/api/c/platforms/{Platform ID}/commands/{Command ID}`

---
### Platform Service
---
Create a new platform: `POST http://localhost:5051/api/platforms`
* Request Body: `{"name": "Name of platform", "publisher": "Name of platform publisher", "cost": "Describe if platform is free, open-source, paid, etc."}`

Get all platforms: `GET http://localhost:5051/api/platforms`

Get an individual platform: `GET http://localhost:5051/api/platforms/{Platform ID}`

---

## Requests for Testing Functionality in K8s Environment
### Commands Service
---
Inbound Connection Test: `POST http://acme.com/api/c/platforms`

Create Command for Platform: `POST http://acme.com/api/c/platforms/{Platform ID}/commands`
* Request Body: `{"HowTo": "Describe command use-case", "CommandLine": "Describe command syntax"}` 

Get All Platforms: `GET http://acme.com/api/c/platforms`

Get all Commands for Platform: `GET http://acme.com/api/c/platforms/{Platform ID}/commands`

Get Command for Platform: `GET http://acme.com/api/c/platforms/{Platform ID}/commands/{Command ID}`

---
### Platform Service
---
Create a new platform: `POST http://acme.com/api/platforms`
* Request Body: `{"name": "Name of platform", "publisher": "Name of platform publisher", "cost": "Describe if platform is free, open-source, paid, etc."}`

Get all platforms: `GET http://acme.com/api/platforms`

Get an individual platform: `GET http://acme.com/api/platforms/{Platform ID}`

---


