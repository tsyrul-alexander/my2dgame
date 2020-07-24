using My2DGame.Core.Property;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Tracker {
	public class ComponentPropertyTracker : BaseTracker<IProperty> {
		public ComponentPropertyTracker(IProperty value) : base(value) { }
	}
}