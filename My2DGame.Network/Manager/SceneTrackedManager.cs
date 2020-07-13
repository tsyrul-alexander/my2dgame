using System.Collections.Specialized;
using My2DGame.Core.GameObject;
using My2DGame.Core.Scene;
using My2DGame.Network.Synchronizer;
using My2DGame.Network.Tracker;

namespace My2DGame.Network.Manager {
	public class SceneTrackedManager : TrackedManager<IScene> {
		public SceneTrackedManager(IGameSynchronizer gameSynchronizer) : base(gameSynchronizer) { }
		protected override ITracker<IScene> CreateTracked(IScene value) {
			value.GameObjects.CollectionChanged += GameObjectsOnCollectionChanged;
			return new SceneTracker(value);
		}
		private void GameObjectsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (e.Action == NotifyCollectionChangedAction.Add) {
				foreach (var newItem in e.NewItems) {
					GameSynchronizer.GameObjectTrackedManager.Create((IGameObject)newItem);
				}
			} else if (e.Action == NotifyCollectionChangedAction.Remove) {
				foreach (var oldItem in e.OldItems) {
					GameSynchronizer.GameObjectTrackedManager.Remove((IGameObject)oldItem);
				}
			}
		}
	}
}
