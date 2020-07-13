using Microsoft.Xna.Framework;

namespace My2DGame.Network.Tracker {
	public class PropertyValue {
		public string Name { get; set; }
		protected object Value { get; set; }
		public PropertyValue(string name, bool value): this(name) {
			Value = value;
		}
		public PropertyValue(string name, Color color): this(name, color.ToVector4()) { }
		public PropertyValue(string name, Vector4 value) : this(name) {
			Value = value;
		}
		private PropertyValue(string name) {
			Name = name;
		}
		public bool GetBoolean() {
			return (bool) Value;
		}
		public Vector4 GetVector4() {
			return (Vector4)Value;
		}
		public Color GetColor() {
			return new Color(GetVector4());
		}
	}
}
