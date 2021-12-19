using EntityGenerator.Profile;
using EntityGenerator.Profile.DataTransferObject.Enums;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace UnitTests
{
  public class ProfileProviderTests : UnitTestFixture
  {
    /// <summary>
    /// Standard json mock profile for test
    /// </summary>
    string jsonProfile = @"
{
 ""Global"":
   {
    ""ProjectName"":""TestProject"",
    ""GeneratedPrefix"":"""",
    ""GeneratedSuffix"":"".Generated"",
    ""GeneratedFolder"":""_Generated"",
    ""LanguageBackend"":""CSharp"",
    ""LanguageFrontend"":""TypeScript"",
    ""NoWipe"":false
   },
 ""Database"":
   {
     ""ConnectionString"":""TestConnection"",
     ""DatabaseName"":""TestDatabase"",
     ""SourceDatabaseType"":""MicrosoftSqlServer"",
     ""TargetDatabaseTypes"":
     [
      ""MicrosoftSqlServer"",
      ""Oracle""
     ]
   },
 ""Path"":
   {
     ""RootDir"":""C:\\RootDir"",
     ""SqlDir"":""C:\\SqlDir"",
     ""DataAccessDir"":""C:\\DataAccessDir"",
     ""DataAccessFacadeDir"":""C:\\DataAccessFacadeDir"",
     ""CommonDir"":""C:\\CommonDir"",
     ""AbstractionsDir"":""C:\\AbstractionsDir"",
     ""BusinessLogicDir"":""C:\\BusinessLogicDir"",
     ""FrontendDir"":""C:\\FrontendDir""
   },
 ""Generator"":
   {
     ""GeneratorFrontend"":
       {
         ""Constants"":true,
         ""ViewModels"":false
       },
     ""GeneratorBusinessLogic"":
       {
         ""Logic"":false
       },
     ""GeneratorCommon"":
       {
         ""ConstantsBackend"":false,
         ""DataTransferObjects"":false
       },
     ""GeneratorDatabase"":
       {
         ""HistoryTables"":false,
         ""HistoryTriggers"":false,
         ""MergeScripts"":false,
         ""CheckConstraintScripts"":false
       }
   }
}";

    /// <summary>
    /// Standard xml mock profile for test
    /// </summary>
    string xmlProfile = @"<?xml version = ""1.0"" encoding=""utf-16""?>
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
     <ConnectionString>TestConnection</ConnectionString>
     <DatabaseName>TestDatabase</DatabaseName>
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

    [Fact]
    [Trait("Category", "Unit-Tests")]
    public void LoadJsonStructureToProfile()
    {
      // arrange
      ProfileProvider profileProvider = _serviceProvider.GetRequiredService<ProfileProvider>();

      // act
      profileProvider.LoadProfile(jsonProfile);

      // assert
      Assert.Equal("TestProject", profileProvider.Profile.Global.ProjectName);
      Assert.Equal(string.Empty, profileProvider.Profile.Global.GeneratedPrefix);
      Assert.Equal(".Generated", profileProvider.Profile.Global.GeneratedSuffix);
      Assert.Equal("_Generated", profileProvider.Profile.Global.GeneratedFolder);
      Assert.Equal(Languages.CSharp, profileProvider.Profile.Global.LanguageBackend);
      Assert.Equal(Languages.TypeScript, profileProvider.Profile.Global.LanguageFrontend);
      Assert.False(profileProvider.Profile.Global.NoWipe);

      Assert.Equal("TestConnection", profileProvider.Profile.Database.ConnectionString);
      Assert.Equal("TestDatabase", profileProvider.Profile.Database.DatabaseName);
      Assert.Equal(DatabaseTypes.MicrosoftSqlServer, profileProvider.Profile.Database.SourceDatabaseType);
      Assert.Equal(DatabaseTypes.MicrosoftSqlServer, profileProvider.Profile.Database.TargetDatabaseTypes[0]);
      Assert.Equal(DatabaseTypes.Oracle, profileProvider.Profile.Database.TargetDatabaseTypes[1]);

      Assert.Equal(@"C:\RootDir", profileProvider.Profile.Path.RootDir);
      Assert.Equal(@"C:\SqlDir", profileProvider.Profile.Path.SqlDir);
      Assert.Equal(@"C:\DataAccessDir", profileProvider.Profile.Path.DataAccessDir);
      Assert.Equal(@"C:\DataAccessFacadeDir", profileProvider.Profile.Path.DataAccessFacadeDir);
      Assert.Equal(@"C:\CommonDir", profileProvider.Profile.Path.CommonDir);
      Assert.Equal(@"C:\AbstractionsDir", profileProvider.Profile.Path.AbstractionsDir);
      Assert.Equal(@"C:\BusinessLogicDir", profileProvider.Profile.Path.BusinessLogicDir);
      Assert.Equal(@"C:\FrontendDir", profileProvider.Profile.Path.FrontendDir);

      Assert.True(profileProvider.Profile.Generator.GeneratorFrontend.Constants);
      Assert.False(profileProvider.Profile.Generator.GeneratorFrontend.ViewModels);
      Assert.False(profileProvider.Profile.Generator.GeneratorBusinessLogic.Logic);
      Assert.False(profileProvider.Profile.Generator.GeneratorCommon.ConstantsBackend);
      Assert.False(profileProvider.Profile.Generator.GeneratorCommon.DataTransferObjects);
      Assert.False(profileProvider.Profile.Generator.GeneratorDatabase.HistoryTables);
      Assert.False(profileProvider.Profile.Generator.GeneratorDatabase.HistoryTriggers);
      Assert.False(profileProvider.Profile.Generator.GeneratorDatabase.MergeScripts);
      Assert.False(profileProvider.Profile.Generator.GeneratorDatabase.CheckConstraintScripts);

    }

    [Fact]
    [Trait("Category", "Unit-Tests")]
    public void LoadXmlStructureToProfile()
    {
      // arrange
      ProfileProvider profileProvider = _serviceProvider.GetRequiredService<ProfileProvider>();

      // act
      profileProvider.LoadProfile(xmlProfile);

      // assert
      Assert.Equal("TestProject", profileProvider.Profile.Global.ProjectName);
      Assert.Equal(string.Empty, profileProvider.Profile.Global.GeneratedPrefix);
      Assert.Equal(".Generated", profileProvider.Profile.Global.GeneratedSuffix);
      Assert.Equal("_Generated", profileProvider.Profile.Global.GeneratedFolder);
      Assert.Equal(Languages.CSharp, profileProvider.Profile.Global.LanguageBackend);
      Assert.Equal(Languages.TypeScript, profileProvider.Profile.Global.LanguageFrontend);
      Assert.False(profileProvider.Profile.Global.NoWipe);

      Assert.Equal("TestConnection", profileProvider.Profile.Database.ConnectionString);
      Assert.Equal("TestDatabase", profileProvider.Profile.Database.DatabaseName);
      Assert.Equal(DatabaseTypes.MicrosoftSqlServer, profileProvider.Profile.Database.SourceDatabaseType);
      Assert.Equal(DatabaseTypes.MicrosoftSqlServer, profileProvider.Profile.Database.TargetDatabaseTypes[0]);
      Assert.Equal(DatabaseTypes.Oracle, profileProvider.Profile.Database.TargetDatabaseTypes[1]);

      Assert.Equal(@"C:\RootDir", profileProvider.Profile.Path.RootDir);
      Assert.Equal(@"C:\SqlDir", profileProvider.Profile.Path.SqlDir);
      Assert.Equal(@"C:\DataAccessDir", profileProvider.Profile.Path.DataAccessDir);
      Assert.Equal(@"C:\DataAccessFacadeDir", profileProvider.Profile.Path.DataAccessFacadeDir);
      Assert.Equal(@"C:\CommonDir", profileProvider.Profile.Path.CommonDir);
      Assert.Equal(@"C:\AbstractionsDir", profileProvider.Profile.Path.AbstractionsDir);
      Assert.Equal(@"C:\BusinessLogicDir", profileProvider.Profile.Path.BusinessLogicDir);
      Assert.Equal(@"C:\FrontendDir", profileProvider.Profile.Path.FrontendDir);

      Assert.True(profileProvider.Profile.Generator.GeneratorFrontend.Constants);
      Assert.False(profileProvider.Profile.Generator.GeneratorFrontend.ViewModels);
      Assert.False(profileProvider.Profile.Generator.GeneratorBusinessLogic.Logic);
      Assert.False(profileProvider.Profile.Generator.GeneratorCommon.ConstantsBackend);
      Assert.False(profileProvider.Profile.Generator.GeneratorCommon.DataTransferObjects);
      Assert.False(profileProvider.Profile.Generator.GeneratorDatabase.HistoryTables);
      Assert.False(profileProvider.Profile.Generator.GeneratorDatabase.HistoryTriggers);
      Assert.False(profileProvider.Profile.Generator.GeneratorDatabase.MergeScripts);
      Assert.False(profileProvider.Profile.Generator.GeneratorDatabase.CheckConstraintScripts);

    }
  }
}
