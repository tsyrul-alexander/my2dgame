using My2DGame.Core;
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
	}
}
