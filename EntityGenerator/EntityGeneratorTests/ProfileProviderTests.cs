using EntityGenerator.Profile;
using EntityGenerator.Profile.DataTransferObject.Enums;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EntityGeneratorTests
{
  public class ProfileProviderTests : UnitTestFixture
  {
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


    [Fact]
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
  }
}