using System.Collections.Generic;
using System.ComponentModel;

namespace My2DGame.Core.Component.GameObject {
	public interface IGameObjectComponentCollection : IEnumerable<IGameObjectComponent>,
		INotifyPropertyChanged {
		void Add(IGameObjectComponent component);
	}
}