Sotiris Ploumis

Individual Project for C# BootCamp by AfDEmp

Task: Create a program that manages a school (DB, roles, password hashing)

Program stores all information to DB using Entity Framework 6
Program uses hashing to store user passwords

Program starts with an empty DB, checks if a HeadMaster is present and if not
prompts the user to register as a HeadMaster (first time setup)
If Headmaster is present then the user can login directly

HeadMaster creates Trainers, Students, Assignments and Courses

User can log in as HeadMaster, Trainer or Student
user can also logout and relogin as different role without exiting the Program
(as long as he / she knows the username and the password)

Available operations per role:

Student:
	- See his / her daily Schedule per Course (Check if he / she has a course active at a specific date)
	- See the dates of submission of his / her Assignments per Course
	- Submit any of his / her Assignments

Trainer:
	- View all the Courses he / she is enrolled 
	- View all the Students per Course
	- View all the Assignments per Student per Course
	- Mark all the Assignments per Student per Course (assignment must be submitted first)

HeadMaster:
	- CRUD on Courses
	- CRUD on Students (username, password and other student info)
	- CRUD on Assignments
	- CRUD on Trainers (username, password and other trainer info)
	- CRUD on Students per Courses (student is also connected automatically to any assignment connected to this course)
	- CRUD on Trainers per Courses
	- CRUD on Assignments per Courses (assignment is also connected automatically to any student connected to this course)
	