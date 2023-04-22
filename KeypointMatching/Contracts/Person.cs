using Emgu.CV;

namespace KeypointMatching.Contracts
{
	public class Person
	{
		public List<Mat> Descriptors { get; set; } = null!;
		public int ID { get; set; }

	}
}
