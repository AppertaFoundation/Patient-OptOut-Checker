![Logo](/README-Pics/UHPTLogoClear.png)

# Patient Opt Out Checker

***

#### What is the 'Patient Opt Out Checker'? 

The Patient Opt Out Checker application allows a user to check if an NHS patient has decided to opt-out of allowing their medical data to be used for research and planning. The user must enter patients local hospital identifiers or NHS numbers into the provided text box and click the 'Check' button. This will run a comparison between the inputs and the entries within the targeted database, and any patients that have opted-out will be marked and sorted appropriately. The results can also then be copied by selecting the icon on the top-right of the box, which will retain formatting an allow the results to be transferred elsewhere

The app relies on a database populated from the MESH, please see guidance below for configuing the database. 

For more information of the Opt-Out scheme, please visit https://digital.nhs.uk/services/national-data-opt-out

***

#### Running the 'Opt-Out Checker'  

To run the application through a website, two new sites will need to be created in IIS, one for the API and one for the front-end. Once they are created, copy the files from the *API* folder in the downloaded zip and paste them in the folder for the website, which can be accessed by selecting the *Explore* option. Do the same for the *FrontEnd* folder

Windows Authentication will also need to be enabled on the API. Selecting *Authentication* and then switch ***Windows Authentication*** to ***'Enabled'***. 

IIS must also be configured to allow JSON files. To do this, on the file for the front-end website, select *MIME Types*, click *Add...* and enter ***.json*** in the file extension and ***application/json*** in the MIME type. Once thats been added, go back and select *Handler Mappings*, click *Add Script Map...* and enter ****.json*** in the request path, ***C:\WINDOWS\system32\inetsrv\asp.dll*** in the executable text box and a name in the Name box.

***

#### Configuring the 'Opt-Out Checker'
Upon downloading the application, there are two files that must be configured for your trust.  

***'appsettings.json'*** is located at PublishedFiles/API/appsettings.json. There are three variables to change here:
- ***'DataWarehouseContext'*** stores the connection string for the database that contains the table of patients that have opted-out. This is what the program will compare the entered numbers against.
- ***'LogContext'*** stores the connection string for the database in which the application will create a table to store logs of what users entered, should the ***'LoggingEnabled'*** attribute be set to true. Should you decide to use logging, the command ```Update-Database -Context LogContext``` must be run in the package manager console prior to running the program
- ***'FrontEndUrl'*** will store the desired url address for the front end, this is used for CORS.
- ***'ActiveDirectoryGroupName'*** stores the name of the AD Group that the user must be a part of the access the application. It is recommended that a new AD group is created to add users of the application to. The API will pick up the name of the domain it is running on automatically.
- ***'LoggingEnabled'*** will enable logs to be created and stored if set to true, whereas no logs will be created if set to false

 
***'environment.ts'*** is located at PublishedFiles/FrontEnd/ClientApp/dist/PatientOptOutFrontEnd/assets/settings.json. There are four more variables to change here:
- ***'apiUrl'*** is the web url for the 'Opt-Out Checker' API that points to the location of both the authentication service and the numbers service. Only the base part is needed
- ***'trustDisclaimer'*** is the text that will be displayed at the bottom of the page. It is recomended that a link to NHS Digital's page on the Opt-Out Scheme is provided, as well as a link to your trust's individual privacy notice
- ***'initialDisclaimer'*** is the text that will show on the 'Terms of Use' pop-up that will appear upon entering the website
- ***'noAccess'*** text that will be displayed on the screen when an unauthorised user attempts to access the website 
 
You can also change the logo displayed in the top right of the screen by changing the image ***'Logo.jpg'*** located at PublishedFiles/FrontEnd/ClientApp/dist/PatientOptOutFrontEnd/assets/Logo.jpeg. The file name must remain the same.

***

#### Configuring a Database Table
In order to use this application, a view must ceated in your chosen databse called ***'[Patient].[vw_PatientOptOut]'***.  

Within this table, two columns ***'HospitalNumber'*** and ***'NHSNumber'*** are required, as shown below:

![Table](/README-Pics/vw_PatientOptOut.png)

These columns will be populated with the Hospital Number and NHS Number of patients that have opted-out of having their data used.

***

#### Using the 'Opt-Out Checker'  
For the full user guide, please see the included pdf [Patient Opt-Out User Guide](./PatientOpt-OutUserGuide.pdf)

Instructions for the use of the 'Patient Opt-Out Checker' are as follows:

- If the user is not in the right permissions group, the website will display a message and prevent access to the application
- Upon loading the website, the Terms of Use will be displayed (User set). Simply click the 'I Agree' button to access the application
- The website displays the following:
  - A black bar at the top of the screen displays the username of the account 

  - The disclaimer will be at the bottom of the screen (User Set) 

  - The 'Check' button will take any inputs in the text box above and check them against the database to mark any patients that have opted-out. The box will only take inputs of hospital numbers with no/one/two letters followed by six numbers and NHS numbers in the standard '3-3-4' format with/without a '-' or space. There must also only be one number per line
Any other results will be ommited, and a message will be displayed above the text box when the results are checked to inform the user of the number of results that were removed. 

  - The text box facilitates copying/pasting, and the amount of inputs has no limits

  - The copy button will copy anything currently in the text box and save it to the clipboard. It is located at the top right of the text box.

***

#### Developing/Contributing to the 'Opt-Out Checker'  

To download and run the application, please follow the steps below:

1. Select the 'Clone or Download' button in the top right of the project
2. **If cloning with HTTPS** 
    - Copy the url 

    - Open Git Bash 
    
    - Navigate to the directory location you wish to clone the project into 
    
    - Run the command ``` $ git clone https://github.com/YOUR-USERNAME/YOUR-REPOSITORY ```

3. **If using GitHub Desktop** 
    - Select the desktop icon and follow the onscreen instructions to access the code through GitHub Desktop and complete the clone  

4. Open the project in Microsoft Visual Studio 2019 - A free 'Community Edition' can be downloaded at https://visualstudio.microsoft.com/
    - Please note that 'PatientOptOutAPI' is built using .NET Core and 'PatientOptOutFrontEnd' is built using Angular

5. Both the API and Angular site should be configured to run together, and open automatically when the project is run. 

***