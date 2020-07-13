using System;
using My2DGame.Core;
using My2DGame.Core.Scene;
using My2DGame.Network.Client.Contract;
using My2DGame.Network.Client.Tracker;

namespace My2DGame.Network.Client.Utilities {
	public static class TrackerUtilities {
		public static ITracker<IScene> Tracked(this IScene scene) {
			return new SceneTracker(scene);
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
