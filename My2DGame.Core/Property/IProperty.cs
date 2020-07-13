using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace My2DGame.Core.Property {
	public interface IProperty : INotifyPropertyChanged, ICloneable {
		object GetValue();
	}
}
