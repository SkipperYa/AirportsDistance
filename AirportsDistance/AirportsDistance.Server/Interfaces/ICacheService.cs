namespace AirportsDistance.Server.Interfaces
{
	public interface ICacheService<T>
	{
		public T Get(string key);
		public void Set(string key, T entity);
	}
}
