using System.IO;
using System.Reflection;

namespace GCG.Prediction.Core.Helpers
{
    public static class PathHelpers
    {
        public static string GetAbsolutePath(string path)
        {
            var assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var assemblyPath = Directory.GetParent(assemblyLocation).FullName;
            var absolutePath = Path.Combine(assemblyPath, path);
            return absolutePath;
        }
    }
}
