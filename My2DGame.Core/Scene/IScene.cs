using System.Collections.ObjectModel;
using My2DGame.Core.Component.UI;
using My2DGame.Core.GameObject;

namespace My2DGame.Core.Scene {
	public interface IScene: IUpdateable, IDrawable {
		string Name { get; set; }
		ISpriteBatch SpriteBatch { get; }
		ObservableCollection<IGameObject> GameObjects { get; }
		IGameObject CreateGameObject();
	}
}