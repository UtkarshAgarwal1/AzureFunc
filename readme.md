# Timer-Triggered Azure Function Solution

## Overview
This project implements an Azure Function that:
- Fetches data from an external API (https://jsonplaceholder.typicode.com/posts)
- Processes the data by filtering posts where `userId == 1` and converting the `title` field to uppercase
- Stores the processed data in a local SQLite database (ensuring no duplicate entries)
- Logs detailed information about API requests, responses, processing steps, and errors

The solution is built using .NET and follows a clean, modular structure for maintainability and scalability.

---

## Project Structure
```
AzureFunc/
├── Functions/
│   └── FetchProcessStoreDataFunction.cs   // Orchestrates API calls, data processing, and storage
├── Models/
│   └── PostModel.cs                       // C# model for API data
├── Services/
│   └── DatabaseService.cs                 // Handles SQLite database operations
├── host.json                              // Azure Functions host configuration
├── local.settings.json                    // Local environment settings (not deployed)
└── README.md                              // This file
```

---

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- Visual Studio or VS Code with the Azure Functions extension

---

## Setup Instructions

### Running Locally
1. **Clone the Repository:**
   ```bash
   git clone <repository-url>
   cd AzureFunc
   ```

2. **Restore Dependencies:**
   ```bash
   dotnet restore
   ```

3. **Configure Local Settings:** Create a `local.settings.json` file in the project root with the following content:
   ```json
   {
     "IsEncrypted": false,
     "Values": {
       "AzureWebJobsStorage": "UseDevelopmentStorage=true",
       "FUNCTIONS_WORKER_RUNTIME": "dotnet"
     }
   }
   ```

4. **Run Azurite for Local Storage Emulation:**
   - **Visual Studio/VS Code Extension:** Install the Azurite extension if not already installed.
   - **Command Line:** Run `Azurite`

5. **Run the Azure Function Locally:**
   ```bash
   func start
   ```

Monitor the console for log messages indicating that the function has executed and processed data successfully. You can also inspect the `localdata.db` SQLite database using tools like DB Browser for SQLite.

# Timer-Triggered Azure Function Solution

## Overview
This project implements an Azure Function that:
- Fetches data from an external API (https://jsonplaceholder.typicode.com/posts)
- Processes the data by filtering posts where `userId == 1` and converting the `title` field to uppercase
- Stores the processed data in a local SQLite database (ensuring no duplicate entries)
- Logs detailed information about API requests, responses, processing steps, and errors

The solution is built using .NET and follows a clean, modular structure for maintainability and scalability.

---

## Project Structure
├── AzureFunc/
│   ├── Functions/
│   │   └── FetchProcessStoreDataFunction.cs   // Orchestrates API calls, data processing, and storage
│   ├── Models/
│   │   └── PostModel.cs                         // C# model for API data
│   ├── Services/
│   │   └── DatabaseService.cs                   // Handles SQLite database operations
│   ├── host.json                                // Azure Functions host configuration
│   ├── local.settings.json                      // Local environment settings (not deployed)
└── README.md                                    // This file

---

## Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- Visual Studio or VS Code with the Azure Functions extension

---

## Setup Instructions

### Running Locally
1. **Clone the Repository:**
   ```bash
   git clone <repository-url>
   cd AzureFunc
2. Restore Dependencies:
    dotnet restore
3. Configure Local Settings: Create a local.settings.json file in the project root with the following content:
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  }
}
 4. Run Azurite for Local Storage Emulation:
    Visual Studio/VS Code Extension:
    Install the Azurite extension if not already installed.
    Command Line: Azurite
 5. Run the Azure Function Locally
 6. func start
Monitor the console for log messages indicating that the function has executed and processed data successfully. You can also inspect the localdata.db SQLite database using tools like DB Browser for SQLite.
