DECLARE @InstrumentId int
DECLARE @StartDate datetime
DECLARE @EndDate datetime
SET @StartDate = '3/29/2016'
SET @EndDate = null
SET @InstrumentId = 4;

WITH SubParts(ParentPartId, PartId, [Name], [Description], [DocumentNumber], [Action], [ActionDate], [ModifiedDate], [Modifier], [ModificationType], [RowVersion])
AS
(
	SELECT 
		ParentPartId, 
		Part.Id, 
		[Name], 
		[Description], 
		[DocumentNumber], 
		NULL, NULL,
		Part.[ModifiedDate], 
		Modifier.[UserName] As Modifier,
		Part.ModificationType, 
		Part.[RowVersion]
	FROM dbo.Part
	JOIN [User] AS Modifier ON Modifier.Id = Part.ModifiedBy
	WHERE (ParentPartId IS NULL) AND (InstrumentId = @InstrumentId)
	UNION ALL
	SELECT 
		p.ParentPartId, 
		p.Id, 
		p.[Name], 
		p.[Description], 
		p.[DocumentNumber], 
		NULL, NULL,
		p.[ModifiedDate], 
		Modifier2.UserName AS Modifier,
		p.ModificationType, 
		p.[RowVersion]
	FROM Part AS p
	JOIN [User] AS Modifier2 ON Modifier2.Id = p.ModifiedBy
	INNER JOIN SubParts AS sp ON p.ParentPartId = sp.PartId
)
SELECT DISTINCT *
FROM SubParts AS s
WHERE ((@StartDate IS NULL) OR (s.ModifiedDate >= @StartDate)) AND ((@EndDate IS NULL) OR (s.ModifiedDate <= @EndDate))

