# Chattle ![.NET Core](https://github.com/KarolWojtasiuk/Chattle/workflows/.NET%20Core/badge.svg?branch=master) 

Real-time chat server based on MongoDB and SignalR with support of permissions.

## Usage - Library
### Installing
#### NuGet package
> dotnet add package Chattle

### Example usage
```csharp
var database = new MongoDatabase("mongodb://localhost", "ChattleDB");
var chattle = new Chattle(database);
// Now you have possibility to operate on Chattle objects 
// by using controllers avalaible on Chattle instance.

// Every object have `Create`, `Get`, `Delete` and `Set` methods.

// After first connection to database, `ROOT` account will be created
// with password `Chattle`, id `FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF`
// and global permission `Administrator`.
```
![Chattle Objects](https://i.imgur.com/Q9zSi9X.png)  

## Usage - SignalR Server
#### Work In Progress  
> In the future, there will be a Docker image. 

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

> [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1)

### Installing

Clone this repository to your computer.
> git clone https://github.com/KarolWojtasiuk/Chattle.git

Then go to the repository directory.
> cd Chattle

Finally build library.
> dotnet build Chattle

## Running the tests

Tests for this application is provided by xUnit.
.NET Core contains a Test Runner, so you don't have to download anything.

Just run this command.
> dotnet test Chattle.Tests

## Built With

* [xUnit](https://xunit.net) - Testing framework
* [MongoDB](https://www.mongodb.com) - Database
* [MongoDB.Driver](https://mongodb.github.io/mongo-csharp-driver) - Database driver
* [SignalR](http://signalr.net) - Web server framework
* [DiceBear Avatars](https://avatars.dicebear.com) - Random avatars API
