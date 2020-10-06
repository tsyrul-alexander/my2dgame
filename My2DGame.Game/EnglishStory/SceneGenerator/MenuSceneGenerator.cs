using System;
using Microsoft.Xna.Framework;
using My2DGame.Core.Input;
using My2DGame.Core.Scene;
using My2DGame.UI.Control;

namespace My2DGame.Game.EnglishStory.Scene {
	public class MenuSceneGenerator {
		public IServiceProvider ServiceProvider { get; }
		public IMouseInput MouseInput { get; }
		public MenuSceneGenerator(IServiceProvider serviceProvider, IMouseInput mouseInput) {
			ServiceProvider = serviceProvider;
			MouseInput = mouseInput;
		}
		public IScene Generate() {
			//@"Brick\grey_brick\grey_brick_state_1_center_repeating"
			var scene = (IScene)ServiceProvider.GetService(typeof(IScene));
			var btn = new Button(MouseInput, @"Control\button\button_next", new Vector2(50, 50),
				new Rectangle(0, 0, 50, 50));
			btn.MouseClick += BtnOnMouseClick;
			scene.AddGameObject(btn);
			return scene;
		}
		private void BtnOnMouseClick(Button arg1, EventArgs arg2) {
			
		}
	}
}
