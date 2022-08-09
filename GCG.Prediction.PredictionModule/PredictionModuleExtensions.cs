using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.ML;

using GCG.Prediction.Data;
using GCG.Prediction.PredictionModule.Clients;
using GCG.Prediction.PredictionModule.Loaders;

namespace GCG.Prediction.PredictionModule
{
    public static class PredictionModuleExtensions
    {
        public static IServiceCollection AddPredictionEnginePool(this IServiceCollection services)
        {
            services
                .AddPredictionEnginePool<InputData, PredictionData>();
            services
                .AddOptions<PredictionEnginePoolOptions<InputData, PredictionData>>()
                .Configure(options =>
                {
                    options.ModelLoader = new FileLoader();
                });
            services
                .AddTransient<IPredictionClient, PredictionClient>();

            return services;
        }
    }
}
