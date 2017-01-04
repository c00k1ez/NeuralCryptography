using System;

namespace NeuroNetTest
{
	public struct Package
	{

		public readonly string MDKey;
		public readonly int Output;

		public Package (string Hash, int Output)
		{

			this.MDKey = Hash;
			this.Output = Output;

		}



	}

}
