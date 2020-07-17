using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace My2DGame.Core.Utilities
{
	public static class ReflectionUtilities {
		public static IEnumerable<Type> GetTypesIsAssignableFrom(Type type, params Assembly[] assemblies) {
			if (assemblies.Length == 0) {
				assemblies = GetAssemblies();
			}
			return assemblies.SelectMany(assembly => assembly.GetTypes())
			.Where(type.IsAssignableFrom);
		}
		public static IEnumerable<Type> GetTypesWithAttribute<T>(params Assembly[] assemblies) where T : Attribute {
			if (assemblies.Length == 0) {
				assemblies = GetAssemblies();
			}
			return assemblies.SelectMany(assembly => assembly.GetTypes())
			.Where(type => type.GetCustomAttribute<T>() != null);
		}
		public static IEnumerable<Assembly> GetAssemblies<T>() where T : Attribute {
			return GetAssemblies().Where(assembly =>
				assembly.GetCustomAttribute<T>() != null);
		}
		public static Assembly[] GetAssemblies() {
			var currentDomain = AppDomain.CurrentDomain;
			return currentDomain.GetAssemblies();
		}
		public static T GetAttributeValue<T>(this object obj) where T : Attribute {
			var type = obj.GetType();
			return type.GetCustomAttribute<T>();
		}
	}
}
