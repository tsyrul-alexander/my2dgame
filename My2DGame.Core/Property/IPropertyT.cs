using System;
using System.ComponentModel;

namespace My2DGame.Core.Property {
	public interface IProperty<T> : IProperty {
		T Value { get; }
		void SetValue(T value);
	}
}