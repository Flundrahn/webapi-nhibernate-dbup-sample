CREATE PROCEDURE dbo.GetAllDocuments
AS
BEGIN
    SELECT Id, Name, CreateDate
    FROM dbo.Document
END
