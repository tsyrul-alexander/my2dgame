using My2DGame.Core.Component.GameObject;
using My2DGame.Network.Synchronizer;
using My2DGame.Network.Tracker;

namespace My2DGame.Network.Manager {
	public class GameObjectComponentTrackedManager : TrackedManager<IGameObjectComponent> {
		public GameObjectComponentTrackedManager(IGameSynchronizer gameSynchronizer) : base(gameSynchronizer) { }
		protected override ITracker<IGameObjectComponent> CreateTracked(IGameObjectComponent value) {
			return new GameObjectComponentTracker(value);
		}
	}
}