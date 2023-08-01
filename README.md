# SeoSearchApp

The SeoSearchApp is a web application that allows users to perform keyword searches on different search engines and view the search results. The application consists of a frontend built with Angular 16 and a backend built with C# .NET 6 using Entity Framework for the database.

## Getting Started

### Prerequisites

- Node.js and npm: Make sure you have Node.js (version 16 or higher) and npm installed on your machine. You can download them from the official Node.js website: [Node.js](https://nodejs.org/)

- .NET 6 SDK: Ensure that you have the .NET 6 SDK installed. You can download it from the official .NET website: [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

### Frontend Installation

1. Clone the repository:

```bash
git clone https://github.com/GianlucaMeola/SeoSearchApp.git
cd SeoSearchApp/frontend
```

2. Install the dependencies:

```bash
npm install
```

### Backend Installation

1. Open a new terminal or command prompt.

2. Navigate to the backend folder of the cloned repository:

```bash
cd SeoSearchApp/backend
```

3. Install the .NET dependencies:

```bash
dotnet restore
```

4. Install the .NET dependencies:

```bash
cd WebScrapperAPI
dotnet ef database update
```
dotnet ef database update

### Running the Application

1. Start the Angular frontend:

```bash
cd SeoSearchApp/frontend
ng serve
```

The frontend should now be running on `http://localhost:4200/`.

2. Start the .NET backend:

```bash
cd SeoSearchApp/backend
dotnet run
```

The backend server should now be running on `http://localhost:7244/`.

3. Open your web browser and navigate to `http://localhost:4200/` to access the application.

### How to Use

1. Enter the keywords and URL you want to search for in the input fields.

2. Select the search engines you want to use by clicking on the checkboxes.

3. Click the "Submit" button to perform the search.

4. The search results will be displayed in a table, showing the keywords, URL, search engine name, ranking, and timestamp of each result.

5. You can use the pagination controls at the bottom of the table to navigate through the search results.

### Additional Features

- The application includes a loading spinner that appears while the search results are being fetched from the server.

- If there are no search results or an error occurs during the search, a snackbar message will be displayed with the appropriate message.


## Technologies Used

- Angular 16: Frontend framework
- .NET 6: Backend runtime environment
- C# and Entity Framework: Backend development and database management
- SQL: Database for storing search results
- Material UI: Styling and UI components

## Contributing

If you'd like to contribute to this project, feel free to create a pull request or open an issue.

## More to Add

If I have more time I would add more search engines to the project, and the challenge would be to skip the policy page and loop through the result pages (like is already done with Bing).
I would add a service, maybe an AI to dynamically update the CSS selectors of different Search engines pages.
I would add unit and integration tests.

---
