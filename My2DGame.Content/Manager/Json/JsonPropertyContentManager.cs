using System;
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
			var propertyJObj = new JObject {
				new JProperty("type", GetPropertyType(item)),
				new JProperty("value", item.GetValue())
			};
			return propertyJObj.ToString();
		}
		protected virtual string GetPropertyType(IProperty item) {
			switch (item) {
				case IProperty<string> _:
					return nameof(String);
				case IProperty<int> _:
					return nameof(Int32);
				case IProperty<double> _:
					return nameof(Double);
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