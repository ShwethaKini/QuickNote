# QuickNote

In this repository I have front end and backend code. To execute the code clone this repository to local.

*********** Steps to run Frontend code *************

1> Have Node.js installed in your system.

2> In CMD, run the command npm install http-server -g

3> Navigate to the specific path of your frontend application folder(Quick_Notes_Frontend) in CMD and run the command http-server

4> Go to browser and type localhost:8080. Application should run there.

************ Steps to run Backend code *************

1> Open solution file (QuickNotes_Backend -> QuickNotes.sln) in visual studio.

2> Run the application from Visual studio using run button on top menu.

3> SSL waring popup appears click on No and continue.

4> You can see the application's swagger running on the browser(https://localhost:7271/swagger/index.html).

5> Authorization is required to make api calls
username: admin
password admin

Since frond end code makes call to backend first run backend code and then run front end code to get results with out any errors.


*********** Authorization **************************

I have maintained two users in the code

1> username: admin
   password: admin

2> username: user
   password: user

We can use any of this user to login and access the end points.

To login click on Authorize button on swagger page and give username and password.

In the frontend code username and password is hardcoded to admin and added to request headers.
If you want to change it then it has to be done in code.

************ Data **************************

For both user I have maintained one default Note. So for first time when you call get end point you will see one quick note
already existing.

Notes are saved in user specific manner.
For example if you login with admin credential and create notes and then login with user credentials you can not see those notes.
Admin can only see and modify notes created by admin and user can see and modify only notes created by user.

************* Extra Information *****************

If you want to make any front end code change and see the result just to ctrl + f5 to reflect new code changes.

I have also restricted the text area size to enter note to max 1000 characters for time being.


