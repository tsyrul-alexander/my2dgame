using Microsoft.Extensions.DependencyInjection;
using My2DGame.Core.Component.GameObject;
using My2DGame.Core.GameObject;
using My2DGame.Core.Property;
using My2DGame.Core.Scene;
using My2DGame.Network.Client.Client;
using My2DGame.Network.Client.Client.TCP;
using My2DGame.Network.Client.Manager;
using My2DGame.Network.Client.Synchronizer;

namespace My2DGame.Network.Client.Utilities {
	public static class ExtensionUtilities {
		public static void UseGameSynchronization(this IServiceCollection serviceCollection) {
			serviceCollection.UseGameSynchronizer();
			serviceCollection.UseNetworkClient();
			serviceCollection.UseTrackerManager();
		}
		public static void UseGameSynchronizer(this IServiceCollection serviceCollection) {
			serviceCollection.AddTransient<IGameSynchronizer, GameSynchronizer>();
		}
		public static void UseNetworkClient(this IServiceCollection serviceCollection) {
			serviceCollection.AddTransient<INetworkClient, ClientTcp>();
		}
		public static void UseTrackerManager(this IServiceCollection serviceCollection) {
			serviceCollection.AddSingleton<ITrackedManager<IScene>, SceneTrackedManager>();
			serviceCollection.AddSingleton<ITrackedManager<IGameObject>, GameObjectTrackedManager>();
			serviceCollection.AddSingleton<ITrackedManager<IGameObjectComponent>, GameObjectComponentTrackedManager>();
			serviceCollection.AddSingleton<ITrackedManager<IProperty>, ComponentPropertyTrackedManager>();
		}
	}
}
