using Microsoft.Extensions.DependencyInjection;
using My2DGame.Network.Client.Client;
using My2DGame.Network.Client.Client.TCP;
using My2DGame.Network.Client.Synchronizer;

namespace My2DGame.Network.Client.Utilities {
	public static class ExtensionUtilities {
		public static void UseGameSynchronization(this IServiceCollection serviceCollection) {
			serviceCollection.UseGameSynchronizer();
			serviceCollection.UseNetworkClient();
		}
		public static void UseGameSynchronizer(this IServiceCollection serviceCollection) {
			serviceCollection.AddTransient<IGameSynchronizer, GameSynchronizer>();
		}
		public static void UseNetworkClient(this IServiceCollection serviceCollection) {
			serviceCollection.AddTransient<INetworkClient, ClientTcp>();
		}
	}
}
