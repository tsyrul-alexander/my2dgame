using System;
using System.ComponentModel;

namespace My2DGame.Network.Tracker
{
	public interface ITracker<T> where T : INotifyPropertyChanged {
		event Action<T, string, object> PropertyChanged;
		T Value { get; }
		void Initialize();
		void UpdateProperty(PropertyValue propertyValue);
	}
}
