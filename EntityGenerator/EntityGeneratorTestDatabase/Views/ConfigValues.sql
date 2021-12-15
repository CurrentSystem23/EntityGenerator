CREATE VIEW [core].[ConfigValues]
  AS 
SELECT dov.[Id]
      ,dov.[ModifiedDate]
      ,dov.[ModifiedUser]
      ,dov.[TypeId]
      ,dov.[ValueC]
      ,dov.[ValueN]
      ,dov.[ValueD]
      ,dov.[ValueF]
      ,dov.[DivId]
      ,dov.[Description]
      ,dov.[Unit]
      ,dot.[Description] AS TypeDescription
      ,dot.[Mode]
      ,dot.[StandardId]
      ,dot.[Editable]
  FROM [core].[DomainValue] AS dov
 INNER JOIN [core].[DomainType] AS dot ON dot.[Type] = dov.[TypeId]
