CREATE FUNCTION [core].[GetDomainValue]
(
  @id BIGINT
)
RETURNS TABLE AS RETURN
(
SELECT [Id]
      ,[ModifiedDate]
      ,[ModifiedUser]
      ,[TypeId]
      ,[ValueC]
      ,[ValueN]
      ,[ValueD]
      ,[ValueF]
      ,[DivId]
      ,[Description]
      ,[Unit]
  FROM [core].[DomainValue]
 WHERE @id IS NULL OR [Id] = @id
)
