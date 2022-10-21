create database Hotel
use Hotel

create table Client(
ClientID int not null primary key ,
ClientName nvarchar(50) not null unique,
ClientPhone nvarchar(50) not null,
ClientCountry nvarchar(50) not null,
)

go
create procedure [insert into Client]
@clientid int,
@clientname nvarchar(50),
@clientphone nvarchar(15),
@clientcountry nvarchar(25)
as
begin
insert into Client
(ClientID,ClientName,ClientPhone,ClientCountry) 
values
(@clientid,@clientname,@clientphone,@clientcountry)
end
go
create procedure [update Client]
@clientid int,
@clientname nvarchar(50),
@clientphone nvarchar(15),
@clientcountry nvarchar(25)
as
begin
update Client set
ClientID=@clientid,ClientName=@clientname,ClientPhone=@clientphone,ClientCountry=@clientcountry
end
go
create procedure [delete Client]
@clientid int
as
begin
delete Client where 
ClientID=@clientid
end
go
create procedure [Client Date]
as
begin
select GETDATE()
end
insert into Client values 
(1,'The First Customer','0987654321','Ethiopia')
exec [insert into Client] 10,'Storeddemo','098765','Ethiopia'
exec [delete Client] 10
select * from Client

create table Room(
RoomID int not null primary key,
RoomPhone nvarchar(50) not null,
RoomAvailable nvarchar(50) not null
)
go
create procedure [insert into Room]
@roomid int,
@roomphone nvarchar(50),
@roomavailable nvarchar(25)
as
begin
insert into Room
(RoomID,RoomPhone,RoomAvailable) 
values
(@roomid,@roomphone,@roomavailable)
end

go
create procedure [delete Room]
@roomid int
as
begin
delete Room where 
RoomID=@roomid
end

insert into Room values 
(101,'0987064722','Free') --free or rented
select * from Room
exec [insert into Room] 122,'1234','Free'
exec [delete Room] 122

create table Staff(
StaffID int not null primary key,
StaffName nvarchar(50) not null,
StaffPhone nvarchar(50) not null,
StaffGender nvarchar(50) not null,
StaffPassword nvarchar(50) not null
)
go
create procedure [insert into Staff]
@staffid int,
@staffname nvarchar(50),
@staffphone nvarchar(15),
@staffgender nvarchar(25),
@staffpassword nvarchar(50)
as
begin
insert into Staff
(StaffID,StaffName,StaffPhone,StaffGender,StaffPassword) 
values
(@staffid,@staffname,@staffphone,@staffgender,@staffpassword)
end

go
create procedure [delete Staff]
@staffid int
as
begin
delete Staff where 
StaffID=@staffid
end

insert into Staff values
(1,'staff member','0978563412','Male','pass123')
select * from Staff
exec [insert into Staff] 23,'aqwsw','3242','Male','qwerty211'
exec [delete Staff] 23

create table Reception(
ReceptID int not null primary key,
ReceptName nvarchar(50) not null,
ReceptPhone nvarchar(50) not null,
ReceptGender nvarchar(50) not null,
ReceptAddress nvarchar(50) not null,
ReceptPassword nvarchar(50) not null
)
go
create procedure [insert into Reception]
@recepttid int,
@receptname nvarchar(50),
@receptphone nvarchar(15),
@receptgender nvarchar(25),
@receptaddress nvarchar(50),
@receptpassword nvarchar(50)
as
begin
insert into Reception
(ReceptID,ReceptName,ReceptPhone,ReceptGender,ReceptAddress,ReceptPassword) 
values
(@recepttid,@receptname,@receptphone,@receptgender,@receptaddress,@receptpassword)
end

go
create procedure [delete Reception]
@receptid int
as
begin
delete Reception where 
ReceptID=@receptid
end

insert into Reception values
(1,'reception member','2321021','Female','Los Angeles','pass123')
exec [insert into Reception] 123,'demo','911','Female','qwait','my123'
exec [delete Reception] 123
select * from Reception

create table Reservation(
ClientName nvarchar(50) not null,
RoomID int not null,
DateIn nvarchar(50) not null,
DateOut nvarchar(50) not null
Foreign key (ClientName) References Client(ClientName),
Foreign Key (RoomID) References Room(RoomID)
)
go
create procedure [insert into Reservation]
@clientname nvarchar(50),
@roomid int,
@datein nvarchar(25),
@dateout nvarchar(25)
as
begin
insert into Reservation
(ClientName,RoomID,DateIn,DateOut) 
values
(@clientname,@roomid,@datein,@dateout)
end

go
create procedure [delete Reservation]
@roomid int
as
begin
delete Reservation where 
RoomID=@roomid  --reservation deletes based on room id
end

insert into Reservation values
('The First Customer',101,cast(GETDATE()-5 as nvarchar(50)),cast(GETDATE()+5 as nvarchar(50)))

select * from Reservation
exec [insert into Reservation] 'Third',101,'2022-10-14','2022-10-18'
exec [delete Reservation] 101

 go
 create trigger [After Reservation]
 on Reservation 
 after insert
 as
 begin
 declare @roomid int
 select @roomid = RoomID from inserted
 update Room set RoomAvailable='Rented' where RoomID=@roomid 
 end
  go
 create trigger [After Reservation Deleted]
 on Reservation 
 after delete
 as
 begin
 declare @roomid int
 select @roomid = RoomID from deleted
 update Room set RoomAvailable='Free' where RoomID=@roomid 
 end
alter table Reservation disable trigger [After Reservation Deleted] 


