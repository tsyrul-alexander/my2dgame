namespace My2DGame.Core.Property {
	public class IntegerProperty : BaseProperty<int> {
		public override object Clone() {
			return new IntegerProperty {
				Value = Value
			};
		}
	}
}
