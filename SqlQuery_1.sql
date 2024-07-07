CREATE DATABASE EYCourseDB

USE EYCourseDB

CREATE TABLE Course (
CourseCode CHAR(4) PRIMARY KEY,
CourseTitle VARCHAR(20) NOT NULL,
Duration INT,
CourseFee MONEY)

CREATE DATABASE EYBatchDB

USE EYBatchDB
Create Table Course(CourseCode CHAR(4) PRIMARY KEY)
CREATE TABLE Batch (
BatchCode CHAR(4) PRIMARY KEY,
CourseCode CHAR(4) REFERENCES Course(CourseCode),
StartDate DATETIME,
EndDate DATETIME)

CREATE DATABASE EYStudentDB

USE EYStudentDB
Create Table Batch(BatchCode CHAR(4) PRIMARY KEY)

CREATE TABLE Student (
RollNo CHAR(4) PRIMARY KEY,
BatchCode CHAR(4) REFERENCES Batch(BatchCode),
StudentName VARCHAR(30) NOT NULL,
StudentAddress VARCHAR(50))