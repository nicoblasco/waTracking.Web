USE [GEOLOCAL]
GO

/****** Object:  Table [dbo].[Georeference]    Script Date: 3/26/2020 6:04:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Georeference](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Identifier] [nvarchar](50) NOT NULL,
	[TrackerLogId] [bigint] NOT NULL,
	[GeoTrackerId] [int] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Latitude] [nvarchar](50) NULL,
	[Longitude] [nvarchar](50) NULL,
 CONSTRAINT [PK_Georeference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[GeoTracker]    Script Date: 3/26/2020 6:04:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[GeoTracker](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Identifier] [nvarchar](50) NOT NULL,
	[GpsModel] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[CreationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_GeoTracker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SecurityRole]    Script Date: 3/26/2020 6:04:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SecurityRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[descripcion] [varchar](100) NULL,
	[condicion] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SecurityUser]    Script Date: 3/26/2020 6:04:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SecurityUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SecurityRoleId] [int] NOT NULL,
	[nombre] [varchar](100) NOT NULL,
	[tipo_documento] [varchar](20) NULL,
	[num_documento] [varchar](20) NULL,
	[direccion] [varchar](70) NULL,
	[telefono] [varchar](20) NULL,
	[email] [varchar](50) NOT NULL,
	[password_hash] [varbinary](max) NOT NULL,
	[password_salt] [varbinary](max) NOT NULL,
	[condicion] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[TrackerLog]    Script Date: 3/26/2020 6:04:38 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TrackerLog](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_TrackerLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[SecurityRole] ADD  DEFAULT ((1)) FOR [condicion]
GO

ALTER TABLE [dbo].[SecurityUser] ADD  DEFAULT ((1)) FOR [condicion]
GO

ALTER TABLE [dbo].[Georeference]  WITH CHECK ADD  CONSTRAINT [FK_Georeference_GeoTracker] FOREIGN KEY([GeoTrackerId])
REFERENCES [dbo].[GeoTracker] ([Id])
GO

ALTER TABLE [dbo].[Georeference] CHECK CONSTRAINT [FK_Georeference_GeoTracker]
GO

ALTER TABLE [dbo].[Georeference]  WITH CHECK ADD  CONSTRAINT [FK_Georeference_TrackerLog] FOREIGN KEY([TrackerLogId])
REFERENCES [dbo].[TrackerLog] ([Id])
GO

ALTER TABLE [dbo].[Georeference] CHECK CONSTRAINT [FK_Georeference_TrackerLog]
GO

ALTER TABLE [dbo].[SecurityUser]  WITH CHECK ADD FOREIGN KEY([SecurityRoleId])
REFERENCES [dbo].[SecurityRole] ([Id])
GO


