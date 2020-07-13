using My2DGame.Core.Component.GameObject;
using My2DGame.Network.Client.Synchronizer;
using My2DGame.Network.Client.Tracker;

namespace My2DGame.Network.Client.Manager {
	public class GameObjectComponentTrackedManager : TrackedManager<IGameObjectComponent> {
		public GameObjectComponentTrackedManager(IGameSynchronizer gameSynchronizer) : base(gameSynchronizer) { }
		protected override ITracker<IGameObjectComponent> CreateTracked(IGameObjectComponent value) {
			return new GameObjectComponentTracker(value);
		}
		protected override string GetManagerName() {
			return nameof(GameObjectComponentTrackedManager);
		}
	}
}