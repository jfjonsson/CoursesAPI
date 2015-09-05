CREATE TABLE [dbo].[Courses] (
    [ID]         INT          IDENTITY (1, 1) NOT NULL,
    [TemplateID] VARCHAR (20) NOT NULL,
    [Semester]   VARCHAR (10) NOT NULL,
    [StartDate]  DATETIME     NOT NULL,
    [EndDate]    DATETIME     NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Students] (
    [ID]   INT          IDENTITY (1, 1) NOT NULL,
    [Name] VARCHAR (60) NOT NULL,
    [SSN]  VARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[CourseTemplates] (
    [ID]         INT          IDENTITY (1, 1) NOT NULL,
    [TemplateID] VARCHAR (20) NOT NULL,
    [Name]       VARCHAR (60) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[CourseEnrolments] (
    [ID]        INT IDENTITY (1, 1) NOT NULL,
    [CourseID]  INT NOT NULL,
    [StudentID] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

