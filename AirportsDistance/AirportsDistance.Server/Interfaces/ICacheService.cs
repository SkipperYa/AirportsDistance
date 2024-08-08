namespace AirportsDistance.Server.Interfaces
{
	public interface ICacheService<T>
		where T : class
	{
		public T Get(string key);
		public void Set(string key, T entity);
	}
}
