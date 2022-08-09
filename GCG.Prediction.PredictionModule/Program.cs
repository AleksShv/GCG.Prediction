using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using GCG.Prediction.Infrastructure;
using GCG.Prediction.PredictionModule.ModelTrainers;

namespace GCG.Prediction.PredictionModule
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.Services
                .GetService<RegressionModelTrainer>()
                .TrainAndSaveModel();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, configuration) =>
                {
                    configuration.Sources.Clear();
                    configuration
                        .AddJsonFile("appsettings.json", false, true)
                        .Build();
                })
                .ConfigureServices((context, services) =>
                {
                    ContainerConfiguration.Configure(services, context.Configuration);
                    services.AddSingleton<RegressionModelTrainer>();
                });
    }
}