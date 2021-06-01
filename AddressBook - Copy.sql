---Create table AddressBookSystem
use AddressBookDataBase;
Create table AddressBookSystem(FirstName varchar(50),LastName varchar(50),Address varchar(50),City varchar(50), State varchar(50),Zip int,PhoneNumber float,EmailId varchar(50),AddressBookType varchar(50),AddressBookName varchar(50) );
select * from AddressBookSystem;

--insert record for table

insert into AddressBookSystem values('Nilima','Wadal','Warje','Pune','Maharashtra','422058','9870055698','nilima@gmail.com','Family','Ritesh');
select * from AddressBookSystem;