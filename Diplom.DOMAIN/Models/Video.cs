﻿namespace BD_test.Models
{
	public class Video
	{
		public int Id { get; set; }

		public int PersonId { get; set; }
		public Person Person { get; set; } = null!;
	}
}