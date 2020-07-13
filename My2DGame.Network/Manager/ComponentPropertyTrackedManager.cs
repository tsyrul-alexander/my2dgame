using My2DGame.Core.Property;
using My2DGame.Network.Synchronizer;
using My2DGame.Network.Tracker;

namespace My2DGame.Network.Manager {
	public class ComponentPropertyTrackedManager : TrackedManager<IProperty> {
		public ComponentPropertyTrackedManager(IGameSynchronizer gameSynchronizer) : base(gameSynchronizer) { }
		protected override ITracker<IProperty> CreateTracked(IProperty value) {
			return new ComponentPropertyTracker(value);
		}
	}
}