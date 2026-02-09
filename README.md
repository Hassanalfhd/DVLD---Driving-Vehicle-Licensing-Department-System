# 🛡️ DVLD - Driving & Vehicle Licensing Department System

**DVLD** is a high-performance desktop application designed to manage the entire lifecycle of driving licenses, vehicle registrations, and traffic violations. Built using **C#**, **.NET Framework**, and **SQL Server**, this project follows a strict **3-Tier Architecture** to ensure industrial-grade scalability, maintainability, and security.

---

## 🚀 Key Features

### 👤 Identity & User Management
* **Person Management:** Centralized system to manage personal details (National No, Name, Contact Info).
* **Security:** Secure login system with **SHA2-256 Hashed Passwords**.
* **Registry Integration:** Securely remember login credentials using the **Windows Registry**.

### 🚗 Driving License Life Cycle
* **Full Workflow:** Comprehensive management of Local and International licenses from application to issuance.
* **Services:** Automated handling of **Renewals**, **Replacements** (Lost/Damaged), and **Detained Licenses**.
* **Fine Management:** Integrated system for detaining and releasing licenses with automatic fine calculation.

### 📝 Dynamic Test Management
* **Configurable Tests:** Dynamic management for Vision, Written, and Street tests.
* **Smart Scheduling:** Prevents overlapping appointments and ensures all prerequisites are met.

---

## 🏗️ Project Architecture & Database Design

The system is built on a clean **N-Tier Architecture** to separate concerns:

1.  **📂 Presentation Layer (PL):** Built with Windows Forms and **Custom Reusable User Controls** to ensure UI consistency.
2.  **📂 Business Logic Layer (BLL):** Manages validation rules and complex business calculations.
3.  **📂 Data Access Layer (DAL):** * **Stored Procedures:** All CRUD operations are executed via optimized Stored Procedures for maximum security and performance.
    * **Database Views:** Complex data retrieval is handled through SQL Views to simplify data binding and improve speed.

---

 Database Diagram  

 ![Database Diagram](Database/Database_Diagram.png) 

## 📸 Screenshots

### 🔑 Authentication & Home
| Login Screen | Dashboard (Home) | Account Settings |
| :---: | :---: | :---: |
| ![Login](Screenshots/Login.png) | ![Home](Screenshots/Home.png) | ![AccountSettings](Screenshots/AccountSettingsMenu.png) |

### 👥 People & User Management
| Manage People | Add New Person | Manage Users |
| :---: | :---: | :---: |
| ![ManagePeople](screenshots/ManagePeople.png) | ![AddNewPerson](screenshots/AddNewPerson.png) | ![ManageUsers](screenshots/ManageUsers.png) |

| Add User (1) | Add User (2) | Profile View |
| :---: | :---: | :---: |
| ![AddUser1](screenshots/AddNewUsre1.png) | ![AddUser2](screenshots/AddNewUsre2.png) | ![Profile](screenshots/Profile.png) |

### 📜 Licensing Services
| Applications Menu | Local Applications | International Applications |
| :---: | :---: | :---: |
| ![AppsMenu](screenshots/ApplictiomsMenu.png) | ![LocalList](screenshots/LocalLicensesApplictionsList.png) | ![IntList](screenshots/InternationalLicensesApplictionsList.png) |

| New Local License | New International License | Renew License |
| :---: | :---: | :---: |
| ![NewLocal](screenshots/AddNewLocalDrivingLicensesAppliction.png) | ![NewInt](screenshots/AddInternationalLicensesApplictioon.png) | ![Renew](screenshots/RenewLicensesAppliction.png) |

### 🚫 Detention & Enforcement
| Detain Menu | Detain License | Release License |
| :---: | :---: | :---: |
| ![DetainMenu](screenshots/DetainLicensesMenu.png) | ![Detain](screenshots/DetainLicenses.png) | ![Release](screenshots/ReleaseDetainedLicenses.png) |

### 🛠️ Architecture Diagrams
| PL Structure | BLL Structure | DAL Structure |
| :---: | :---: | :---: |
| ![PL](screenshots/DVDL_PL_Structure.png) | ![BLL](screenshots/DVDL_BL_Structure.png.png) | ![DAL](screenshots/DVDL_DAL_Structure.png.png) |

---

## 🛠️ Technical Highlights
* **Security:** SHA2-256 Encryption & SQL Injection prevention via Stored Procedures.
* **Efficiency:** Used **Database Views** for fast data fetching.
* **Logging:** Integrated with **Windows Event Viewer** for system error tracking.
* **Validation:** Robust Regex-based validation for all user inputs.

---

## 🔮 Future Roadmap (Next Steps)
* [ ] **Comprehensive Reporting:** Integrate **Microsoft Report Viewer** or Crystal Reports to generate PDF receipts, license documents, and statistical reports.
* [ ] **Dashboard Analytics:** Add a visual dashboard using Charts to display monthly application trends and revenue.
* [ ] **Email/SMS Notifications:** Automated alerts for license expiration or test reminders.
* [ ] **Multi-Language Support:** Implementation of Localization (Arabic/English).

---

## 💻 Installation & Setup

1.  Clone the repository.
2.  Execute the `Database_Script.sql` on your **SQL Server**.
3.  Update the `App.config` with your connection string:
    ```xml
    <connectionStrings>
       <add name="MyDbConnectionString" connectionString="Server=YOUR_SERVER;Database=DVLD;User Id=sa;Password=your_password;"/>
    </connectionStrings>
    ```
4.  Build and Run using **Visual Studio**.

---
**Developed by [Hasan Ameen Al-fahd]**
*Focused on Clean Code, OOP, and Scalable Design.*
