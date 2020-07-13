using System;
using System.ComponentModel;

namespace My2DGame.Core.Property {
	public interface IProperty : ISilentPropertyChanged, ICloneable {
		object GetValue();
		void SetSilentValue(object value);
	}
}