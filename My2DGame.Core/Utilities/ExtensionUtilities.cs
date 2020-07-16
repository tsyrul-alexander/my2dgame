using Microsoft.Extensions.DependencyInjection;
using My2DGame.Core.GameObject;
using My2DGame.Core.GameObject.Collider;
using My2DGame.Core.Manager;
using My2DGame.Core.Scene;

namespace My2DGame.Core.Utilities
{
	public static class ExtensionUtilities {
		public static void UseGameComponent(this IServiceCollection serviceCollection) {
			serviceCollection.UseFileManager();
			serviceCollection.UseAssetManager();
			serviceCollection.UseCollisionManager();
			serviceCollection.UseGameObject();
			serviceCollection.UseScene();
		}
		public static void UseFileManager(this IServiceCollection serviceCollection) {
			serviceCollection.AddSingleton<IFileManager, FileManager>();
		}
		public static void UseScene(this IServiceCollection serviceCollection) {
			serviceCollection.AddTransient<IScene, Scene.Scene>();
		}
		public static void UseAssetManager(this IServiceCollection serviceCollection) {
			serviceCollection.AddSingleton<IAssetManager, AssetManager>();
		}
		public static void UseCollisionManager(this IServiceCollection serviceCollection) {
			serviceCollection.AddSingleton<ICollisionManager, CollisionManager>();
		}
		public static void UseGameObject(this IServiceCollection serviceCollection) {
			serviceCollection.AddTransient<IGameObject, GameObject.GameObject>();
		}
	}
}
