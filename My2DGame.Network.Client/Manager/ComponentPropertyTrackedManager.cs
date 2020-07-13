using My2DGame.Core.Property;
using My2DGame.Network.Client.Synchronizer;
using My2DGame.Network.Client.Tracker;

namespace My2DGame.Network.Client.Manager {
	public class ComponentPropertyTrackedManager : TrackedManager<IProperty> {
		public ComponentPropertyTrackedManager(IGameSynchronizer gameSynchronizer) : base(gameSynchronizer) { }
		protected override ITracker<IProperty> CreateTracked(IProperty value) {
			return new ComponentPropertyTracker(value);
		}
		protected override string GetManagerName() {
			return nameof(ComponentPropertyTrackedManager);
		}
	}
}