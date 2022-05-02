USE [UltrapacData]
GO

/****** Object:  Table [dbo].[Usuario]    Script Date: 02/05/2022 02:28:05 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuario](
	[Id] [uniqueidentifier] NOT NULL,
	[Gender] [nvarchar](20) NOT NULL,
	[Title] [nvarchar](10) NULL,
	[First] [nvarchar](50) NOT NULL,
	[Last] [nvarchar](50) NOT NULL,
	[Number] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Postcode] [int] NULL,
	[Email] [nvarchar](50) NOT NULL,
	[PictureLarge] [nvarchar](max) NULL,
	[PictureMedium] [nvarchar](max) NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_Id]  DEFAULT (newsequentialid()) FOR [Id]
GO


