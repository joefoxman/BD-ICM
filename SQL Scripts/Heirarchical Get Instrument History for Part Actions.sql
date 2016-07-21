DECLARE @InstrumentId int
DECLARE @StartDate datetime
DECLARE @EndDate datetime
SET @StartDate = '3/29/2016'
SET @EndDate = null
SET @InstrumentId = 5;

WITH SubParts(ParentPartId, PartId, [Name], [Description], [DocumentNumber], [Action], [ActionDate], [ModifiedDate], [Modifier], [ModificationType], [RowVersion])
AS
(
	SELECT 
		P1.ParentPartId,
		PA1.PartId,
		P1.[Name],
		P1.[Description],
		P1.[DocumentNumber],
		[Action],
		ActionDate,
		PA1.[ModifiedDate], 
		Modifier.[UserName] As Modifier,
		PA1.ModificationType, 
		PA1.[RowVersion]
	FROM PartAction PA1
	JOIN Part P1 ON PA1.PartId = P1.Id
	JOIN [User] AS Modifier ON Modifier.Id = PA1.ModifiedBy
	WHERE (ParentPartId IS NULL) AND (InstrumentId = @InstrumentId)
	UNION ALL
	SELECT 
		P2.ParentPartId,
		PA2.PartId,
		P2.[Name],
		P2.[Description],
		P2.[DocumentNumber],
		PA2.[Action],
		PA2.ActionDate,
		PA2.[ModifiedDate], 
		Modifier2.[UserName] As Modifier,
		PA2.ModificationType, 
		PA2.[RowVersion]
	FROM PartAction AS PA2
	JOIN Part P2 ON PA2.PartId = P2.Id
	JOIN [User] AS Modifier2 ON Modifier2.Id = PA2.ModifiedBy
	INNER JOIN SubParts AS SP ON P2.ParentPartId = SP.PartId
)
SELECT DISTINCT *
FROM SubParts AS s
WHERE ((@StartDate IS NULL) OR (s.ModifiedDate >= @StartDate)) AND ((@EndDate IS NULL) OR (s.ModifiedDate <= @EndDate))

