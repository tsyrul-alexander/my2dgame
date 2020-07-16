using System;
using System.Collections.Generic;
using System.Text;

namespace My2DGame.Content.Manager {
	public abstract class BaseContentManager<T> : IContentManager<T> {
		public abstract T Load(string content);
		public abstract string Save(T item);
	}
}