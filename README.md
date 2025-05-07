# Real-Time Chat Backend (ASP.NET Core)

This is the backend part of a real-time chat application built with 
**ASP.NET Core**, using **Azure SignalR Service** for real-time messaging 
and **Azure Cognitive Services** for sentiment analysis. 
All messages are stored in **Azure SQL Database**.

## Technology Stack

[![Technology Stack](https://skillicons.dev/icons?i=dotnet,azure,rider)](https://skillicons.dev)

## Features

- Real-time messaging using Azure SignalR
- Storing Messages in Azure SQL Database
- Integration with Azure Text Analytics
- Deployed to Azure Web App

## Setup
- Clone the repository:
```bash
  git clone https://github.com/heroband/ReenbitChat-backend.git
  cd ReenbitChat-backend
```
- Update your `appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "your-azure-sql-connection-string"
  },
  "Azure": {
    "SignalR": {
      "ConnectionString": "your-signalr-connection-string"
    },
    "TextAnalytics": {
      "Endpoint": "https://your-region.api.cognitive.microsoft.com/",
      "Key": "your-text-analytics-key"
    }
  }
}
```
- Install dependencies
```bash
  dotnet restore
```
- Apply database migrations
```bash
  dotnet ef database update
```
- Run the application
```bash
  dotnet run
```

## Deployment
The [backend](https://reenbit-chat-backend-gscdgycdamguegcp.westeurope-01.azurewebsites.net/) is deployed on Azure