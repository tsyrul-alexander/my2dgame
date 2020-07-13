using System.Collections.ObjectModel;
using System.ComponentModel;
using My2DGame.Core.GameObject;
using My2DGame.Core.Manager;
using My2DGame.Core.UI;

namespace My2DGame.Core.Scene {
	public interface IScene: INotifyPropertyChanged, IUpdateable, IDrawable {
		string Name { get; set; }
		ISpriteBatch SpriteBatch { get; }
		IAssetManager AssetManager { get; }
		ObservableCollection<IGameObject> GameObjects { get; }
		IGameObject CreateGameObject();
		void Initialize();
	}
}