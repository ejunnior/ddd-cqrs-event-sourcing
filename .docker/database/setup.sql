CREATE DATABASE $(MSSQL_DB);
GO
USE $(MSSQL_DB);
GO
CREATE LOGIN $(MSSQL_USER) WITH PASSWORD = '$(MSSQL_PASSWORD)';
GO
CREATE USER $(MSSQL_USER) FOR LOGIN $(MSSQL_USER);
GO
ALTER SERVER ROLE sysadmin ADD MEMBER [$(MSSQL_USER)];
GO
GO

/****** Object:  Table [dbo].[BankAccount]    Script Date: 10/7/2020 9:22:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankAccount](
	[Id] [uniqueidentifier] NOT NULL,
	[Bank] [int] NOT NULL,
	[AccountNumber] [varchar](20) NOT NULL,
 CONSTRAINT [PK__BankAcco__3214EC07D1DBB11F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BankPosting]    Script Date: 10/7/2020 9:22:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankPosting](
	[Id] [uniqueidentifier] NOT NULL,
	[PaymentDate] [datetime2](7) NULL,
	[DocumentDate] [datetime2](7) NULL,
	[DocumentNumber] [nvarchar](255) NULL,
	[Amount] [decimal](7, 2) NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[Description] [varchar](80) NOT NULL,
	[CreditorId] [uniqueidentifier] NOT NULL,
	[BankAccountId] [uniqueidentifier] NOT NULL,
	[CategoryId] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK__BankPosting__3214EC0788E32D9B] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/7/2020 9:22:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[Name] [varchar](80) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Creditor]    Script Date: 10/7/2020 9:22:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Creditor](
	[Id] [uniqueidentifier] NOT NULL,
	[Email] [nvarchar](255) NULL,
	[MobilePhone] [nvarchar](255) NULL,
	[Name] [varchar](80) NOT NULL,
 CONSTRAINT [PK__Creditor__3214EC0783B1FE02] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BankPosting]  WITH CHECK ADD  CONSTRAINT [FK_BAPO_BAAC_01] FOREIGN KEY([BankAccountId])
REFERENCES [dbo].[BankAccount] ([Id])
GO
ALTER TABLE [dbo].[BankPosting] CHECK CONSTRAINT [FK_BAPO_BAAC_01]
GO
ALTER TABLE [dbo].[BankPosting]  WITH CHECK ADD  CONSTRAINT [FK_BAPO_CATE_01] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([Id])
GO
ALTER TABLE [dbo].[BankPosting] CHECK CONSTRAINT [FK_BAPO_CATE_01]
GO
ALTER TABLE [dbo].[BankPosting]  WITH CHECK ADD  CONSTRAINT [FK_BAPO_CRED_01] FOREIGN KEY([CreditorId])
REFERENCES [dbo].[Creditor] ([Id])
GO
ALTER TABLE [dbo].[BankPosting] CHECK CONSTRAINT [FK_BAPO_CRED_01]
GO


