USE [BdIcm]
GO

WITH SubParts(ParentPartId, PartId, Name, [Description], [Level])
AS
(
	SELECT ParentPartId, Id, [Name], [Description], 0 AS [Level]
	FROM dbo.Part
	WHERE ParentPartId IS NULL AND Id = 6
	UNION ALL
	SELECT p.ParentPartId, p.Id, p.[Name], p.[Description], [Level]+1
	FROM Part AS p
	INNER JOIN SubParts AS sp ON p.ParentPartId = sp.PartId
)
SELECT ParentPartId, PartId, [Name], [Description], [Level]
FROM SubParts AS s
ORDER BY ParentPartId
