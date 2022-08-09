using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.ML;

using GCG.Prediction.Core.Contracts;
using GCG.Prediction.Core.Implementations.Evaluaters;
using GCG.Prediction.Core.Implementations.Loader;
using GCG.Prediction.Core.Implementations.Trainers;
using GCG.Prediction.Core.Implementations.Savers;
using GCG.Prediction.Core.Settings;

namespace GCG.Prediction.Infrastructure
{
    public class ContainerConfiguration
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            // Core
            services
                .AddTransient<ITrainer, RegressionTrainer>()
                .AddTransient<IEvaluater, RegressionEvaluater>()
                .AddTransient<IDataLoader, FileLoader>()
                .AddTransient<ISaver, FileSaver>()
                .AddSingleton(new MLContext(seed: 0));

            // Settings
            services
                .Configure<MLSettings>(configuration.GetSection(nameof(MLSettings)));
        }
    }
}