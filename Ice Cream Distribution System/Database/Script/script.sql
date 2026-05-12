USE [master]
GO
/****** Object:  Database [IceCreamDistributionDB]    Script Date: 12/05/2026 3:27:36 PM ******/
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'IceCreamDistributionDB')
BEGIN
CREATE DATABASE [IceCreamDistributionDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'IceCreamDistributionDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\IceCreamDistributionDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'IceCreamDistributionDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\IceCreamDistributionDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
END
GO
ALTER DATABASE [IceCreamDistributionDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [IceCreamDistributionDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [IceCreamDistributionDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [IceCreamDistributionDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [IceCreamDistributionDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [IceCreamDistributionDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [IceCreamDistributionDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET RECOVERY FULL 
GO
ALTER DATABASE [IceCreamDistributionDB] SET  MULTI_USER 
GO
ALTER DATABASE [IceCreamDistributionDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [IceCreamDistributionDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [IceCreamDistributionDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [IceCreamDistributionDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [IceCreamDistributionDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [IceCreamDistributionDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'IceCreamDistributionDB', N'ON'
GO
ALTER DATABASE [IceCreamDistributionDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [IceCreamDistributionDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [IceCreamDistributionDB]
GO
/****** Object:  UserDefinedTableType [dbo].[List]    Script Date: 12/05/2026 3:27:37 PM ******/
IF NOT EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'List' AND ss.name = N'dbo')
CREATE TYPE [dbo].[List] AS TABLE(
	[ID] [int] NOT NULL
)
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Areas]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Areas](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Notes] [nvarchar](500) NULL,
 CONSTRAINT [PK__Areas__3214EC27171E31AC] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Cars]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cars]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Cars](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[AreaID] [int] NOT NULL,
	[CarDetails] [nvarchar](500) NULL,
 CONSTRAINT [PK__Cars__3214EC27A292FF42] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Drivers]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Drivers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Drivers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CarID] [smallint] NOT NULL,
 CONSTRAINT [PK__Drivers__3214EC270252AD56] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Drivers__1788CCADEAFB7C1C] UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[InvoiceRecords]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[InvoiceRecords](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[Count] [smallint] NOT NULL,
	[ProductPrice] [decimal](18, 4) NOT NULL,
	[Total]  AS ([Count]*[ProductPrice]) PERSISTED NOT NULL,
 CONSTRAINT [PK__InvoiceR__3214EC27CF6C5061] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Invoices]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Invoices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[CarID] [smallint] NOT NULL,
	[StoreID] [int] NOT NULL,
	[Notes] [nvarchar](250) NULL,
	[Total] [decimal](18, 4) NOT NULL,
 CONSTRAINT [PK__Invoices__3214EC277A90C751] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Payments]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Payments](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PayedValue] [decimal](18, 4) NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[RepresentativeID] [int] NOT NULL,
	[StoreID] [int] NOT NULL,
	[Notes] [nvarchar](250) NULL,
 CONSTRAINT [PK__Payments__3214EC2751AE37D3] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[People]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[People]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[People](
	[PersonID] [int] IDENTITY(1,1) NOT NULL,
	[PersonName] [nvarchar](200) NOT NULL,
	[Address] [nvarchar](300) NULL,
	[Email] [nvarchar](255) NULL,
	[Phone] [varchar](20) NULL,
 CONSTRAINT [PK__People__AA2FFB854B41C649] PRIMARY KEY CLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[ProductTypeID] [smallint] NOT NULL,
	[Price] [decimal](18, 4) NOT NULL,
 CONSTRAINT [PK__Products__3214EC27E05FA1C5] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ProductTypes]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProductTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ProductTypes](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
 CONSTRAINT [PK__ProductT__3214EC27BB1856AB] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Representatives]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Representatives]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Representatives](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[CarID] [smallint] NOT NULL,
 CONSTRAINT [PK__Represen__3214EC27ED28EF31] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Represen__1788CCADCA5AF74E] UNIQUE NONCLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[RepresentativesStock]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RepresentativesStock]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[RepresentativesStock](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[RepresentativeID] [int] NOT NULL,
	[Count] [smallint] NOT NULL,
 CONSTRAINT [PK__Represen__3214EC2758213D3F] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Shifts]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Shifts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Shifts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FromDate] [datetime2](7) NOT NULL,
	[ToDate] [datetime2](7) NULL,
	[RepresentativeID] [int] NOT NULL,
	[DriverID] [int] NOT NULL,
	[CarID] [smallint] NOT NULL,
 CONSTRAINT [PK__Shifts__3214EC2764D6B26F] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Stores]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Stores]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Stores](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Balance] [decimal](18, 4) NOT NULL,
	[AreaID] [int] NOT NULL,
	[OwnerID] [int] NOT NULL,
 CONSTRAINT [PK__Stores__3214EC27F186817C] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PersonID] [int] NOT NULL,
	[PasswordHash] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK__Users__1788CCAC83799C9E] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Users__AA2FFB84EECC5C9C] UNIQUE NONCLUSTERED 
(
	[PersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__Users__C9F28456322FAA8B] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Index [IX_Payments_Representative]    Script Date: 12/05/2026 3:27:37 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Payments]') AND name = N'IX_Payments_Representative')
CREATE NONCLUSTERED INDEX [IX_Payments_Representative] ON [dbo].[Payments]
(
	[RepresentativeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Products_Name]    Script Date: 12/05/2026 3:27:37 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND name = N'IX_Products_Name')
CREATE NONCLUSTERED INDEX [IX_Products_Name] ON [dbo].[Products]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_RepStock_Rep_Product]    Script Date: 12/05/2026 3:27:37 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[RepresentativesStock]') AND name = N'IX_RepStock_Rep_Product')
CREATE NONCLUSTERED INDEX [IX_RepStock_Rep_Product] ON [dbo].[RepresentativesStock]
(
	[RepresentativeID] ASC,
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Shifts_Dates]    Script Date: 12/05/2026 3:27:37 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Shifts]') AND name = N'IX_Shifts_Dates')
CREATE NONCLUSTERED INDEX [IX_Shifts_Dates] ON [dbo].[Shifts]
(
	[FromDate] ASC,
	[ToDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Stores_Area]    Script Date: 12/05/2026 3:27:37 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Stores]') AND name = N'IX_Stores_Area')
CREATE NONCLUSTERED INDEX [IX_Stores_Area] ON [dbo].[Stores]
(
	[AreaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_PersonID]    Script Date: 12/05/2026 3:27:37 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_Users_PersonID')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_PersonID] ON [dbo].[Users]
(
	[PersonID] ASC
)
WHERE ([IsDeleted]=(0))
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Users_UserName]    Script Date: 12/05/2026 3:27:37 PM ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND name = N'IX_Users_UserName')
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_UserName] ON [dbo].[Users]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Invoices__Date__0D7A0286]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Invoices] ADD  CONSTRAINT [DF__Invoices__Date__0D7A0286]  DEFAULT (getdate()) FOR [Date]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Invoices__Total__10566F31]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Invoices] ADD  CONSTRAINT [DF__Invoices__Total__10566F31]  DEFAULT ((0)) FOR [Total]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Payments__Date__66603565]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Payments] ADD  CONSTRAINT [DF__Payments__Date__66603565]  DEFAULT (getdate()) FOR [Date]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Represent__Count__6D0D32F4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[RepresentativesStock] ADD  CONSTRAINT [DF__Represent__Count__6D0D32F4]  DEFAULT ((0)) FOR [Count]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Stores__Balance__3D5E1FD2]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Stores] ADD  CONSTRAINT [DF__Stores__Balance__3D5E1FD2]  DEFAULT ((0)) FOR [Balance]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Users__IsActive__4AB81AF0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__IsActive__4AB81AF0]  DEFAULT ((1)) FOR [IsActive]
END
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DF__Users__IsDeleted__4BAC3F29]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF__Users__IsDeleted__4BAC3F29]  DEFAULT ((0)) FOR [IsDeleted]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Cars__AreaID__4222D4EF]') AND parent_object_id = OBJECT_ID(N'[dbo].[Cars]'))
ALTER TABLE [dbo].[Cars]  WITH CHECK ADD  CONSTRAINT [FK__Cars__AreaID__4222D4EF] FOREIGN KEY([AreaID])
REFERENCES [dbo].[Areas] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Cars__AreaID__4222D4EF]') AND parent_object_id = OBJECT_ID(N'[dbo].[Cars]'))
ALTER TABLE [dbo].[Cars] CHECK CONSTRAINT [FK__Cars__AreaID__4222D4EF]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Drivers__CarID__5535A963]') AND parent_object_id = OBJECT_ID(N'[dbo].[Drivers]'))
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK__Drivers__CarID__5535A963] FOREIGN KEY([CarID])
REFERENCES [dbo].[Cars] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Drivers__CarID__5535A963]') AND parent_object_id = OBJECT_ID(N'[dbo].[Drivers]'))
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK__Drivers__CarID__5535A963]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Drivers__UserID__5441852A]') AND parent_object_id = OBJECT_ID(N'[dbo].[Drivers]'))
ALTER TABLE [dbo].[Drivers]  WITH CHECK ADD  CONSTRAINT [FK__Drivers__UserID__5441852A] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Drivers__UserID__5441852A]') AND parent_object_id = OBJECT_ID(N'[dbo].[Drivers]'))
ALTER TABLE [dbo].[Drivers] CHECK CONSTRAINT [FK__Drivers__UserID__5441852A]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__InvoiceRe__Invoi__1332DBDC]') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]'))
ALTER TABLE [dbo].[InvoiceRecords]  WITH CHECK ADD  CONSTRAINT [FK__InvoiceRe__Invoi__1332DBDC] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Invoices] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__InvoiceRe__Invoi__1332DBDC]') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]'))
ALTER TABLE [dbo].[InvoiceRecords] CHECK CONSTRAINT [FK__InvoiceRe__Invoi__1332DBDC]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__InvoiceRe__Produ__14270015]') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]'))
ALTER TABLE [dbo].[InvoiceRecords]  WITH CHECK ADD  CONSTRAINT [FK__InvoiceRe__Produ__14270015] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__InvoiceRe__Produ__14270015]') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]'))
ALTER TABLE [dbo].[InvoiceRecords] CHECK CONSTRAINT [FK__InvoiceRe__Produ__14270015]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Invoices__CarID__0E6E26BF]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoices]'))
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK__Invoices__CarID__0E6E26BF] FOREIGN KEY([CarID])
REFERENCES [dbo].[Cars] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Invoices__CarID__0E6E26BF]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoices]'))
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK__Invoices__CarID__0E6E26BF]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Invoices__StoreI__0F624AF8]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoices]'))
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK__Invoices__StoreI__0F624AF8] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Stores] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Invoices__StoreI__0F624AF8]') AND parent_object_id = OBJECT_ID(N'[dbo].[Invoices]'))
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK__Invoices__StoreI__0F624AF8]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Payments__Repres__6754599E]') AND parent_object_id = OBJECT_ID(N'[dbo].[Payments]'))
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK__Payments__Repres__6754599E] FOREIGN KEY([RepresentativeID])
REFERENCES [dbo].[Representatives] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Payments__Repres__6754599E]') AND parent_object_id = OBJECT_ID(N'[dbo].[Payments]'))
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK__Payments__Repres__6754599E]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Payments__StoreI__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[Payments]'))
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [FK__Payments__StoreI__68487DD7] FOREIGN KEY([StoreID])
REFERENCES [dbo].[Stores] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Payments__StoreI__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[Payments]'))
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [FK__Payments__StoreI__68487DD7]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Products__Produc__44FF419A]') AND parent_object_id = OBJECT_ID(N'[dbo].[Products]'))
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK__Products__Produc__44FF419A] FOREIGN KEY([ProductTypeID])
REFERENCES [dbo].[ProductTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Products__Produc__44FF419A]') AND parent_object_id = OBJECT_ID(N'[dbo].[Products]'))
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK__Products__Produc__44FF419A]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Represent__CarID__5070F446]') AND parent_object_id = OBJECT_ID(N'[dbo].[Representatives]'))
ALTER TABLE [dbo].[Representatives]  WITH CHECK ADD  CONSTRAINT [FK__Represent__CarID__5070F446] FOREIGN KEY([CarID])
REFERENCES [dbo].[Cars] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Represent__CarID__5070F446]') AND parent_object_id = OBJECT_ID(N'[dbo].[Representatives]'))
ALTER TABLE [dbo].[Representatives] CHECK CONSTRAINT [FK__Represent__CarID__5070F446]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Represent__UserI__4F7CD00D]') AND parent_object_id = OBJECT_ID(N'[dbo].[Representatives]'))
ALTER TABLE [dbo].[Representatives]  WITH CHECK ADD  CONSTRAINT [FK__Represent__UserI__4F7CD00D] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Represent__UserI__4F7CD00D]') AND parent_object_id = OBJECT_ID(N'[dbo].[Representatives]'))
ALTER TABLE [dbo].[Representatives] CHECK CONSTRAINT [FK__Represent__UserI__4F7CD00D]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Represent__Produ__6B24EA82]') AND parent_object_id = OBJECT_ID(N'[dbo].[RepresentativesStock]'))
ALTER TABLE [dbo].[RepresentativesStock]  WITH CHECK ADD  CONSTRAINT [FK__Represent__Produ__6B24EA82] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Represent__Produ__6B24EA82]') AND parent_object_id = OBJECT_ID(N'[dbo].[RepresentativesStock]'))
ALTER TABLE [dbo].[RepresentativesStock] CHECK CONSTRAINT [FK__Represent__Produ__6B24EA82]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Represent__Repre__6C190EBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[RepresentativesStock]'))
ALTER TABLE [dbo].[RepresentativesStock]  WITH CHECK ADD  CONSTRAINT [FK__Represent__Repre__6C190EBB] FOREIGN KEY([RepresentativeID])
REFERENCES [dbo].[Representatives] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Represent__Repre__6C190EBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[RepresentativesStock]'))
ALTER TABLE [dbo].[RepresentativesStock] CHECK CONSTRAINT [FK__Represent__Repre__6C190EBB]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Shifts__CarID__59FA5E80]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shifts]'))
ALTER TABLE [dbo].[Shifts]  WITH CHECK ADD  CONSTRAINT [FK__Shifts__CarID__59FA5E80] FOREIGN KEY([CarID])
REFERENCES [dbo].[Cars] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Shifts__CarID__59FA5E80]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shifts]'))
ALTER TABLE [dbo].[Shifts] CHECK CONSTRAINT [FK__Shifts__CarID__59FA5E80]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Shifts__DriverID__59063A47]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shifts]'))
ALTER TABLE [dbo].[Shifts]  WITH CHECK ADD  CONSTRAINT [FK__Shifts__DriverID__59063A47] FOREIGN KEY([DriverID])
REFERENCES [dbo].[Drivers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Shifts__DriverID__59063A47]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shifts]'))
ALTER TABLE [dbo].[Shifts] CHECK CONSTRAINT [FK__Shifts__DriverID__59063A47]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Shifts__Represen__5812160E]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shifts]'))
ALTER TABLE [dbo].[Shifts]  WITH CHECK ADD  CONSTRAINT [FK__Shifts__Represen__5812160E] FOREIGN KEY([RepresentativeID])
REFERENCES [dbo].[Representatives] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Shifts__Represen__5812160E]') AND parent_object_id = OBJECT_ID(N'[dbo].[Shifts]'))
ALTER TABLE [dbo].[Shifts] CHECK CONSTRAINT [FK__Shifts__Represen__5812160E]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Stores__AreaID__3E52440B]') AND parent_object_id = OBJECT_ID(N'[dbo].[Stores]'))
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK__Stores__AreaID__3E52440B] FOREIGN KEY([AreaID])
REFERENCES [dbo].[Areas] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Stores__AreaID__3E52440B]') AND parent_object_id = OBJECT_ID(N'[dbo].[Stores]'))
ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK__Stores__AreaID__3E52440B]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Stores__OwnerID__3F466844]') AND parent_object_id = OBJECT_ID(N'[dbo].[Stores]'))
ALTER TABLE [dbo].[Stores]  WITH CHECK ADD  CONSTRAINT [FK__Stores__OwnerID__3F466844] FOREIGN KEY([OwnerID])
REFERENCES [dbo].[People] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Stores__OwnerID__3F466844]') AND parent_object_id = OBJECT_ID(N'[dbo].[Stores]'))
ALTER TABLE [dbo].[Stores] CHECK CONSTRAINT [FK__Stores__OwnerID__3F466844]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Users__PersonID__49C3F6B7]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK__Users__PersonID__49C3F6B7] FOREIGN KEY([PersonID])
REFERENCES [dbo].[People] ([PersonID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Users__PersonID__49C3F6B7]') AND parent_object_id = OBJECT_ID(N'[dbo].[Users]'))
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK__Users__PersonID__49C3F6B7]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_InvoiceRecords]') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]'))
ALTER TABLE [dbo].[InvoiceRecords]  WITH CHECK ADD  CONSTRAINT [CK_InvoiceRecords] CHECK  (([Count]>(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_InvoiceRecords]') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]'))
ALTER TABLE [dbo].[InvoiceRecords] CHECK CONSTRAINT [CK_InvoiceRecords]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_InvoiceRecords_1]') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]'))
ALTER TABLE [dbo].[InvoiceRecords]  WITH CHECK ADD  CONSTRAINT [CK_InvoiceRecords_1] CHECK  (([ProductPrice]>(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_InvoiceRecords_1]') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]'))
ALTER TABLE [dbo].[InvoiceRecords] CHECK CONSTRAINT [CK_InvoiceRecords_1]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_InvoiceRecords_2]') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]'))
ALTER TABLE [dbo].[InvoiceRecords]  WITH CHECK ADD  CONSTRAINT [CK_InvoiceRecords_2] CHECK  (([Total]>(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_InvoiceRecords_2]') AND parent_object_id = OBJECT_ID(N'[dbo].[InvoiceRecords]'))
ALTER TABLE [dbo].[InvoiceRecords] CHECK CONSTRAINT [CK_InvoiceRecords_2]
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Payments]') AND parent_object_id = OBJECT_ID(N'[dbo].[Payments]'))
ALTER TABLE [dbo].[Payments]  WITH CHECK ADD  CONSTRAINT [CK_Payments] CHECK  (([PayedValue]>(0)))
GO
IF  EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[CK_Payments]') AND parent_object_id = OBJECT_ID(N'[dbo].[Payments]'))
ALTER TABLE [dbo].[Payments] CHECK CONSTRAINT [CK_Payments]
GO
/**types**/

IF NOT EXISTS (SELECT 1 FROM sys.types WHERE name = 'List' AND is_table_type = 1)
BEGIN
    CREATE TYPE dbo.List AS TABLE(
        ID INT NOT NULL
    );
END
GO
/****** Object:  StoredProcedure [dbo].[SP_Store_CalcualteBalance]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_Store_CalcualteBalance]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_Store_CalcualteBalance] AS' 
END
GO
ALTER   PROCEDURE [dbo].[SP_Store_CalcualteBalance]
	@StoreIDs dbo.List READONLY
AS
BEGIN

	WITH StoresPayments AS(
		SELECT si.ID AS StoreID,SUM(ISNULL(p.PayedValue,0)) as TotalPayed FROM Payments p RIGHT JOIN @StoreIDs si ON p.StoreID = si.ID
		GROUP BY si.ID
	),
	StoresInvoices AS (
		SELECT si.ID AS StoreID,SUM(ISNULL(ir.Total,0)) as TotalInvoice FROM InvoiceRecords ir
		RIGHT JOIN Invoices i ON ir.InvoiceID = i.ID
		RIGHT JOIN @StoreIDs si ON i.StoreID = si.ID
		GROUP BY si.ID
	),
	StoresBalance AS(
		SELECT si.StoreID, (ISNULL(TotalInvoice, 0) - ISNULL(TotalPayed, 0)) AS Balance
		FROM StoresPayments sp right JOIN StoresInvoices si
		ON sp.StoreID = si.StoreID
	)

	UPDATE Stores
	SET Balance = ISNULL(sb.Balance,0)
	FROM Stores s JOIN StoresBalance sb
	ON s.ID = sb.StoreID

END;
GO
/****** Object:  Trigger [dbo].[Insert_Update_DeleteInvoiceRecords]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[Insert_Update_DeleteInvoiceRecords]'))
EXEC dbo.sp_executesql @statement = N'
CREATE TRIGGER [dbo].[Insert_Update_DeleteInvoiceRecords]
ON [dbo].[InvoiceRecords]
AFTER INSERT , UPDATE, DELETE
AS
BEGIN
	WITH ChangedInvoices  AS (
		SELECT InvoiceID FROM inserted
			UNION
		SELECT InvoiceID FROM deleted
	),
	NewTotals AS(
		SELECT I.InvoiceID, SUM(IR.Total) AS ComputedTotal FROM InvoiceRecords IR Right JOIN ChangedInvoices I ON IR.InvoiceID = I.InvoiceID
		GROUP BY I.InvoiceID
	)
	UPDATE Invoices
    SET Total = ISNULL(t.ComputedTotal, 0)
    FROM Invoices  
    INNER JOIN NewTotals t ON Invoices.ID = t.InvoiceID; -- هنا كان الخطأ

END;' 
GO
ALTER TABLE [dbo].[InvoiceRecords] ENABLE TRIGGER [Insert_Update_DeleteInvoiceRecords]
GO
/****** Object:  Trigger [dbo].[trg_CalcualteStoresBalance_Insert_Update_DeleteInvoiceRecords]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_CalcualteStoresBalance_Insert_Update_DeleteInvoiceRecords]'))
EXEC dbo.sp_executesql @statement = N'CREATE TRIGGER [dbo].[trg_CalcualteStoresBalance_Insert_Update_DeleteInvoiceRecords]
ON [dbo].[InvoiceRecords]
AFTER INSERT , UPDATE, DELETE
AS
BEGIN

	DECLARE @List dbo.List;


	WITH ChangedInvoices AS(
		SELECT InvoiceID FROM inserted 
			UNION 
		SELECT InvoiceID FROM deleted 
	),
	AffectedStores AS (
		SELECT i.StoreID FROM Invoices i JOIN ChangedInvoices ci ON i.ID = ci.InvoiceID
	)
	INSERT INTO @List
	SELECT StoreID FROM AffectedStores;


	EXEC SP_Store_CalcualteBalance
		@StoreIDs = @list;
END;' 
GO
ALTER TABLE [dbo].[InvoiceRecords] ENABLE TRIGGER [trg_CalcualteStoresBalance_Insert_Update_DeleteInvoiceRecords]
GO
/****** Object:  Trigger [dbo].[trg_CalcualteStoresBalance_Insert_Update_DeletePayments]    Script Date: 12/05/2026 3:27:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'[dbo].[trg_CalcualteStoresBalance_Insert_Update_DeletePayments]'))
EXEC dbo.sp_executesql @statement = N'CREATE TRIGGER [dbo].[trg_CalcualteStoresBalance_Insert_Update_DeletePayments]
ON [dbo].[Payments]
AFTER INSERT , UPDATE, DELETE
AS
BEGIN

	DECLARE @List dbo.List;


	WITH AffectedStores AS(
		SELECT StoreID FROM inserted 
			UNION 
		SELECT StoreID FROM deleted 
	)
	INSERT INTO @List
	SELECT StoreID FROM AffectedStores;


	EXEC SP_Store_CalcualteBalance
		@StoreIDs = @list;
END;' 
GO
ALTER TABLE [dbo].[Payments] ENABLE TRIGGER [trg_CalcualteStoresBalance_Insert_Update_DeletePayments]
GO
USE [master]
GO
ALTER DATABASE [IceCreamDistributionDB] SET  READ_WRITE 
GO
