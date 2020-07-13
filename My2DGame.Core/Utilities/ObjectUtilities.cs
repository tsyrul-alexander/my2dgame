using System;
using System.Collections.Generic;

namespace My2DGame.Core.Utilities {
	public static class ObjectUtilities {
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action) {
			foreach (var value in collection) {
				action.Invoke(value);
			}
		}
		public static object GetPropertyValue(this object obj, string propName) {
			return obj.GetType().GetProperty(propName)?.GetValue(obj, null);
		}
	}
}
