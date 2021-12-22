CREATE TRIGGER [SampleTrigger]
  ON [core].[TimeStampType]
  FOR DELETE, INSERT, UPDATE
  AS
  BEGIN
    SET NOCOUNT ON
  END
