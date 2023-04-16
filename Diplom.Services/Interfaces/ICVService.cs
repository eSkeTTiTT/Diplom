using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.Services.Interfaces
{
	public interface ICVService
	{
		public Task KeypointMatching(object scene);
	}
}
