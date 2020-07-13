using System.ComponentModel;

namespace My2DGame.Core {
	public class SilentPropertyChangedEventArgs: PropertyChangedEventArgs {
		public bool IsSilent { get; }
		public SilentPropertyChangedEventArgs(string propertyName, bool isSilent = false) : base(propertyName) {
			IsSilent = isSilent;
		}
	}
}
