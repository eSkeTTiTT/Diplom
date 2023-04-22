namespace KeypointMatching.Infrastructure.Interfaces
{
	public interface ICVService
	{
		public Task<string> KeypointMatching(object scene);
	}
}
