using System;
using System.Collections.Generic;

namespace My2DGame.Core.Utility {
	public static class ObjectUtilities {
		public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action) {
			foreach (var value in collection) {
				action.Invoke(value);
			}
		}
	}
}
