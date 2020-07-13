using My2DGame.Core.GameObject;
using My2DGame.Network.Synchronizer;
using My2DGame.Network.Tracker;

namespace My2DGame.Network.Manager {
	public class GameObjectTrackedManager : TrackedManager<IGameObject> {
		public GameObjectTrackedManager(IGameSynchronizer gameSynchronizer): base(gameSynchronizer) { }
		protected override ITracker<IGameObject> CreateTracked(IGameObject value) {
			return new GameObjectTracker(value);
		}
	}
}
