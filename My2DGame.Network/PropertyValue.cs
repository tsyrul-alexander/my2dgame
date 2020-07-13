using System;

namespace My2DGame.Network.Client.Contract {
	[Serializable]
	public class PropertyValue: INetworkObject {
		public string Name { get; set; }
		protected object Value { get; set; }
		public PropertyValue(string name, bool value): this(name) {
			Value = value;
		}
		public PropertyValue(string name, string value) : this(name) {
			Value = value;
		}
		public PropertyValue(string name, double value) : this(name) {
			Value = value;
		}
		public PropertyValue(string name, object value) : this(name) {
			Value = value;
		}
		private PropertyValue(string name) {
			Name = name;
		}
		public object GetValue() {
			return Value;
		}
		public bool GetBoolean() {
			return (bool)Value;
		}
		public string GetString() {
			return (string)Value;
		}
		public double GetDouble() {
			return (double)Value;
		}
	}
}
