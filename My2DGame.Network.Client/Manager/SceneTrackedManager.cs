using My2DGame.Core.Scene;
using My2DGame.Network.Client.Synchronizer;
using My2DGame.Network.Client.Tracker;

namespace My2DGame.Network.Client.Manager {
	public class SceneTrackedManager : TrackedManager<IScene> {
		public SceneTrackedManager(IGameSynchronizer gameSynchronizer) : base(gameSynchronizer) { }
		protected override ITracker<IScene> CreateTracked(IScene value) {
			return new SceneTracker(value);
		}
		protected override string GetManagerName() {
			return nameof(SceneTrackedManager);
		}
	}
}
