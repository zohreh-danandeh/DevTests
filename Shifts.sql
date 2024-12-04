USE [Scheduling]
GO

/****** Object:  Table [dbo].[Shifts]    Script Date: 2024-12-03 11:40:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Shifts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Monday] [int] NOT NULL,
	[Tuesday] [int] NOT NULL,
	[Wednesday] [int] NOT NULL,
	[Thursday] [int] NOT NULL,
	[ShiftNum] [int] NOT NULL,
 CONSTRAINT [PK_Shits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


