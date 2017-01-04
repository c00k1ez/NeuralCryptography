using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace NeuroNetTest
{
	class MainClass
	{
		//private int CountOfInput = 6;
		private static int CountOfHiddenNeirons = 3;
		private static int Range = 20;
		private static int CountOfInputsInOneNeuron = 4;

		public static void Main (string[] args)
		{

			var FirstAtack = new TestEveAtack (CountOfInputsInOneNeuron, CountOfHiddenNeirons, Range, true);


		}
	}
}