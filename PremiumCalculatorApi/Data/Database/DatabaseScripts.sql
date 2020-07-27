USE [PremiumCalculator]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PremiumRule](
	[PremiumRuleId] [int] NOT NULL IDENTITY,
	[State] [varchar](2) NULL,
	[MonthOfBirth] [varchar](10) NULL,
	[Age] [varchar](10) NULL,
	[Premium] [decimal](18, 2) NULL
) ON [PRIMARY]
GO


USE [PremiumCalculator]
GO

INSERT INTO [dbo].[PremiumRule] ([State],[MonthOfBirth],[Age],[Premium]) VALUES ('NY','August','18-45',150.00)
INSERT INTO [dbo].[PremiumRule] ([State],[MonthOfBirth],[Age],[Premium]) VALUES ('NY','January','46-65',200.00)
INSERT INTO [dbo].[PremiumRule] ([State],[MonthOfBirth],[Age],[Premium]) VALUES ('NY','*','18-65',120.99)
INSERT INTO [dbo].[PremiumRule] ([State],[MonthOfBirth],[Age],[Premium]) VALUES ('AL','November','18-65',85.50)
INSERT INTO [dbo].[PremiumRule] ([State],[MonthOfBirth],[Age],[Premium]) VALUES ('AL','*','18-65',100.00)
INSERT INTO [dbo].[PremiumRule] ([State],[MonthOfBirth],[Age],[Premium]) VALUES ('AK','December','65+',175.20)
INSERT INTO [dbo].[PremiumRule] ([State],[MonthOfBirth],[Age],[Premium]) VALUES ('AK','December','18-64',125.16)
INSERT INTO [dbo].[PremiumRule] ([State],[MonthOfBirth],[Age],[Premium]) VALUES ('AK','*','18-65',100.80)
INSERT INTO [dbo].[PremiumRule] ([State],[MonthOfBirth],[Age],[Premium]) VALUES ('*','*','18-65',90.00)

GO
