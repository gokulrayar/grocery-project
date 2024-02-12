USE master;
GO

DROP DATABASE IF EXISTS [GroceryAppDB];
GO

CREATE DATABASE [GroceryAppDB];
GO

-- Creating a new database for GroceryApp project.
USE [GroceryAppDB];
GO

-- Creating table for storing user details.
CREATE TABLE [Users]
(
	[Id] INT IDENTITY(1, 1),
	[FirstName] VARCHAR(100) CONSTRAINT [NN_Users_FirstName]NOT NULL,
	[LastName] VARCHAR(100) CONSTRAINT [NN_Users_LastName] NOT NULL,
	[Email] VARCHAR(200) CONSTRAINT [NN_Users_Email] NOT NULL,
	[Password] VARCHAR(256) CONSTRAINT [NN_Users_Password] NOT NULL,
	[Address] VARCHAR(MAX) CONSTRAINT [NN_Users_Address] NOT NULL,
	[Gender] INT CONSTRAINT [NN_Users_Gender] NOT NULL,
	[Role] INT CONSTRAINT [NN_Users_Role] NOT NULL,
	CONSTRAINT [PK_Users_Id] PRIMARY KEY ([Id])
);

-- Creating table for storing payment details.
CREATE TABLE [Payments]
(
	[Id] INT IDENTITY(1, 1),
	[Amount] INT CONSTRAINT [NN_Payments_Amount] NOT NULL,
	[PaymentType] INT CONSTRAINT [NN_Payments_PaymentId] NOT NULL,
	CONSTRAINT [PK_Payments_Id] PRIMARY KEY ([Id])
);

-- Creating table for storing order details.
CREATE TABLE [Orders]
(
	[Id] INT IDENTITY(1, 1),
	[UserId] INT,
	[PaymentId] INT,
	[OrderedAt] DATETIME CONSTRAINT [NN_Orders_OrderedAt] NOT NULL,
	CONSTRAINT [PK_Orders_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [CHK_Orders_OrderedAt] CHECK([OrderedAt] <= GETUTCDATE()),
	CONSTRAINT [FK_Orders.UserId_Users.Id] FOREIGN KEY ([UserId])
	REFERENCES [Users]([Id]),
	CONSTRAINT [FK_Orders.PaymentId_Payments.Id] FOREIGN KEY ([PaymentId])
	REFERENCES [Payments]([Id])
);

-- Creating table for storing product details.
CREATE TABLE [Products]
(
	[Id] INT IDENTITY(1, 1),
	[Name] VARCHAR(100) CONSTRAINT [NN_Products_Name] NOT NULL,
	[Price] INT CONSTRAINT [NN_Products_Price] NOT NULL,
	[Stock] INT CONSTRAINT [NN_Products_Stock] NOT NULL,
	[ImageUrl] VARCHAR(MAX),
	[Status] INT,
	CONSTRAINT [PK_Products_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [CHK_Products_Price] CHECK([Price] >= 0),
	CONSTRAINT [CHK_Products_Stock] CHECK([Stock] >= 0)
);

-- Creating table for storing cart details.
CREATE TABLE [Carts]
(
	[Id] INT IDENTITY(1, 1),
	[UserId] INT,
	CONSTRAINT [PK_Carts_Id] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Carts.UserId_Users.Id] FOREIGN KEY ([UserId])
	REFERENCES [Users]([Id])
);

-- Creating table for storing products related to a particular order.
CREATE TABLE Orders_Products
(
	[Id] INT IDENTITY(1, 1),
	[OrderId] INT,
	[ProductId] INT,
	CONSTRAINT [PK_Orders_Products] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Orders_Products.OrderId_Orders.Id] FOREIGN KEY ([OrderId])
	REFERENCES [Orders]([Id]),
	CONSTRAINT [FK_Orders_Products.ProductId_Products.Id] FOREIGN KEY ([ProductId])
	REFERENCES [Products]([Id])
);

-- Creating table for storing products related to a particular cart.
CREATE TABLE [Carts_Products]
(
	[Id] INT IDENTITY(1, 1),
	[CartId] INT,
	[ProductId] INT,
	CONSTRAINT [PK_Carts_Products] PRIMARY KEY ([Id]),
	CONSTRAINT [FK_Carts_Products.CartId_Carts.Id] FOREIGN KEY ([CartId])
	REFERENCES [Carts]([Id]),
	CONSTRAINT [FK_Carts_Products.ProductId_Products.Id] FOREIGN KEY ([ProductId])
	REFERENCES [Products]([Id])
);

-- We are creating one unique login for using across developers
-- in their local machine. So that no need to change connection string
-- inside appsettings.json again and again.
IF NOT EXISTS
(
SELECT [name]
FROM [sys].[server_principals]
WHERE [name] = 'devuser'
)
BEGIN
CREATE LOGIN [devuser]
WITH PASSWORD = 'dev123';
END
GO

-- Creating an user for the login whose default schema will be dbo.
CREATE USER [devuser]
FOR LOGIN [devuser]
WITH DEFAULT_SCHEMA = [dbo];
GO

-- For GroceryAppDB give the permission to the user as database owner (db_owner)
-- so that the user can execute any kind of query in the database
ALTER  ROLE [db_owner]
ADD MEMBER [devuser];
GO