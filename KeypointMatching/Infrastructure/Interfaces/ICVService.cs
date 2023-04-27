using Emgu.CV;

namespace KeypointMatching.Infrastructure.Interfaces
{
	public interface ICVService
	{
		public Task<int> KeypointMatching(Mat photo);
	}
}
