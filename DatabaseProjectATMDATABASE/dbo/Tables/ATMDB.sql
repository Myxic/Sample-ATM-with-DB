CREATE TABLE [dbo].[ATMDB] (
    [id]           INT          IDENTITY (1, 1) NOT NULL,
    [First_name]   VARCHAR (50) NOT NULL,
    [Last_name]    VARCHAR (50) NOT NULL,
    [UserName]     VARCHAR (50) NOT NULL,
    [Gender]       VARCHAR (50) NOT NULL,
    [Card_No]      VARCHAR (50) NOT NULL,
    [Balance]      INT          NOT NULL,
    [Pin_No]       INT          NOT NULL,
    [Phone_Number] VARCHAR (50) NOT NULL,
    CONSTRAINT [PK_ATMDB] PRIMARY KEY CLUSTERED ([id] ASC)
);


GO

