CREATE TRIGGER [core].[SampleTrigger]
  ON [core].[TimeStampType]
  FOR DELETE, INSERT, UPDATE
  AS
  BEGIN
    SET NOCOUNT ON
  END
