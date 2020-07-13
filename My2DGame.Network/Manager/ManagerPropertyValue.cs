using System;
using System.Collections.Generic;
using System.Text;
using My2DGame.Network.Tracker;

namespace My2DGame.Network.Manager {
	public class ManagerPropertyValue {
		public Guid Id { get; set; }
		public string ManagerName { get; set; }
		public PropertyValue Value { get; set; }
	}
}
