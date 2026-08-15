# InventoryHub

A full-stack inventory/product management application built with **ASP.NET Core** (backend) and **Blazor WebAssembly** (frontend), organized as a multi-project .NET solution.

## Project Structure

```
InventoryHub/
├── ClientApp/                  # Blazor WebAssembly frontend
├── ServerApp/                  # ASP.NET Core backend API
├── Shared/                     # Shared models/DTOs used by both client and server
└── FullStackSolution.slnx      # Solution file referencing ClientApp and ServerApp
```

## Tech Stack

- **Backend (`ServerApp`)** — ASP.NET Core Web API (.NET)
- **Frontend (`ClientApp`)** — Blazor WebAssembly
- **Shared (`Shared`)** — Common models/contracts referenced by both projects, to keep client and server DTOs in sync

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (matching the version targeted by `ClientApp.csproj` / `ServerApp.csproj`)
- A code editor such as Visual Studio, VS Code, or JetBrains Rider

### Clone and build

```bash
git clone https://github.com/NinaKEAT/InventoryHub.git
cd InventoryHub
dotnet restore FullStackSolution.slnx
dotnet build FullStackSolution.slnx
```

### Run the app

**Backend:**
```bash
cd ServerApp
dotnet run
```

**Frontend (in a separate terminal):**
```bash
cd ClientApp
dotnet run
```

Once both are running, open the ClientApp URL shown in the terminal to use the app; it will call the ServerApp API in the background.

## Development Notes

This project is being developed with the assistance of GitHub Copilot, which is helping scaffold the client/server/shared project structure, generate CRUD endpoints and Blazor components, wire up client-server communication (HttpClient/DTOs), and debug build and integration issues as the app is built out.