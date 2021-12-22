using EntityGenerator.DatabaseObjects.DataAccessObjects;
using EntityGenerator.DatabaseObjects.DataTransferObjects;
using EntityGenerator.Profile;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Tests.Integrationstests
{
  [TestFixture]
  [Explicit]
  public class MicrosoftSqlServerReadTests : TestBase
  {
    [Test]
    [Category("IntegrationTest")]
    public void DatabaseObjectCount()
    {
      // arrange
      MicrosoftSqlServerDao microsoftSqlServerDao = GetMicrosoftSqlServerDao();

      // act
      int dbObjectCount = microsoftSqlServerDao.DatabaseObjectCount();

      // assert
      Assert.Greater(dbObjectCount, 0);
    }

    [Test]
    [Category("IntegrationTest")]
    public void DatabaseSchemas()
    {
      // arrange
      MicrosoftSqlServerDao microsoftSqlServerDao = GetMicrosoftSqlServerDao();

      // act
      List<SchemaDto> schemas = microsoftSqlServerDao.DatabaseSchemas();

      // assert
      Assert.NotNull(schemas.FirstOrDefault(s => s.SchemaName.Equals("dbo", StringComparison.InvariantCultureIgnoreCase)));
      Assert.NotNull(schemas.FirstOrDefault(s => s.SchemaName.Equals("core", StringComparison.InvariantCultureIgnoreCase)));
    }

    [Test]
    [Category("IntegrationTest")]
    public void DatabaseTableValueObjects()
    {
      // arrange
      MicrosoftSqlServerDao microsoftSqlServerDao = GetMicrosoftSqlServerDao();

      // act
      List<TableValueObjectDto> tableValueObjects = microsoftSqlServerDao.DatabaseTableValueObjects();

      // assert
      Assert.Greater(tableValueObjects.Count, 0);
    }

    [Test]
    [Category("IntegrationTest")]
    public void DatabaseFunctions()
    {
      // arrange
      MicrosoftSqlServerDao microsoftSqlServerDao = GetMicrosoftSqlServerDao();

      // act
      List<FunctionDto> functions = microsoftSqlServerDao.DatabaseFunctions();

      // assert
      Assert.Greater(functions.Count, 0);
    }

    [Test]
    [Category("IntegrationTest")]
    public void DatabaseFunctionReturnColumns()
    {
      // arrange
      MicrosoftSqlServerDao microsoftSqlServerDao = GetMicrosoftSqlServerDao();
      List<FunctionDto> functions = new List<FunctionDto>
      {
        new() { SchemaName = "core", FunctionName = "GetDomainValue" }
      };

      // act
      microsoftSqlServerDao.DatabaseFunctionReturnColumns(functions);

      // assert
      FunctionDto? function = functions.FirstOrDefault();
      Assert.NotNull(function);
      Assert.Greater(function?.ReturnColumns?.Count, 0);
    }

    [Test]
    [Category("IntegrationTest")]
    public void DatabaseColumns()
    {
      // arrange
      MicrosoftSqlServerDao microsoftSqlServerDao = GetMicrosoftSqlServerDao();

      // act
      List<ColumnDto> columns = microsoftSqlServerDao.DatabaseColumns();

      // assert
      Assert.Greater(columns.Count, 0);
    }

    [Test]
    [Category("IntegrationTest")]
    public void DatabaseForeignKeys()
    {
      // arrange
      MicrosoftSqlServerDao microsoftSqlServerDao = GetMicrosoftSqlServerDao();

      // act
      List<ForeignKeyDto> foreignKeys = microsoftSqlServerDao.DatabaseForeignKeys();

      // assert
      Assert.Greater(foreignKeys.Count, 0);
    }

    [Test]
    [Category("IntegrationTest")]
    public void DatabaseCheckConstraints()
    {
      // arrange
      MicrosoftSqlServerDao microsoftSqlServerDao = GetMicrosoftSqlServerDao();

      // act
      List<CheckConstraintDto> checkConstraints = microsoftSqlServerDao.DatabaseCheckConstraints();

      // assert
      Assert.Greater(checkConstraints.Count, 0);
    }

    [Test]
    [Category("IntegrationTest")]
    public void DatabaseIndices()
    {
      // arrange
      MicrosoftSqlServerDao microsoftSqlServerDao = GetMicrosoftSqlServerDao();

      // act
      List<IndexDto> indices = microsoftSqlServerDao.DatabaseIndices();

      // assert
      Assert.Greater(indices.Count, 0);
    }

    [Test]
    [Category("IntegrationTest")]
    public void DatabaseTriggers()
    {
      // arrange
      MicrosoftSqlServerDao microsoftSqlServerDao = GetMicrosoftSqlServerDao();

      // act
      List<TriggerDto> triggers = microsoftSqlServerDao.DatabaseTriggers();

      // assert
      Assert.Greater(triggers.Count, 0);
    }


    private MicrosoftSqlServerDao GetMicrosoftSqlServerDao(string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False")
    {
      string xmlProfile = $@"<?xml version = ""1.0"" encoding=""utf-16""?>
   <ProfileDto xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
   <Global>
     <ProjectName>TestProject</ProjectName>
     <GeneratedPrefix />
     <GeneratedSuffix>.Generated</GeneratedSuffix>
     <GeneratedFolder>_Generated</GeneratedFolder>
     <LanguageBackend>CSharp</LanguageBackend>
     <LanguageFrontend>TypeScript</LanguageFrontend>
     <NoWipe>false</NoWipe>
   </Global>
   <Database>
     <ConnectionString>{connectionString}</ConnectionString>
     <DatabaseName>EntityGeneratorTestDatabase</DatabaseName>
     <SourceDatabaseType>MicrosoftSqlServer</SourceDatabaseType>
     <TargetDatabaseTypes>
        <DatabaseTypes>MicrosoftSqlServer</DatabaseTypes>
        <DatabaseTypes>Oracle</DatabaseTypes>
     </TargetDatabaseTypes>
   </Database>
   <Path>
      <RootDir>C:\RootDir</RootDir>
      <SqlDir>C:\SqlDir</SqlDir>
      <DataAccessDir>C:\DataAccessDir</DataAccessDir>
      <DataAccessFacadeDir>C:\DataAccessFacadeDir</DataAccessFacadeDir>
      <CommonDir>C:\CommonDir</CommonDir>
      <AbstractionsDir>C:\AbstractionsDir</AbstractionsDir>
      <BusinessLogicDir>C:\BusinessLogicDir</BusinessLogicDir>
      <FrontendDir>C:\FrontendDir</FrontendDir>
   </Path>
   <Generator>
      <GeneratorFrontend>
         <Constants>true</Constants>
         <ViewModels>false</ViewModels>
      </GeneratorFrontend>
      <GeneratorBusinessLogic>
         <Logic>false</Logic>
      </GeneratorBusinessLogic>
      <GeneratorCommon>
         <ConstantsBackend>false</ConstantsBackend>
         <DataTransferObjects>false</DataTransferObjects>
      </GeneratorCommon>
      <GeneratorDatabase>
         <HistoryTables>false</HistoryTables>
         <HistoryTriggers>false</HistoryTriggers>
         <MergeScripts>false</MergeScripts>
         <CheckConstraintScripts>false</CheckConstraintScripts>
      </GeneratorDatabase>
   </Generator>
</ProfileDto>
";

      ProfileProvider profileProvider = ServiceProvider.GetRequiredService<ProfileProvider>();
      profileProvider.LoadProfile(xmlProfile);
      return ServiceProvider.GetRequiredService<MicrosoftSqlServerDao>();
    }

  }
}
