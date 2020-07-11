using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace My2DGame.Core.Component.GameObject {
	public class GameObjectComponentCollection : ObservableCollection<IGameObjectComponent>,
			IGameObjectComponentCollection {
		public T Get<T>() where T : IGameObjectComponent {
			return (T) this.Items.FirstOrDefault(component => component is T);
		}
	}
}