using My2DGame.Core.Property;
using My2DGame.Network.Client.Contract;

namespace My2DGame.Network.Client.Tracker {
	public class ComponentPropertyTracker : BaseTracker<IProperty> {
		public ComponentPropertyTracker(IProperty value) : base(value) { }
		public override void UpdateProperty(PropertyValue propertyValue) {
			if (propertyValue.Name == nameof(IProperty<string>.Value)) {
				Value.SetSilentValue(propertyValue.GetValue());
			}
		}
		protected override PropertyValue GetPropertyValue(string columnName) {
			if (columnName == nameof(IProperty<string>.Value)) {
				return new PropertyValue(columnName, Value.GetValue());
			}
			return null;
		}
	}
}