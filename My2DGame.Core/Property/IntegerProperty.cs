using System;

namespace My2DGame.Core.Property {
	public class IntegerProperty : BaseProperty<int> {
		public override object Clone() {
			return new IntegerProperty(Value);
		}
		public IntegerProperty(int value = default){
			Value = value;
		}
	}
}
