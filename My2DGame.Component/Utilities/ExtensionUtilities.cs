using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using My2DGame.Component.Script;
using My2DGame.Core.Utilities;

namespace My2DGame.Component.Utilities {
	public static class ExtensionUtilities {
		public static void UseScriptActions(this IServiceCollection serviceCollection) {
			var assemblies = ReflectionUtilities.GetAssemblies<ScriptActionAssemblyAttribute>();
			var scriptActions = ReflectionUtilities.GetTypesIsAssignableFrom(typeof(IScriptAction), assemblies.ToArray());
			scriptActions.ForEach(type => {
				if (type.IsAbstract || type.IsInterface) {
					return;
				}
				serviceCollection.AddTransient(type);
			});
		}
	}
}
