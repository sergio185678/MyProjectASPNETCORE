USE [database_example]
GO
/****** Object:  Table [dbo].[cargo]    Script Date: 4/05/2024 12:35:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cargo](
	[cargo_id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](45) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[cargo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[documento]    Script Date: 4/05/2024 12:35:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[documento](
	[id_documento] [int] IDENTITY(1,1) NOT NULL,
	[tipo_documento] [varchar](45) NULL,
	[ruta] [varchar](200) NULL,
	[id_usuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id_documento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuario]    Script Date: 4/05/2024 12:35:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuario](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[nombre_completo] [varchar](200) NOT NULL,
	[correo] [varchar](200) NOT NULL,
	[contraseña] [varchar](255) NOT NULL,
	[fecha_registro] [datetime] NULL,
	[id_cargo] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[cargo] ON 

INSERT [dbo].[cargo] ([cargo_id], [nombre]) VALUES (1, N'Administrador')
INSERT [dbo].[cargo] ([cargo_id], [nombre]) VALUES (2, N'Usuario')
INSERT [dbo].[cargo] ([cargo_id], [nombre]) VALUES (3, N'Supervisor')
INSERT [dbo].[cargo] ([cargo_id], [nombre]) VALUES (4, N'Analista')
INSERT [dbo].[cargo] ([cargo_id], [nombre]) VALUES (5, N'Contador')
INSERT [dbo].[cargo] ([cargo_id], [nombre]) VALUES (6, N'agronomo')
INSERT [dbo].[cargo] ([cargo_id], [nombre]) VALUES (7, N'Astronomo')
SET IDENTITY_INSERT [dbo].[cargo] OFF
GO
SET IDENTITY_INSERT [dbo].[documento] ON 

INSERT [dbo].[documento] ([id_documento], [tipo_documento], [ruta], [id_usuario]) VALUES (5, N'prueba6', N'/Uploads/5481fac1-be95-4d7f-9a5f-bea6150da29e_Sergio Flores CV.docx', 15)
SET IDENTITY_INSERT [dbo].[documento] OFF
GO
SET IDENTITY_INSERT [dbo].[usuario] ON 

INSERT [dbo].[usuario] ([user_id], [nombre_completo], [correo], [contraseña], [fecha_registro], [id_cargo]) VALUES (15, N'sergio16', N'sergio16@gmail.com', N'202cb962ac59075b964b07152d234b70', CAST(N'2024-04-28T18:15:16.110' AS DateTime), 1)
INSERT [dbo].[usuario] ([user_id], [nombre_completo], [correo], [contraseña], [fecha_registro], [id_cargo]) VALUES (17, N'Sergio17', N'sergio17@gmail.com', N'202cb962ac59075b964b07152d234b70', CAST(N'2024-04-28T19:47:18.313' AS DateTime), 3)
SET IDENTITY_INSERT [dbo].[usuario] OFF
GO
ALTER TABLE [dbo].[usuario] ADD  DEFAULT (getdate()) FOR [fecha_registro]
GO
ALTER TABLE [dbo].[documento]  WITH CHECK ADD  CONSTRAINT [id_usuario_fk] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[usuario] ([user_id])
GO
ALTER TABLE [dbo].[documento] CHECK CONSTRAINT [id_usuario_fk]
GO
ALTER TABLE [dbo].[usuario]  WITH CHECK ADD  CONSTRAINT [FK_usuario_cargo] FOREIGN KEY([id_cargo])
REFERENCES [dbo].[cargo] ([cargo_id])
GO
ALTER TABLE [dbo].[usuario] CHECK CONSTRAINT [FK_usuario_cargo]
GO
