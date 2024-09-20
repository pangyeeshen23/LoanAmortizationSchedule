### **Project Setup**

The approach that i went for is to divide the project into frontend and backend.
The frontend is using Angular JS and the backend is using .NET
This approach allows a better seperation of concern that make the project more easier and more organized.
Other than that, these are two framework that i been learning/using.

In the frontend, i had setup a shared folder and a module folder that is dedicated to loan domain.
each folder will have their own set of components, interfaces, services and etc.

In the backend, i had setup controller, handler, DTOs, models
controller - handle incoming request
handler - handle the logic to generate the response that the controller will handle
DTOs - they are data transfer object like input and output
models - they are the informative representation of an object. etc: schedule.

### **Additonal Features**

1. Automation Testing With xUnit
   There are two automation test that had been added to the backend project using xUnit
2. Input Prevention
   For input number, symbols and characters like -,+,e,E can be keyed in the input. 
   So i added a checker to check the keyed in character to prevent symbols and characters that had been listed 