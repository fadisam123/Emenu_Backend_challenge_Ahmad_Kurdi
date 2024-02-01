# Emenu Backend challenge (Ahmad Al Kurdi)

***

## Table of Contents

<a href="https://github.com/fadisam123/Emenu_Backend_challenge_Ahmad_Kurdi#Run">Run</a>

<a href="https://github.com/fadisam123/Emenu_Backend_challenge_Ahmad_Kurdi#Notes">Notes</a>

***

### Run

- It is important to know that I have used the **ASP.NET Core 6** in this project, so to run it make sure that you have the **same version installed** on your device.

- Download the project as a zip file or make a clone with `git clone https://github.com/fadisam123/Emenu_Backend_challenge_Ahmad_Kurdi.git` command.

- Simply, either open the `.sln` file and from the `Debug` tab, choose `Start Without Debuging`. Or go to the folder named: `Emenu_Backend_challenge_Ahmad_Kurdi`, then open the command window in this path and execute the `dotnet run` command; It will download the required packages and compile the files, and then the program will run.

- Upon running, a database will be created using SQL Server named `EmenuDB_Ahmad_kurdi`, and some data that I entered manually will be placed. You will notice that the server is now listening on `https://localhost:7137`. Now you can interact with the application at that address, or you can go to the documentation page at `https://localhost:7137/swagger/index.html` and see all possible actions an routs and interact with them directly.

<p align="center">
 <img src="https://github.com/fadisam123/Emenu_Backend_challenge_Ahmad_Kurdi/blob/master/Attachments/1.png?raw=true" alt="image"/>
</p>

<p align="center">
 <img src="https://github.com/fadisam123/Emenu_Backend_challenge_Ahmad_Kurdi/blob/master/Attachments/2.png?raw=true" alt="image"/>
</p>

<p align="center">
 <img src="https://github.com/fadisam123/Emenu_Backend_challenge_Ahmad_Kurdi/blob/master/Attachments/Doc.png?raw=true" alt="image"/>
</p>

### Notes

- Below is a diagram depicting my implementation of the database according to my understanding of the topic and requirements (Note that it can be further normalized and optimized if I conduct some inquiries and obtain more details to deepen my understanding)

<p align="center">
 <img src="https://github.com/fadisam123/Emenu_Backend_challenge_Ahmad_Kurdi/blob/master/Attachments/DB.png?raw=true" alt="image"/>
</p>

- The following image shows the project structure:
<p align="center">
 <img src="https://github.com/fadisam123/Emenu_Backend_challenge_Ahmad_Kurdi/blob/master/Attachments/ProjStruct.png?raw=true" alt="image"/>
</p>

<p>
  The <i>Emenu_Backend_challenge_Ahmad_Kurdi</i> is a console application (Web API) project representing the presentation layer (i.e., API).</br>
  The <i>Application</i> is a cross-platform class library representing the application logic layer.</br>
  The <i>Infrastructure</i> is a class library representing the data layer.</br>
  The <i>Domain</i> is a class library that contains the database mapped object (ORM), or the model of the system.</br>
  The <i>DTOs</i> is a shared class library used in the <i>Application</i> & <i>Emenu_Backend_challenge_Ahmad_Kurdi</i> projects.</br>
</p>

- Please note that I did not follow the N-tier architecture style 100% in this project. For example, I placed the repository interfaces (and unit of work) in the application layer rather than the infrastructure layer.
- Also note that in this project I did not implement many things, for example, there is no data validation on input, no error/exception handling, auto-mapping, etc.
- If you have any questions or issues about anything related to the project, code, or the running process; Please feel free to contact me immediately.

> Thanks for your time.
