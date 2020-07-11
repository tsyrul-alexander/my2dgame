using System.Collections.ObjectModel;
using System.Linq;

namespace My2DGame.Core.Component.GameObject {
	public class GameObjectComponentCollection : ObservableCollection<IGameObjectComponent> {
		public T Get<T>() {
			return (T) this.FirstOrDefault(component => component is T);
		}
	}
}