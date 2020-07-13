using System;
using My2DGame.Core;
using My2DGame.Network.Client.Contract;

namespace My2DGame.Network.Client.Tracker
{
	public interface ITracker<T> where T : ISilentPropertyChanged {
		Guid Id { get; set; }
		event Action<ITracker<T>, PropertyValue> PropertyChanged;
		T Value { get; }
		void Initialize();
		void UpdateProperty(PropertyValue propertyValue);
	}
}
