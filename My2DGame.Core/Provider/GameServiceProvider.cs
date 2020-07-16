using System;
using Microsoft.Extensions.DependencyInjection;

namespace My2DGame.Core.Provider {
	public class GameServiceProvider : IServiceProvider {
		private readonly IServiceCollection _serviceCollection;
		private ServiceProvider _provider;
		public GameServiceProvider(IServiceCollection serviceCollection) {
			_serviceCollection = serviceCollection;
		}
		public object GetService(Type serviceType) {
			return _provider.GetService(serviceType);
		}
		public virtual void Build() {
			_provider?.Dispose();
			_provider = _serviceCollection.BuildServiceProvider();
		}
	}
}