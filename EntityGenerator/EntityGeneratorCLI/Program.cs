﻿using EntityGenerator.DatabaseObjects.DataAccessObjects;
using EntityGenerator.Initializer;
using EntityGenerator.Profile;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using EntityGenerator.Core.Interfaces;
using EntityGenerator.Core.Services;
using EntityGenerator.Profile.DataTransferObject;
using EntityGenerator.InformationExtractor.Interfaces;
using EntityGenerator.Core.Models.ModelObjects;
using EntityGenerator.CodeGeneration.Interfaces;

namespace EntityGeneratorCLI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("EntityGenerator V4");
            ServiceProvider serviceProvider = CreateServiceProvider();
            IStandardOutput standardOutput = serviceProvider.GetService<IStandardOutput>();
            IProfileProvider profileProvider = serviceProvider.GetRequiredService<IProfileProvider>();
            ICoreServiceWorker coreServiceWorker = serviceProvider.GetRequiredService<ICoreServiceWorker>();


            //TestOnly
            standardOutput.PrimaryOutputProvider = new CliOutputProvider();
            Database db = coreServiceWorker.ExtractData(CreateProfileDto());
            coreServiceWorker.GenerateCode(db);

            profileProvider.SaveProfileToFileJson(args.FirstOrDefault());
            profileProvider.SaveProfileToFileXml(args.FirstOrDefault());
        }

        private static ProfileDto CreateProfileDto()
        {
            ProfileDto profileDto = new ProfileDto();
            profileDto.Database = new ProfileDatabaseDto();
            profileDto.Database.ConnectionString = @"Server=LOCALHOST\LOCALDB; Database=Lenny; Trusted_Connection=True; TrustServerCertificate=True;";
            profileDto.Database.DatabaseName = "Lenny";
            profileDto.Global.ProjectName = "Lenny";
            profileDto.Path.BusinessLogicDir = System.IO.Directory.GetCurrentDirectory() + "\\BusinessLogic";
            profileDto.Path.CommonDir = System.IO.Directory.GetCurrentDirectory() + "\\Common";
            profileDto.Path.FrontendDir = System.IO.Directory.GetCurrentDirectory() + "\\Frontend";
            return profileDto;
        }

        protected static ServiceProvider CreateServiceProvider()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IStandardOutput, CliStandardOutput>();

            serviceCollection.AddSingleton(new EntityGeneratorInitializer(serviceCollection));
            serviceCollection.AddTransient<IDataAccessObject, DataAccessObject>();
            serviceCollection.AddTransient<IInformationExtractor, EntityGenerator.InformationExtractor.MSSqlServer.Services.InformationExtractor.InformationExtractor>();
            serviceCollection.AddTransient<IInformationExtractorWorker, EntityGenerator.InformationExtractor.MSSqlServer.Services.InformationExtractor.InformationExtractorWorker>();
            serviceCollection.AddTransient<IFileWriterService, EntityGenerator.CodeGeneration.Services.FileWriterService>();
            serviceCollection.AddTransient<ILanguageProvider, EntityGenerator.CodeGeneration.Languages.LanguageProvider>();
            serviceCollection.AddTransient<ICodeGenerator, EntityGenerator.CodeGeneration.Services.CodeGenerator>();
            serviceCollection.AddTransient<ICodeGeneratorWorker, EntityGenerator.CodeGeneration.Services.CodeGeneratorWorker>();
            serviceCollection.AddTransient<ICoreServiceWorker, CoreServiceWorker>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
