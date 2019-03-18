Created by Sotiris Ploumis

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
1. See his / her daily Schedule per Course (Check if he / she has a course active at a specific date)
2. See the dates of submission of his / her Assignments per Course
3. Submit any of his / her Assignments

Trainer:
	1. View all the Courses he / she is enrolled 
	2. View all the Students per Course
	3. View all the Assignments per Student per Course
	4. Mark all the Assignments per Student per Course (assignment must be submitted first)

HeadMaster:
	1. CRUD on Courses
	2. CRUD on Students (username, password and other student info)
	3. CRUD on Assignments
	4. CRUD on Trainers (username, password and other trainer info)
	5. CRUD on Students per Courses (student is also connected automatically to any assignment connected to this course)
	6. CRUD on Trainers per Courses
	7. CRUD on Assignments per Courses (assignment is also connected automatically to any student connected to this course)