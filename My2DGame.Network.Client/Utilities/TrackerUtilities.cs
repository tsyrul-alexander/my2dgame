using System;
using My2DGame.Core;
using My2DGame.Core.Property;
using My2DGame.Network.Client.Synchronizer;
using My2DGame.Network.Contract;

namespace My2DGame.Network.Client.Utilities {
	public static class TrackerUtilities {
		public static void Add(this IGameSynchronizer synchronizer, Guid id, IProperty property) {
			synchronizer.ComponentPropertyTrackedManager.Add(id, property);
		}
		public static bool SetUpdateable(this IUpdateable drawable, PropertyValue propertyValue) {
			if (propertyValue.Name != nameof(drawable.Enabled)) {
				return false;
			}
			drawable.Enabled = propertyValue.GetBoolean();
			return true;
		}
		public static bool SetDrawable(this IDrawable drawable, PropertyValue propertyValue) {
			if (propertyValue.Name != nameof(drawable.Visible)) {
				return false;
			}
			drawable.Visible = propertyValue.GetBoolean();
			return true;
		}
	}
}
