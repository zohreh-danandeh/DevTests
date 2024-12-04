USE [Scheduling]
GO

/****** Object:  Table [dbo].[Employee]    Script Date: 2024-12-03 11:30:29 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Employee](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Monday] [bit] NOT NULL,
	[Tuesday] [bit] NOT NULL,
	[Wednesday] [bit] NOT NULL,
	[Thursday] [bit] NOT NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


