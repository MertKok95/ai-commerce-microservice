# AI-Commerce Microservices

A modern, microservices-based e-commerce backend demonstrating the integration of Artificial Intelligence (LLM) with scalable architecture. Developed with .NET 10.

This project serves as a showcase of utilizing external APIs, implementing caching, handling relational/non-relational databases, and building a secure AI shopping assistant in a distributed system.

## 🏗️ Architecture & Technologies

* **Catalog.API (MongoDB & Redis):** Manages the product catalog. Uses MongoDB for flexible product storage and Redis for caching data to handle high traffic.
* **Payment.API (PostgreSQL & EF Core):** Simulates a secure payment gateway integration (Stripe/Iyzico logic). Uses PostgreSQL to ensure ACID compliance for transaction records.
* **AIChat.API (Semantic Kernel & OpenAI):** An intelligent shopping assistant that interacts with the user, searches for products contextually, and drives sales.
* **Infrastructure:** Docker Compose for seamless database and cache provisioning.

## 🚀 Getting Started

### Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (Must be installed and running in the background)
* A valid OpenAI API Key

### Configuration (Important)
For security reasons, `appsettings.json` files are excluded from this repository. **You must use your own OpenAI API Key.** Any keys previously found in forks or commit histories are invalid and revoked.

1. Navigate to the `Services/AIChat.API/` directory.
2. Create a new file named `appsettings.json`.
3. Add the following configuration, replacing the placeholder with your actual OpenAI API key:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "OpenAI": {
    "ApiKey": "sk-YOUR_OWN_API_KEY",
    "ModelId": "gpt-4o-mini"
  }
}
```

*(Note: Ensure `appsettings.json` files are also present in `Services/Catalog.API` for MongoDB connection `mongodb://localhost:27017` and `Services/Payment.API` for PostgreSQL connection `Host=localhost;Database=AiCommercePayment;Username=postgres;Password=postgres`).*

### Running the System

1. Clone the repository to your local machine:
```bash
git clone [https://github.com/YOUR_USERNAME/ai-commerce-microservices.git](https://github.com/YOUR_USERNAME/ai-commerce-microservices.git)
cd ai-commerce-microservices
```

2. Open a terminal in the root directory of the project and start the required databases (MongoDB, PostgreSQL, Redis) via Docker Compose:
```bash
docker-compose up -d
```

3. Once the containers are running, start each microservice:
```bash
dotnet run --project Services/Catalog.API/Catalog.API.csproj
dotnet run --project Services/Payment.API/Payment.API.csproj
dotnet run --project Services/AIChat.API/AIChat.API.csproj
```