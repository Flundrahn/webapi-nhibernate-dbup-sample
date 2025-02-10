IF OBJECT_ID('dbo.GetAllDocuments', 'P') IS NOT NULL
    DROP PROCEDURE dbo.GetAllDocuments;
GO

CREATE PROCEDURE dbo.GetAllDocuments
AS
BEGIN
    SELECT Id, Name, CreateDate
    FROM dbo.Document
END
GO
