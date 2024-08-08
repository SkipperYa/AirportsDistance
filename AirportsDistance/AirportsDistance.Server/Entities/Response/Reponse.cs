namespace AirportsDistance.Server.Entities.Response
{
	public class Response
	{
		public bool Success { get; set; }
		public string Error { get; set; }
		public object Data { get; set; }

		private Response()
		{

		}

		public Response(object data)
		{
			Data = data;
			Success = true;
		}

		public Response(string error)
		{
			Error = error;
			Success = false;
		}
	}
}
