create database DbPseudoCloudStorage
use DbPseudoCloudStorage



CREATE TABLE Users
(
    [Id] int primary key identity,
    [Login] VARCHAR(20) not null,
    [Password] VARCHAR(20) not null
)


CREATE TABLE UserFiles
(
    [File_id] int primary key identity,
    [Name] NVARCHAR(20) not null,
    [user_id] int not null REFERENCES Users (Id) on update cascade on delete cascade
)


select * from Users
select * from UserFiles


INSERT INTO Users VALUES ('Login2','2')