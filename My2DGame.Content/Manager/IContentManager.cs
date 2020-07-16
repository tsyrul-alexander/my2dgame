namespace My2DGame.Content.Manager
{
	public interface IContentManager<T> {
		T Load(string content);
		string Save(T item);
	}
}
