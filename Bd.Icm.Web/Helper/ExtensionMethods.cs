using System.Reflection;

namespace Bd.Icm.Web.Helper {
    public static class ExtensionMethods {
        public static string GetVersion() {
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName();
            var version = assemblyName.Version;
            return version.ToString();
        }
    }
}