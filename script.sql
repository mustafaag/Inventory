USE [master]
GO
/****** Object:  Database [Inventory]    Script Date: 2/25/2019 11:34:03 PM ******/
CREATE DATABASE [Inventory]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Inventory', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Inventory.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Inventory_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\Inventory_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Inventory] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Inventory].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Inventory] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Inventory] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Inventory] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Inventory] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Inventory] SET ARITHABORT OFF 
GO
ALTER DATABASE [Inventory] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Inventory] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Inventory] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Inventory] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Inventory] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Inventory] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Inventory] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Inventory] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Inventory] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Inventory] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Inventory] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Inventory] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Inventory] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Inventory] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Inventory] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Inventory] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Inventory] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Inventory] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Inventory] SET  MULTI_USER 
GO
ALTER DATABASE [Inventory] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Inventory] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Inventory] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Inventory] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Inventory] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Inventory] SET QUERY_STORE = OFF
GO
USE [Inventory]
GO
/****** Object:  Table [dbo].[Depots]    Script Date: 2/25/2019 11:34:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Depots](
	[Depot_ID] [int] IDENTITY(1,1) NOT NULL,
	[Depot_Name] [varchar](50) NOT NULL,
	[Depot_Desc] [varchar](150) NULL,
	[Deleted] [bit] NULL,
	[Created_By] [int] NULL,
	[Created_Date] [datetime] NULL,
	[Depot_Type] [tinyint] NULL,
	[Address] [varchar](100) NULL,
 CONSTRAINT [PK_Depots] PRIMARY KEY CLUSTERED 
(
	[Depot_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 2/25/2019 11:34:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[Group_ID] [int] IDENTITY(1,1) NOT NULL,
	[Group_Name] [varchar](50) NOT NULL,
	[Group_desc] [varchar](150) NULL,
	[Deleted] [bit] NULL,
	[Date_Created] [datetime] NULL,
	[Created_By] [int] NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[Group_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Njesite]    Script Date: 2/25/2019 11:34:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Njesite](
	[Njesia_ID] [int] IDENTITY(1,1) NOT NULL,
	[Njesia_Name] [varchar](50) NOT NULL,
	[Njesia_Desc] [varchar](150) NULL,
	[Deleted] [bit] NULL,
	[Created_Date] [datetime] NULL,
	[Created_By] [int] NULL,
 CONSTRAINT [PK_Njesite] PRIMARY KEY CLUSTERED 
(
	[Njesia_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2/25/2019 11:34:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Product_ID] [int] IDENTITY(1,1) NOT NULL,
	[Product_Name] [varchar](50) NOT NULL,
	[Product_Desc] [varchar](150) NULL,
	[Deleted] [bit] NULL,
	[Created_By] [int] NULL,
	[Created_Date] [datetime] NULL,
	[Depot_ID] [int] NULL,
	[Sub_Group_ID] [int] NULL,
	[Quantity] [decimal](18, 2) NULL,
	[Price] [decimal](18, 2) NULL,
	[Njesia_ID] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Product_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubGroups]    Script Date: 2/25/2019 11:34:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubGroups](
	[SubGroup_ID] [int] IDENTITY(1,1) NOT NULL,
	[SubGroup_Name] [varchar](50) NOT NULL,
	[SubGroup_Desc] [varchar](150) NULL,
	[Group_ID] [int] NULL,
	[Deleted] [bit] NULL,
	[Date_Created] [datetime] NULL,
	[Created_By] [int] NULL,
 CONSTRAINT [PK_SubGroups] PRIMARY KEY CLUSTERED 
(
	[SubGroup_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/25/2019 11:34:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[User_ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[First_Name] [varchar](50) NULL,
	[Last_Name] [varchar](50) NULL,
	[Password] [varchar](150) NULL,
	[User_Type] [tinyint] NULL,
	[Deleted] [bit] NULL,
	[Active] [bit] NULL,
	[Date_Created] [datetime] NULL,
	[Created_By] [int] NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[User_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Depots] ADD  CONSTRAINT [DF_Depots_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Depots] ADD  CONSTRAINT [DF_Depots_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Groups_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Groups_Date_Created]  DEFAULT (getdate()) FOR [Date_Created]
GO
ALTER TABLE [dbo].[Njesite] ADD  CONSTRAINT [DF_Njesite_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Njesite] ADD  CONSTRAINT [DF_Njesite_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Quantity]  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_Price]  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[SubGroups] ADD  CONSTRAINT [DF_SubGroups_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[SubGroups] ADD  CONSTRAINT [DF_SubGroups_Date_Created]  DEFAULT (getdate()) FOR [Date_Created]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_Date_Created]  DEFAULT (getdate()) FOR [Date_Created]
GO
USE [master]
GO
ALTER DATABASE [Inventory] SET  READ_WRITE 
GO
