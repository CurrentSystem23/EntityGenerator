CREATE FUNCTION [core].[TableValuedFunction]
(
  @paramInt int,
  @paramChar5 char(5)
)
RETURNS @returntable TABLE
(
  c1 int,
  c2 char(5)
)
AS
BEGIN
  INSERT @returntable
  SELECT @paramInt, @paramChar5
  RETURN
END
