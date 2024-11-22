# Claims System: Contract Monthly Claim System (CMCS)

## Overview
The Claims System (CMCS) is a comprehensive web application designed for managing contract monthly claims within an academic institution. This application provides an intuitive user interface, seamless claim workflows, and automated processes to simplify claim submission, verification, and payment. Built using ASP.NET Core MVC, the final application delivers robust functionality tailored to lecturers, programme coordinators, academic managers, and HR managers. 

This README details the development journey, features, and functionality of the final build of the CMCS application.

---

## Table of Contents
- [Overview](#overview)
- [Repository Contents](#repository-contents)
- [Sprints](#sprints)
  - [Sprint Part 1](#sprint-part-1)
  - [Sprint Part 2](#sprint-part-2)
  - [Sprint Part 3 - Final Application Build](#sprint-part-3---final-application-build)
- [Cloning the Project and Restoring the Database](#cloning-the-project-and-restoring-the-database)
- [Assumptions for Usage](#assumptions-for-usage)
- [Lecturer Feedback](#lecturer-feedback)
- [Functionality and App Usage](#functionality-and-app-usage)
- [Future Requirements](#future-requirements)
- [Code Attribution](#code-attribution)
- [Acknowledgements](#acknowledgements)

---

## Sprints

### Sprint Part 1
#### Deliverables:
- **UML Class Diagram**: Outlined the database structure and relationships.
- **Project Plan**: Defined the development timeline and milestones.
- **GUI Design**: Developed a front-end wireframe focusing on user experience.

#### Goal:
- Establish a strong foundation for the application through structured design and planning.

---

### Sprint Part 2
#### Deliverables:
- Developed an ASP.NET Core MVC web application linked to SQL Server.
- Implemented initial claim submission and approval workflows.

#### Features:
- **Lecturer Claim Submission**: Lecturers can submit claims with fields for hours worked, hourly rate, and additional notes.
- **Document Upload**: Allowed secure uploading of PDFs up to 15MB.
- **Approval Workflow**: Programme Coordinators and Academic Managers could approve or reject claims.
- **Claim Status Tracking**: Real-time status updates for Pending, Approved, and Rejected claims.
- **Unit Testing**: Comprehensive tests for claim submission and file validation.

---

### Sprint Part 3 - Final Application Build
#### Deliverables:
- Automation of workflows to streamline claim management and payment processing.

#### Features:
- **Lecturer Claim Submission Automation**:
  - Auto-calculates final payment based on hours worked and hourly rate.
  - Validation checks for accurate data entry using FluentValidation.
  - Integration of jQuery for client-side calculations.

- **Claim Verification and Approval Automation**:
  - Automated verification of claims based on predefined criteria (hours worked, hourly rate).
  - Simplified workflows for coordinators and managers.
  - Role-based access using ASP.NET Identity.

- **HR Management and Payment Automation**:
  - HR Managers can view and process approved claims, generate invoices, and download reports.
  - Automated PDF report generation summarizing approved claims.
  - Added functionality for managing user data, such as updating contact information.

#### Tools:
- ASP.NET Core MVC, Entity Framework, FluentValidation, jQuery, and Web API.

---

## Cloning the Project and Restoring the Database
1. Clone the repository from GitHub.
2. Open the project in Visual Studio and restore NuGet packages.
3. Restore the database:
   - Open SQL Server Management Studio (SSMS) and connect to your server.
   - Right-click on "Databases" and select "Restore Database".
   - Restore from the provided `.bak` file.
4. Update the `appsettings.json` connection string with your database configuration.

---

## Assumptions for Usage
- **User Roles**: Four roles are implemented:
  - **Lecturers**: Submit claims.
  - **Programme Coordinators**: Verify claims.
  - **Academic Managers**: Final approval of claims.
  - **HR Managers**: Process claims and generate reports.
- **Claim Submission**:
  - Hourly rate must be between R200 and R1000.
  - Hours worked must be between 1 and 150.
  - Claims can only be submitted for the current or previous month.
- **Supporting Documentation**:
  - Required proof of hourly rate and contract details.
  - Verified by the Programme Coordinator.

---



---

## Functionality and App Usage
1. **Claim Submission**:
   - Lecturers submit claims with hours worked, hourly rate, and notes.
   - Upload supporting documents (PDF format).

2. **Approval Process**:
   - **Programme Coordinator**: Reviews claims and forwards approved ones to the Academic Manager.
   - **Academic Manager**: Final approval step.

3. **HR Role**:
   - Process approved claims and generate payment reports.
   - View, download, and manage lecturer data.

4. **Claim Tracking**:
   - Real-time updates on claim status visible to lecturers.



## Code Attribution

HTML & CSS: 

Author: w3schools 

Link: https://www.w3schools.com/css/ 

Date Accessed: 15 November 2024 

 

Fluent Validation: 

Author: FluentValidation 

Link: FluentValidation — FluentValidation documentation 

Date Accessed: 16 Novemeber 2024 

 

PDF File Handling: 

Author: Fatima Shaik 

Link: PROG6212-Group1-2024/FileHandlingApp at main · fb-shaik/PROG6212-Group1-2024 · GitHub 

Date Accessed: 16 November 2024 

 

LINQ and File Handling: 

Author: Fatima Shaik 

Link: PROG6212-Group1-2024/Employee_Management_LINQ_FileHandling_G1 at main · fb-shaik/PROG6212-Group1-2024 · GitHub 

Date Accessed: 17 November 2024 

 

Database: 

Author: Microsoft 

Link: https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-8.0&tabs=visual-studio 

Date Accessed: 17 November 2024 

 
  
