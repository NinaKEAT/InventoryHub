# InventoryHub

A full-stack .NET solution for managing inventory, composed of a client application, a server-side API, and a shared library of common models/contracts.

> **Status:** Early scaffold. The solution currently consists of the base `ClientApp`, `ServerApp`, and `Shared` projects; features and endpoints are still being built out. This README will be expanded as functionality is added.

## Solution Structure

```
InventoryHub/
├── ClientApp/                  # Front-end application (ClientApp.csproj)
├── ServerApp/                  # Back-end API (ServerApp.csproj)
├── Shared/                     # Shared models/contracts used by both Client and Server
└── FullStackSolution.slnx      # Solution file linking ClientApp and ServerApp
```

- **ClientApp** — the front-end project responsible for the user interface.
- **ServerApp** — the back-end project responsible for exposing the API and business logic.
- **Shared** — a class library holding types (e.g., models/DTOs) shared between the client and server so both sides stay in sync.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (latest version recommended)

### Clone and build

```bash
git clone https://github.com/NinaKEAT/InventoryHub.git
cd InventoryHub
dotnet restore FullStackSolution.slnx
dotnet build FullStackSolution.slnx
```

### Run the server

```bash
cd ServerApp
dotnet run
```

### Run the client

In a separate terminal:

```bash
cd ClientApp
dotnet run
```

Check each project's launch settings (`Properties/launchSettings.json`) for the exact local URLs once available.

## Roadmap

- [ ] Define inventory data model(s) in `Shared`
- [ ] Implement CRUD API endpoints in `ServerApp`
- [ ] Build inventory UI in `ClientApp`
- [ ] Add validation and error handling
- [ ] Add authentication (if required)
- [ ] Add automated tests

## Development Notes

This project is being developed with the assistance of GitHub Copilot. This section will be updated with a summary of how Copilot contributed to each part of the implementation (scaffolding, endpoint generation, debugging, validation, etc.) as development progresses.