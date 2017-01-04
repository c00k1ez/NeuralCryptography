using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace NeuroNetTest
{
	class MainClass
	{
		//private int CountOfInput = 6;
		private static int CountOfHiddenNeirons = 15;
		private static int Range = 50;
		private static int CountOfInputsInOneNeuron = 10;

		public static void Main (string[] args)
		{

			var FirstAtack = new TestEveAtack (CountOfInputsInOneNeuron, CountOfHiddenNeirons, Range, true);


		}
	}
}