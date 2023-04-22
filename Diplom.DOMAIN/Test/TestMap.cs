using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.DOMAIN.Test
{
	public static class TestMap
	{
		public static Location TestLocation_1 => new()
		{
			Id = 1,
			Latitude = 55.724552,
			Longitude = 37.553738
		};

		public static Location TestLocation_2 => new()
		{
			Id = 1,
			Latitude = 55.725037,
			Longitude = 37.553534
		};
	}
}
