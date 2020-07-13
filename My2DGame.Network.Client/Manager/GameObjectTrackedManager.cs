using My2DGame.Core.GameObject;
using My2DGame.Network.Client.Synchronizer;
using My2DGame.Network.Client.Tracker;

namespace My2DGame.Network.Client.Manager {
	public class GameObjectTrackedManager : TrackedManager<IGameObject> {
		public GameObjectTrackedManager(IGameSynchronizer gameSynchronizer): base(gameSynchronizer) { }
		protected override ITracker<IGameObject> CreateTracked(IGameObject value) {
			return new GameObjectTracker(value);
		}
		protected override string GetManagerName() {
			return nameof(GameObjectTrackedManager);
		}
	}
}
