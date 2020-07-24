using My2DGame.Core.Property;
using My2DGame.Network.Client.Tracker;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Manager {
	public class ComponentPropertyTrackedManager : TrackedManager<IProperty> {
		protected override ITracker<IProperty> CreateTracked(IProperty value) {
			return new ComponentPropertyTracker(value);
		}
		protected override CreateManagerItem GetCreateManagerItem(ITracker<IProperty> tracker) {
			return new CreateManagerItem {
				Id = tracker.Id,
				ManagerName = GetManagerName(),
				Values = new [] {
					new PropertyValue(nameof(IProperty<string>.Value), tracker.Value.GetValue()) 
				}
			};
		}
		protected override string GetManagerName() {
			return nameof(ComponentPropertyTrackedManager);
		}
		protected override IProperty CreateItem(CreateManagerItem createManagerItem) {
			throw new System.NotImplementedException();
		}
	}
}