# 🚗 DVLD - Driver & Vehicle License Department System

## 📌 Overview
DVLD is a Desktop Management System developed using C# and Windows Forms.

The system simulates a Driver and Vehicle License Department (DVLD) and manages the full lifecycle of driving license applications, drivers, tests, and license issuance.

The application uses ADO.NET for database connectivity and SQL Server (SSMS) as the backend database.

---

## 🛠 Technologies Used
- C#
- Windows Forms
- ADO.NET
- SQL Server (SSMS)
- .NET Framework

---

## 🗄 Database Design
The database is fully relational and normalized.

Main Entities:
- People
- Drivers
- Users
- Applications
- Application Types
- License Classes
- Licenses
- Tests
- Test Appointments
- Detained Licenses
- Countries

Primary Keys and Forei…
[20:28, 23/02/2026] Hassan: # 🚗 DVLD - Driver & Vehicle License Department System

## 📌 Overview
DVLD is a Desktop Application developed using C# and Windows Forms.

The system simulates a Driver and Vehicle License Department and manages:
- Driving license applications
- Drivers
- Tests & appointments
- License issuance & renewal
- Detained licenses

The project follows a professional 3-Tier Architecture to ensure separation of concerns, maintainability, and scalability.

---

## 🛠 Technologies Used
- C#
- Windows Forms
- ADO.NET
- SQL Server (SSMS)
- .NET Framework

---

## 🏗 Architecture (3-Tier)

### 🖥 1️⃣ Presentation Layer (PL)
- Windows Forms UI
- Handles user interaction
- Displays data and receives input
- Communicates only with Business Layer

### 🧠 2️⃣ Business Logic Layer (BLL)
- Contains business rules
- Validates data
- Controls application workflow
- Decides when and how data should be saved or retrieved

### 💾 3️⃣ Data Access Layer (DAL)
- Handles database communication
- Uses ADO.NET (SqlConnection, SqlCommand, SqlDataReader)
- Executes stored procedures / queries
- Isolated from UI

This separation improves:
- Code maintainability
- Testability
- Scalability
- Clean architecture principles

---

## 🗄 Database Features
- Fully relational database
- Primary & Foreign Keys
- Data integrity enforcement
- Business rule constraints
- License-test-application lifecycle tracking

---

## ✨ Core Functionalities
- People & Drivers Management
- License Applications
- Test Scheduling & Results
- License Issuance / Renewal / Replacement
- Detained License Management
- User Management System

---

## 🔐 Business Rules Implemented
- One driver per person
- Tests required before issuing license
- One active license per class
- Detain and release workflow
- Application required before license issuance

---

## 🚀 Future Enhancements
- Role-based authentication
- Reports & statistics
- Export to PDF / Excel
- Dashboard
- Logging system

---

## 👨‍💻 Author
Developed by Hassan Koubali
