# 🛡️ DVLD - Driving & Vehicle Licensing Department System

**DVLD** is a comprehensive desktop application designed to manage the lifecycle of driving licenses, vehicle registrations, and traffic violations. Built using **C#**, **.NET Framework**, and **SQL Server**, this project follows a strict **3-Tier Architecture** to ensure scalability, maintainability, and security.



## 🚀 Key Features

### 👤 Identity & User Management
* **Person Management:** A centralized system to manage personal details (National No, Name, Birth Date, Gender, Nationality, Contact Info).
* **User Accounts:** Secure login system with **SHA2-256 Hashed Passwords** and their active status.
* **Registry-Based Credentials:** Feature to remember login credentials securely using the Windows Registry.

### 🚗 Driving License Life Cycle
* **Local Driving Licenses:** Complete workflow from application entry to vision, written, and street tests.
* **International Licenses:** Logic to issue international permits based on valid local licenses.
* **License Renewals:** Handling expiration dates and renewal fees.
* **Replacements:** Management of lost or damaged license replacements.
* **Detained Licenses:** System to detain and release licenses with fine management.

### 📝 Test Management
* **Dynamic Test Types:** Configurable fees and descriptions for Vision, Written, and Street tests.
* **Test Appointment System:** Prevents overlapping appointments and ensures prerequisites (e.g., must pass Vision before Written).
* **Retake Logic:** Automatic application creation for retake exams with extra fees.

### 🛠️ Technical Highlights (Advanced Concepts)
* **Custom Reusable Controls:** Developed specialized UI components (e.g., `ctrlPersonCard`, `ctrlScheduleTest`) to reduce code redundancy.
* **Event Logging:** Integrated with **Windows Event Viewer** to log system exceptions and critical errors.
* **Regex Validation:** Robust data validation for Emails, National IDs, and numeric inputs.
* **Transaction Safety:** Business logic ensures data integrity before committing to the database.



## 🏗️ Technical Architecture

The system is built using the **N-Tier Pattern**:
1.  **Presentation Layer (PL):** Windows Forms with advanced UI/UX controls.
2.  **Business Logic Layer (BLL):** Handles validation, calculations, and rules.
3.  **Data Access Layer (DAL):** ADO.NET for high-performance communication with SQL Server.

## 💻 Screenshots
*(Add your project screenshots here to showcase your beautiful UI)*

## 🛠️ Installation & Setup
1. Clone the repository.
2. Run the `Database_Script.sql` on your **SQL Server**.
3. Update the `App.config` file with your connection string:
   ```xml
   <connectionStrings>
      <add name="MyDbConnectionString" connectionString="Server=YOUR_SERVER;Database=DVLD;User Id=sa;Password=your_password;"/>
   </connectionStrings>
2. Build and Run the project using **Visual Studio**.

 --
### 🌟 Skills Demonstrated
1. **Object-Oriented Programming (OOP):** Deep implementation of Inheritance, Polymorphism, and Encapsulation.
2. **Database Design:** Mastery of Normalization (3NF), complex Relationships, Triggers, and Stored Procedures.
3. **Security Best Practices:** Implementation of SHA2-256 Hashing, and Windows Registry Access.
4. **WinForms Advanced UI:** Creating Custom User Controls, utilizing Event Delegates for cross-control communication, and advanced Data Binding.

---
**Developed by [Hasan Ameen Al-fahd]** *Focused on Clean Code, OOP, and Scalable Design.*
