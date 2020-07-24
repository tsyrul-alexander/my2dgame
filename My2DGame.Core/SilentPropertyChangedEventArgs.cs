using System.ComponentModel;

namespace My2DGame.Core {
	public class SilentPropertyChangedEventArgs: PropertyChangedEventArgs {
		public bool IsSilent { get; }
		public object Value { get; set; }
		public SilentPropertyChangedEventArgs(string propertyName,object value, bool isSilent = false) : base(propertyName) {
			IsSilent = isSilent;
			Value = value;
		}
	}
}
