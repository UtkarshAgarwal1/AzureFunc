# Timer-Triggered Azure Function Solution

## **Overview**
This project implements an Azure Function that fetches and processes data from an external API, filters and transforms the data, and stores it in a local SQLite database. Detailed logging and error handling are included to ensure robustness.

---

## **Features**
- **Data Fetching:** Calls the API at `https://jsonplaceholder.typicode.com/posts` every 10 minutes.
- **Data Processing:** Filters posts where `userId == 1` and converts the title to uppercase.
- **Data Storage:** Stores processed records in an SQLite database, ensuring no duplicate entries.
- **Logging and Error Handling:** Logs API requests/responses, database operations, and errors.

---

## **Prerequisites**
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Azure Functions Core Tools](https://docs.microsoft.com/en-us/azure/azure-functions/functions-run-local)
- Visual Studio or VS Code with the Azure Functions extension

---

## **Setup Instructions**

### **1. Clone the Repository**
```bash
git clone <repository-url>
cd AzureFunction_CleanSolution
```

### **2. Install Dependencies**
Ensure you have the necessary NuGet packages installed:
```bash
dotnet restore
```

### **3. Configure Local Settings**
Create a `local.settings.json` file in the project root with the following content:
```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  }
}
```

### **4. Build and Run the Azure Function Locally**
```bash
func start
```

### **5. Test the Solution**
- Verify logs in the console for successful API requests and database operations.
- Check the SQLite database `localdata.db` in the project directory to ensure data has been stored.

### **6. Inspect the Database**
Use [DB Browser for SQLite](https://sqlitebrowser.org/) or Visual Studio Code with an SQLite extension to inspect and query the data.

---

## **Code Structure**
```
AzureFunction_CleanSolution/
├── AzureFunctionApp/
│   ├── Functions/
│   │   └── FetchProcessStoreDataFunction.cs
│   ├── Models/
│   │   └── PostModel.cs
│   ├── Services/
│   │   └── DatabaseService.cs
│   ├── host.json
│   ├── local.settings.json
└── README.md
```

### **Key Components**
- `FetchProcessStoreDataFunction.cs`: Orchestrates data fetching, processing, and storage.
- `PostModel.cs`: Defines the data model for API responses.
- `DatabaseService.cs`: Handles SQLite database operations.

---

## **Design Decisions and Trade-offs**
- **SQLite as Storage:** Chosen for simplicity and local testing. Could be replaced with a cloud database for production.
- **Separation of Concerns:** Database operations are isolated in `DatabaseService.cs` for better maintainability.
- **Logging:** Comprehensive logging ensures visibility into API calls and database interactions.
- **Scalability:** Ready to integrate cloud storage and additional API endpoints.

---

## **Future Improvements**
- Implement cloud-based storage (e.g., Azure Table Storage, Cosmos DB).
- Add retry mechanisms for API failures.
- Use environment variables for sensitive configuration values.
- Optimize for concurrency and better performance.

---

## **Contact**
For questions or issues, please contact [Your Name/Email Here].

