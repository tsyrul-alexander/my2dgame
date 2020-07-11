using System;
using System.ComponentModel;

namespace My2DGame.Core.Property {
	public interface IProperty<T> : INotifyPropertyChanged, ICloneable {
		T Value { get; set; }
	}
}