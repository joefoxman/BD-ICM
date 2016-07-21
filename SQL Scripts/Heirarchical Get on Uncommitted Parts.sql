USE [BdIcm]
GO

WITH SubParts(ParentPartId, PartId, Name, [Description], [ModifiedBy], [Level])
AS
(
	SELECT ParentPartId, Id, [Name], [Description], [ModifiedBy], 0 AS [Level]
	FROM dbo.Part
	WHERE (ParentPartId IS NULL) AND (InstrumentId = 4) AND (InstrumentCommitId IS NULL)
	UNION ALL
	SELECT p.ParentPartId, p.Id, p.[Name], p.[Description], p.[ModifiedBy], [Level]+1
	FROM Part AS p
	INNER JOIN SubParts AS sp ON p.ParentPartId = sp.PartId
)
SELECT ParentPartId, PartId, [Name], [Description], [Level], [ModifiedBy]
FROM SubParts AS s
ORDER BY ParentPartId
