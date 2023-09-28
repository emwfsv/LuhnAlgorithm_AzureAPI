# LuhnAlgorithm_AzureAPI

Simple Azure function for checking a Luhn calculation thru a Post Request


# Usage
This project is using my other Git repository as a project reference.
The following is needed in order to install this reference:

- Open up a command prompt
- make sure you are located in this projects base folder.
- Write command in command prompt
>  `git submodule add https://github.com/emwfsv/LuhnAlgorithm.git externals/LuhnAlgorithm`
- Will create a folder namned "Externals" and place the reference library there.
- To update the external reference package write the following command in the command prompt:
>  `git submodule update --init --recursive`

# API Requests
There is only one API request available in this repository and thats included as a Postman Collection in this project
Its found in folder /Postman Import Files/ and namned "AzureLuhnAPI.postman_collection.json"
Import it to Postman and just send luhn checks to the API on the localhost
