using System;
using System.Collections.Generic;
using System.Linq;

namespace NeuroNetTest
{
	public class OptionalMethods
	{

		public static bool IsEqual (List<int> FirstData, List<int> SecondData)
		{

			//HashSet<int> tmp = new HashSet<int> (FirstData);
			return FirstData.SequenceEqual (SecondData);

		}

	}
}

