using System;
using My2DGame.Core;
using My2DGame.Network.Client.Manager;
using Newtonsoft.Json.Linq;

namespace My2DGame.Content.Utilities {
	public static class JsonUtilities {
		public static void JPropertyToEnabled(this IUpdateable updateable, JObject jObject) {
			var enabled = jObject.GetValue("enabled").Value<bool>();
			updateable.Enabled = enabled;
		}
		public static void JPropertyToVisible(this IDrawable drawable, JObject jObject) {
			var visible = jObject.GetValue("visible").Value<bool>();
			drawable.Visible = visible;
		}
		public static JProperty EnabledToJProperty(this IUpdateable updateable) {
			return new JProperty("enabled", updateable.Enabled);
		}
		public static JProperty VisibleToJProperty(this IDrawable drawable) {
			return new JProperty("visible", drawable.Visible);
		}
		public static void SetNetworkItem<T>(this JObject jObject, ITrackedManager<T> trackedManager, T item) where T : ISilentPropertyChanged {
			var networkId = jObject.Value<string>("network");
			if (networkId == null) {
				return;
			}
			trackedManager.Add(Guid.Parse(networkId), item);
		}
		public static JProperty NetworkItemToJProperty<T>(this ITrackedManager<T> trackedManager, T item) where T : ISilentPropertyChanged {
			if (trackedManager.TryGetItem(item, out var networkId)) {
				return new JProperty("network", networkId);
			}
			return new JProperty("network", null);
		}
	}
}
