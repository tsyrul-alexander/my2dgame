using System;
using System.Collections;
using System.Linq;
using My2DGame.Core.Property;
using Newtonsoft.Json.Linq;

namespace My2DGame.Content.Manager.Json {
	public class JsonPropertyContentManager : BaseContentManager<IProperty> {
		public override IProperty Load(string content) {
			var propertyJObj = JObject.Parse(content);
			var type = propertyJObj.GetValue("type").Value<string>();
			var valueJToken = propertyJObj.GetValue("value");
			return GetPropertyFromType(type, valueJToken);
		}
		public override string Save(IProperty item) {
			var (typeName, propertyValue) = GetPropertyInfo(item);
			var propertyJObj = new JObject {
				new JProperty("type", typeName),
				new JProperty("value", propertyValue)
			};
			return propertyJObj.ToString();
		}
		protected virtual (string typeName, JToken propertyValue) GetPropertyInfo(IProperty item) {
			switch (item) {
				case IProperty<string> stringProperty:
					return (nameof(String), new JValue(stringProperty.Value));
				case IProperty<int> intProperty:
					return (nameof(Int32), new JValue(intProperty.Value));
				case IProperty<double> doubleProperty:
					return (nameof(Double), new JValue(doubleProperty.Value));
				default:
					throw new NotImplementedException(nameof(item));
			}
		}
		protected virtual IProperty GetPropertyFromType(string type, JToken jValue) {
			switch (type) {
				case nameof(String):
					return new StringProperty(jValue.Value<string>());
				case nameof(Int32):
					return new IntegerProperty(jValue.Value<int>());
				case nameof(Double):
					return new DoubleProperty(jValue.Value<double>());
				default:
					throw new NotImplementedException(type);
			}
		}
	}
}