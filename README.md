
# Global-IMC-Task
A task assigned by Global IMC as a test. Finished at 1st of April 2021.

## Task Details
### Task #1: Persist the created products to a database supported by Azure, following the best practices.
Task finished. Used SQLite as a simple database provider to store the tables required to finish the task.

### Task #2: Build RESTful APIs Endpoint to allow clients to carry out CRUD operations on a single product
Task finished. API have been created that allows client to create, update, delete, and retrieve the data for the products. Documentation of the API is provided under /swagger URL.

### Task #3: Build RESTful APIs Endpoint to allow clients to search for products by name or description
Task finished. 2 APIs have been created that allow clients to provide title and description of the product or portions of them. The APIs return the data available according to the provided criteria. Documentation of the API is provided under /swagger URL.

### Task #4: Build a front end single page application to use task 2 & 3 APIs
Task finished. Using the latest Angular framework, the Single Page Application (SPA) is created that consumes the API mentioned before. The UI project is separated in a different repository in consideration that a separate CI/CD process will be used for it. Project repository should have been provided in the email your side has received.

### Task #5: Upload both API & SPA onto azure and include the link in the readme file
Task not finished. Currently I do not have an account on Azure to allow me to deploy these project. That been said, the deployment process should not be an issue if an account is provided. 

 - Using visual studio, deploying the API to an Azure App Service is very smooth and requires minimum configurations. 
 - Using Azure portal, we can create a Static Web App. Linking the Github repository for the UI Project will allow Azure to pull and build the UI and deploy it to the created resource. 

