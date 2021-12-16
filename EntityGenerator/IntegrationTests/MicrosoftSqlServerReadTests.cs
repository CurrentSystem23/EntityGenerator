using EntityGenerator.DatabaseObjects.DataAccessObjects;
using EntityGenerator.DatabaseObjects.DataTransferObjects;
using EntityGenerator.Profile;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace IntegrationTests
{
  public class MicrosoftSqlServerReadTests : UnitTestFixture
  {
    /// <summary>
    /// xmlProfile to connect test database
    /// </summary>
    string xmlProfile = @"<?xml version = ""1.0"" encoding=""utf-16""?>
   <ProfileDTO xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
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
     <ConnectionString>Data Source=(localdb)\MSSQLLocalDB;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False</ConnectionString>
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
</ProfileDTO>
";

    [RunnableInDebugOnly]
    public void DatabaseObjectCount()
    {
      // arrange
      ProfileProvider profileProvider = ServiceProvider.GetRequiredService<ProfileProvider>();
      profileProvider.LoadProfile(xmlProfile);
      MicrosoftSqlServerDataAccessObject microsoftSqlServerDataAccessObject = ServiceProvider.GetRequiredService<MicrosoftSqlServerDataAccessObject>();

      // act
      int dbObjectCount = microsoftSqlServerDataAccessObject.DatabaseObjectCount();

      // assert
      Assert.True(0 < dbObjectCount);
    }

    [RunnableInDebugOnly]
    public void DatabaseSchemas()
    {
      // arrange
      ProfileProvider profileProvider = ServiceProvider.GetRequiredService<ProfileProvider>();
      profileProvider.LoadProfile(xmlProfile);
      MicrosoftSqlServerDataAccessObject microsoftSqlServerDataAccessObject = ServiceProvider.GetRequiredService<MicrosoftSqlServerDataAccessObject>();

      // act
      List<SchemaDTO> schemas = microsoftSqlServerDataAccessObject.DatabaseSchemas();

      // assert
      Assert.NotNull(schemas.FirstOrDefault(s => s.SchemaName.Equals("dbo", StringComparison.InvariantCultureIgnoreCase)));
      Assert.NotNull(schemas.FirstOrDefault(s => s.SchemaName.Equals("core", StringComparison.InvariantCultureIgnoreCase)));
    }

    [RunnableInDebugOnly]
    public void DatabaseTableValueObjects()
    {
      // arrange
      ProfileProvider profileProvider = ServiceProvider.GetRequiredService<ProfileProvider>();
      profileProvider.LoadProfile(xmlProfile);
      MicrosoftSqlServerDataAccessObject microsoftSqlServerDataAccessObject = ServiceProvider.GetRequiredService<MicrosoftSqlServerDataAccessObject>();

      // act
      List<TableValueObjectDTO> tableValueObjects = microsoftSqlServerDataAccessObject.DatabaseTableValueObjects();

      // assert
      Assert.True(0 < tableValueObjects.Count);
    }

    [RunnableInDebugOnly]
    public void DatabaseFunctions()
    {
      // arrange
      ProfileProvider profileProvider = ServiceProvider.GetRequiredService<ProfileProvider>();
      profileProvider.LoadProfile(xmlProfile);
      MicrosoftSqlServerDataAccessObject microsoftSqlServerDataAccessObject = ServiceProvider.GetRequiredService<MicrosoftSqlServerDataAccessObject>();

      // act
      List<FunctionDTO> functions = microsoftSqlServerDataAccessObject.DatabaseFunctions();

      // assert
      Assert.True(0 == functions.Count);
    }

    [RunnableInDebugOnly]
    public void DatabaseFunctionReturnColumns()
    {
      // arrange
      ProfileProvider profileProvider = ServiceProvider.GetRequiredService<ProfileProvider>();
      profileProvider.LoadProfile(xmlProfile);
      MicrosoftSqlServerDataAccessObject microsoftSqlServerDataAccessObject = ServiceProvider.GetRequiredService<MicrosoftSqlServerDataAccessObject>();

      // act
      //void microsoftSqlServerDataAccessObject.DatabaseFunctionReturnColumns(List<FunctionDTO> databaseFunctions)

      // assert
    }

    [RunnableInDebugOnly]
    public void DatabaseColumns()
    {
      // arrange
      ProfileProvider profileProvider = ServiceProvider.GetRequiredService<ProfileProvider>();
      profileProvider.LoadProfile(xmlProfile);
      MicrosoftSqlServerDataAccessObject microsoftSqlServerDataAccessObject = ServiceProvider.GetRequiredService<MicrosoftSqlServerDataAccessObject>();

      // act
      List<ColumnDTO> columns = microsoftSqlServerDataAccessObject.DatabaseColumns();

      // assert
      Assert.True(0 < columns.Count);
    }

    [RunnableInDebugOnly]
    public void DatabaseForeignKeys()
    {
      // arrange
      ProfileProvider profileProvider = ServiceProvider.GetRequiredService<ProfileProvider>();
      profileProvider.LoadProfile(xmlProfile);
      MicrosoftSqlServerDataAccessObject microsoftSqlServerDataAccessObject = ServiceProvider.GetRequiredService<MicrosoftSqlServerDataAccessObject>();

      // act
      List<ForeignKeyDTO> foreignKeys = microsoftSqlServerDataAccessObject.DatabaseForeignKeys();

      // assert
      Assert.True(0 < foreignKeys.Count);
    }

    [RunnableInDebugOnly]
    public void DatabaseCheckConstraints()
    {
      // arrange
      ProfileProvider profileProvider = ServiceProvider.GetRequiredService<ProfileProvider>();
      profileProvider.LoadProfile(xmlProfile);
      MicrosoftSqlServerDataAccessObject microsoftSqlServerDataAccessObject = ServiceProvider.GetRequiredService<MicrosoftSqlServerDataAccessObject>();

      // act
      List<CheckConstraintDTO> checkConstraints = microsoftSqlServerDataAccessObject.DatabaseCheckConstraints();

      // assert
      Assert.True(0 < checkConstraints.Count);
    }
  }
}
