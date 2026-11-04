# AI-Commerce Microservices

A modern, microservices-based e-commerce backend demonstrating the integration of Artificial Intelligence (LLM) with scalable architecture. Developed with .NET 10.

This project serves as a showcase of utilizing external REST APIs, implementing caching, handling relational/non-relational databases, and building a secure AI shopping assistant in a distributed system.

## 🏗️ Architecture & Technologies

* **Catalog.API (MongoDB & Redis):** Manages the product catalog. Uses MongoDB for flexible product storage and Redis for caching data to handle high traffic.
* **Payment.API (PostgreSQL & EF Core):** Simulates a secure payment gateway integration (Stripe/Iyzico logic). Uses PostgreSQL and Fluent API configurations to ensure ACID compliance for transaction records.
* **AIChat.API (Google Gemini API):** An intelligent shopping assistant built via direct REST integration (`HttpClient`) with Google Gemini (`v1beta/interactions`), interacting with the user, recommending products contextually, and driving sales.
* **Infrastructure:** Docker Compose for seamless database and cache provisioning.

## 🚀 Getting Started

### Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Must be installed and running in the background)
* A valid Google Gemini API Key (via [Google AI Studio](https://aistudio.google.com/))

### Configuration (Important)
For security reasons, `appsettings.json` files are excluded from this repository via `.gitignore`. **You must use your own Google Gemini API Key.** Any keys previously found in forks or commit histories are invalid and revoked.

#### 1. AIChat.API Configuration
1. Navigate to the `Services/AIChat.API/` directory.
2. Create a new file named `appsettings.json`.
3. Add the following configuration, replacing the placeholder with your actual Gemini API key:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Gemini": {
    "ApiKey": "AIzaSy_YOUR_OWN_API_KEY",
    "ModelId": "gemini-3.5-flash-lite"
  }
}
```

#### 2. Catalog.API & Payment.API Configuration
Ensure `appsettings.json` files are also created in the respective directories for database connections:

* **`Services/Catalog.API/appsettings.json`**:
```json
{
  "ConnectionStrings": {
    "MongoDb": "mongodb://localhost:27017",
    "Redis": "localhost:6379"
  },
  "DatabaseSettings": {
    "DatabaseName": "CatalogDb",
    "CollectionName": "Products"
  }
}
```

* **`Services/Payment.API/appsettings.json`**:
```json
{
  "ConnectionStrings": {
    "PostgreSql": "Host=localhost;Port=5432;Database=AiCommercePayment;Username=postgres;Password=postgres"
  }
}
```

### Running the System

1. Clone the repository to your local machine:
```bash
git clone https://github.com/YOUR_USERNAME/ai-commerce-microservices.git
cd ai-commerce-microservices
```

2. Open a terminal in the root directory of the project and start the required databases (MongoDB, PostgreSQL, Redis) via Docker Compose:
```bash
docker-compose up -d
```

3. Once the containers are running, start each microservice in separate terminal tabs:
```bash
dotnet run --project Services/Catalog.API/Catalog.API.csproj
dotnet run --project Services/Payment.API/Payment.API.csproj
dotnet run --project Services/AIChat.API/AIChat.API.csproj
```

## 🧪 API Endpoints Overview

Once the services are up and running, you can test the core functionalities using Postman or Thunder Client:

* **Catalog Service:** `GET /api/products` & `POST /api/products`
* **Payment Service:** `POST /api/payments/process`
* **AI Chat Service:** `POST /api/chat`
  * **Sample Request Body:**
    ```json
    {
      "message": "Spor yaparken kullanabileceğim kulaklık önerir misin?"
    }
    ```