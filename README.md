# 📌 Job Tracker API (.NET)

A modern, secure and scalable **Job Application Tracker API** built with **ASP.NET Core Web API**.

This backend powers a full-stack job tracking application with authentication, filtering, and user-specific data management.

--- 

## 🚀 Overview 

This API allows users to:

- Register and authenticate using JWT
- Manage their job applications (CRUD)
- Filter and search jobs efficiently
- Track application status (Applied, Interview, Offer, Rejected)
- Access only their own data (secure, user-based isolation)

--- 

## ✨ Features

- 🔐 JWT Authentication (Login / Register)
- 🔑 Password hashing with BCrypt
- 🧾 Full CRUD operations for jobs
- 👤 User-based data isolation (each user sees only their jobs)
- 🔍 Search functionality (company / position)
- 🏷 Status filtering (Applied, Interview, Rejected, Offer)
- 📊 Ready for dashboard statistics (status counts)
- 📅 Job creation timestamp (CreatedAt)
- 📘 Swagger API documentation with JWT support

--- 

## 🛠 Tech Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- SQL Server (Docker)
- JWT Authentication
- BCrypt (Password Hashing)
- Swagger / OpenAPI

--- 

## Tools
- Git & GitHub
- Azure (Backend Deployment)

## ⚙️ Getting Started

Follow the steps below to run the project locally:


```bash
# Clone the repository
git clone https://github.com/TheDutchman68/job-tracker-dotnet

# Navigate to the project folder
cd job-tracker-dotnet

# Install dependencies
dotnet restore

# Configure environment
In appsettings.json:

"Jwt": { "Key": "your_super_secret_key" }

# Setup database
Make sure SQL Server is running (Docker recommended).

Example using Docker:

docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword123!" \
-p 1433:1433 --name sqlserver -d mcr.microsoft.com/mssql/server:2022-latest

Then run:

dotnet ef database update



# Run the API
dotnet run

API will be available at:
http://localhost:5109

Swagger:
http://localhost:5109/swagger
```
---

## 🔐 Authentication

This API uses JWT Bearer Authentication.

### Flow:

1. Register user → /api/auth/register
2. Login → /api/auth/login
3. Copy token
4. Use it in Swagger or frontend:

Authorization: YOUR_TOKEN

---

## 📡 API Endpoints

### Auth

- POST /api/auth/register
- POST /api/auth/login

### Jobs

- GET /api/jobs
- GET /api/jobs?status=Applied
- GET /api/jobs?search=google
- POST /api/jobs
- PUT /api/jobs/{id}
- DELETE /api/jobs/{id}

---

## 🧠 What I Learned

- Building RESTful APIs with ASP.NET Core
- Implementing JWT authentication and authorization
- Securing user data with hashing (BCrypt)
- Designing scalable backend architecture
- Working with Entity Framework Core and migrations
- Structuring a real-world backend project
- Implementing filtering and search at API level

---

## 🔮 Possible Improvements

- Role-based authorization (Admin/User)
- Pagination for large datasets
- Advanced filtering & sorting
- Refresh tokens
- Logging & monitoring

---

## 👤 Author

Natanael Dobie
Frontend Developer -> Full-Stack Developer

- GitHub: https://github.com/TheDutchman68
- LinkedIn: www.linkedin.com/in/natanael-dobie-776059249
- Portfolio: https://portfolio-react-nu-taupe.vercel.app

---

## 📄 License

This project is licensed under the MIT License.
