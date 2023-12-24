# Welcome to the SoonStack.Backend!

Welcome to the  **SoonStack.Backend**. Kindly refer wiki page for more infomation about the code

## 1. Getting Started

Welcome to SoonMonoCleanStore, an exemplary e-commerce backend API. SoonMonoCleanStore is designed to provide a clear architectural structure that combines Clean Architecture principles, the concept of vertical slices, and 'Screaming Architecture' to achieve modularity and scalability.

### 1.1 Prerequisites

Before you begin, ensure you have the following prerequisites installed on your system:

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [MySQL](https://www.mysql.com/) (for database)

## 2. Installation

Follow these steps to set up and run SoonMonoCleanStore on your local machine:

1. **Clone the Repository:**

   ```bash
   git clone https://github.com/yourusername/SoonMonoCleanStore.git
   cd SoonMonoCleanStore

### 2.1 Database Configuration

Before running the SoonMonoCleanStore application, you need to set the database. Follow these steps:
0. Make sure mysql is installed
1. Run all db script provided at folder MySql in MySql which can be found at ~\SoonMonoCleanStore\Persistance\DatabaseScript\MySQL.

2. Update the `ConnectionStrings` section with your database connection information.

3. Open the `appsettings.json` file in the 'SoonMonoCleanStore' project.

4. Update the `ConnectionStrings` section with your database connection information.



## 3. Architecture
![Architecture diagram](https://github.com/sooneeatwork/SoonStack.Backend/blob/master/SoonMonoCleanStore/Docs/architecture.png)


- **Bug Reports and Feature Requests**: Use the issue tracker to report bugs or request new features.

- **Pull Requests**: Submit pull requests with clear descriptions and details of the changes made.


