using System;
using Microsoft.Extensions.DependencyInjection;
using My2DGame.Core;
using My2DGame.Core.Input;
using My2DGame.Core.UI;
using My2DGame.Game.EnglishStory.Scene;

namespace My2DGame.Game.EnglishStory {
	public class EnglishStoryGame: BaseGame {
		public GameInput GameInput { get; }
		public EnglishStoryGame(GameInput gameInput, IServiceProvider baseServiceProvider, GameOptions options) :
			base(baseServiceProvider, options) {
			GameInput = gameInput;
		}
		public override void ConfigureGameServices(IServiceCollection serviceCollection) {
			base.ConfigureGameServices(serviceCollection);
			serviceCollection.AddSingleton<MenuSceneGenerator>();
			serviceCollection.AddSingleton<IMouseInput>(GameInput);
		}
		public override void Initialize(ISpriteBatch spriteBatch) {
			base.Initialize(spriteBatch);
			var menuSceneGenerator = (MenuSceneGenerator)ServiceProvider.GetService(typeof(MenuSceneGenerator));
			var menuScene = menuSceneGenerator.Generate();
			menuScene.Initialize();
			Scenes.Add(menuScene);
		}
	}
}
