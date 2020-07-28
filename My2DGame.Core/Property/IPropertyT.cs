namespace My2DGame.Core.Property {
	public interface IProperty<T> : IProperty {
		T Value { get; set; }
	}
}