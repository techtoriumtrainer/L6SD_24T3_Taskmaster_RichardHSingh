# .Net MAUI Taskmaster App Developement
The objective is to create "Taskmaster," a task management programme aimed at helping project managers, students, and working professionals. The aim of this project is to develop a mobile application that is both efficient and easy to use, enabling users to plan their daily activities and long-term initiatives. By letting users organise, manage, and monitor their work in a seamless and safe manner, the software should increase productivity. 

<h5>The application will need to contain these functionalities listed:</h5>
1.	Category Creation and Management:<br>
 &nbsp; &nbsp; •	Implement the functionality that allows users to create multiple categories to organise their tasks. Each category should serve as a container for related tasks, such as "Work," "Personal," or "School Projects."<br>
&nbsp; &nbsp;  •	Ensure that users can easily view these categories from the main page.<br>
2.	Task Creation and Assignment:<br>
 &nbsp; &nbsp; •	Develop a feature that lets users add tasks to specific categories.<br>
&nbsp; &nbsp;  •	Provide an intuitive interface that makes it easy for users to add and edit their tasks within each category.<br>
3.	Main Page Dashboard:<br>
  &nbsp; &nbsp;•	Design a main page that displays all created categories along with the following metrics:<br>
   &nbsp; &nbsp;&nbsp; &nbsp; o	Total number of tasks in each category.<br>
   &nbsp; &nbsp;&nbsp; &nbsp; o	Number of remaining tasks.<br>
  &nbsp; &nbsp;&nbsp; &nbsp;  o	Number of completed tasks.<br>
  &nbsp; &nbsp;&nbsp; &nbsp;  o	A unique-coloured progress bar for each category, dynamically updating to reflect task completion within that category.<br>
 &nbsp; &nbsp; •	This dashboard should give users a clear overview of their workload and progress across all categories.<br>
4.	Task List and Completion:<br>
&nbsp; &nbsp;  •	Create a section on the main page where users can view and manage their tasks.<br>
&nbsp; &nbsp;  •	Visually represent the relationship between tasks and their categories on the main page<br>
&nbsp; &nbsp;  •	Allow users to mark tasks as complete, with real-time updates to the progress bar, remaining tasks count, and completed tasks count.<br>
&nbsp; &nbsp;  •	Ensure there is an obvious way to differentiate between completed and non-completed tasks.<br>
5.	User Interface, Design, and Aesthetics:<br>
&nbsp; &nbsp;  •	Design a visually appealing and user-friendly interface that enhances the overall user experience.<br>
&nbsp; &nbsp;  •	Ensure that the design is consistent across the app, with attention to colour schemes, typography, and layout.<br>
&nbsp; &nbsp;  •	Consider the aesthetics of the app, making it engaging and easy to navigate while maintaining functionality.<br>
6.	Database Connectivity:<br>
&nbsp; &nbsp;  •	Establish a secure and reliable database connection to ensure that all tasks, categories, and other required information are permanently stored and consistently retrievable.<br>
7.	Privacy and Data Security:<br>
&nbsp; &nbsp;  •	Implement a Privacy Page that users must read and agree to before accessing the app. This is crucial to ensure that users are informed about data handling practices and consent to them.<br>
 &nbsp; &nbsp; •	Allow users to delete their tasks and categories, giving them full control over their data and ensuring they can maintain privacy by removing any personal or sensitive information they choose not to retain in the app.<br>
8.	Testing and Debugging:<br>
&nbsp; &nbsp;  •	Conduct unit testing using an industry-standard framework to ensure that individual components of the application function correctly. This will help identify and resolve issues at the code level, ensuring the reliability and robustness of the app.<br>
9.	Debugging:<br>
&nbsp; &nbsp;  •	Debug and identify issues during the development process, fix those issues, and document them for future reference.<br>
10.	Mobile Deployment:<br>
&nbsp; &nbsp;  •	The app should be designed for deployment on mobile devices, ensuring that users can manage their tasks on the go. Consider factors like responsive design, touch-friendly interfaces, and optimised performance for mobile platforms.

<br><br>
<h5>The Privacy Principles page should be professional, and must include the following:</h5>
• Introduction<br>
• Information we collect:<br>
&nbsp; &nbsp; ○ Personal Data<br>
&nbsp; &nbsp; ○ Task Data<br>
• How we use Information<br>
• Disclosure of Information<br>
• Security of Information<br>
• Protection of Data Rights<br>
• Changes to Privacy Policy<br>
• Contact Us
<br><br>
# Required NuGet Packages For App Develeopment
The following are additional NUGETS required other than the ones that are automatically installed when one choosed to develop with .NET MAUI<br>
• CommunityToolkit.Mvvm<br>
• Fody<br>
• Microsoft.Maui.Controls<br>
• PropertyChanged.Fody
<br><br>
# Required SQL NuGet Packages
There are a number of NuGet packages with similar names so please use the following packages that have these attributes:<br>
<h4>First Package</h4>
• <strong>ID: </strong> sqlite-net-pcl<br>
• <strong>Authors:</strong>  SQLite-net<br>
• <strong>Owners:</strong>  praeclarum
<br>
<h4>Second Package</h4>
<strong>ID: </strong> SQLitePCLRaw.bundle_green<br>
<strong>Version:</strong>  >= 2.1.0<br>
<strong>Authors</strong> : Eric Sink<br>
<strong>Owners:</strong>  Eric Sink
<br>
<h4>Third Package</h4>
• <strong>ID: </strong>  CommunityToolkit.Mvvm<br>
• <strong>Authors:</strong> Microsoft<br>
• <strong>Owners:</strong> Microsoft<br>

