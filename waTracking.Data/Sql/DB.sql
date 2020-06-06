USE [GEOLOCAL]
GO

/****** Object:  Table [dbo].[Company]    Script Date: 29/05/2020 22:15:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Company](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CreationDate] [datetime] NOT NULL,
	[Enabled] [bit] NOT NULL,
	[IsAdmin] [bit] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ConfigScreen]    Script Date: 29/05/2020 22:15:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConfigScreen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Entity] [nvarchar](50) NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_Screen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ConfigScreenField]    Script Date: 29/05/2020 22:15:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConfigScreenField](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConfigScreenId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Required] [bit] NOT NULL,
	[FieldName] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Visible] [bit] NOT NULL,
	[DefaultValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_ConfigScreenField] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Department]    Script Date: 29/05/2020 22:15:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[DepartmentChild]    Script Date: 29/05/2020 22:15:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[DepartmentChild](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentId] [int] NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_DepartmentChild] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Georeference]    Script Date: 29/05/2020 22:15:30 ******/
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

/****** Object:  Table [dbo].[GeoTracker]    Script Date: 29/05/2020 22:15:31 ******/
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
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_GeoTracker] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SecurityAction]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SecurityAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ConfigScreenId] [int] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Enabled] [bit] NULL,
 CONSTRAINT [PK_ConfigAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SecurityRole]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SecurityRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Description] [varchar](100) NULL,
	[Enabled] [bit] NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK__Security__3214EC071301632D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SecurityRoleAction]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SecurityRoleAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SecurityRoleId] [int] NOT NULL,
	[SecurityActionId] [int] NOT NULL,
 CONSTRAINT [PK_SecurityRoleAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SecurityUser]    Script Date: 29/05/2020 22:15:31 ******/
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
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK__Security__3214EC074EE7C13F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Service]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Service](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ServiceTypeId] [int] NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ServiceType]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ServiceType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](1000) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_ServiceType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Status]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Status](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_Status] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SystemAction]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SystemAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemScreenId] [int] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_SystemAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SystemRole]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SystemRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](30) NOT NULL,
	[Description] [nvarchar](100) NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_SystemRole] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SystemRoleAction]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SystemRoleAction](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemRoleId] [int] NOT NULL,
	[SystemActionId] [int] NOT NULL,
 CONSTRAINT [PK_SystemRoleAction] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SystemScreen]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SystemScreen](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[ParentId] [int] NULL,
	[Orden] [int] NOT NULL,
	[Entity] [nvarchar](50) NULL,
	[Path] [nvarchar](100) NULL,
	[Icon] [nvarchar](50) NULL,
 CONSTRAINT [PK_SystemScreen] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[SystemScreenField]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SystemScreenField](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SystemScreenId] [int] NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Required] [bit] NOT NULL,
	[FieldName] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
	[Visible] [bit] NOT NULL,
	[DefaultValue] [nvarchar](50) NULL,
 CONSTRAINT [PK_SystemScreenField] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[TrackerLog]    Script Date: 29/05/2020 22:15:31 ******/
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

/****** Object:  Table [dbo].[TypeOfUse]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TypeOfUse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_TypeOfUse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VehicleBrand]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VehicleBrand](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_VehicleBrand] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VehicleDocumentation]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VehicleDocumentation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_VehicleDocumentation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VehicleModel]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VehicleModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[VehicleBrandId] [int] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_VehicleModel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VehicleSegment]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VehicleSegment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_VehicleSegment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VehicleType]    Script Date: 29/05/2020 22:15:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VehicleType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Enabled] [bit] NOT NULL,
 CONSTRAINT [PK_VehicleType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[SecurityRole] ADD  CONSTRAINT [DF__SecurityR__condi__46E78A0C]  DEFAULT ((1)) FOR [Enabled]
GO

ALTER TABLE [dbo].[SecurityUser] ADD  CONSTRAINT [DF__SecurityU__condi__5070F446]  DEFAULT ((1)) FOR [condicion]
GO

ALTER TABLE [dbo].[ConfigScreen]  WITH CHECK ADD  CONSTRAINT [FK_ConfigScreen_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO

ALTER TABLE [dbo].[ConfigScreen] CHECK CONSTRAINT [FK_ConfigScreen_Company]
GO

ALTER TABLE [dbo].[ConfigScreenField]  WITH CHECK ADD  CONSTRAINT [FK_ConfigScreenField_ConfigScreen] FOREIGN KEY([ConfigScreenId])
REFERENCES [dbo].[ConfigScreen] ([Id])
GO

ALTER TABLE [dbo].[ConfigScreenField] CHECK CONSTRAINT [FK_ConfigScreenField_ConfigScreen]
GO

ALTER TABLE [dbo].[DepartmentChild]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentChild_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Id])
GO

ALTER TABLE [dbo].[DepartmentChild] CHECK CONSTRAINT [FK_DepartmentChild_Department]
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

ALTER TABLE [dbo].[GeoTracker]  WITH CHECK ADD  CONSTRAINT [FK_GeoTracker_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO

ALTER TABLE [dbo].[GeoTracker] CHECK CONSTRAINT [FK_GeoTracker_Company]
GO

ALTER TABLE [dbo].[SecurityAction]  WITH CHECK ADD  CONSTRAINT [FK_ConfigAction_ConfigScreen] FOREIGN KEY([ConfigScreenId])
REFERENCES [dbo].[ConfigScreen] ([Id])
GO

ALTER TABLE [dbo].[SecurityAction] CHECK CONSTRAINT [FK_ConfigAction_ConfigScreen]
GO

ALTER TABLE [dbo].[SecurityRole]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRole_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([Id])
GO

ALTER TABLE [dbo].[SecurityRole] CHECK CONSTRAINT [FK_SecurityRole_Company]
GO

ALTER TABLE [dbo].[SecurityRoleAction]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRoleAction_SecurityAction] FOREIGN KEY([SecurityActionId])
REFERENCES [dbo].[SecurityAction] ([Id])
GO

ALTER TABLE [dbo].[SecurityRoleAction] CHECK CONSTRAINT [FK_SecurityRoleAction_SecurityAction]
GO

ALTER TABLE [dbo].[SecurityRoleAction]  WITH CHECK ADD  CONSTRAINT [FK_SecurityRoleAction_SecurityRole] FOREIGN KEY([SecurityRoleId])
REFERENCES [dbo].[SecurityRole] ([Id])
GO

ALTER TABLE [dbo].[SecurityRoleAction] CHECK CONSTRAINT [FK_SecurityRoleAction_SecurityRole]
GO

ALTER TABLE [dbo].[SecurityUser]  WITH CHECK ADD  CONSTRAINT [FK__SecurityU__Secur__5165187F] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[SecurityUser] ([Id])
GO

ALTER TABLE [dbo].[SecurityUser] CHECK CONSTRAINT [FK__SecurityU__Secur__5165187F]
GO

ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceType] FOREIGN KEY([ServiceTypeId])
REFERENCES [dbo].[ServiceType] ([Id])
GO

ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_ServiceType]
GO

ALTER TABLE [dbo].[SystemRoleAction]  WITH CHECK ADD  CONSTRAINT [FK_SystemRoleAction_SystemAction] FOREIGN KEY([SystemActionId])
REFERENCES [dbo].[SystemAction] ([Id])
GO

ALTER TABLE [dbo].[SystemRoleAction] CHECK CONSTRAINT [FK_SystemRoleAction_SystemAction]
GO

ALTER TABLE [dbo].[SystemRoleAction]  WITH CHECK ADD  CONSTRAINT [FK_SystemRoleAction_SystemRole] FOREIGN KEY([SystemRoleId])
REFERENCES [dbo].[SystemRole] ([Id])
GO

ALTER TABLE [dbo].[SystemRoleAction] CHECK CONSTRAINT [FK_SystemRoleAction_SystemRole]
GO

ALTER TABLE [dbo].[SystemScreenField]  WITH CHECK ADD  CONSTRAINT [FK_SystemScreenField_SystemScreen] FOREIGN KEY([SystemScreenId])
REFERENCES [dbo].[SystemScreen] ([Id])
GO

ALTER TABLE [dbo].[SystemScreenField] CHECK CONSTRAINT [FK_SystemScreenField_SystemScreen]
GO

ALTER TABLE [dbo].[VehicleModel]  WITH CHECK ADD  CONSTRAINT [FK_VehicleModel_VehicleBrand] FOREIGN KEY([VehicleBrandId])
REFERENCES [dbo].[VehicleBrand] ([Id])
GO

ALTER TABLE [dbo].[VehicleModel] CHECK CONSTRAINT [FK_VehicleModel_VehicleBrand]
GO

