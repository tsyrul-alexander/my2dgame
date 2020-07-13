namespace My2DGame.Core.Property {
	public class StringProperty : BaseProperty<string> {
		public StringProperty(string value = default) {
			Value = value;
		}
		public override object Clone() {
			return new StringProperty {
				Value = Value
			};
		}
	}
}