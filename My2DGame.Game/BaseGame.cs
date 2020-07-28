using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using My2DGame.Component.Utilities;
using My2DGame.Content.Manager;
using My2DGame.Content.Utilities;
using My2DGame.Core;
using My2DGame.Core.Manager;
using My2DGame.Core.Provider;
using My2DGame.Core.Scene;
using My2DGame.Core.UI;
using My2DGame.Core.Utilities;
using My2DGame.Network.Client.Synchronizer;
using My2DGame.Network.Client.Utilities;

namespace My2DGame.Game {
	public class BaseGame : IGame {
		protected IServiceProvider BaseServiceProvider { get; }
		protected GameOptions Options { get; }
		public virtual IList<IScene> Scenes { get; set; } = new List<IScene>();
		public IServiceProvider ServiceProvider { get; protected set; }
		public BaseGame(IServiceProvider baseServiceProvider, GameOptions options) {
			BaseServiceProvider = baseServiceProvider;
			Options = options;
		}
		public virtual void Initialize(ISpriteBatch spriteBatch) {
			ConfigureServiceProvider(spriteBatch);
		}
		public virtual void ConfigureServiceProvider(ISpriteBatch spriteBatch) {
			IServiceCollection serviceCollection = new ServiceCollection();
			serviceCollection.AddSingleton(spriteBatch);
			ConfigureGameServices(serviceCollection);
			var serviceProvider = new GameServiceProvider(serviceCollection);
			serviceProvider.Build();
			ServiceProvider = serviceProvider;
		}
		public virtual IScene CreateScene() {
			return ServiceProvider.GetService<IScene>();
		}
		public virtual IScene LoadScene(string directory, string sceneName) {
			var fileManager = GetFileManager();
			var content = fileManager.ReadAllText(fileManager.CombinePath(directory, sceneName));
			var sceneContentManager = GetContentManager<IScene>();
			return sceneContentManager.Load(content);
		}
		public virtual IFileManager GetFileManager() {
			return ServiceProvider.GetService<IFileManager>();
		}
		public virtual IContentManager<T> GetContentManager<T>() {
			return ServiceProvider.GetService<IContentManager<T>>();
		}
		public virtual IGameSynchronizer GetGameSynchronizer() {
			return ServiceProvider.GetService<IGameSynchronizer>();
		}
		public virtual void ConfigureGameServices(IServiceCollection serviceCollection) {
			serviceCollection.AddSingleton(new GameSynchronizerOptions(Options.ServerIpAddress, Options.ServerPort,
				Options.NetworkRoomId));
			serviceCollection.AddSingleton(new AssetManagerOptions(Options.ContentFolderPath));
			serviceCollection.UseGameComponent();
			serviceCollection.UseGameContent();
			serviceCollection.UseGameSynchronization();
			serviceCollection.UseScriptActions();
			var graphicsDeviceManager = (GraphicsDeviceManager) BaseServiceProvider.GetService(typeof(IGraphicsDeviceManager));
			serviceCollection.AddSingleton<IGraphicsDeviceManager>(graphicsDeviceManager);
			serviceCollection.AddSingleton<IGraphicsDeviceService>(graphicsDeviceManager);
			serviceCollection.AddLogging(builder => builder.AddFile($"{Options.LogFolderPath}\\Log.txt"));
			//serviceCollection.AddLogging(builder => builder.AddDebug());
		}
		public virtual void Draw(GameTime gameTime) {
			Scenes.ForEach(scene => scene.Draw(gameTime));
		}
		public virtual void Update(GameTime gameTime) {
			Scenes.ForEach(scene => scene.Update(gameTime));
		}
	}
}
