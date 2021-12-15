CREATE FUNCTION [core].[CheckDomainType]
(
  @dovId bigint,
	@dovTypId bigint
)
RETURNS bit
AS 
BEGIN
   DECLARE @retBit bit

   IF @dovId IS NULL BEGIN
     RETURN CAST(1 AS bit)
   END

   SELECT @retBit = CAST(Count(0) AS bit) FROM [core].[DomainValue] WHERE [Id] = @dovId AND [TypeId] = @dovTypId
   RETURN @retBit
END;
GO
