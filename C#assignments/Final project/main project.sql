create database newtravelbookingsystem
USE [newtravelbookingsystem]
GO



/****** Object:  Table [dbo].[TravelRequest]    Script Date: 06-12-2022 06:15:02 ******/
SET ANSI_NULLS ON
GO



SET QUOTED_IDENTIFIER ON
GO



CREATE TABLE [dbo].[TravelRequest](
    [Requestid] [int] IDENTITY(1,1) NOT NULL,
    [RequestDate] [date] NULL,
    [FromLocation] [varchar](50) NULL,
    [ToLocation] [varchar](50) NULL,
    [UserId] [int] NULL,
    [CurrentStatus] [varchar](50) NULL,
PRIMARY KEY CLUSTERED
(
    [Requestid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



ALTER TABLE [dbo].[TravelRequest]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([User_id])
GO





USE [newtravelbookingsystem]
GO



/****** Object:  Table [dbo].[Users]    Script Date: 06-12-2022 06:16:03 ******/
SET ANSI_NULLS ON
GO



SET QUOTED_IDENTIFIER ON
GO



CREATE TABLE [dbo].[Users](
    [User_id] [int] NOT NULL,
    [Login_Id] [varchar](50) NULL,
    [Password] [varchar](40) NULL,
    [User_type_id] [int] NULL,
    [Name] [varchar](40) NULL,
    [Manageruserid] [int] NULL,
PRIMARY KEY CLUSTERED
(
    [User_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



ALTER TABLE [dbo].[Users]  WITH CHECK ADD FOREIGN KEY([User_type_id])
REFERENCES [dbo].[Usertype] ([User_type_Id])
GO



ALTER TABLE [dbo].[Users]  WITH CHECK ADD CHECK  ((len([PASSWORD])>(5) AND [PASSWORD] like '%[0-9]%' AND [PASSWORD] like '%[A-Z]%'))
GO





USE [newtravelbookingsystem]
GO



/****** Object:  Table [dbo].[Usertype]    Script Date: 06-12-2022 06:16:26 ******/
SET ANSI_NULLS ON
GO



SET QUOTED_IDENTIFIER ON
GO



CREATE TABLE [dbo].[Usertype](
    [User_type_Id] [int] NOT NULL,
    [description] [varchar](40) NULL,
PRIMARY KEY CLUSTERED
(
    [User_type_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO





USE [newtravelbookingsystem]
GO



/****** Object:  StoredProcedure [dbo].[employeelist]    Script Date: 06-12-2022 06:24:54 ******/
SET ANSI_NULLS ON
GO



SET QUOTED_IDENTIFIER ON
GO



create    procedure [dbo].[employeelist]
as
begin
select users.Name ,TravelRequest.RequestDate,TravelRequest.FromLocation,TravelRequest.ToLocation,TravelRequest.CurrentStatus from Users join Usertype on
Users.User_type_id = Usertype.User_type_Id  join TravelRequest on Usertype.User_type_Id=TravelRequest.UserId
where Usertype.User_type_Id=2
end
GO




USE [newtravelbookingsystem]
GO



/****** Object:  StoredProcedure [dbo].[pendingtable]    Script Date: 06-12-2022 06:25:28 ******/
SET ANSI_NULLS ON
GO



SET QUOTED_IDENTIFIER ON
GO



create      procedure [dbo].[pendingtable]
as
begin
select * from TravelRequest where CurrentStatus='Pending'
end
GO





USE [newtravelbookingsystem]
GO



/****** Object:  StoredProcedure [dbo].[sp_Storeprocuser]    Script Date: 06-12-2022 06:25:52 ******/
SET ANSI_NULLS ON
GO



SET QUOTED_IDENTIFIER ON
GO






create   procedure [dbo].[sp_Storeprocuser]
@Requestid int,
@RequestDate date,
@fromlocation varchar(50),
@ToLocation varchar(50),
@Userid int,
@currentStatus varchar(50),
@Option varchar(50)

as
begin
if(@Option='Insert')
begin
insert into TravelRequest (RequestDate,FromLocation,ToLocation,UserId,CurrentStatus)values(@RequestDate,@fromlocation,@ToLocation,@Userid,@currentStatus)
end
if(@Option='Pending')
begin
Update TravelRequest set CurrentStatus='Approved' where RequestId=@Requestid
end
if(@Option='Reject')
begin
Update TravelRequest set CurrentStatus='Rejected' where RequestId=@Requestid
end
if(@Option='BookTicket')
begin
Update TravelRequest set CurrentStatus='TicketConformed' where RequestId=@Requestid
end
if(@Option='RejectTicket')
begin
Update TravelRequest set CurrentStatus='TicketnotConformed' where RequestId=@Requestid
end
if(@Option='Getrequestid')
begin
select *from TravelRequest where RequestId=@Requestid
end
select *from TravelRequest
end
GO


insert into Usertype values(1,'Admin'),(2,'Employee'),(3,'Manager'),(4,'TravelAgent')


insert into Users values(1,'Anup','A12345',1,'Anup',0)

select * from usertype
select * from users