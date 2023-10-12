# PhoneBook Project

This project is a contact directory application built using microservices architecture with .NET 6. The project consists of four separate components: PhoneBook.ApiGateway, PhoneBook.Common, PhoneBook.Contact.API, and PhoneBook.Report.API.

## Project Architecture

- PhoneBook.ApiGateway: This is the API Gateway project, allowing centralized management of all microservices. It facilitates communication between the Contact API and Report API using RabbitMQ.
- PhoneBook.Common: It is a library that contains common code, used to extract repetitive code from other projects.
- PhoneBook.Common.Tests: A test project that includes tests for the common code.
- PhoneBook.Contact.API: This API manages the contact information of the directory. It communicates with the Report API through RabbitMQ.
- PhoneBook.Contact.API.Tests: A test project that includes tests for the Contact API.
- PhoneBook.Report.API: This API manages operations related to reporting. It communicates with the Contact API through RabbitMQ.
- PhoneBook.Report.API.Tests: A test project that includes tests for the Report API.

## Installation and Usage

1. You should have the .NET 6 SDK installed for this project.

2. Install NuGet packages and build the project using the following commands:
   - Run "dotnet restore" and "dotnet build."

3. To migrate the databases, run the following commands in CMD or PowerShell:
   - Navigate to the location of the projects using the "cd" command.
   - If dotnet-ef is not installed, run: "dotnet tool install --global dotnet-ef."
   - Navigate to each of the two API projects using the "cd" command, and then execute the following commands sequentially:
     - "dotnet ef migrations add InitialCreate"
     - "dotnet ef database update"

4. Start the PhoneBook.ApiGateway project. The project will run on localhost:5550.

5. Start the PhoneBook.Contact.API and PhoneBook.Report.API projects separately. The Contact API will run on localhost:5551, and the Report API will run on localhost:5552.

6. To access the Swagger UI interface for the project, use the following addresses:
   - API Gateway: http://localhost:5000
   - Contact API: http://localhost:5550/swagger
   - Report API: http://localhost:5551/swagger

7. Remember that communication between the Contact API and Report API is established through RabbitMQ. This enables data exchange between both APIs.


## Communication

If you have any question or feedback about the project please get in touch with me.

- **Enes Kartal**
- E-mail: eneskartal117@gmail.com
