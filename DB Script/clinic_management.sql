create database Clinic

use Clinic

create table users (username varchar(10) unique constraint check_username check(username not like '%[^a-zA-Z0-9]%'),
firstname varchar(20), lastname varchar(20), password varchar(25) constraint check_password check(password like '%@%'))

insert into users values ('santo19', 'Santo', 'Brighton', 'Santo@19')
insert into users values ('deepak11', 'Deepak', 'Kumar', 'Deepak@11'),('atul07', 'Atul', 'Lakkapragada', 'Atul@07'),('basid29', 'Mohammed', 'Basid', 'Basid@29')

create table doctors (doctor_id int primary key, firstname varchar(20) constraint check_firstname check(firstname not like '%[^a-zA-Z0-9]%'), 
lastname varchar(20) constraint check_lastname check(lastname not like '%[^a-zA-Z0-9]%'), 
sex varchar(7), specialization varchar(50), visiting_from time, visiting_to time)

insert into doctors values (101, 'Kiruba', 'Karan', 'M', 'Orthopedics', '14:00', '17:00'),
(102, 'Kevin', 'Anderson', 'M', 'Internal Medicine', '18:00', '21:00'),
(103, 'Sharon', 'Angel', 'F', 'Pediatrics', '09:00', '12:00'),
(104, 'Seetha', 'Mahalakshmi', 'F', 'General', '10:00', '12:00'),
(105, 'Dinesh', 'Moorthy', 'M', 'Opthamology', '16:00', '18:00')


create table patients (patient_id int identity(2000, 1) primary key, firstname varchar(20) constraint check_firstname_patient check(firstname not like '%[^a-zA-Z0-9]%'),
lastname varchar(20) constraint check_lastname_patient check(lastname not like '%[^a-zA-Z0-9]%'), sex varchar(7),
age int constraint check_age_patient check(age between 0 and 120), dob datetime)


create table appointments (apt_id int identity(500,1) primary key, doctor_id int foreign key references doctors(doctor_id), visiting_date date,
timeslot varchar(30), apt_status varchar(30), patient_id int foreign key references patients(patient_id))

insert into appointments values (101, '2022-08-26', '14:00 - 15:00', 'Available', null),
(101, '2022-08-26',  '15:00 - 16:00', 'Available', null),
(101, '2022-08-26', '16:00 - 17:00', 'Available', null),
(102, '2022-08-26', '18:00 - 19:00', 'Available', null),
(102, '2022-08-26', '19:00 - 20:00', 'Available', null),
(102, '2022-08-26', '20:00 - 21:00', 'Available', null),
(103, '2022-08-26', '09:00 - 10:00', 'Available', null),
(103, '2022-08-26', '10:00 - 11:00', 'Available', null),
(103, '2022-08-26', '11:00 - 12:00', 'Available', null),
(104, '2022-08-26', '10:00 - 11:00', 'Available', null),
(104, '2022-08-26', '11:00 - 12:00', 'Available', null),
(105, '2022-08-26', '16:00 - 17:00', 'Available', null),
(105, '2022-08-26', '17:00 - 18:00', 'Available', null)

insert into appointments values (101, '2022-08-27', '14:00 - 15:00', 'Available', null),
(101, '2022-08-27', '15:00 - 16:00', 'Available', null),
(101, '2022-08-27', '16:00 - 17:00', 'Available', null),
(102, '2022-08-27', '18:00 - 19:00', 'Available', null),
(102, '2022-08-27', '19:00 - 20:00', 'Available', null),
(102, '2022-08-27', '20:00 - 21:00', 'Available', null),
(103, '2022-08-27', '09:00 - 10:00', 'Available', null),
(103, '2022-08-27', '10:00 - 11:00', 'Available', null),
(103, '2022-08-27', '11:00 - 12:00', 'Available', null),
(104, '2022-08-27', '10:00 - 11:00', 'Available', null),
(104, '2022-08-27', '11:00 - 12:00', 'Available', null),
(105, '2022-08-27', '16:00 - 17:00', 'Available', null),
(105, '2022-08-27', '17:00 - 18:00', 'Available', null)

insert into appointments values (101, '2022-08-28', '14:00 - 15:00', 'Available', null),
(101,'2022-08-28', '15:00 - 16:00', 'Available', null),
(101, '2022-08-28', '16:00 - 17:00', 'Available', null),
(102, '2022-08-28', '18:00 - 19:00', 'Available', null),
(102, '2022-08-28', '19:00 - 20:00', 'Available', null),
(102, '2022-08-28', '20:00 - 21:00', 'Available', null),
(103, '2022-08-28', '09:00 - 10:00', 'Available', null),
(103, '2022-08-28', '10:00 - 11:00', 'Available', null),
(103, '2022-08-28', '11:00 - 12:00', 'Available', null),
(104, '2022-08-28', '10:00 - 11:00', 'Available', null),
(104, '2022-08-28', '11:00 - 12:00', 'Available', null),
(105, '2022-08-28', '16:00 - 17:00', 'Available', null),
(105, '2022-08-28', '17:00 - 18:00', 'Available', null)

drop table users
drop table doctors
drop table patients
drop table appointments

select * from users
select * from doctors
select * from patients
select * from appointments

--Stored Procedures
create procedure spselectUserLogin(@username varchar(10), @password varchar(25))  as 
select * from users where username = @username and password = @password

execute spselectUserLogin

drop procedure [spselectUserLogin]

create procedure spselectAllDoctors as
select * from doctors

execute spselectAllDoctors

create procedure spinsertAddPatients (@firstname varchar(20), @lastname varchar(20), @sex varchar(7), @age int, @dob datetime)
as insert into patients (firstname, lastname, sex, age, dob) values (@firstname, @lastname, @sex, @age, @dob)

execute spinsertAddPatients

create procedure spselectAddPatients (@firstname varchar(20), @lastname varchar(20), @sex varchar(7), @age int, @dob datetime)
as select patient_id from patients where firstname=@firstname and lastname=@lastname and sex=@sex and age=@age and dob=@dob