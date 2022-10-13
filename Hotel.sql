create database Hotel
use Hotel

create table Client(
ClientID int not null primary key ,
ClientName nvarchar(50) not null unique,
ClientPhone nvarchar(50) not null,
ClientCountry nvarchar(50) not null,
)

insert into Client values 
(1,'The First Customer','0987654321','Ethiopia')
select * from Client

create table Room(
RoomID int not null primary key,
RoomPhone nvarchar(50) not null,
RoomAvailable nvarchar(50) not null
)
insert into Room values 
(101,'0987064722','Free') --free or rented
select * from Room

create table Staff(
StaffID int not null primary key,
StaffName nvarchar(50) not null,
StaffPhone nvarchar(50) not null,
StaffGender nvarchar(50) not null,
StaffPassword nvarchar(50) not null
)

insert into Staff values
(1,'staff member','0978563412','Male','pass123')
select * from Staff

create table Reception(
ReceptID int not null primary key,
ReceptName nvarchar(50) not null,
ReceptPhone nvarchar(50) not null,
ReceptGender nvarchar(50) not null,
ReceptAddress nvarchar(50) not null,
ReceptDob nvarchar(50) not null,
ReceptPassword nvarchar(50) not null
)
insert into Reception values
(1,'reception member','2321021','Female','Los Angeles','2000-3-2','pass123')

select * from Reception

create table Reservation(
ClientName nvarchar(50) not null,
RoomID int not null,
DateIn nvarchar(50) not null,
DateOut nvarchar(50) not null
Foreign key (ClientName) References Client(ClientName),
Foreign Key (RoomID) References Room(RoomID)
)
insert into Reservation values
('The First Customer',101,cast(GETDATE()-5 as nvarchar(50)),cast(GETDATE()+5 as nvarchar(50)))

select * from Reservation
