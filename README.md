# AttendEdge

AttendEdge is an ASP.NET MVC web console for managing attendance and related company, user, leave, and administrative records. The solution is organized into separate Domain, Infrastructure, Service, and Web Console projects.

## Features

- **Authentication** — sign-in page and authentication flow.
- **Dashboard** — landing page after entering the console.
- **User management** — list, create, edit/save user details, basic information, metadata, email-existence checks, and user document/image upload, fetch, and deletion.
- **Attendance management** — attendance listing, creation, and deletion.
- **Leave requests** — list, create, view, and delete leave requests.
- **Company management** — company records and company image upload.
- **Contacts** — view contact submissions and messages, list records, and delete records.
- **AMC management** — create, list, view, and delete AMC records.
- **Holiday management** — create, list, view, and delete holiday records.
- **Account support** — forgot-password and change-password screens.
- **Shared web UI** — Razor views, shared layout, pagination helper, and client-side validation.

> Feature descriptions are based on the controllers, domain models, services, and views present in this repository. Actual permissions and backend behavior may depend on the connected API.

## Technology stack

- C# and .NET Framework 4.8
- ASP.NET MVC with Razor views
- Visual Studio solution (`AttendEdgeWebConsole.sln`)
- Dependency injection configured through Unity
- HTTP client/service layer for communicating with the backend API
- HTML, CSS, JavaScript, and jQuery-based MVC views (where included in the UI)

## Solution structure

```text
V4/
├── AttendEdgeWebConsole.sln
├── AttendEdgeWebConsole/                 # ASP.NET MVC web application
│   ├── Controllers/                      # MVC request handling
│   ├── Views/                            # Razor UI pages
│   ├── App_Start/                        # Routes, filters, Unity setup
│   ├── Config/                           # Environment-specific app settings
│   └── Web.config
├── AttendEdgeWebConsole.Domain/          # Domain models and list/pagination models
├── AttendEdgeWebConsole.Infrastructure/  # Service interfaces, helpers, constants, exceptions
└── AttendEdgeWebConsole.Service/         # Service implementations and API communication
```

### Project responsibilities

| Project | Responsibility |
| --- | --- |
| `AttendEdgeWebConsole` | MVC controllers, Razor views, routing, authentication filter, and web configuration |
| `AttendEdgeWebConsole.Domain` | Models such as User, Attendance, LeaveRequest, Company, Contact, Holiday, and AMC |
| `AttendEdgeWebConsole.Infrastructure` | Service contracts, constants, helper methods, HTTP client abstractions, and exceptions |
| `AttendEdgeWebConsole.Service` | Implements service contracts and communicates with the configured API |

## Requirements

- Windows
- Visual Studio with **.NET desktop development** and **ASP.NET/web development** workloads
- .NET Framework 4.8 Developer Pack/Targeting Pack
- Access to the AttendEdge backend API
- Any backend database and services required by that API

## Getting started

1. Clone the repository:

   ```bash
   git clone <REPOSITORY_URL>
   ```

2. Open `AttendEdgeWebConsole.sln` in Visual Studio.
3. Restore NuGet packages if Visual Studio does not restore them automatically.
4. Confirm the backend API is running and reachable.
5. Set the `APIBaseUrl` value in the appropriate file under `AttendEdgeWebConsole/Config/`.
6. Check that `AttendEdgeWebConsole/Web.config` references the intended settings file through `appSettings configSource`.
7. Set `AttendEdgeWebConsole` as the startup project.
8. Build the solution, then run with IIS Express or the configured IIS profile.

## Environment configuration

The repository includes these API endpoint configuration files:

| File | Purpose | API base URL currently configured |
| --- | --- | --- |
| `Config/App.Dev.config` | Local development | `https://localhost:44358/api` |
| `Config/App.DevServer.config` | Development server | `http://service.attendedge.local/api` |
| `Config/App.Prod.config` | Production/deployment configuration | `https://dev-attend-edge-web-service.shastrasoft.net/api` |

Review these values with your team before running or deploying. The production-named configuration currently points to a `dev-` hostname, so verify whether that is intentional. Do not commit secrets, access tokens, passwords, or private connection strings to source control.

## Main MVC areas

| Area | Controller | Main capabilities |
| --- | --- | --- |
| Authentication | `AuthenticationController` | Sign-in |
| Dashboard | `DashboardController` | Dashboard landing page |
| Users | `UserController` | User CRUD-style actions and document handling |
| Attendance | `AttendanceController` | Attendance list, create, delete |
| Leave requests | `LeaveRequestController` | Leave request list, create, view, delete |
| Companies | `CompanyController` | Company list, create, view, delete, image upload |
| Contacts | `ContactController` | Contact list, message details, view, delete |
| AMC | `AMCController` | AMC list, create, view, delete |
| Holidays | `HolidayController` | Holiday list, create, view, delete |
| Password | `ForgotPasswordController`, `ChangePasswordController` | Password recovery and password change screens |

## Build and troubleshooting

- **API requests fail:** check that the backend API is running and that `APIBaseUrl` has the correct scheme, hostname, port, and `/api` path.
- **Local HTTPS errors:** ensure the local development certificate is trusted and the API port matches the backend launch settings.
- **Missing dependencies:** restore NuGet packages and rebuild the solution.
- **Configuration errors:** verify that the `configSource` file exists and is valid XML.
- **Build errors:** confirm the .NET Framework 4.8 Developer Pack is installed and the correct startup project is selected.

## Security and repository hygiene

Before publishing the repository to GitHub:

- Keep credentials and environment-specific secrets out of committed configuration.
- Review `Web.config` and all files under `Config/` for internal URLs or sensitive values.
- Avoid committing generated folders such as `.vs/`, `bin/`, and `obj/`.
- Confirm that uploaded user documents and images are stored securely and are not exposed unintentionally.
- Use HTTPS for deployed environments.

## License

No license file was identified in the project files reviewed. Add a `LICENSE` file if you want to define how others may use, modify, and distribute this project.
