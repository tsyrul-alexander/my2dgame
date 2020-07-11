namespace My2DGame.Core.Property {
	public class StringProperty : BaseProperty<string> {
		public override object Clone() {
			return new StringProperty {
				Value = Value
			};
		}
	}
}