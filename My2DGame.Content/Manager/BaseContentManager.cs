using System;
using System.Collections.Generic;
using System.Linq;
using My2DGame.Core;
using My2DGame.Network.Client.Manager;
using Newtonsoft.Json.Linq;

namespace My2DGame.Content.Manager {
	public abstract class BaseContentManager<T> : IContentManager<T> where T : ISilentPropertyChanged {
		public abstract T Load(string content);
		public abstract string Save(T item);
		protected virtual JArray ToJArray<TContentManager>(IContentManager<TContentManager> contentManager, IEnumerable<TContentManager> items) {
			return new JArray(items.Select(item => JObject.Parse(contentManager.Save(item))).Cast<object>().ToArray());
		}

		protected virtual IEnumerable<TContentManager> FromJArray<TContentManager>(IContentManager<TContentManager> contentManager, JArray jArray) {
			return jArray.Select(token => contentManager.Load(token.ToString()));
		}
		protected virtual Guid? GetNetworkId(ITrackedManager<T> trackedManager, T item) {
			if (trackedManager.GetIfExistsItem(item, out var networkId)) {
				return networkId;
			}
			return null;
		}
	}
}