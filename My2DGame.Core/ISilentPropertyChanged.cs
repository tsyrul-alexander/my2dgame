namespace My2DGame.Core {
	public interface ISilentPropertyChanged {
		event SilentPropertyChangedEventHandler PropertyChanged;
		void SetSilentValue(string propertyName, object value);
	}
}
