USE [MassMails]
GO
/****** Object:  Table [dbo].[Contacts_List]    Script Date: 10/5/2018 12:40:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts_List](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](150) NOT NULL,
	[Company] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Remarks] [nvarchar](max) NULL,
	[CatMail] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[idNameContact] [int] NULL,
 CONSTRAINT [PK_Contacts_List] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FILES]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FILES](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[path] [nvarchar](max) NULL,
	[created_at] [datetime] NULL,
	[updated_at] [datetime] NULL,
	[idFolders] [int] NULL,
 CONSTRAINT [PK_FILES] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Folders]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Folders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[created_at] [nvarchar](50) NULL,
	[updated_at] [nvarchar](50) NULL,
	[created_time] [nvarchar](50) NULL,
	[Cate] [int] NULL,
 CONSTRAINT [PK_Folders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LOG_ACCESS]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LOG_ACCESS](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[User_Id] [int] NOT NULL,
	[UserName] [nvarchar](500) NULL,
	[page] [nvarchar](max) NULL,
	[status] [bit] NULL,
	[created_at] [datetime] NULL,
 CONSTRAINT [PK_LOG_ACCESS] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MailConfig]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailConfig](
	[ID] [int] NOT NULL,
	[User] [nvarchar](150) NULL,
	[Pass] [nvarchar](150) NULL,
	[IP] [nvarchar](50) NULL,
	[Protocol] [nvarchar](50) NULL,
	[Port] [nvarchar](50) NULL,
 CONSTRAINT [PK_MailConfig] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhaPhanPhoi]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaPhanPhoi](
	[NPP_ID] [int] NOT NULL,
	[NPPName] [nvarchar](250) NULL,
	[Region] [nvarchar](50) NULL,
	[ManagerMail] [nvarchar](50) NULL,
	[AdminMail] [nvarchar](50) NULL,
	[SEMail] [nvarchar](50) NULL,
	[AcMail] [nvarchar](50) NULL,
	[Adress] [nvarchar](500) NULL,
	[State] [bit] NULL,
 CONSTRAINT [PK_NhaPhanPhoi] PRIMARY KEY CLUSTERED 
(
	[NPP_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PERMISSIONS]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERMISSIONS](
	[Permission_Id] [int] IDENTITY(1,1) NOT NULL,
	[PermissionName] [nvarchar](200) NULL,
	[PermissionDescription] [nvarchar](200) NULL,
 CONSTRAINT [PK_PERMISSIONS] PRIMARY KEY CLUSTERED 
(
	[Permission_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[QueueMails]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[QueueMails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fromAddress] [nvarchar](550) NULL,
	[fromName] [nvarchar](550) NULL,
	[ToAdd] [nvarchar](max) NULL,
	[Cc] [nvarchar](max) NULL,
	[Bcc] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Template] [int] NULL,
	[QueueTime] [nvarchar](50) NULL,
	[SentTime] [nvarchar](50) NULL,
	[SentBy] [nvarchar](500) NULL,
	[Subject] [nvarchar](500) NULL,
	[Files] [nvarchar](500) NULL,
 CONSTRAINT [PK_QueueMails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Region]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](500) NULL,
	[Note] [nvarchar](500) NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ROLE_PERMISSION]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLE_PERMISSION](
	[Role_Id] [int] NOT NULL,
	[Permission_Id] [int] NOT NULL,
 CONSTRAINT [PK_LNK_ROLE_PERMISSION] PRIMARY KEY CLUSTERED 
(
	[Role_Id] ASC,
	[Permission_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ROLES]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ROLES](
	[Role_Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[RoleDescription] [nvarchar](250) NULL,
	[IsSysAdmin] [bit] NULL,
 CONSTRAINT [PK_tbl_Roles] PRIMARY KEY CLUSTERED 
(
	[Role_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SendMails]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SendMails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fromAddress] [nvarchar](550) NULL,
	[fromName] [nvarchar](550) NULL,
	[ToAdd] [nvarchar](max) NULL,
	[Cc] [nvarchar](max) NULL,
	[Bcc] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Template] [int] NULL,
	[QueueTime] [nvarchar](50) NULL,
	[SentTime] [nvarchar](50) NULL,
	[SentBy] [nvarchar](500) NULL,
	[Subject] [nvarchar](500) NULL,
	[Files] [nvarchar](500) NULL,
	[Code] [nvarchar](50) NULL,
 CONSTRAINT [PK_SendMails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Distributors]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Distributors](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](250) NULL,
	[Address] [nvarchar](250) NULL,
	[Region] [int] NULL,
	[AdminMail] [nvarchar](max) NULL,
	[SEmail] [nvarchar](max) NULL,
	[AcMail] [nvarchar](max) NULL,
	[ManagerMail] [nvarchar](500) NULL,
	[State] [bit] NULL,
 CONSTRAINT [PK_tbl_Distributors] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_QueueEmails]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_QueueEmails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fromAddress] [nvarchar](250) NULL,
	[fromName] [nvarchar](250) NULL,
	[toAddress] [nvarchar](max) NULL,
	[ccAddress] [nvarchar](max) NULL,
	[bccAddress] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Template] [int] NULL,
	[QueueTime] [datetime] NULL,
	[SentTime] [datetime] NULL,
	[SentBy] [nvarchar](128) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_SentEmails]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_SentEmails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fromAddress] [nvarchar](250) NULL,
	[fromName] [nvarchar](250) NULL,
	[ToAdd] [nvarchar](max) NULL,
	[Cc] [nvarchar](max) NULL,
	[Bcc] [nvarchar](max) NULL,
	[Body] [nvarchar](max) NULL,
	[Template] [int] NULL,
	[QueueTime] [nvarchar](50) NULL,
	[SentTime] [nvarchar](50) NULL,
	[SentBy] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Signatures]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Signatures](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[Name] [nvarchar](50) NULL,
	[Contents] [nvarchar](max) NULL,
	[UpdateTime] [nvarchar](50) NULL,
	[State] [bit] NULL,
 CONSTRAINT [PK_tbl_Signatures] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tbl_Templates]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_Templates](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Contents] [nvarchar](max) NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[CreationTime] [nvarchar](50) NULL,
	[LastUpdatedBy] [nvarchar](max) NULL,
	[UpdateTime] [nvarchar](50) NULL,
	[Active] [bit] NULL,
 CONSTRAINT [PK_tbl_Templates] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USER_ROLE]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USER_ROLE](
	[User_Id] [int] NOT NULL,
	[Role_Id] [int] NOT NULL,
 CONSTRAINT [PK_LNK_USER_ROLE] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC,
	[Role_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[USERS]    Script Date: 10/5/2018 12:40:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[USERS](
	[User_Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](30) NOT NULL,
	[Password] [nvarchar](100) NULL,
	[LastModified] [datetime] NULL,
	[Inactive] [bit] NULL,
	[Firstname] [nvarchar](50) NULL,
	[Lastname] [nvarchar](50) NULL,
	[Title] [nvarchar](30) NULL,
	[Initial] [nvarchar](3) NULL,
	[EMail] [nvarchar](100) NULL,
 CONSTRAINT [PK_USERS] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Contacts_List] ON 

INSERT [dbo].[Contacts_List] ([ID], [Name], [Email], [Company], [Phone], [Address], [Remarks], [CatMail], [State], [idNameContact]) VALUES (1, N'Cu Viet Dung', N'dungcv@gmail.com', NULL, NULL, N'Ha Noi', NULL, N'TO', N'-1', 9)
INSERT [dbo].[Contacts_List] ([ID], [Name], [Email], [Company], [Phone], [Address], [Remarks], [CatMail], [State], [idNameContact]) VALUES (2, N'Cu Viet Dung', N'dungcv@epu.edu.vn', NULL, NULL, N'Ha Noi', NULL, N'CC', N'-1', 9)
INSERT [dbo].[Contacts_List] ([ID], [Name], [Email], [Company], [Phone], [Address], [Remarks], [CatMail], [State], [idNameContact]) VALUES (18, N'Hong Ly', N'hongly441@gmail.com', NULL, NULL, N'Nam Dinh', NULL, N'BCC', N'-1', 9)
INSERT [dbo].[Contacts_List] ([ID], [Name], [Email], [Company], [Phone], [Address], [Remarks], [CatMail], [State], [idNameContact]) VALUES (44, N'Cu Viet Dung', N'dungcv1210@gmail.com', NULL, NULL, N'Ha Noi', NULL, N'CC', N'-1', 9)
INSERT [dbo].[Contacts_List] ([ID], [Name], [Email], [Company], [Phone], [Address], [Remarks], [CatMail], [State], [idNameContact]) VALUES (45, N'Test', N'dungcv1210@gmail.com', NULL, NULL, N'Ha Noi', NULL, N'CC', N'-1', 13)
INSERT [dbo].[Contacts_List] ([ID], [Name], [Email], [Company], [Phone], [Address], [Remarks], [CatMail], [State], [idNameContact]) VALUES (46, N'A', N'hongly441@gmail.com', NULL, NULL, N'Nam Dinh', NULL, N'BCC', N'-1', 13)
INSERT [dbo].[Contacts_List] ([ID], [Name], [Email], [Company], [Phone], [Address], [Remarks], [CatMail], [State], [idNameContact]) VALUES (47, N'B', N'dungcv@gmail.com', NULL, NULL, N'Ha Noi', NULL, N'TO', N'-1', 13)
INSERT [dbo].[Contacts_List] ([ID], [Name], [Email], [Company], [Phone], [Address], [Remarks], [CatMail], [State], [idNameContact]) VALUES (48, N'C', N'dungcv@epu.edu.vn', NULL, NULL, N'Ha Noi', NULL, N'CC', N'-1', 13)
SET IDENTITY_INSERT [dbo].[Contacts_List] OFF
SET IDENTITY_INSERT [dbo].[FILES] ON 

INSERT [dbo].[FILES] ([id], [userId], [path], [created_at], [updated_at], [idFolders]) VALUES (19, 1, N'ExcelUpload contact 04 Apr 2018.xls', CAST(0x0000A96E0184C1AE AS DateTime), NULL, 14)
INSERT [dbo].[FILES] ([id], [userId], [path], [created_at], [updated_at], [idFolders]) VALUES (20, 1, N'ExcelUpload contact 04 Apr 2018.xlsx', CAST(0x0000A96E0184C1B0 AS DateTime), NULL, 14)
SET IDENTITY_INSERT [dbo].[FILES] OFF
SET IDENTITY_INSERT [dbo].[Folders] ON 

INSERT [dbo].[Folders] ([ID], [Name], [Description], [created_at], [updated_at], [created_time], [Cate]) VALUES (9, N'Contact list vs2', N'Contact list des vs2', N'CrmInstall', NULL, N'10/3/2018 11:02:36 PM', 2)
INSERT [dbo].[Folders] ([ID], [Name], [Description], [created_at], [updated_at], [created_time], [Cate]) VALUES (13, N'Contact list vs1', N'Contact list des vs1', N'CrmInstall', NULL, N'10/3/2018 11:12:09 PM', 2)
INSERT [dbo].[Folders] ([ID], [Name], [Description], [created_at], [updated_at], [created_time], [Cate]) VALUES (14, N'Oct 2018', N'fff', N'CrmInstall', NULL, N'10/3/2018 11:35:25 PM', NULL)
SET IDENTITY_INSERT [dbo].[Folders] OFF
SET IDENTITY_INSERT [dbo].[LOG_ACCESS] ON 

INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (1, 1, NULL, N'Home-Index', 1, CAST(0x0000A961017D3E76 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (2, 1, NULL, N'User-Index', 1, CAST(0x0000A961017D4924 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (8, 1, NULL, N'Home-Index', 1, CAST(0x0000A962016A8059 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (9, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016A8DD1 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (10, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016ABA4A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (11, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016AF909 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (12, 1, NULL, N'Home-Index', 1, CAST(0x0000A962016B5A51 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (13, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016B6251 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (14, 1, NULL, N'Log-Delete', 1, CAST(0x0000A962016B6741 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (15, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016B6752 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (16, 1, NULL, N'Log-Delete', 1, CAST(0x0000A962016B69A5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (17, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016B69AD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (18, 1, NULL, N'Log-Delete', 1, CAST(0x0000A962016B6C23 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (19, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016B6C2B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (20, 1, NULL, N'Log-Delete', 1, CAST(0x0000A962016B6D58 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (21, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016B6D60 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (22, 1, NULL, N'Log-Delete', 1, CAST(0x0000A962016B6E73 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (23, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016B6E7B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (24, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016B7509 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (25, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016B7A99 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (26, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016B7E74 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (27, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016B81B2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (28, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016BB2C3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (29, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016BB693 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (30, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016BBFE7 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (31, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016BC663 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (32, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016C3871 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (33, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016C45E3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (34, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016C5258 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (35, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016DE1F4 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (36, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016DF43E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (37, 1, NULL, N'Log-Index', 1, CAST(0x0000A962016E011D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (38, 1, NULL, N'Role-Index', 1, CAST(0x0000A96500C1C4E1 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (39, 1, NULL, N'Role-Index', 1, CAST(0x0000A96500C1E6B3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (40, 1, NULL, N'Role-Index', 1, CAST(0x0000A96500C1EBD5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (41, 1, NULL, N'Role-Index', 1, CAST(0x0000A96500F79F8C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (42, 1, NULL, N'Role-Index', 1, CAST(0x0000A96500F894AF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (43, 1, NULL, N'Role-Index', 1, CAST(0x0000A96500F90A8B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (44, 1, NULL, N'Role-Index', 1, CAST(0x0000A96500F94976 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (45, 1, NULL, N'Role-Index', 1, CAST(0x0000A96500F9D125 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (46, 1, NULL, N'Role-Create', 1, CAST(0x0000A96500F9DA20 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (47, 1, NULL, N'Role-Index', 1, CAST(0x0000A96500FA1421 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (48, 1, NULL, N'Role-Create', 1, CAST(0x0000A96500FA171B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (49, 1, NULL, N'Role-Create', 1, CAST(0x0000A96500FB242D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (50, 1, NULL, N'Role-Create', 1, CAST(0x0000A96500FD2FAE AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (51, 1, NULL, N'Role-Create', 1, CAST(0x0000A96500FEF822 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (52, 1, NULL, N'Role-Create', 1, CAST(0x0000A96500FF0E93 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (53, 1, NULL, N'Role-Create', 1, CAST(0x0000A96500FF68E4 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (54, 1, NULL, N'Role-Create', 1, CAST(0x0000A96500FFE2C6 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (55, 1, NULL, N'Role-Create', 1, CAST(0x0000A96500FFF112 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (56, 1, NULL, N'Role-Index', 1, CAST(0x0000A96500FFF155 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (57, 1, NULL, N'Role-Delete', 1, CAST(0x0000A96501000526 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (58, 1, NULL, N'Role-Index', 1, CAST(0x0000A96501000585 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (59, 1, NULL, N'User-Index', 1, CAST(0x0000A96501000DE3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (60, 1, NULL, N'Role-Create', 1, CAST(0x0000A9650100A063 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (61, 1, NULL, N'Role-Index', 1, CAST(0x0000A9650100A679 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (62, 1, NULL, N'Role-Edit', 1, CAST(0x0000A9650100A995 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (63, 1, NULL, N'Role-Index', 1, CAST(0x0000A9650100DF40 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (64, 1, NULL, N'Role-Edit', 1, CAST(0x0000A9650100E1B6 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (65, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96501013BA4 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (66, 1, NULL, N'Role-Index', 1, CAST(0x0000A96501017FD8 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (67, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96501018670 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (68, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96501020C77 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (69, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96501021CD3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (70, 1, NULL, N'Role-Index', 1, CAST(0x0000A96501023B00 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (71, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96501023F2A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (72, 1, NULL, N'Role-Index', 1, CAST(0x0000A9650102C5F8 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (73, 1, NULL, N'Role-Edit', 1, CAST(0x0000A9650102CB0B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (74, 1, NULL, N'Role-Index', 1, CAST(0x0000A9650103A2B3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (75, 1, NULL, N'Role-Edit', 1, CAST(0x0000A9650103A56F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (76, 1, NULL, N'Role-Index', 1, CAST(0x0000A96501042B5C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (77, 1, NULL, N'Role-Edit', 1, CAST(0x0000A9650104305A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (78, 1, NULL, N'Role-Index', 1, CAST(0x0000A9650106AF6E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (79, 1, NULL, N'Role-Edit', 1, CAST(0x0000A9650106BBFE AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (80, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96501080B80 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (81, 1, NULL, N'Role-Edit', 1, CAST(0x0000A9650108203C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (82, 1, NULL, N'Role-Index', 1, CAST(0x0000A96501084595 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (83, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96501084810 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (84, 1, NULL, N'Role-Index', 1, CAST(0x0000A9650109A568 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (85, 1, NULL, N'Role-Edit', 1, CAST(0x0000A9650109B474 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (86, 1, NULL, N'Role-Index', 1, CAST(0x0000A9650109C351 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (87, 1, NULL, N'Role-Create', 1, CAST(0x0000A965010A1DAD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (88, 1, NULL, N'Role-Create', 1, CAST(0x0000A965010A3270 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (89, 1, NULL, N'Role-Index', 1, CAST(0x0000A965010A32A8 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (90, 1, NULL, N'Role-Edit', 1, CAST(0x0000A965010A3657 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (91, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A965010A5247 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (92, 1, NULL, N'Role-Edit', 1, CAST(0x0000A965010A572E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (93, 1, NULL, N'Role-Details', 1, CAST(0x0000A965010A5774 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (94, 1, NULL, N'Role-Index', 1, CAST(0x0000A965010A5CFD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (95, 1, NULL, N'Role-Create', 1, CAST(0x0000A965010A614D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (96, 1, NULL, N'Role-Create', 1, CAST(0x0000A965010A6781 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (97, 1, NULL, N'Role-Index', 1, CAST(0x0000A965010A67B3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (98, 1, NULL, N'Role-Edit', 1, CAST(0x0000A965010A6ABE AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (99, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A965010A705F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (100, 1, NULL, N'Role-Edit', 1, CAST(0x0000A965010A71AC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (101, 1, NULL, N'Role-Details', 1, CAST(0x0000A965010A71DB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (102, 1, NULL, N'Role-Index', 1, CAST(0x0000A965010A797B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (103, 1, NULL, N'Role-Edit', 1, CAST(0x0000A965010A7C03 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (104, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A965010AAB51 AS DateTime))
GO
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (105, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A965010AAECF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (106, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A965010AB7B9 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (107, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A965010ABAA2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (108, 1, NULL, N'Role-Edit', 1, CAST(0x0000A965010B2A84 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (109, 1, NULL, N'Role-AddAllPermissions2RoleReturnPartialView', 1, CAST(0x0000A965010B2F11 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (110, 1, NULL, N'Role-Index', 1, CAST(0x0000A965010B635E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (111, 1, NULL, N'User-Index', 1, CAST(0x0000A965010B7601 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (112, 1, NULL, N'User-Index', 1, CAST(0x0000A965010BB450 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (113, 1, NULL, N'User-Create', 1, CAST(0x0000A965010BB918 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (114, 1, NULL, N'User-Create', 1, CAST(0x0000A965010BCACC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (115, 1, NULL, N'User-Create', 1, CAST(0x0000A965010C037F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (116, 1, NULL, N'User-Create', 1, CAST(0x0000A965010C1130 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (117, 1, NULL, N'User-Index', 1, CAST(0x0000A965010C117F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (118, 1, NULL, N'User-Details', 1, CAST(0x0000A965010C1463 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (119, 1, NULL, N'User-Details', 1, CAST(0x0000A965010C2D7A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (120, 1, NULL, N'User-Details', 1, CAST(0x0000A965010C509B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (121, 1, NULL, N'User-Index', 1, CAST(0x0000A965010C5BA3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (122, 1, NULL, N'User-Details', 1, CAST(0x0000A965010C5FD6 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (123, 1, NULL, N'Role-Details', 1, CAST(0x0000A965010C63BB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (124, 1, NULL, N'User-Index', 1, CAST(0x0000A965010C7FC3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (125, 1, NULL, N'User-Edit', 1, CAST(0x0000A965010C8449 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (126, 1, NULL, N'User-Edit', 1, CAST(0x0000A965010C9AA2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (127, 1, NULL, N'User-AddUserRoleReturnPartialView', 1, CAST(0x0000A965010CAEE8 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (128, 1, NULL, N'User-AddUserRoleReturnPartialView', 1, CAST(0x0000A965010CB2A1 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (129, 1, NULL, N'User-AddUserRoleReturnPartialView', 1, CAST(0x0000A965010CB639 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (130, 4, NULL, N'User-Index', 0, CAST(0x0000A965010D4622 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (131, 4, NULL, N'User-Create', 1, CAST(0x0000A965010D4F99 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (132, 4, NULL, N'User-edit', 1, CAST(0x0000A965010D5AEA AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (133, 4, NULL, N'User-edit', 1, CAST(0x0000A965010D64EC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (134, 4, NULL, N'User-DeleteUserRoleReturnPartialView', 0, CAST(0x0000A965010D6FF4 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (135, 4, NULL, N'User-edit', 1, CAST(0x0000A965010D784A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (136, 4, NULL, N'User-DeleteUserRoleReturnPartialView', 0, CAST(0x0000A965010DB79E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (137, 1, NULL, N'Permission-Index', 1, CAST(0x0000A965010E9575 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (138, 1, NULL, N'Permission-Index', 1, CAST(0x0000A965010EE0DF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (139, 1, NULL, N'Permission-Index', 1, CAST(0x0000A965010EF9CF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (140, 1, NULL, N'Permission-Create', 1, CAST(0x0000A965010F030E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (141, 1, NULL, N'Permission-Index', 1, CAST(0x0000A965010F3DC8 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (142, 1, NULL, N'Permission-Import', 1, CAST(0x0000A965010F3FB7 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (143, 1, NULL, N'Permission-Index', 1, CAST(0x0000A965010FF103 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (144, 1, NULL, N'Permission-Create', 1, CAST(0x0000A965010FF750 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (145, 1, NULL, N'Permission-Index', 1, CAST(0x0000A965010FFA3C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (146, 1, NULL, N'Permission-Import', 1, CAST(0x0000A965010FFC58 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (147, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96501100031 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (148, 1, NULL, N'Permission-Index', 1, CAST(0x0000A965011005EB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (149, 1, NULL, N'Permission-Index', 1, CAST(0x0000A965011008C7 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (150, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96501100F8F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (151, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96501101201 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (152, 1, NULL, N'Permission-Details', 1, CAST(0x0000A96501101614 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (153, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96501106E8E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (154, 1, NULL, N'Permission-Details', 1, CAST(0x0000A965011070DB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (155, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A965011076B9 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (156, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A96501108295 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (157, 1, NULL, N'Permission-Details', 1, CAST(0x0000A965011082CD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (158, 1, NULL, N'Permission-Index', 1, CAST(0x0000A965011084C0 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (159, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96501108B5C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (160, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96501108E0A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (161, 1, NULL, N'Role-Index', 1, CAST(0x0000A96501110634 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (162, 1, NULL, N'User-Index', 1, CAST(0x0000A96501110AD8 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (163, 1, NULL, N'User-Edit', 1, CAST(0x0000A9650111108D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (164, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96501111CB7 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (165, 1, NULL, N'Role-Index', 1, CAST(0x0000A9650111242A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (166, 1, NULL, N'Role-Edit', 1, CAST(0x0000A9650111284C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (167, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96501112EDF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (168, 1, NULL, N'Role-AddAllPermissions2RoleReturnPartialView', 1, CAST(0x0000A9650111B90A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (169, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600F6D36F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (170, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600F6D774 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (171, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600F6E883 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (172, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600F6F83F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (173, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600F727CF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (174, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600F72AE5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (175, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600F77A44 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (176, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600F7803E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (177, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96600F7874D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (178, 1, NULL, N'Permission-Import', 1, CAST(0x0000A96600F78AF6 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (179, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600F7C096 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (180, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96600F7C2BC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (181, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7C8B0 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (182, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CAAB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (183, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CB41 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (184, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CBF0 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (185, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CC63 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (186, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CC9E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (187, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CCD5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (188, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CD07 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (189, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CD99 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (190, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CE30 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (191, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CEEC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (192, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7CF82 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (193, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7D018 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (194, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7D2DC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (195, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7D328 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (196, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7D35B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (197, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7D395 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (198, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7D40D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (199, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7D4A4 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (200, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7D53F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (201, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7D5D5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (202, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7D66C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (203, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96600F7DD85 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (204, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7E3B8 AS DateTime))
GO
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (205, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F7E454 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (206, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7E673 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (207, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7E6BA AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (208, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7E6FE AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (209, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7E732 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (210, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7E774 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (211, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7E83B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (212, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7E8D1 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (213, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7E967 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (214, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7E9FD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (215, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7EA93 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (216, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7F307 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (217, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7F39D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (218, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F7F433 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (219, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600F7FB82 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (220, 1, NULL, N'Role-Details', 1, CAST(0x0000A96600F7FE22 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (221, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96600F804DC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (222, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F8110E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (223, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F811A5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (224, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96600F823C5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (225, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96600F82D41 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (226, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600F83491 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (227, 1, NULL, N'Role-Details', 1, CAST(0x0000A96600F83700 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (228, 1, NULL, N'Role-DeletePermissionFromRoleReturnPartialView', 1, CAST(0x0000A96600F842E3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (229, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600F8E57A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (230, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600F8E752 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (231, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600FA4188 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (232, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600FA4428 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (233, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600FAD2D3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (234, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600FAD5EE AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (235, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600FBE22D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (236, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600FBE4FE AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (237, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600FCF671 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (238, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600FCFB82 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (239, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600FE8681 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (240, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600FE894B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (241, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600FF1CB3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (242, 1, NULL, N'Role-Import', 1, CAST(0x0000A96600FF3CAA AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (243, 1, NULL, N'Role-Index', 1, CAST(0x0000A96600FF9EA0 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (244, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96601001BD7 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (245, 1, NULL, N'Role-Index', 1, CAST(0x0000A96601005B24 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (246, 1, NULL, N'Role-Delete', 1, CAST(0x0000A96601006174 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (247, 1, NULL, N'Role-Index', 1, CAST(0x0000A96601006204 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (248, 1, NULL, N'Role-Delete', 1, CAST(0x0000A9660100661B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (249, 1, NULL, N'Role-Index', 1, CAST(0x0000A96601006667 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (250, 1, NULL, N'Role-Delete', 1, CAST(0x0000A9660100696E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (251, 1, NULL, N'Role-Index', 1, CAST(0x0000A966010069AE AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (252, 1, NULL, N'Role-Import', 1, CAST(0x0000A96601006D42 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (253, 1, NULL, N'Role-Import', 1, CAST(0x0000A96601009070 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (254, 1, NULL, N'Role-Index', 1, CAST(0x0000A966010099E7 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (255, 1, NULL, N'Role-Delete', 1, CAST(0x0000A96601009FAC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (256, 1, NULL, N'Role-Index', 1, CAST(0x0000A9660100A036 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (257, 1, NULL, N'Role-Index', 1, CAST(0x0000A9660100A422 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (258, 1, NULL, N'Role-Import', 1, CAST(0x0000A9660100A675 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (259, 1, NULL, N'Role-Index', 1, CAST(0x0000A9660100DDC1 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (260, 1, NULL, N'Role-Import', 1, CAST(0x0000A9660100E2CC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (261, 1, NULL, N'Role-Import', 1, CAST(0x0000A9660100F927 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (262, 1, NULL, N'Role-Import', 1, CAST(0x0000A96601016915 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (263, 1, NULL, N'Role-Index', 1, CAST(0x0000A96601042C7A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (264, 1, NULL, N'Role-Import', 1, CAST(0x0000A966010430C0 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (265, 1, NULL, N'Role-Index', 1, CAST(0x0000A9660104D79D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (266, 1, NULL, N'Role-Import', 1, CAST(0x0000A9660104DC42 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (267, 1, NULL, N'Role-Index', 1, CAST(0x0000A9660106342C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (268, 1, NULL, N'Role-Import', 1, CAST(0x0000A96601063871 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (269, 1, NULL, N'Role-Index', 1, CAST(0x0000A9660107481F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (270, 1, NULL, N'Role-Import', 1, CAST(0x0000A96601074BA3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (271, 1, NULL, N'Role-Index', 1, CAST(0x0000A966013FFC24 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (272, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96601400233 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (273, 1, NULL, N'Permission-Index', 1, CAST(0x0000A966014141DF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (274, 1, NULL, N'Role-Index', 1, CAST(0x0000A966014147B7 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (275, 1, NULL, N'User-Index', 1, CAST(0x0000A96601414CAB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (276, 1, NULL, N'User-Edit', 1, CAST(0x0000A966014154D1 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (277, 1, NULL, N'User-Edit', 1, CAST(0x0000A96601416DEC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (278, 1, NULL, N'User-Details', 1, CAST(0x0000A96601416E35 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (279, 1, NULL, N'User-Details', 1, CAST(0x0000A9660141EF03 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (280, 1, NULL, N'Role-Details', 1, CAST(0x0000A9660141F7EA AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (281, 1, NULL, N'Role-Index', 1, CAST(0x0000A96601420325 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (282, 1, NULL, N'Role-Edit', 1, CAST(0x0000A9660142096D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (283, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014212D4 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (284, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421304 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (285, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A9660142130E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (286, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421315 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (287, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421323 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (288, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421328 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (289, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421331 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (290, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421339 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (291, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A9660142133F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (292, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421347 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (293, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421350 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (294, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A9660142135A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (295, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421361 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (296, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421369 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (297, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421371 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (298, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A9660142137A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (299, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421381 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (300, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421388 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (301, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421391 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (302, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A9660142139A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (303, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014213A5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (304, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014213AC AS DateTime))
GO
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (305, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014213B3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (306, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014213C3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (307, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014213C8 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (308, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014213D2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (309, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014213D9 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (310, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014213E1 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (311, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014213E9 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (312, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014213F5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (313, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421401 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (314, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421411 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (315, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421420 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (316, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A9660142142F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (317, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421442 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (318, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A9660142144C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (319, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A9660142145B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (320, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421468 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (321, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421473 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (322, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421483 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (323, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421495 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (324, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014214A1 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (325, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014214AF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (326, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014214BF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (327, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014214C9 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (328, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014214D5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (329, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421537 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (330, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421540 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (331, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A966014215D0 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (332, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96601421626 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (333, 1, NULL, N'Role-Details', 1, CAST(0x0000A9660142164C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (334, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421667 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (335, 1, NULL, N'Role-AddPermission2RoleReturnPartialView', 1, CAST(0x0000A96601421701 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (336, 1, NULL, N'Permission-Index', 1, CAST(0x0000A966014228AE AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (337, 1, NULL, N'Role-Index', 1, CAST(0x0000A96601422CB6 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (338, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96601423341 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (339, 1, NULL, N'File-Index', 1, CAST(0x0000A96800A628D9 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (340, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800A639AD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (341, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800A64798 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (342, 1, NULL, N'File-Index', 1, CAST(0x0000A96800A64B35 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (343, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800A66751 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (344, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800A67271 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (345, 1, NULL, N'File-Index', 1, CAST(0x0000A96800A676E2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (346, 1, NULL, N'File-Index', 1, CAST(0x0000A96800A8A022 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (347, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800A8A7D1 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (348, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800A8B214 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (349, 1, NULL, N'File-Index', 1, CAST(0x0000A96800A8C638 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (350, 1, NULL, N'File-Index', 1, CAST(0x0000A96800A9EF28 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (351, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800A9F1AE AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (352, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800A9F2AD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (353, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800A9FE4B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (354, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800AA77C5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (355, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800AA867C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (356, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800AB21ED AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (357, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800AB2DE2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (358, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800ABF1E5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (359, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800ABF925 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (360, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800ACA8F4 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (361, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800ACB28A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (362, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800ACE553 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (363, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800AD4D4B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (364, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800AD60EF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (365, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96800AD74F5 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (366, 1, NULL, N'File-Index', 1, CAST(0x0000A96800AE4EBD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (367, 1, NULL, N'File-Index', 1, CAST(0x0000A96800B10F64 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (368, 1, NULL, N'File-Index', 1, CAST(0x0000A968011107B2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (369, 1, NULL, N'Role-Edit', 1, CAST(0x0000A96900D555E2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (370, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96A0088F65E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (371, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96A0089277E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (372, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A96A008930EF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (373, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A96A008B5DBD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (374, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A96A008B78ED AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (375, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A96A008BBB94 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (376, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A96A008BC8AB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (377, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A96A008C1548 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (378, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A96A008C478E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (379, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A96A008C85A2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (380, 1, NULL, N'Permission-Edit', 1, CAST(0x0000A96A008C928C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (381, 1, NULL, N'Role-Index', 1, CAST(0x0000A96A009B2C4C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (382, 1, NULL, N'Permission-Create', 1, CAST(0x0000A96A00AF3455 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (383, 1, NULL, N'Permission-Create', 1, CAST(0x0000A96A00AF959E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (385, 1, NULL, N'Role-Index', 1, CAST(0x0000A96C0122129A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (386, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96C01221B0F AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (387, 1, NULL, N'User-Index', 1, CAST(0x0000A96C01258F2D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (388, 1, NULL, N'Role-Index', 1, CAST(0x0000A96C0125989B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (389, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96C0125A1CD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (390, 1, NULL, N'Role-Index', 1, CAST(0x0000A96C0125A4CF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (391, 1, NULL, N'User-Index', 1, CAST(0x0000A96C0125ABE3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (392, 1, NULL, N'User-Details', 1, CAST(0x0000A96C0125B066 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (393, 1, NULL, N'User-Details', 1, CAST(0x0000A96C0125B932 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (394, 1, NULL, N'Role-Details', 1, CAST(0x0000A96C012711F8 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (395, 1, NULL, N'Role-Index', 1, CAST(0x0000A96C01271B5E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (396, 1, NULL, N'Role-Import', 1, CAST(0x0000A96C012774E0 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (397, 1, NULL, N'Permission-Index', 1, CAST(0x0000A96D00ACD191 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (398, 1, NULL, N'File-Index', 1, CAST(0x0000A96D00AE9858 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (399, 1, NULL, N'File-Index', 1, CAST(0x0000A96D00B43069 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (400, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D00B4336C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (401, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D00B4C16B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (402, 1, NULL, N'File-Index', 1, CAST(0x0000A96D00B5143B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (403, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D00B51A66 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (404, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D00B5657D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (405, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D00B573F4 AS DateTime))
GO
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (406, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D0155A5DB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (407, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D01560112 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (408, 1, NULL, N'File-Index', 1, CAST(0x0000A96D0156BF0B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (409, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D0156C438 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (410, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D0156F41D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (411, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D01570DEB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (412, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D01573B1A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (413, 1, NULL, N'File-Index', 1, CAST(0x0000A96D0157C48E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (414, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D01581AE9 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (415, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D0158286D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (416, 1, NULL, N'File-Index', 1, CAST(0x0000A96D01590AF2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (417, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D01590F9A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (418, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D01591EB2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (419, 1, NULL, N'File-Index', 1, CAST(0x0000A96D015A2D39 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (420, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D015A35FD AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (421, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D015A4294 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (422, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D015A50F4 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (423, 1, NULL, N'File-Index', 1, CAST(0x0000A96D015AA216 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (424, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D015AA5AB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (425, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96D015AB4B9 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (426, 1, NULL, N'File-Index', 1, CAST(0x0000A96D015B372E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (427, 1, NULL, N'File-Index', 1, CAST(0x0000A96E0103B70D AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (428, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96E01778AEF AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (429, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96E0177963B AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (430, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96E01782809 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (431, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96E01784845 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (432, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96E01788E13 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (433, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96E0178B83A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (434, 1, NULL, N'File-Index', 1, CAST(0x0000A96E0183A423 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (435, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96E0183A77C AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (436, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96E0183C420 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (437, 1, NULL, N'File-Index', 1, CAST(0x0000A96E0184B4BC AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (438, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96E0184B6B7 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (439, 1, NULL, N'File-UploadFiles', 1, CAST(0x0000A96E0184C1AB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (440, 1, NULL, N'File-Index', 1, CAST(0x0000A96E0184C4A2 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (441, 1, NULL, N'File-Index', 1, CAST(0x0000A97000C8A775 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (442, 1, NULL, N'File-Index', 1, CAST(0x0000A97000CA4AE0 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (443, 1, NULL, N'File-Index', 1, CAST(0x0000A97000CA7ADB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (444, 1, NULL, N'File-Index', 1, CAST(0x0000A97000CA8980 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (445, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CAB6E3 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (446, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CB1CEB AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (447, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CB4AE4 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (448, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CB4CC4 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (449, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CB4E19 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (450, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CB4F2A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (451, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CB5204 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (452, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CB8F44 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (453, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CBBC1E AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (454, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CBD643 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (455, 1, NULL, N'Folder-Index', 1, CAST(0x0000A97000CBE75A AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (456, 1, NULL, N'Role-Index', 1, CAST(0x0000A97000CBFB55 AS DateTime))
INSERT [dbo].[LOG_ACCESS] ([id], [User_Id], [UserName], [page], [status], [created_at]) VALUES (457, 1, NULL, N'User-Index', 1, CAST(0x0000A97000CBFF05 AS DateTime))
SET IDENTITY_INSERT [dbo].[LOG_ACCESS] OFF
INSERT [dbo].[MailConfig] ([ID], [User], [Pass], [IP], [Protocol], [Port]) VALUES (1, N'dungcv@epu.edu.vn', N'23032001@#$', N'192.168.1.100', N'smtp.gmail.com', N'587')
INSERT [dbo].[NhaPhanPhoi] ([NPP_ID], [NPPName], [Region], [ManagerMail], [AdminMail], [SEMail], [AcMail], [Adress], [State]) VALUES (1, N'Công ty DL', NULL, N'aa', N'hongly44@gmail.com', N'bb', N'cc', NULL, 1)
INSERT [dbo].[NhaPhanPhoi] ([NPP_ID], [NPPName], [Region], [ManagerMail], [AdminMail], [SEMail], [AcMail], [Adress], [State]) VALUES (4, N'Công ty TNHH', N'Trung', N'dungcv@epu.edu.vn', NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[NhaPhanPhoi] ([NPP_ID], [NPPName], [Region], [ManagerMail], [AdminMail], [SEMail], [AcMail], [Adress], [State]) VALUES (7, N'Công ty Nam Ha', N'Nam', N'dungcv@epu.edu.vn', NULL, NULL, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[PERMISSIONS] ON 

INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (56, N'Distributors-Index', N'Distributors-Index')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (57, N'Distributors-Insert', N'Distributors-Insert')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (58, N'Distributors-Edit', N'Sửa Distributors')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (59, N'Distributors-Delete', N'Distributors-Delete')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (60, N'Home-Index', N'Home-Index')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (61, N'Log-Index', N'Log-Index')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (62, N'Log-Delete', N'Log-Delete')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (63, N'Login-Index', N'Login-Index')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (64, N'MailManagement-Mailbox', N'MailManagement-Mailbox')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (65, N'MailManagement-SendMails', N'MailManagement-SendMails')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (66, N'MailManagement-GetTemplateData', N'MailManagement-GetTemplateData')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (67, N'MailManagement-GetSigData', N'MailManagement-GetSigData')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (68, N'MailManagement-ImportExcel', N'MailManagement-ImportExcel')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (69, N'MailManagement-GetListSearch', N'MailManagement-GetListSearch')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (70, N'MailManagement-GetMailDistributors', N'MailManagement-GetMailDistributors')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (71, N'MailManagement-GetList', N'MailManagement-GetList')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (72, N'MailManagement-SearchDis', N'MailManagement-SearchDis')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (73, N'Permission-Index', N'Permission-Index')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (74, N'Permission-Details', N'Permission-Details')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (75, N'Permission-Create', N'Permission-Create')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (76, N'Permission-Edit', N'Permission-Edit')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (77, N'Permission-Delete', N'Permission-Delete')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (78, N'Permission-AddPermission2RoleReturnPartialView', N'Permission-AddPermission2RoleReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (79, N'Permission-AddAllPermissions2RoleReturnPartialView', N'Permission-AddAllPermissions2RoleReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (80, N'Permission-DeletePermissionFromRoleReturnPartialView', N'Permission-DeletePermissionFromRoleReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (81, N'Permission-DeleteRoleFromPermissionReturnPartialView', N'Permission-DeleteRoleFromPermissionReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (82, N'Permission-AddRole2PermissionReturnPartialView', N'Permission-AddRole2PermissionReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (83, N'Permission-Import', N'Permission-Import')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (84, N'Role-Index', N'Role-Index')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (85, N'Role-Create', N'Role-Create')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (86, N'Role-Edit', N'Role-Edit')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (87, N'Role-Delete', N'Role-Delete')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (88, N'Role-Details', N'Role-Details')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (89, N'Role-AddPermission2RoleReturnPartialView', N'Role-AddPermission2RoleReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (90, N'Role-AddAllPermissions2RoleReturnPartialView', N'Role-AddAllPermissions2RoleReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (91, N'Role-DeletePermissionFromRoleReturnPartialView', N'Role-DeletePermissionFromRoleReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (92, N'Templates-Index', N'Templates-Index')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (93, N'Templates-Create', N'Templates-Create')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (94, N'Unauthorised-Index', N'Unauthorised-Index')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (95, N'User-Index', N'User-Index')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (96, N'User-Details', N'User-Details')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (97, N'User-Create', N'User-Create')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (98, N'User-Edit', N'User-Edit')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (99, N'User-DeleteUserRole', N'User-DeleteUserRole')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (100, N'User-filter4Users', N'User-filter4Users')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (101, N'User-filterReset', N'User-filterReset')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (102, N'User-DeleteUserReturnPartialView', N'User-DeleteUserReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (103, N'User-DeleteUserRoleReturnPartialView', N'User-DeleteUserRoleReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (104, N'User-AddUserRoleReturnPartialView', N'User-AddUserRoleReturnPartialView')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (105, N'Distributors-Edit', N'Distributors-Edit')
INSERT [dbo].[PERMISSIONS] ([Permission_Id], [PermissionName], [PermissionDescription]) VALUES (106, N'Role-Import', N'Role-Import')
SET IDENTITY_INSERT [dbo].[PERMISSIONS] OFF
SET IDENTITY_INSERT [dbo].[QueueMails] ON 

INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (1, N'CrmInstall@ivg.vn', N'Crm Install', N'dungcv@gmail.com', N'dungcv1210@gmail.com', N'hongly441@gmail.com', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;Cu Viet Dung,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', 5, NULL, N'10/1/2018 11:39:33 PM', N'CrmInstall', N'Thông báo chương trình khuyến mại ', NULL)
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (2, N'CrmInstall@ivg.vn', N'Crm Install', N'dungcv@gmail.com', N'dungcv@epu.edu.vn', N'hongly441@gmail.com', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;Cu Viet Dung,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', 5, NULL, N'10/1/2018 11:39:39 PM', N'CrmInstall', N'Thông báo chương trình khuyến mại ', NULL)
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (3, N'admin@gmail.com', N'Cu Dung', N'hanhbt@tuanviet-trading.com;', N'hanhbt@tuanviet-trading.com;', N'nult@tuanviet-trading.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 1:29:02 PM', NULL, N'CrmInstall', N'test list', N'[9]Contact list vs2;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (4, N'admin@gmail.com', N'Cu Dung', N'thuy.ducloi@gmail.com;', N'nppducloi@gmail.com;', N'thuy.ducloi@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 1:29:02 PM', NULL, N'CrmInstall', N'test list', N'[9]Contact list vs2;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (5, N'admin@gmail.com', N'Cu Dung', N'trtrangml@gmail.com;', N'nthuy1102@gmail.com;', N'trtrangml@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 1:29:02 PM', NULL, N'CrmInstall', N'test list', N'[9]Contact list vs2;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (6, N'admin@gmail.com', N'Cu Dung', N'truongthanhphathoian@gmail.com;', N'truongthanhphathoianco.ld@gmail.com;', N'truongthanhphathoianco.ld@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 1:29:02 PM', NULL, N'CrmInstall', N'test list', N'[9]Contact list vs2;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (7, N'admin@gmail.com', N'Cu Dung', N'tohien76@gmail.com;', N'tohien76@gmail.com;', N'laminhky66@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 1:29:02 PM', NULL, N'CrmInstall', N'test list', N'[9]Contact list vs2;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (8, N'admin@gmail.com', N'Cu Dung', N'hanhbt@tuanviet-trading.com;', NULL, N'tamanhhaiqb@gmail.com;tam.nguyenanh@dutchlady.com.vn;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', 5, N'10/4/2018 4:23:33 PM', NULL, N'CrmInstall', N'fdsdsdsd', N'[9]Contact list vs2;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (9, N'admin@gmail.com', N'Cu Dung', N'thuy.ducloi@gmail.com;', NULL, NULL, N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', 5, N'10/4/2018 4:23:34 PM', NULL, N'CrmInstall', N'fdsdsdsd', N'[9]Contact list vs2;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (10, N'admin@gmail.com', N'Cu Dung', N'nppthanhloi.hue@gmail.com;', N'nppthanhloi.hue@gmail.com;', N'vuong.leminh@dutchlady.com.vn; toan.truongnguyen@dutchlady.com.vn;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', 5, N'10/4/2018 4:23:34 PM', NULL, N'CrmInstall', N'fdsdsdsd', N'[9]Contact list vs2;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (11, N'admin@gmail.com', N'Cu Dung', N'nppthanhloi.hue@gmail.com;', N'vuong.leminh@dutchlady.com.vn; toan.truongnguyen@dutchlady.com.vn;', N'nppthanhloi.hue@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;XN THANH LOI,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 4:24:38 PM', NULL, N'CrmInstall', N'Thông báo chương trình khuyến mại mùa hè 2018', N'[14]Oct 2018;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (12, N'admin@gmail.com', N'Cu Dung', N'tuyettranthi272@gmail.com;', N'levannhatthanh@yahoo.com;thanh.levannhat@dutchlady.com.vn;Duc.lequang@frieslandcampina.com;', N'trantruongdng@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;XN THANH LOI,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 4:24:38 PM', NULL, N'CrmInstall', N'Thông báo chương trình khuyến mại mùa hè 2018', N'[14]Oct 2018;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (13, N'admin@gmail.com', N'Cu Dung', N'nguyenthithuy23101985@gmail.com;lanrungdt@gmail.com;', N'tuxh.fcv@gmail.com;tu.hoangxuan@dutchlady.com.vn;giac.tranvan@frieslandcampina.com;haigiamsat_dlv@yahoo.com;hai.nguyenhuu@dutchlady.com.vn;', N'npphanhduc1@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;XN THANH LOI,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 4:24:38 PM', NULL, N'CrmInstall', N'Thông báo chương trình khuyến mại mùa hè 2018', N'[14]Oct 2018;')
INSERT [dbo].[QueueMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files]) VALUES (14, N'admin@gmail.com', N'Cu Dung', N'dungcv@gmail.com;hongly441@gmail.com;', NULL, NULL, N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;#DistributorName,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/5/2018 9:33:34 AM', NULL, N'CrmInstall', N'Thông báo chương trình khuyến mại ', N'[13]Contact list vs1;')
SET IDENTITY_INSERT [dbo].[QueueMails] OFF
INSERT [dbo].[Region] ([ID], [Name], [Note]) VALUES (1, N'Central', NULL)
INSERT [dbo].[Region] ([ID], [Name], [Note]) VALUES (2, N'East', NULL)
INSERT [dbo].[Region] ([ID], [Name], [Note]) VALUES (3, N'Ha Noi', NULL)
INSERT [dbo].[Region] ([ID], [Name], [Note]) VALUES (4, N'HCM City', NULL)
INSERT [dbo].[Region] ([ID], [Name], [Note]) VALUES (5, N'Mekong', NULL)
INSERT [dbo].[Region] ([ID], [Name], [Note]) VALUES (6, N'MT', NULL)
INSERT [dbo].[Region] ([ID], [Name], [Note]) VALUES (7, N'North', NULL)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 56)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 57)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 58)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 59)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 60)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 61)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 62)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 63)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 64)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 65)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 66)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 67)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 68)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 69)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 70)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 71)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 72)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 73)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 74)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 75)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 76)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 77)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 78)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 79)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 80)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 81)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 82)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 83)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 84)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 85)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 86)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 87)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 88)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 89)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 90)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 91)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 92)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 93)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 94)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 95)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 96)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 97)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 98)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 99)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 100)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 101)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 102)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 103)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 104)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 105)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (1, 106)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 56)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 57)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 58)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 59)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 60)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 61)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 62)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 63)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 64)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 65)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 66)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 67)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 68)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 69)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 70)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 71)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 72)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 73)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 74)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 75)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 76)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 77)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 78)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 79)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 80)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 81)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 82)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 83)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 84)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 85)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 86)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 87)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 88)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 89)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 90)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 91)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 92)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 93)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 94)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 95)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 96)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 97)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 98)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 99)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 100)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 101)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 102)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 103)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (2, 104)
GO
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (6, 56)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (6, 57)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (6, 59)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (6, 105)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (7, 60)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (8, 61)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (8, 62)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (9, 63)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (10, 64)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (10, 65)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (10, 66)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (10, 67)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (10, 68)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (10, 69)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (10, 70)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (10, 71)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (10, 72)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 73)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 74)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 75)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 76)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 77)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 78)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 79)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 80)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 81)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 82)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (11, 83)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (12, 84)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (12, 85)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (12, 86)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (12, 87)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (12, 88)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (12, 89)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (12, 90)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (12, 91)
INSERT [dbo].[ROLE_PERMISSION] ([Role_Id], [Permission_Id]) VALUES (12, 106)
SET IDENTITY_INSERT [dbo].[ROLES] ON 

INSERT [dbo].[ROLES] ([Role_Id], [RoleName], [RoleDescription], [IsSysAdmin]) VALUES (1, N'Administrator', N'Administrator can access all areas of the application by default.', 1)
INSERT [dbo].[ROLES] ([Role_Id], [RoleName], [RoleDescription], [IsSysAdmin]) VALUES (2, N'Standard User', N'Users of the application should be granted this role who are not permitted to undertake administrator duties.  This role must have individual permissions assigned unlike the Administrator role.', 0)
INSERT [dbo].[ROLES] ([Role_Id], [RoleName], [RoleDescription], [IsSysAdmin]) VALUES (5, N'Sửa người dùng', N'Sửa người dùng', 0)
INSERT [dbo].[ROLES] ([Role_Id], [RoleName], [RoleDescription], [IsSysAdmin]) VALUES (6, N'Distributors Manager', N'Distributors Manager', 0)
INSERT [dbo].[ROLES] ([Role_Id], [RoleName], [RoleDescription], [IsSysAdmin]) VALUES (7, N'Home Manager', N'Home Manager', 0)
INSERT [dbo].[ROLES] ([Role_Id], [RoleName], [RoleDescription], [IsSysAdmin]) VALUES (8, N'Log Manager', N'Log Manager', 0)
INSERT [dbo].[ROLES] ([Role_Id], [RoleName], [RoleDescription], [IsSysAdmin]) VALUES (9, N'Login Manager', N'Login Manager', 0)
INSERT [dbo].[ROLES] ([Role_Id], [RoleName], [RoleDescription], [IsSysAdmin]) VALUES (10, N'MailManagement Manager', N'MailManagement Manager', 0)
INSERT [dbo].[ROLES] ([Role_Id], [RoleName], [RoleDescription], [IsSysAdmin]) VALUES (11, N'Permission Manager', N'Permission Manager', 0)
INSERT [dbo].[ROLES] ([Role_Id], [RoleName], [RoleDescription], [IsSysAdmin]) VALUES (12, N'Role Manager', N'Role Manager', 0)
SET IDENTITY_INSERT [dbo].[ROLES] OFF
SET IDENTITY_INSERT [dbo].[SendMails] ON 

INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (1, N'CrmInstall@ivg.vn', N'Crm Install', N'dungcv@gmail.com', N'dungcv1210@gmail.com', N'hongly441@gmail.com', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;Cu Viet Dung,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', 0, NULL, N'10/1/2018 11:39:46 PM', N'CrmInstall', N'Thông báo chương trình khuyến mại ', NULL, NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (2, N'CrmInstall@ivg.vn', N'Crm Install', N'dungcv@gmail.com', N'dungcv@epu.edu.vn', N'hongly441@gmail.com', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;Cu Viet Dung,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', 0, NULL, N'10/1/2018 11:39:46 PM', N'CrmInstall', N'Thông báo chương trình khuyến mại ', NULL, NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (3, N'admin@gmail.com', N'Cu Dung', N'hanhbt@tuanviet-trading.com;', N'hanhbt@tuanviet-trading.com;', N'nult@tuanviet-trading.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 1:29:02 PM', N'10/4/2018 1:29:02 PM', N'CrmInstall', N'test list', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (4, N'admin@gmail.com', N'Cu Dung', N'thuy.ducloi@gmail.com;', N'nppducloi@gmail.com;', N'thuy.ducloi@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 1:29:02 PM', N'10/4/2018 1:29:02 PM', N'CrmInstall', N'test list', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (5, N'admin@gmail.com', N'Cu Dung', N'trtrangml@gmail.com;', N'nthuy1102@gmail.com;', N'trtrangml@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 1:29:02 PM', N'10/4/2018 1:29:02 PM', N'CrmInstall', N'test list', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (6, N'admin@gmail.com', N'Cu Dung', N'truongthanhphathoian@gmail.com;', N'truongthanhphathoianco.ld@gmail.com;', N'truongthanhphathoianco.ld@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 1:29:02 PM', N'10/4/2018 1:29:02 PM', N'CrmInstall', N'test list', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (7, N'admin@gmail.com', N'Cu Dung', N'tohien76@gmail.com;', N'tohien76@gmail.com;', N'laminhky66@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 1:29:02 PM', N'10/4/2018 1:29:02 PM', N'CrmInstall', N'test list', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (8, N'admin@gmail.com', N'Cu Dung', N'hanhbt@tuanviet-trading.com;', NULL, N'tamanhhaiqb@gmail.com;tam.nguyenanh@dutchlady.com.vn;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', 5, N'10/4/2018 4:23:33 PM', N'10/4/2018 4:23:33 PM', N'CrmInstall', N'fdsdsdsd', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (9, N'admin@gmail.com', N'Cu Dung', N'thuy.ducloi@gmail.com;', NULL, NULL, N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', 5, N'10/4/2018 4:23:34 PM', N'10/4/2018 4:23:33 PM', N'CrmInstall', N'fdsdsdsd', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (10, N'admin@gmail.com', N'Cu Dung', N'nppthanhloi.hue@gmail.com;', N'nppthanhloi.hue@gmail.com;', N'vuong.leminh@dutchlady.com.vn; toan.truongnguyen@dutchlady.com.vn;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', 5, N'10/4/2018 4:23:34 PM', N'10/4/2018 4:23:33 PM', N'CrmInstall', N'fdsdsdsd', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (11, N'admin@gmail.com', N'Cu Dung', N'nppthanhloi.hue@gmail.com;', N'vuong.leminh@dutchlady.com.vn; toan.truongnguyen@dutchlady.com.vn;', N'nppthanhloi.hue@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;XN THANH LOI,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 4:24:38 PM', N'10/4/2018 4:24:38 PM', N'CrmInstall', N'Thông báo chương trình khuyến mại mùa hè 2018', N'[14]Oct 2018;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (12, N'admin@gmail.com', N'Cu Dung', N'tuyettranthi272@gmail.com;', N'levannhatthanh@yahoo.com;thanh.levannhat@dutchlady.com.vn;Duc.lequang@frieslandcampina.com;', N'trantruongdng@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;XN THANH LOI,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 4:24:38 PM', N'10/4/2018 4:24:38 PM', N'CrmInstall', N'Thông báo chương trình khuyến mại mùa hè 2018', N'[14]Oct 2018;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (13, N'admin@gmail.com', N'Cu Dung', N'nguyenthithuy23101985@gmail.com;lanrungdt@gmail.com;', N'tuxh.fcv@gmail.com;tu.hoangxuan@dutchlady.com.vn;giac.tranvan@frieslandcampina.com;haigiamsat_dlv@yahoo.com;hai.nguyenhuu@dutchlady.com.vn;', N'npphanhduc1@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;XN THANH LOI,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>

<p>Quang Vo,</p>
', 5, N'10/4/2018 4:24:38 PM', N'10/4/2018 4:24:38 PM', N'CrmInstall', N'Thông báo chương trình khuyến mại mùa hè 2018', N'[14]Oct 2018;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (20, N'admin@gmail.com', N'Cu Dung', N'hanhbt@tuanviet-trading.com;', N'hanhbt@tuanviet-trading.com;', N'nult@tuanviet-trading.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>', 5, NULL, N'10/4/2018 1:29:02 PM', N'CrmInstall', N'test list', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (22, N'admin@gmail.com', N'Cu Dung', N'thuy.ducloi@gmail.com;', N'nppducloi@gmail.com;', N'thuy.ducloi@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>', 5, NULL, N'10/4/2018 1:29:02 PM', N'CrmInstall', N'test list', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (24, N'admin@gmail.com', N'Cu Dung', N'trtrangml@gmail.com;', N'nthuy1102@gmail.com;', N'trtrangml@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>', 5, NULL, N'10/4/2018 1:29:02 PM', N'CrmInstall', N'test list', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (26, N'admin@gmail.com', N'Cu Dung', N'truongthanhphathoian@gmail.com;', N'truongthanhphathoianco.ld@gmail.com;', N'truongthanhphathoianco.ld@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>', 5, NULL, N'10/4/2018 1:29:02 PM', N'CrmInstall', N'test list', N'[9]Contact list vs2;', NULL)
INSERT [dbo].[SendMails] ([ID], [fromAddress], [fromName], [ToAdd], [Cc], [Bcc], [Body], [Template], [QueueTime], [SentTime], [SentBy], [Subject], [Files], [Code]) VALUES (28, N'admin@gmail.com', N'Cu Dung', N'tohien76@gmail.com;', N'tohien76@gmail.com;', N'laminhky66@gmail.com;', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;CTY TNHH TMTH TUAN VIET,</p>', 5, NULL, N'10/4/2018 1:29:02 PM', N'CrmInstall', N'test list', N'[9]Contact list vs2;', NULL)
SET IDENTITY_INSERT [dbo].[SendMails] OFF
SET IDENTITY_INSERT [dbo].[tbl_Distributors] ON 

INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (1, N'337449', N'CN DN TU NHAN THUONG MAI MINH CHIEU', N'Thôn thạch trụ Tây, Xã Đức Lân, Huyện Mộ Đức, Tỉnh Quảng Ngãi', 1, N'dungcv1210@gmail.com', N'hongly44@gmail.com', N'', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (2, N'337456', N'DOANH NGHIEP TU NHAN TM GIA PHU KHANG', N'4 Lê Thị Hồng Gấm, TT Eakar, Eakar, Daklak', 1, N'', N'dungcv1210@gmail.com', N'dungcv@epu.edu.vn', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (3, N'337349', N'CTY TNHH TMTH TUAN VIET', N'Đường Thống Nhất – TK Diêm Thượng- Phường Đức Ninh Đông- TP Đồng Hới –Tỉnh Quảng Bình', 1, N'hanhbt@tuanviet-trading.com', N'tamanhhaiqb@gmail.com;tam.nguyenanh@dutchlady.com.vn', N'nult@tuanviet-trading.com', N'hanhbt@tuanviet-trading.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (4, N'337355', N'CTY TNHH TM DV DUC LOI', N'215B Nguyễn Du, Đông Hà, Quảng Trị', 1, N'nppducloi@gmail.com', N'lenhattan.vn@gmail.com;tan.lenhat@dutchlady.com.vn', N'thuy.ducloi@gmail.com', N'thuy.ducloi@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (5, N'337354', N'XN THANH LOI', N'06 - ĐƯỜNG SỐ 3 KCN AN HÒA - TT HUẾ', 1, N'nppthanhloi.hue@gmail.com', N'vuong.leminh@dutchlady.com.vn; toan.truongnguyen@dutchlady.com.vn', N'nppthanhloi.hue@gmail.com', N'nppthanhloi.hue@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (6, N'337341', N'CONG TY TNHH TM VA   DVTH TRAN TRUO', N'53 Thanh Sơn, Đà Nẵng', 1, N'suong.ttb@gmail.com;tranhuuhai.trantruong@gmail.com', N'levannhatthanh@yahoo.com;thanh.levannhat@dutchlady.com.vn;Duc.lequang@frieslandcampina.com', N'trantruongdng@gmail.com', N'tuyettranthi272@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (7, N'337342', N'CTY TNHH HUU PHUC', N'452 Trần Cao Vân, Đà Nẵng', 1, N'truongquangkhai452@yahoo.com;Ngoc.HoangThi@frieslandcampina.com;', N'truongnguyentoan82@gmail.com;toan.truongnguyen@dutchlady.com.vn;Duc.lequang@frieslandcampina.com', N'truongquangkhai452@yahoo.com', N'huuphucltdco@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (8, N'337433', N'CONG TY TNHH TM VA DV TH MINH LOC', N'294 CMT8 - Quận Cẩm Lệ - TP Đà Nẵng', 1, N'nthuy1102@gmail.com', N'trung.nguyenquang@dutchlady.com.vn;duc.lequang@frieslandcampina.com', N'trtrangml@gmail.com', N'trtrangml@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (9, N'337351', N'CTY TNHH TMDV TRUONG THANH PHAT', N' 63 Nguyễn Văn Cừ, Tân An, Hội An, Quảng Nam', 1, N'truongthanhphathoianco.ld@gmail.com', N'thi.nguyenviet@dutchlady.com.vn;Duc.lequang@frieslandcampina.com', N'truongthanhphathoianco.ld@gmail.com', N'truongthanhphathoian@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (10, N'337350', N'DNTN NGHIA LOI', N'451-453 Hùng Vương, Tam Kỳ, Quảng Nam', 1, N'tohien76@gmail.com', N'dungvovan@gmail.com;dung.vovan@dutchlady.com.vn;Duc.lequang@frieslandcampina.com;hung.lechan@dutchlady.com.vn', N'laminhky66@gmail.com', N'tohien76@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (11, N'337353', N'DNTN TM MINH CHIEU', N'Lô 63, 64, 65 đường 30/4 TP Quảng Ngãi', 1, N'ltmchieu1011@yahoo.com', N'levannghiaqn@yahoo.com.vn;nghia.levan@dutchlady.com.vn;Duc.lequang@frieslandcampina.com', N'ltmchieu1011@yahoo.com', N'anhvu1089@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (12, N'337352', N'DNTN VAN KHOI', N'Bình Trung, Bình Sơn, Quảng Ngãi', 1, N'vankhoidlv@gmail.com', N'huyvunguyen1977@yahoo.com.vn;vu.nguyenhuy@dutchlady.com.vn;Duc.lequang@frieslandcampina.com', N'vankhoico@yahoo.com.vn', N'vankhoidlv@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (13, N'337395', N'CTY TNHH THANH HUNG', N'7-8 Phan Huy Ích, P.Nhơn Bình, TP.Quy Nhơn, Bình Định', 1, N'nguyenvy.29@gmail.com', N'ledat_pro@yahoo.com.vn;dat.leminh@dutchlady.com.vn;quang.lehuy@frieslandcampina.com', N'nppthanhhung01@gmail.com', N'luongthixuan@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (14, N'337339', N'CONG TY TNHH TM HOANG BINH', N'Thôn Ngọc Thạnh - Xã Phước An - Huyện Tuy Phước - Tỉnh Bình Định', 1, N'maitranghoangbinh@yahoo.com.vn', N'truongthanhfcv@yahoo.com.vn;thanh.votruong@dutchlady.com.vn;quang.lehuy@frieslandcampina.com', N'maitranghoangbinh@yahoo.com.vn', N'dat8369@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (15, N'337340', N'CTY TNHH TM TRUNG TIN', N'Khối 5-Thị trấn Bồng Sơn-huyện Hoài Nhơn-Tỉnh Bình Định', 1, N'entanhiepphat@gmail.com', N'lanvpt2005@yahoo.com;lan.vuphanthe@dutchlady.com.vn;quang.lehuy@frieslandcampina.com', N'trungquynh70@gmail.com', N'ngocnhi043@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (16, N'337348', N'CTY TNHH TM VAN PHUC', N'A37-40 đường Cần Vương - P2- TP Tuy Hòa-Phú Yên', 1, N'vanphucdutchlady@gmail.com', N'vuhdntrang@yahoo.com.vn;vu.voquang@dutchlady.com.vn;quang.lehuy@frieslandcampina.com', N'vanphucpy@gmail.com', N'vanphucpy@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (17, N'337410', N'CONG TY TNHH THUONG MAI THIEN PHUC', N'Lô 7,8 Đường số 6, KDC Tây Dân Phước,Phường Xuân Thành, TX Sông Cầu, Tỉnh Phú Yên', 1, N'thienphucdutchlady@gmail.com', N'quang.lehuy@frieslandcampina.com', N'thienphucdutchlady@gmail.com', N'minhvinh610@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (18, N'337428', N'CUA HANG SO 1-CONG TY TNHH QUOC PHO', N'68 Trần Qúy Cáp - Phường Ninh Hiệp - Thị Xã Ninh Hòa - Tỉnh Khánh Hòa', 1, N'quocphongco@gmail.com', N'minhvuong0077@gmail.com;vuong.nguyenminh@dutchlady.com.vn;quang.lehuy@frieslandcampina.com', N'chinhanhquocphongco@gmail.com;ldhoang6169@gmail.com', N'nguyentthanhdutchlady@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (19, N'337346', N'DNTN NAM TIEN', N'88 Đường Phạm Văn Đồng, Khóm Phú Sơn, Cam Phú, Khánh Hòa', 1, N'thuthuy_ntt24@yahoo.com.vn', N'katty.poncho@gmail.com;son.vongoc@dutchlady.com.vn;quang.lehuy@frieslandcampina.com', N'ngocthanh_namtien@yahoo.com', N'thanhnamtien.camranh@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (20, N'337344', N'DNTN TM HUNG NHAN', N'20 Trần Bình Trọng, Pleiku, Gia Lai', 1, N'duyenchaupleiku@gmail.com', N'Nguyenlinhpleiku@yahoo.com;linh.nguyen@dutchlady.com.vn;giac.tranvan@frieslandcampina.com', N'ceo_hungnhan@yahoo.com.vn', N'huynhdienpk@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (21, N'337416', N'DOANH NGHIEP THUONG MAI HOANG YEN', N'Thôn 4 Xã I Pal -CHư Sê- gia Lai', 1, N'hoangyengialai@gmail.com', N'Nguyenlinhpleiku@yahoo.com;linh.nguyen@dutchlady.com.vn;giac.tranvan@frieslandcampina.com', N'hoangyengialai@gmail.com', N'hoangyengialai@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (22, N'337343', N'CTY TNHH MOT THANH VIEN NGUYEN THAN', N'430 Quang Trung- An Khê -Gia lai', 1, N'hungnguyenak2009@gmail.com', N'Nguyenlinhpleiku@yahoo.com;linh.nguyen@dutchlady.com.vn;giac.tranvan@frieslandcampina.com', N'diepdung64@gmail.com', N'hungnguyenak2009@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (23, N'337347', N'CTY THUONG MAI THAI BINH DUONG(TNHH', N'370 Bà Triệu - Tp. Kon Tum', 1, N'npptbduong@gmail.com', N'giac.tranvan@frieslandcampina.com;tuan.nguyenquoc@dutchlady.com.vn;giac.tranvan@frieslandcampina.com', N'npptbduong@gmail.com', N'huyenphamthile@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (24, N'337313', N'DNTN TM DUC HANH', N'85 Hoàng Diệu,Phường An Bình, Thị xã Buôn Hồ, Daklak/ 360 Hùng Vương nối dài, P. Tân Lập, Tp. Ban Mê Thuột, Daklak', 1, N'thanhthuy280983@gmail.com;dunghalan11@gmail.com', N'tuxh.fcv@gmail.com;tu.hoangxuan@dutchlady.com.vn;giac.tranvan@frieslandcampina.com;haigiamsat_dlv@yahoo.com;hai.nguyenhuu@dutchlady.com.vn', N'npphanhduc1@gmail.com', N'nguyenthithuy23101985@gmail.com;lanrungdt@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (25, N'337419', N'CONG TY TNHH MOT THANH VIEN NINH UY', N'26A, Thôn Kim Châu, xã Dray Bhang, Huyện Cư Kuin, Tỉnh Daklak', 1, N'phuonghalan84@gmail.com', N'giac.tranvan@frieslandcampina.com', N'tram.nguyenthihoang@gmail.com;nppninhuyen@gmail.com', N'tram.nguyenthihoang@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (26, N'337308', N'DNTN HUU LAM (TRAN THI NGA)', N'460/16c,KP Đông Tân,Thị Trấn Dĩ An, Huyện Dĩ An, Bình Dương', 2, N'dntnnhuulam@yahoo.com.vn', N'khoa.phanhoangviet@dutchlady.com.vn', N'dntnhuulam@yahoo.com.vn', N'dntnnhuulam@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (27, N'337486', N'CONG TY TNHH TONG HOP DINH THANH', N'', 2, N'nppanhthanhgd@gmail.com', N'', N'nppanhthanhgd@gmail.com', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (28, N'337950', N'CTY TNHH TM DV DUNG THANH DAT', N'28/1 KHU PHO THANH BINH, P.AN THANH, TX THUAN AN, TINH BINH DUONG', 2, N'thaithihuyen78@gmail.com', N'', N'congtytnhhdungthanhdat@gmail.com', N'congtytnhhdungthanhdat@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (29, N'338061', N'CTY TNHH DV - TM QUANG XUAN TRUONG', N'SO 322, TO 5, AP 1B, XA PHUOC HOA, HUYEN PHU GIAO, TINH BINH DUONG', 2, N'tieuquangia86@gmail.com', N'', N'congtytnhhquangtruongxuan@gmail.com', N'hongnam1904@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (30, N'338062', N'CTY TNHH MTV TRAN MINH TIEN', N'SO 220/12A, KP4, P.TAN DINH, TX BEN CAT, BINH DUONG', 2, N'minhtien@masandistribution.vn', N'', N'utbeo.bmt@gmail.com', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (31, N'338104', N'CTY CO PHAN THUONG MAI BINH THUAN', N'TANG 4-5, SO 40 NGUYEN THI MINH KHAI, P.DUC NGHIA, TP. PHAN THIET, TINH BINH THUAN', 2, N'phuongnguyen.tmbt@gmail.com', N'', N'phuongnguyen.tmbt@gmail.com', N'mainguyen09ctk@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (32, N'337446', N'CONG TY TNHH HUNG HUY', N'Tổ 6 khu phố 7,  P. thống Nhất, TP Biên Hòa, Tinh Đồng Nai', 2, N'trungthanh.nguyen86@gmail.com', N'thanh.nguyentrung@dutchlady.com.vn', N'npphunghuy@gmail.com', N'ketoannpphunghuy@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (33, N'337422', N'CONG TY TNHH MTV NGOC KHANH TAM', N'Số 43, Đường Chợ 2,Ấp Hòa Bình, Xã Đông Hòa, H.Trảng Bom, Tỉnh Đồng Nai', 2, N'congtyngockhanhtam@gmail.com', N'duc.trannguyentri@frieslandcampina.com', N'ngocbakhanh@gmail.com', N'ngocbakhanh@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (34, N'337314', N'CTY TNHH HANH HUYEN', N'54 Quốc Lộ 1, Khu Xuân Bình, Thị trấn Xuân Lộc, Long Khánh, Đồng Nai;K8, QL1A, TT Gia Ray, Xuân Lộc, Đồng Nai', 2, N'thanhtrungintel@gmail.com;ndthimba@gmail.com', N'trung.hovan@dutchlady.com.vn;thi.nguyendinh@dutchlady.com.vn', N'hanhhuyenco@gmail.com', N'thanhthaolk2000@yahoo.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (35, N'337315', N'DNTN KHOI KHUYEN', N'120 Tổ 4, Ấp Phương Lâm,Xã Phú Lâm,Tân Phú, Đồng Nai', 2, N'leduy.fcv@gmail.com;thaothanh399@gmail.com', N'duy.le@dutchlady.com.vn', N'vuduykhuong2107@gmail.com', N'hoangmy2410@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (36, N'337397', N'CONG TY TNHH MTV NGOC OANH', N'18, Lý Thường Kiệt, Phường Phú Cường, Thị xã Thủ Dầu Một, Bình Dương', 2, N'THUONG.DUONGVAN@YAHOO.COM.VN;Ngoc.HoangThi@frieslandcampina.com;', N'tuyetsuong.28kt@gmail.com', N'congty_ngocoanh@yahoo.com', N'congty_ngocoanh@yahoo.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (37, N'337319', N'CTY TNHH TM & DV HIEP THANH', N'51, Phạm Văn Xuyên, khu phố 6, Phường 3, Thành Phố Tây Ninh, Tỉnh Tây Ninh.', 2, N'', N'thanh.nguyenthithao@dutchlady.com.vn', N'kimhoa.ht_tn@yahoo.com', N'kimhoa.ht_tn@yahoo.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (38, N'337447', N'DOANH NGHIEP TU NHAN NGUYEN NGOC PHUNG', N'Ấp Tân Mai 2, Xã Phước Tân, Biên hòa, Đồng Nai', 2, N'binhdn44@yahoo.com', N'binh.nguyenvan@dutchlady.com;binhnguyen@gmail.com', N'nguyenngocnga76@gmail.com', N'nguyenngocnga76@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (39, N'337439', N'CONG TY TNHH DAO PHUONG LINH', N'242/1 xã Gia Tân 1, huyện Thống Nhất, tỉnh Đồng Nai', 2, N'npp.phuonglinh1206@gmail.com', N'binh.nguyenvan@dutchlady.com.vn', N'npp.daophuonglinh@gmail.com;npp.phuonglinh1206@gmail.com', N'npp.daophuonglinh@gmail.com;npp.phuonglinh1206@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (40, N'337389', N'CTY TNHH XUAN BAC HA', N'30/4, Đường Nguyễn Hải - Tổ 4, Ấp 1 - An Phước - Long thành - Đồng Nai', 2, N'npp.xuanbacha@gmail.com;', N'binh.vovan@dutchlady.com.vn', N'npp.xuanbacha@gmail.com;', N'linh.nth0506@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (41, N'337391', N'CTY TNHH TM-DV TONG HOP THANH BUOC', N'68 Đinh Tiên Hoàng, KP Phú Bình, Phường An Lộc, Thị xã Bình Long, Bình Phước;Đường Nguyễn Huệ, Tổ 3, Khu phố Suối Đá, P.Tân Xuân, Thị xã Đồng Xoài, Bình Phước', 2, N'nguyenquocthien2000@yahoo.com.vn;duongthanh6706@yahoo.com', N'thoai.duongthanh@dutchlady.com.vn', N'thanhbuoc@yahoo.com', N'thanhbuoc@yahoo.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (42, N'337412', N'CONG TY TNHH MTV TM&DV NGUYEN HUNG', N'209, DT 759, Khu phố 1, Phường Phước Bình, Thị xã Phước Long, Bình Phước', 2, N'lehoaihoan@live.com', N'hoan.lehoai@dutchlady.com.vn', N'nguyenhung_pb@yahoo.com', N'hoyden300491@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (43, N'337306', N'CTY TNHH TMDV PHUONG ANH', N'Tổ 12 Khu Phố Hải Dinh, P.Kim Dinh- Thị Xã Bà Rịa-Bà Rịa Vũng Tàu', 2, N'bang_vtdl@yahoo.com', N'tuankiet.bec@gmail.com', N'hoangtq.thong@gmail.com', N'ctyphuonganh.tnhh@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (44, N'337417', N'CHI NHANH CONG TY TNHH TMDV PHUONG', N'120 Đường Phạm Hùng,KP Rạch Sơn, TT Phước Bửu, Xuyên Mộc, BRVT', 2, N'lehoainam.friesland@gmail.com', N'nam.lehoai@dutchlady.com.vn', N'hoangtq.thong@gmail.com', N'huyentrangqtxm@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (45, N'337445', N'CONG TY TNHH THUONG MAI MINH GIA', N'132/10 Nguyễn Tri Phương P7 Thành Phố Vũng Tàu ', 2, N'ngadthuulinh@gmail.com', N'Nga.dothi@dutchlady.com.vn', N'tranduylinh@huulinh.com.vn', N'tuyen.minhtam@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (46, N'337310', N'DNTN TRUNG YEN HUNG', N'83 Trần Huy Cáp- Phan Thiết- Bình Thuận', 2, N'thai.hosy@frieslandcampina.com', N'quyen.maivan@dutchlady.com.vn', N'trungyenhung59@gmail.com', N'trungyenhung59pt@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (47, N'337312', N'DNTN TM HOANG HA', N'103 Đường 3/2 Thị Trấn Đức Tài - Đức Linh - Bình Thuận', 2, N'transam1601@gmail.com', N'hop.duongkhanh@dutchlady.com.vn;duongkhanhhop@gmail.com', N'ha.ntc2903@gmail.com', N'ha.ntc2903@gmail.com;thanhchipham1966@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (48, N'337318', N'CTY TNHH TM-DV PHUONG DINH', N'199 B, Đường 21/8, Phan Rang, Ninh Thuận', 2, N'lequynuong@gmail.com;phucanh_pr@yahoo.com', N'canh.dangphu@dutchlady.com.vn', N'phuongdinhninhthuan@gmail.com', N'lyngocsuong@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (49, N'337316', N'CTY TNHH QUANG THAI', N'2B, Hoàng Văn Thụ nối dài, Phường 5, Đà Lạt;24 Nguyễn Thái Học , Khu Phố 2, Tổ 13, Đức TRọng , Lâm Đồng', 2, N'Cuong.ford@gmail.com', N'cuong.nguyenhuy@dutchlady.com.vn', N'phuong.dinhthimy@quangthai.com', N'quocthienkt@quangthai.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (50, N'337317', N'CTY TNHH SX-TM & DV TAM DAN', N'26 Lê Thị Pha, Bảo Lộc , Lâm Đồng', 2, N'87anhtuan@gmail.com;thanhdunghl.tamdan@gmail.com', N'tuan.maianh@dutchlady.com.vn', N'manhhungbloc@gmail.com', N'thanhdunghl.tamdan@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (51, N'337396', N'CTY TNHH MTV VINH PHUOC', N'Tổ 4- Đường Tống Duy Tân-Phường Nghĩa Thành- Thị Xã Gia Nghĩa- Tỉnh Đak Nông', 2, N'thuongsondn@yahoo.com', N'Son.phamthuong@dutchlady.com.vn', N'npp.vinhphuoc@gmail.com', N'npp.vinhphuoc@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (52, N'337434', N'CTY TNHH MTV TM VA DV T.TRANG DAK N', N'Thôn Đức Tân, Xã Đức Mạnh, Huyện Đakmin, Tỉnh ĐakNông', 2, N'npptuyettrang@gmail.com', N'', N'nguyenthuy7899@yahoo.com', N'npptuyettrang@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (53, N'337459', N'CTY TNHH TUONG VAN 45', N'', 2, N'thuphuong2431988@gmail.com', N'qui.tran@dutchlady.com.vn', N'npptuongvan79@gmail.com', N'antienphat8445@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (54, N'337336', N'CTY TNHH TM & DV KT TRI TIN', N'24A Bàu Cát 2, Phường 14, Tân Bình, TP.HCM', 4, N'thutrinh77@gmail.com;haimarine2.vn@gmail.com;h_cuc_84@yahoo.com', N'kiet.caoanh@dutchlady.com.vn;tuan.tathanh@frieslandcampina.com;lynh.hongoc@dutchlady.com.vn', N'phhlien64@gmail.com', N'bichphung0206@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (55, N'337337', N'CTY TNHH TM & DV HOANG PHONG', N'21/6B Phan Huy Ích , F 14, Q. Gò Vấp, Tp.HCM', 4, N'hoangphongco@gmail.com;tuan.luongthanh@dutchlady.com.vn;Ngoc.HoangThi@frieslandcampina.com;', N'', N'yenhoangphong@gmail.com;ketoanhoangphong@gmail.com', N'hoangphong.dms@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (56, N'337385', N'CONG TY TNHH TM NHAT TRI KHANG', N'Số 179, Đường Tam Bình, KP2, P.Tam Phú, Q.Thủ Đức, Tp.HCM', 4, N'npptrikhang@yahoo.com.vn', N'dung.tranchi@frieslandcampina.com', N'nhattrikhang.ltd@gmail.com', N'npptrikhang@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (57, N'337330', N'CTY TNHH THAO VY', N'194/12 - Đường 8, KP3, Linh Xuân, Thủ Đức, Tp. HCM', 4, N'thaovy.dms@gmail.com', N'dung.tranchi@frieslandcampina.com', N'thaovy.dms@gmail.com', N'ailethaovy@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (58, N'337418', N'CONG TY TNHH DAU TU TMDV TOAN DUC PH 1', N'240 Tôn Thất Thuyết - Phường 3 - Quận 4_x000D_, Tp. HCM/ 701/18/1 Nguyễn Văn Tạo, Xã Long Thới, Huyện Nhà Bè, Tp. HCM', 4, N'hoatulip1006@yahoo.com', N'tri.caothien@frieslandcampina.com', N'vinhphuong.dang@yahoo.com', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (59, N'337403', N'CONG TY TNHH MTV TM CHIN NGUYEN', N'248 Lương Định Của, Phường An Phú Quận 2, Tp. HCM', 4, N'congtychinnguyen@gmail.com', N'dung.tranchi@frieslandcampina.com', N'congtychinnguyen@gmail.com', N'hoanguyen3567@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (60, N'337331', N'CTY TNHH MINH NGUYET', N'30/2 ẤP Trung chánh 2, xã Trung Chánh, huyện Hóc Môn, TP.HCM', 4, N'xuanyenq12@yahoo.com', N'', N'nppminhnguyet@yahoo.com.vn', N'nppminhnguyet12@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (61, N'337426', N'CONG TY TNHH THUONG   MAI PHU AN', N'15-Đường 61-Phường Tân Kiểng-Quận 7', 4, N'npp.ngocnhi@gmail.com', N'tri.caothien@frieslandcampina.com;quan.hoangvan@dutchlady.com.vn', N'npp.phuan.ltd@gmail.com', N'hongdiepkt9484@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (62, N'337329', N'CTY TNHH TM DUYET HY', N'12 đường số 10, F11,Q6. TPHCM ', 4, N'ngoc186vn@yahoo.com;hue.duyethy@gmail.com', N'nhut.trancao@dutchlady.com.vn;tuan.tathanh@frieslandcampina.com', N'ctduyethy@yahoo.com.vn', N'pthanhtruc01@yahoo.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (63, N'337485', N'CONG TY TNHH SX TM DV CO DIEN LANH HUYNH THAO', N'', 4, N'npphuynhthao@yahoo.com', N'', N'npphuynhthao@yahoo.com', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (64, N'337949', N'CONG TY TNHH DUC BINH AN', N'128 GO DUA, KP3, P. TAM BINH, Q. THU DUC', 4, N'ctyducbinhan@gmail.com', N'', N'ctyducbinhan@gmail.com', N'ctyducbinhan@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (65, N'337371', N'CTY TNHH TM & DV CUONG THINH', N'Số 4 Ngô Thì Nhậm - Hoàn Kiếm Tp. Hà Nội', 3, N'thongbaocuongthinh@gmail.com;ngoc.HoangThi@frieslandcampina.com;', N'nguyenvanha.fcv@gmail.com;huy.vuquang@frieslandcampina.com', N'tram.cuongthinh@gmail.com', N'thuy.dt199@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (66, N'337374', N'CONG TY TNHH THUONG MAI & XNK', N' Số 7/544 Nguyễn Văn Cừ - Long Biên - Hà Nội', 3, N'le.vannga2009@gmail.com', N'tung.bt80@gmail.com;cuong.nguyenthe@frieslandcampina.com', N'anhntxnk@gmail.com', N'buiphuonghao@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (67, N'337372', N'CTY CO PHAN PHAN PHOI THUC PHAM PT', N'352 Giải Phóng - Phương Liệt - Thanh Xuân - Hà Nội', 3, N'nppptfood@phuthaigroup.com', N'tranquangtung.fcv@gmail.com;cuong.nguyenthe@frieslandcampina.com', N'ngoc.nguyenbich@phuthaigroup.com', N'lan.phanngoc@phuthaigroup.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (68, N'337450', N'CTY TNHH KINH DOANH THUONG MAI ANH HUY', N'71 Lê Lợi -Thị Trấn Vân Đình- Huyện Ứng Hòa - TP Hà Nội', 3, N'ctyanhhuy.ht@gmail.com', N'buihung2702@gmail.com;duc.nguyenthe@frieslandcampina.com', N'ctyanhhuy.ht@gmail.com', N'ctyanhhuy.ht@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (69, N'337484', N'CHI NHANH CONG TY TNHH THUONG MAI TONG HOP THU DO', N'', 3, N'thudonpp@gmail.com', N'', N'phungnguyenquy@gmail.com;', N'thudonpp@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (70, N'337478', N'CTY TNHH DAU TU TM VA DV LAN CHI', N'', 3, N'phongketoan@lanchi.vn;', N'', N'nguyenthilanhuong@lanchi.vn', N'phongketoan@lanchi.vn;', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (71, N'337471', N'CTY TNHH DV & TM THAI SON', N'Đường 305 phường Hội Hợp, Thành phố Vĩnh Yên', 3, N'hahuong120993vp@gmail.com', N'letruongnam.fcv@gmail.com;duc.nguyenthe@frieslandcampina.com', N'pttrang80@gmail.com', N'nguyenthao94vp@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (72, N'337466', N'CTY CO PHAN XUAT NHAP KHAU HP VN ', N'', 3, N'hp.npphalan@gmail.com', N'hp.npphalan@gmail.com;cuong.nguyenthe@frieslandcampina.com;ngothanhvinh.fcv@gmail.com', N'nguyenhuyviet.hanoi@gmail.com;chi.tl@celia.vn', N'hp.npphalan@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (73, N'337375', N'CTY TNHH TM TIN NGHIA', N'Thôn Phú Thụy - Phú Thị - Gia Lâm - Tp. Hà Nội', 3, N'tinnghia1.fcv@gmail.com', N'nguyentienduan.fcv@gmail.com;duc.nguyenthe@frieslandcampina.com', N'tinnghia1.fcv@gmail.com', N'hongson2601@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (74, N'337656', N'HOP TAC XA THUONG MAI QUAN 3', N'', 6, N'thuy.ntt@tricoop.vn', N'', N'vkthanh6920@yahoo.com', N'dieuq3@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (75, N'337464', N'CTY TNHH MTV HUYNH HOANG PHAT ', N'', 5, N'kieuphuong.hh@gmail.com', N'khanh.huynhminh@dutchlady.com.vn', N'npphuynhhoangphat@gmail.com', N'npphuynhhoangphat@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (76, N'337467', N'CONG TY TNHH TM BAO HAN SA DEC ', N'', 5, N'duongmongngoc39@gmail.com', N'', N'nguyencongthanhnpp@gmail.com', N'phungkieudt@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (77, N'337476', N'CTY TNHH MTV THUC PHAM NGUYEN KHOI', N'18/47 Khu pho 1, P.4,Thi xa Cai Lay, Tien Giang', 5, N'ngocgiaufcvn@yahoo.com', N'', N'', N'ngocgiaufcvn@yahoo.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (78, N'337479', N'CONG TY TNHH TM DO NGOC DIEP', N'03 NGUYEN DUY, PHUONG 1;THANH PHO TAN AN, TINH LONG AN', 5, N'ngochanhchso3@yahoo.com', N'', N'nhknguyen@yahoo.com', N'nhknguyen@yahoo.com; ngochanhchso3@yahoo.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (79, N'337475', N'CTY TNHH MTV 168 LU QUOC THAI', N'560-562 LE LOI, KIEN TUONG,LONG AN', 5, N'nppkimchung2012@yahoo.com.vn', N'caotri260793@gmail.com', N'', N'nguyenngoctruc1988@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (80, N'337477', N'DNTN LAN QUY', N'80B Phan Boi Chau,P.2,Thi xa Go Cong,Tien Giang', 5, N'trucmai.2000@gmail.com;', N'', N'cuahanglanqui@yahoo.com', N'trucmai.2000@gmail.com;mai.lanqui@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (81, N'337480', N'Trúc Giang', N'', 5, N'lehuutam83@gmail.com', N'', N'lehuutam83@gmail.com', N'tuyet.trucgiang@yahoo.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (82, N'337488', N'CONG TY TNHH MTV MAI TRAM', N'1622, XA THANH LOI, BINH TAN, VINH LONG', 5, N'nppmaitram@gmail.com', N'', N'nppmaitram@gmail.com', N'nppmaitram@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (83, N'337454', N'CONG TY TNHH MTV TM HUNG HOA', N'', 5, N'lvloi2003@gmail.com', N'', N'lvloi2003@gmail.com', N'lklcdth@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (84, N'337462', N'CTY TNHH MTV TM DV BHTH KIM DUNG ', N'', 5, N'', N'quan.hoangvan@frieslandcampina.com;nga.vothihong@dutchlady.com.vn', N'nguyenthikimdungminimart@gmail.com', N'nguyenthikimdungminimart@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (85, N'337405', N'CONG TY TNHH TM-DV-DL THANH THANH T', N'Số 62 tổ 2, ấp An Lợi, xã An Bình A, Tp. Hồng Ngự, tỉnh Đồng Tháp', 5, N'thuykieu010316@gmail.com', N'khanh.huynhminh@dutchlady.com.vn', N'nppthanhtanhongngu@gmail.com', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (86, N'337328', N'DNTN MY  LOAN', N'273D Nguyễn Trung Trực, Phường 3, Tp. Mỹ Tho, Tiền Giang', 5, N'lecam2012@yahoo.com.vn;myduyen010191@gmail.com', N'hungchuongct@gmail.com;chuong.luuhung@dutchlady.com.vn;Quan.HoangVan@frieslandcampina.com', N'nppmyloantg@yahoo.com.vn', N'myduyen010191@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (87, N'337399', N'CTY TNHH MTV TAN THE PHAT', N'A21 Điện Biên Phủ, P. Vĩnh Quang, Tp. Rạch Giá, tỉnh Kiên Giang', 5, N'ennguyenkg@yahoo.com', N'huynguyenqt@yahoo.com.vn;huy.nguyenkhac@dutchlady.com.vn;cua.nguyenvan@dutchlady.com.vn', N'oanhdu80@yahoo.com.vn', N'thithi389@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (88, N'337321', N'CTY TNHH HIEP PHONG', N'327 Hoàng Diệu, Phường B, TX Châu Đốc, Tỉnh An Giang', 5, N'hothimaithi@gmail.com', N'thanhthamdl@yahoo.com.vn;tham.lethanh@dutchlady.com.vn;cua.nguyenvan@frieslandcampina.com', N'hiepphongco.ltd@gmail.com', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (89, N'337404', N'CTY TNHH MTV PHUOC HIEP', N'TL954, K. Long Hưng 1, Long Thạnh, TX. Tân Châu, Tỉnh An Giang', 5, N'duongkieuly@gmail.com', N'thanhthamdl@yahoo.com.vn;tham.lethanh@dutchlady.com.vn;cua.nguyenvan@frieslandcampina.com', N'Kiet.huynhminh@gmail.com', N'hongthutc2009@yahoo.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (90, N'337420', N'DOANH NGHIEP TU NHAN VI THI', N'Tổ 7, KP 5, Lý Thường Kiệt, TT Dương Đông , Tp. Phú Quốc', 5, N'hothiha13@yahoo.com', N'cua.nguyenvan@frieslandcampina.com', N'Phanthivythi@gmail.com', N'ngoc.ktpq@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (91, N'337406', N'CONG TY TNHH HONG HAI NGAN', N'Đường số 25, Khu Đô Thị Mới Hà Tiên, KP2, P. Bình San, TX Hà Tiên, Tỉnh Kiên Giang', 5, N'Mytu.HHN@gmail.com', N'cua.nguyenvan@frieslandcampina.com', N'npphonghaingan@gmail.com', N'tranthachvuht@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (92, N'337390', N'CTY TNHH TDK', N'Lô 26 Đường A1, KDC Hưng Phú 1, Q. Cái Răng, Tp. Cần Thơ', 5, N'trieudantdk.pkt@gmail.com', N'n.hoangvu0110@gmail.com;vu.nguyenhoang@dutchlady.com.vn;loc.khangoc@frieslandcampina.com', N'npptdk.director@gmail.com', N'sa.tdk.cantho@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (93, N'337325', N'DNTN NGOC THU', N'999 QL 91, Thới Thạnh, Thới thuận, Q. Thốt  Nốt, Tp. Cần Thơ', 5, N'phadfd@yahoo.com.vn', N'tri.diepminh@gmail.com;tri.diepminh@dutchlady.com.vn;cua.nguyenvan@frieslandcampina.com', N'dntnngocthu@gmail.com', N'dntntruongngocthu@yahoo.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (94, N'337424', N'CONG TY TNHH MTV NGOC PHUONG VINH L', N'191/5BPhạm Hùng, P. 9, Tp. Vĩnh Long', 5, N'p.thao88@yahoo.com.vn;tuaingan@gmail.com', N'thanh.nguyenthithanh@dutchlady.com.vn', N'ctyngocphuongvl@yahoo.com.vn', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (95, N'337327', N'DNTN DAI THANH PHAT', N'28 Nguyễn Trãi - Tp. Sóc Trăng, Tỉnh Sóc Trăng', 5, N'liennguyen306@gmail.com', N'thong.trantri@dutchlady.com.vn;loc.khangoc@frieslandcampina.com', N'hm.loan@hotmail.com', N'liennguyen306@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (96, N'337320', N'CTY TNHH MAI HUNG', N'61-63, Lê Minh Ngươn, Mỹ Long, Long Xuyên, Tp. An Giang', 5, N'maitung71@gmail.com', N'ngnhutquang839@gmail.com;quang.nguyennhut@dutchlady.com.vn;cua.nguyenvan@frieslandcampina.com', N'thuylynh1970@gmail.com', N'nppmaihunglongxuyen@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (97, N'337386', N'CTY TNHH PHAT DAT', N'Khu D9-tuyến D4 - KTM Bạch Đằng - Lý Văn Lâm - Tp. Cà Mau/ Ấp Chống Mỹ, Xã Hàm Rồng, huyện Năm Căn, Cà Mau', 5, N'nguyenthisen105@yahoo.com.vn;chauphutai@gmail.com', N'nguyenvanhuudlv@yahoo.com.vn;huu.nguyenvan@dutchlady.com.vn;loc.khangoc@frieslandcampina.com;dinh.lethi.fcv@gmail.com;dinh.lethi@dutchlady.com.vn', N'nguyenleminh1974@yahoo.com', N'tranphucdai1979@yahoo.com;vohueport@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (98, N'337322', N'DNTN TRAN PHONG', N'số 359 QL 1A, ấp Tân Tạo, TT. Châu Hưng, Vĩnh Lợi, Tỉnh Bạc Liêu', 5, N'httphuongbl@yahoo.com;Ngoc.HoangThi@frieslandcampina.com;', N'loc.khangoc@frieslandcampina.com;loi.nguyenphuoc@dutchlady.com.vn', N'chenfull_2006@yahoo.com', N'huahuekhanhbl@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (99, N'337407', N'CONG  TY  TNHH  MTV  NGUYEN PHUOC C', N'104 Ấp Bình Hữu 2, xã Đức Hòa Thượng, huyện Đức Hòa, Tỉnh Long An', 5, N'vanthanhthao873@gmail.com', N'thekhuong99@yahoo.com;khuong.nguthe@dutchlady.com.vn;quan.hoangvan@frieslandcampina.com', N'npchung09@gmail.com', N'truchue91@gmail.com', NULL)
GO
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (100, N'337437', N'CONG TY TNHH MOT THANH VIEN CONG LINH', N'289 Điện Biên Phủ, K.4, Phường 6, TP. Trà Vinh', 5, N'vothanhtrangtv@gmail.com', N'an.letuong@dutchlady.com.vn;quan.hoangvan@frieslandcampina.com', N'conglinhtv@yahoo.com.vn', N'tranvanbangtv@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (101, N'337411', N'DOANH NGHIEP TU NHAN NAM HONG', N'Khóm Minh Thuận A, TT Cầu Ngang, Cầu Ngang , Tỉnh Trà Vinh', 5, N'thuhuynh091990@gmail.com', N'letuonganbt@gmail.com;an.letuong@dutchlady.com.vn;Quan.HoangVan@frieslandcampina.com', N'lamvinh9584@yahoo.com.vn', N'lamvinh9584@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (102, N'337378', N'CTY TNHH TM VAN TRANG', N'Số 6 khu 200 Tô Hiệu- Trại Cau- Lê Chân- Hải Phòng', 7, N'nppvantrang2014@gmail.com;Ngoc.HoangThi@frieslandcampina.com;', N'nguyenvanthang.fcv@gmail.com;thang.nguyenvan@dutchlady.com.vn;thanh01.nguyenviet@frieslandcampina.com', N'dungvantrangminh@yahoo.com.vn', N'dungvantrangminh@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (103, N'337460', N'CONG TY TNHH THUONG MAI CANH TRUONG', N'TAI NHA ONG NGUYEN DANG CANH, THON 1, YEN HO, XA YEN HO, DUC THO, HA TINH', 7, N'phanmo88@gmail.com', N'nguyenvanhain3.fcv@gmail.com;hai.nguyenvan@dutchlady.com.vn;thang.dominh@frieslandcampina.com', N'nppcanhtruong@gmail.com', N'Phanthimo.fcv@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (104, N'337414', N'CONG TY TNHH TM-TH AN THUAN PHAT', N'Khu 6-TT Lam Sơn- Huyện Thọ Xuân- Tỉnh Khánh Hòa', 7, N'lichlamkinh@gmail.com', N'diep.doanvan.fcv@gmail.com;diep.doanvan@dutchlady.com.vn;dam.dinhvan@frieslandcampina.com', N'ngodoanluan1989@gmail.com', N'ngodoanluan1989@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (105, N'337415', N'CONG TY TNHH DONG TIEN', N'Trung Chính - Hải Hòa-Tĩnh Gia, Tp. Thanh Hóa', 7, N'dutchlady@dongtienvn.com', N'dam.dinhvan@frieslandcampina.com', N'hung.lhd@gmail.com', N'hung.lhd@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (106, N'337379', N'CTY TNHH TAN HOANG PHAT', N'Khu 3/2 Thị Trấn Vĩnh Bảo', 7, N'nguyenthuy6785@gmail.com', N'phamxuanninh.fcv@gmail.com;ninh.phamxuan@dutchlady.com.vn;hung.nguyenanh@frieslandcampina.com', N'nguyenthuy6785@gmail.com', N'nguyenthuy6785@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (107, N'337365', N'CTY TNHH SX&TM HUNG PHAT', N'134-136 Lê Quý Đôn - P. Tiền Phong - Tỉnh Thái Bình', 7, N'hungphatthaibinh272tb@gmail.com', N'gianghoangha.fcv@gmail.com;ha.gianghoang@dutchlady.com.vn;thang.ngomanh@frieslandcampina.com', N'thuonghungphat@yahoo.com.vn', N'thuonghungphat@yahoo.com.vn', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (108, N'337377', N'CONG TY TNHH TM & DV HUNG THINH', N'Khu Đồng Khê - TT Nam Sách - Hải Dương', 7, N'lethanhhungthinhhd@gmail.com', N'nguyenduchung.fcv@gmai.com;hung.nguyenduc@dutchlady.com.vn;thang.ngomanh@frieslandcampina.com', N'npphungthinh.hd@gmail.com', N'thuhuehaiduong@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (109, N'337432', N'CONG TY TNHH THAI NHAT HOA', N'199 Phan Bội Châu - Hồng Bàng - Hải PHòng', 7, N'hoanghanew@gmail.com', N'nguyenhuuthai.fcv@gmail.com;thai.nguyenhuu@dutchlady.com.vn;thanh01.nguyenviet@frieslandcampina.com', N'thainhathoa199@gmail.com', N'phuongnga.ngn@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (110, N'337356', N'CTY TNHH TMDV DUNG TIEN', N'Số 18 - Ngõ 26- P. Trần Phú - Tp Bắc Giang- Tỉnh Bắc Giang', 7, N'thukhoi2010@gmail.com;linhha151@gmail.com', N'nguyendangtung.fcv@gmail.com;tung.nguyendang@dutchlady.com.vn;thanh01.nguyenviet@frieslandcampina.com', N'dungtien.fcv@gmail.com', N'nhathuy050809@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (111, N'337357', N'DNTN TOAN THANG', N'Đường Bình Than- Khả Lễ- Võ Cường - Bắc Ninh', 7, N'dntoanthangbacninh@gmail.com', N'duongdaithang.fcv@gmail.com;thang.duongdai@dutchlady.com.vn;thanh01.nguyenviet@frieslandcampina.com', N'dntoanthangbacninh@gmail.com', N'dntoanthangbacninh@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (112, N'337362', N'DNTN - XI NGHIEP BIA THANG LONG', N'Phường Yên Thanh- Tp. Uông Bí- Tỉnh Quảng Ninh', 7, N'xuantuyen80@gmail.com', N'tranvietcuong.fcv@gmail.com;cuong.tranviet@dutchlady.com.vn;thanh01.nguyenviet@frieslandcampina.com', N'thuytapbtl@gmail.com', N'ngabiathanglong@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (113, N'337363', N'CTY TNHH TMDV SINH PHUONG', N'134 Lê Thánh Tông- Tp. Hạ Long - Tỉnh Quảng Ninh', 7, N'spp.sinhphuong@gmail.com', N'nguyenhoaigiang.fcv@gmail.com;giang.nguyenhoai@dutchlady.com.vn;thanh01.nguyenviet@frieslandcampina.com', N'cty.sinhphuong@gmail.com', N'spp.sinhphuong@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (114, N'337364', N'CTY TNHH NGHIA HUNG', N'Tổ 4- khu 8- P. Cẩm Thành- Tp. Cẩm Thành tỉnh Quảng Ninh', 7, N'nghiahungcp2015@gmail.com', N'thanh01.nguyenviet@frieslandcampina.com', N'duckinh_npp@yahoo.com', N'nghiahungcp2015@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (115, N'337376', N'CTY TNHH TOAN DUONG', N'Khu CN Phía tây Ngô Quyền- Phường Cẩm Thượng- Tp. Hải Dương', 7, N'congtytoanduong@gmail.com', N'nguyenthehung.fcv@gmail.com;hung.nguyenthe@dutchlady.com.vn;thang.ngomanh@frieslandcampina.com', N'congtytoanduong@gmail.com', N'congtytoanduong@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (116, N'337384', N'DOANH NGHIEP TU NHAN TUNG HOA', N'32 Minh Khai- P. Nguyễn Du- Tp. Nam Định', 7, N'yenhalan@gmail.com', N'maixuanhai.fcv@gmail.com;hai.maixuan@dutchlady.com.vn;thang.ngomanh@frieslandcampina.com', N'tunghoa.nd@gmail.com', N'tunghoa.nd@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (117, N'337360', N'CTY TNHH TM VAN HUNG', N'SỐ 66 - Cao Xuân Huy - Vinh Tân- Tp.Vinh Tỉnh Nghệ An', 7, N'anhtuyetdfd@gmail.com', N'vuquangthin.fcv@gmail.com;thin.vuquang@dutchlady.com.vn;thang.dominh@frieslandcampina.com', N'vanhung.66.cty@gmail.com', N'anhtuyetdfd@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (118, N'337361', N'CTY TNHH NGOC SON', N'Số 297 - Trần Hưng Đạo - P. Đông Thành, Tỉnh Ninh Bình', 7, N'linh.lethuy86@gmail.com', N'nguyenmanhhung.fcv@gmail.com;hung.nguyenmanh@dutchlady.com.vn;dam.dinhvan@frieslandcampina.com', N'ngocson.nb7779@gmail.com', N'ngocson.frieslandcampina@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (119, N'337409', N'CTY TNHH VAN TAI VA THUONG MAI NAM', N'229 - khu 4 - TT Yên Định - Hải Hậu - tỉnh Nam Định', 7, N'hoanglan.mc@gmail.com', N'maixuanhai.fcv@gmail.com;hai.maixuan@dutchlady.com.vn;thang.ngomanh@frieslandcampina.com', N'ctnamthai.nd@gmail.com', N'oanh8x@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (120, N'337380', N'DOANH NGHIEP TU NHAN PHUONG SANG', N'Số 108 Điện Biên Phủ - P. Phương Lâm- Tp. Hòa Bình, Tỉnh Hòa Bình', 7, N'oanhnam1201@gmail.com', N'ngongocson.fcv@gmail.com;son.nguyenngoc@dutchlady.com.vn', N'phuongsanghb2012@gmail.com', N'oanhnam1201@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (121, N'337425', N'CONG TY TNHH BAO TRANG PHU THO', N'2335 Đại lộ Hùng Vương - Nông Trang - Việt Trì - Phú Thọ', 7, N'hoabantrang15684@yahoo.com.vn', N'kieuquanghuy.fcv@gmail.com;kieu.quanghuy@dutchlady.com.vn', N'tranhuu2335@yahoo.com', N'huan.tn27@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (122, N'337359', N'CTY TNHH TM NAM LONG', N'19 Nguyễn Du - Tp. Vinh Tỉnh Nghệ An', 7, N'thanhngadfd@gmail.com', N'thin.vuquang@dutchlady.com.vn;thang.dominh@frieslandcampina.com', N'namlongna1@gmail.com', N'thanhngadfd@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (123, N'337345', N'CTY TNHH THUY CHAU', N'58 Hải Thượng Lãn Ông- Tp.  Hà Tĩnh', 7, N'hongthu281289@gmail.com', N'hai.nguyenvan@dutchlady.com.vn;thang.dominh@frieslandcampina.com', N'thanhthuyht1973@gmail.com', N'ntthuha09091985@gmail.com;ngangodaidongtien@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (124, N'337368', N'CTY TNHH TM HAI YEN', N'KP 5- P. Phú Sơn - Bỉm Sơn - Tp. Thanh Hóa', 7, N'thanhmaihuy@gmail.com', N'hoangvanphong.fcv@gmail.com;phong.hoangvan@dutchlady.com.vn;dam.dinhvan@frieslandcampina.com', N'haiyenbimson@gmail.com', N'thanhmaihuy@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (125, N'337358', N'DNTN THANH DAT', N'Đường Lê Hoàn - P. Hai Bà Trưng- Tp. Phủ Lý tỉnh Hà Nam', 7, N'buikimthanh81@gmail.com', N'doanlelinh.fcv@gmail.com;linh.doanle@dutchlady.com.vn;dam.dinhvan@frieslandcampina.com', N'nguyenthihang171@yahoo.com', N'tuyetbachmvn@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (126, N'337367', N'CTY TNHH TM QUANG CUONG', N'Lô B1-2 khu Công nghiệp Tây Bắc Ga - P. Đông Thọ - Tp. Thanh Hóa', 7, N'hienquangcuong1983@gmail.com;Ngoc.HoangThi@frieslandcampina.com;', N'hoatrungtuyen.fcv@gmail.com;tuyen.hoatrung@dutchlady.com.vn;dam.dinhvan@frieslandcampina.com', N'loanluong20@gmail.com', N'quangcuong1290@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (127, N'337369', N'DNTN HUONG TOAN', N'Số 47 tổ 13 - P. Tân Quang - Tp. Tuyên Quang - Tỉnh Tuyên Quang', 7, N'huongtoanhltq@yahoo.com.vn;', N'vuvanquy.fcv@gmail.com;vu.vanquy@dutchlady.com.vn;hung.dongmanh@frieslandcampina.com', N'dntnhuongtoantq@gmail.com', N'dntnhuongtoantq@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (128, N'337430', N'CONG TY TNHH MINH    LIEN PHU THO', N'SỐ 33 -Phú Bình - Phong Châu - Phú Thọ', 7, N'trananhthong241086@gmail.com', N'nguyenthetoan.fcv@gmail.com;toan.nguyenthe@dutchlady.com.vn', N'minhlienpt@gmail.com', N'mailananhpt@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (129, N'337429', N'CTY TNHH SAN XUAT VA  THUONG MAI TU', N'071- Tổ 3- P. Sông Hiến - Tp. cao bằng - Tỉnh Cao bằng ', 7, N'dungtucb@gmail.com', N'', N'dungtucb@gmail.com', N'oanhhalan1@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (130, N'337435', N'CONG TY TNHH DICH VU THUONG MAI HOA', N'Khối Tân Sơn, Phường Hòa Hiếu, TX Thái Hòa, Nghệ An', 7, N'trang.npphoahung@gmail.com', N'chuvanthai.fcv@gmail.com;thai.chuvan@dutchlady.com.vn;thang.dominh@frieslandcampina.com', N'hunghoa.thaihoa@gmail.com', N'hunghoa.thaihoa@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (131, N'337381', N'NPP NGO THANH CHI', N'Số  09, Tổ dân phố 12, P. Mường Thanh, TP Điện Biên Phủ.', 7, N'qtdldienbien@gmail.com', N'', N'qtdldienbien@gmail.com', N'qtdldienbien@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (132, N'337370', N'CTY TNHH TM VA DV HAI PHUONG', N'Thông Nước Mát - xã Âu Lâu- Tp. Yên Bái', 7, N'Chinhdt589@gmail.com', N'doanquocthanh.fcv@gmail.com;thanh.doanquoc@dutchlady.com.vn', N'chinhdt589@gmail.com', N'thaohp77@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (133, N'337383', N'DOANH NGHIEP TU NHAN MAI LINH', N'Đường A2 - KCN Bắc Duyên Hải - Tp. Lào Cai - Tỉnh Lào Cai', 7, N'leloan.lc@gmail.com', N'dodinhxuan.fcv@gmail.com;xuan.dodinh@dutchlady.com.vn', N'haimai68@gmail.com', N'trananhlc@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (134, N'337468', N'CTY TNHH MTV MAI PHUONG HA GIANG', N'', 7, N'nppphamphuong.hg@gmail.com', N'', N'hagiangkc@gmail.com', N'yenhg88@gmail.com;doquynh.hg@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (135, N'337469', N'DNTN TIEN THANH T&T', N'', 7, N'quynhtrang.dutchlady@gmail.com', N'', N'quynhtrang.dutchlady@gmail.com', N'lankt8586@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (136, N'337472', N'CTY TNHH LAP TUAN AN', N'21 PHO VUON TRAU, PHUONG TRAN PHU, TP MONG CAI, QUANG NINH', 7, N'laptuanan@gmail.com', N'', N'laptuanan@gmail.com', N'laptuanan@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (137, N'337483', N'CONG TY TNHH MTV HOA VIET LS', N'', 7, N'anhtuyet1104@gmail.com', N'', N'hoaviet.ls08@gmail.com', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (138, N'337487', N'CONG TY TNHH MTV LAM DIEU', N'', 7, N'nppdieu.lamsonla@gmail.com', N'', N'nppdieu.lamsonla@gmail.com', N'', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (139, N'337442', N'CONG TY TNHH AN PHU MAI', N'Thôn Cổ Dũng 2 Đông La - Đông Hưng - Thái Bình', 7, N'phanyenkt9026@gmail.com', N'gianghoangha.fcv@gmail.com;ha.gianghoang@dutchlady.com.vn;thanh.nguyenviet@frieslandcampina.com', N'tieuvetb@gmail.com', N'hongnhung@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (140, N'337441', N'CONG TY TNHH TM-DV TIEN MINH', N'Phố Đồi Ngô - Thị trấn Đồi Ngô - Huyện Lục Nam - Bắc Giang', 7, N'linhha151@gmail.com', N'nguyendangtung.fcv@gmail.com;tung.nguyendang@dutchlady.com.vn;cuong.nguyenthe@frieslandcampina.com', N'tienminhfcv@gmail.com', N'nhathuy050809@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (141, N'337443', N'CONG TY TNHH THUONG MAI THUAN MINH', N'Cao xá 1 Phường Lam Sơn TP Hưng Yên', 7, N'thuthuy282.hy@gmail.com', N'thanh.nguyenviet@frieslandcampina.com', N'minhthuanhy81@gmail.com', N'thuthuy282.hy@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (142, N'337451', N'CTY TNHH PHAN PHOI MANH THANG', N'Tổ 14 - Đường Phú Thái - Phường Tân Thịnh - TP Thái Nguyên - Thái Nguyên', 7, N'Ngothutrangtn@gmail.com', N'nguyenvanhung.fcv@gmail.com', N'Lequanghien6886@gmail.com', N'lequanghien6886@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (143, N'337458', N'CONG TY TNHH TRUNG DONG NGHE AN', N'Xóm 3  Xã Đặng Sơn - Huyện Đô Lương - Tỉnh Nghệ An', 7, N'adtrungdong@gmail.com', N'nguyentientrien.fcv@gmail.com;trien.nguyentien@dutchlady.com.vn;thang.dominh@frieslandcampina.com', N'npptrungdong147@gmail.com', N'tktrungdong@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (144, N'337453', N'CTY TNHH TM NAM THINH PHAT HUNG YEN', N'Số 9, đường Nguyễn Khuyến, Phường An Tảo, TP Hưng Yên, Tỉnh Hưng Yên', 7, N'phuonglanhy@gmail.com', N'tranvandoan.fcv@gmail.com;doan.tranvan@dutchlady.com.vn;thang.ngomanh@frieslandcampina.com', N'Namthinhphathungyen@gmail.com', N'Phuonglinhhy@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (145, N'337455', N'CONG TY TNHH TM DONG DUNG', N'Số 024 tổ 10 Phường Đoàn Kết, TX Lai Châu, tỉnh Lai Châu', 7, N'doanhnghiepnamhai@gmail.com', N'dodinhxuan.fcv@gmail.com;xuan.dodinh@dutchlady.com.vn', N'doanhnghiepnamhai@gmail.com', N'doanhnghiepnamhai@gmail.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (146, N'337652', N'CTY CO PHAN PHAN PHOI THUC PHAM PT-MT', N'', 6, N'thu.tavan@phuthaigroup.com', N'huyen.dangthi@frieslandcampina.com', N'ngoc.nguyenbich@phuthaigroup.com;nppptfood@phuthaigroup.com', N'lan.phanngoc@phuthaigroup.com;nhung.vuthihong@phuthaigroup.com', NULL)
INSERT [dbo].[tbl_Distributors] ([ID], [Code], [Name], [Address], [Region], [AdminMail], [SEmail], [AcMail], [ManagerMail], [State]) VALUES (147, N'337449', N'CN DN TU NHAN THUONG MAI MINH CHIEU', N'Thôn thạch trụ Tây, Xã Đức Lân, Huyện Mộ Đức, Tỉnh Quảng Ngãi', 1, N'hadang200382@gmail.com', NULL, N'anhvu1089@gmail.com', N'ltmchieu1011@yahoo.com', 1)
SET IDENTITY_INSERT [dbo].[tbl_Distributors] OFF
SET IDENTITY_INSERT [dbo].[tbl_Signatures] ON 

INSERT [dbo].[tbl_Signatures] ([ID], [UserID], [Name], [Contents], [UpdateTime], [State]) VALUES (1, N'fcvad', N'Quang', N'<p>Quang Vo,</p>
', N'Jul 21 2018  2:53PM', 1)
INSERT [dbo].[tbl_Signatures] ([ID], [UserID], [Name], [Contents], [UpdateTime], [State]) VALUES (2, N'fcvad', N' Logo', N'<p><img alt="" src="https://ucarecdn.com/235b3571-f0b7-437b-aaeb-b5e131c43493/-/preview/" /><br />
&nbsp;</p>
', N'Jul 21 2018  2:55PM', 1)
INSERT [dbo].[tbl_Signatures] ([ID], [UserID], [Name], [Contents], [UpdateTime], [State]) VALUES (4, NULL, N'a', N'<p>a</p>
', N'9/25/2018', 0)
INSERT [dbo].[tbl_Signatures] ([ID], [UserID], [Name], [Contents], [UpdateTime], [State]) VALUES (5, NULL, N'ffdff', N'<p>dfdfdfdf</p>
', N'9/25/2018', 0)
INSERT [dbo].[tbl_Signatures] ([ID], [UserID], [Name], [Contents], [UpdateTime], [State]) VALUES (6, NULL, N'bf', N'<p>vvvvdvdv</p>
', N'9/25/2018', 0)
SET IDENTITY_INSERT [dbo].[tbl_Signatures] OFF
SET IDENTITY_INSERT [dbo].[tbl_Templates] ON 

INSERT [dbo].[tbl_Templates] ([ID], [Name], [Contents], [CreatedBy], [CreationTime], [LastUpdatedBy], [UpdateTime], [Active]) VALUES (5, N'Công nợ', N'<p>K&iacute;nh gửi nh&agrave; ph&acirc;n phối&nbsp;#DistributorName,</p>

<p>C&ocirc;ng nợ th&aacute;ng n&agrave;y đ&atilde; được đ&iacute;nh k&egrave;m trong file</p>

<p>Xin cảm ơn!</p>
', N'fcvad', N'Jul 13 2018 10:41AM', NULL, N'9/30/2018', 1)
INSERT [dbo].[tbl_Templates] ([ID], [Name], [Contents], [CreatedBy], [CreationTime], [LastUpdatedBy], [UpdateTime], [Active]) VALUES (6, N'Chương trình khuyến mãi', N'<p>Gửi nh&agrave; ph&acirc;n phối #DistributorName,</p>

<p>Chương tr&igrave;nh khuyến m&atilde;i th&aacute;ng n&agrave;y đ&atilde; được ra mắt, chi tiết được đ&iacute;nh k&egrave;m trong file, xin cảm ơn!</p>
', N'fcvad', N'Jul 13 2018 10:42AM', NULL, N'9/30/2018', 0)
INSERT [dbo].[tbl_Templates] ([ID], [Name], [Contents], [CreatedBy], [CreationTime], [LastUpdatedBy], [UpdateTime], [Active]) VALUES (11, N'1', N'<p>1</p>
', NULL, NULL, NULL, N'9/30/2018', 0)
INSERT [dbo].[tbl_Templates] ([ID], [Name], [Contents], [CreatedBy], [CreationTime], [LastUpdatedBy], [UpdateTime], [Active]) VALUES (12, N'2', N'<p>2</p>
', NULL, N'9/28/2018', NULL, N'9/30/2018', 0)
SET IDENTITY_INSERT [dbo].[tbl_Templates] OFF
INSERT [dbo].[USER_ROLE] ([User_Id], [Role_Id]) VALUES (1, 1)
INSERT [dbo].[USER_ROLE] ([User_Id], [Role_Id]) VALUES (2, 1)
INSERT [dbo].[USER_ROLE] ([User_Id], [Role_Id]) VALUES (4, 2)
INSERT [dbo].[USER_ROLE] ([User_Id], [Role_Id]) VALUES (4, 5)
SET IDENTITY_INSERT [dbo].[USERS] ON 

INSERT [dbo].[USERS] ([User_Id], [Username], [Password], [LastModified], [Inactive], [Firstname], [Lastname], [Title], [Initial], [EMail]) VALUES (1, N'CrmInstall', N'21232f297a57a5a743894a0e4a801fc3', CAST(0x0000A96601416E02 AS DateTime), 0, N'Cu', N'Dung', NULL, NULL, N'dungcv1210@gmail.com')
INSERT [dbo].[USERS] ([User_Id], [Username], [Password], [LastModified], [Inactive], [Firstname], [Lastname], [Title], [Initial], [EMail]) VALUES (2, N'longtd', N'C4CA4238A0B923820DCC509A6F75849B', CAST(0x0000A961016B7031 AS DateTime), 1, N'21', N'21', NULL, NULL, NULL)
INSERT [dbo].[USERS] ([User_Id], [Username], [Password], [LastModified], [Inactive], [Firstname], [Lastname], [Title], [Initial], [EMail]) VALUES (3, N'1', N'C4CA4238A0B923820DCC509A6F75849B', CAST(0x0000A961016C6E9B AS DateTime), 1, N'1', N'1', NULL, NULL, NULL)
INSERT [dbo].[USERS] ([User_Id], [Username], [Password], [LastModified], [Inactive], [Firstname], [Lastname], [Title], [Initial], [EMail]) VALUES (4, N'abc', N'C4CA4238A0B923820DCC509A6F75849B', CAST(0x0000A965010C1170 AS DateTime), 0, N'1', N'1', NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[USERS] OFF
ALTER TABLE [dbo].[ROLES] ADD  CONSTRAINT [DF_ROLES_IsSysAdmin]  DEFAULT ((0)) FOR [IsSysAdmin]
GO
ALTER TABLE [dbo].[tbl_QueueEmails] ADD  CONSTRAINT [DF_tbl_QueueEmails_QueueTime]  DEFAULT (getdate()) FOR [QueueTime]
GO
ALTER TABLE [dbo].[USERS] ADD  CONSTRAINT [DF_USERS_Inactive]  DEFAULT ((0)) FOR [Inactive]
GO
ALTER TABLE [dbo].[FILES]  WITH CHECK ADD  CONSTRAINT [FK_FILES_USERS] FOREIGN KEY([userId])
REFERENCES [dbo].[USERS] ([User_Id])
GO
ALTER TABLE [dbo].[FILES] CHECK CONSTRAINT [FK_FILES_USERS]
GO
ALTER TABLE [dbo].[LOG_ACCESS]  WITH CHECK ADD  CONSTRAINT [FK_LOG_ACCESS_USERS] FOREIGN KEY([User_Id])
REFERENCES [dbo].[USERS] ([User_Id])
GO
ALTER TABLE [dbo].[LOG_ACCESS] CHECK CONSTRAINT [FK_LOG_ACCESS_USERS]
GO
ALTER TABLE [dbo].[ROLE_PERMISSION]  WITH NOCHECK ADD  CONSTRAINT [FK_LNK_ROLE_PERMISSION_PERMISSIONS] FOREIGN KEY([Permission_Id])
REFERENCES [dbo].[PERMISSIONS] ([Permission_Id])
GO
ALTER TABLE [dbo].[ROLE_PERMISSION] CHECK CONSTRAINT [FK_LNK_ROLE_PERMISSION_PERMISSIONS]
GO
ALTER TABLE [dbo].[ROLE_PERMISSION]  WITH NOCHECK ADD  CONSTRAINT [FK_LNK_ROLE_PERMISSION_ROLES] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[ROLES] ([Role_Id])
GO
ALTER TABLE [dbo].[ROLE_PERMISSION] CHECK CONSTRAINT [FK_LNK_ROLE_PERMISSION_ROLES]
GO
ALTER TABLE [dbo].[USER_ROLE]  WITH NOCHECK ADD  CONSTRAINT [FK_LNK_USER_ROLE_ROLES] FOREIGN KEY([Role_Id])
REFERENCES [dbo].[ROLES] ([Role_Id])
GO
ALTER TABLE [dbo].[USER_ROLE] CHECK CONSTRAINT [FK_LNK_USER_ROLE_ROLES]
GO
ALTER TABLE [dbo].[USER_ROLE]  WITH NOCHECK ADD  CONSTRAINT [FK_LNK_USER_ROLE_USERS] FOREIGN KEY([User_Id])
REFERENCES [dbo].[USERS] ([User_Id])
GO
ALTER TABLE [dbo].[USER_ROLE] CHECK CONSTRAINT [FK_LNK_USER_ROLE_USERS]
GO
