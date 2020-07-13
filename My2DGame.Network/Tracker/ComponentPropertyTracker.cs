using System;
using My2DGame.Core.Property;

namespace My2DGame.Network.Tracker {
	public class ComponentPropertyTracker : BaseTracker<IProperty> {
		public ComponentPropertyTracker(IProperty value) : base(value) { }
		public override void UpdateProperty(PropertyValue propertyValue) {
			throw new NotImplementedException();
		}
	}
}