# FlightSimulator

### Summary
FlightSimulatorApp has been programed by the architecture of MVVM. 
We developed view model for each view component we created and those view model inherits an abstract class that defines all of them.
We also created one model that will notify certain view model about properties that have been modified by data received from the server 
and receive information from certain view model and send that data to the server.
Furthermore, the view models and the model are initialized at the startup of the program, which happens in the App level, 
in order to separate the model from the view so that they do not know each other and thus the MVVM principle will be realized.

### Additional Features
1. In the sub main window we addedd a back button for the user to have the option to disconnect from the server and move to another server.
2. We added an exit button that is displayed throughout the program and gives the user the option to exit the program whenever they want.
3. As soon as an error has occurred in the model, a text box appears on the screen displaying the error with a corresponding message so that we can inform the user.
4. In the login window (which is the main window) we have added a button for ip and port values and will be displayed to the user in the respective text boxes.
5. Our sub main window supports in resizing the screen. This allows the user to decide what size he wants the screen for his convenience.
6. We added Nugget Packages like MaterialDesignColors and MaterialDesignThemes for our application design, for example buttons, background, etc..
7. Errors messages displays on the UI if there is a any problem with the simulator server, for example invalid inputs given from the simulator, 10 sec timeout for reading a massage, etc..
