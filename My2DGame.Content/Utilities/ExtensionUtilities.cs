using Microsoft.Extensions.DependencyInjection;
using My2DGame.Content.Manager;
using My2DGame.Content.Manager.Json;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.Property;
using My2DGame.Core.Scene;

namespace My2DGame.Content.Utilities {
	public static class ExtensionUtilities {
		public static void UseGameContent(this IServiceCollection serviceCollection) {
			serviceCollection.UseJsonContentManagers();
		}
		public static void UseJsonContentManagers(this IServiceCollection serviceCollection) {
			serviceCollection.UseJsonSceneContentManager();
			serviceCollection.UseJsonGameObjectContentManager();
			serviceCollection.UseJsonGameObjectComponentContentManager();
			serviceCollection.UseJsonPropertyContentManager();
		}
		public static void UseJsonSceneContentManager(this IServiceCollection serviceCollection) {
			serviceCollection.AddTransient<IContentManager<IScene>, JsonSceneContentManager>();
		}
		public static void UseJsonGameObjectContentManager(this IServiceCollection serviceCollection) {
			serviceCollection.AddTransient<IContentManager<IGameObject>, JsonGameObjectContentManager>();
		}
		public static void UseJsonGameObjectComponentContentManager(this IServiceCollection serviceCollection) {
			serviceCollection.AddTransient<IContentManager<IGameObjectComponent>, JsonGameObjectComponentContentManager>();
		}
		public static void UseJsonPropertyContentManager(this IServiceCollection serviceCollection) {
			serviceCollection.AddTransient<IContentManager<IProperty>, JsonPropertyContentManager>();
		}
	}
}
