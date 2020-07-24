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

		protected virtual JObject GetContentManagerItemJObject<TContentManager>(IContentManager<TContentManager> contentManager, TContentManager item) {
			return JObject.Parse(contentManager.Save(item));
		}
		protected virtual JArray ToJArray<TContentManager>(IContentManager<TContentManager> contentManager, IEnumerable<TContentManager> items) {
			return new JArray(items.Select(item => GetContentManagerItemJObject(contentManager, item)).Cast<object>().ToArray());
		}
		protected virtual IEnumerable<TContentManager> FromJArray<TContentManager>(IContentManager<TContentManager> contentManager, JArray jArray) {
			return jArray.Select(token => contentManager.Load(token.ToString()));
		}
		protected virtual Guid? GetNetworkId(ITrackedManager<T> trackedManager, T item) {
			if (trackedManager.TryGetItem(item, out var networkId)) {
				return networkId;
			}
			return null;
		}
		protected virtual JArray DictionaryToJArray<TKey, TValue>(IDictionary<TKey, TValue> dictionary) {
			return new JArray(dictionary.Select(pair => new JObject {
				new JProperty("key", pair.Key),
				new JProperty("value", pair.Value)
			}));
		}
		protected virtual Dictionary<TKey, TValue> JArrayToDictionary<TKey, TValue>(JArray jArray) {
			var dictionary = new Dictionary<TKey, TValue>();
			foreach (var item in jArray) {
				var jObject = (JObject)item;
				dictionary.Add(jObject.Value<TKey>("key"), jObject.Value<TValue>("value"));
			}
			return dictionary;
		}
	}
}