/*using System;
using System.Collections.Generic;

namespace NeuroNetTest
{
	public class KeyGeneration
	{

		private int CountOfInputNeirons;
		private int CountOfHiddenNeirons;
		private int Range;
		private List<int> InputData = new List<int> ();
		private List<int> WeightsA = new List<int> ();
		private List<int> WeightsB = new List<int> ();

		public KeyGeneration (int CountOfInputNeirons, int CountOfHiddenNeirons, int Range)
		{

			this.CountOfHiddenNeirons = CountOfHiddenNeirons;
			this.CountOfInputNeirons = CountOfInputNeirons;
			this.Range = Range;
			Init ();

		}

		private void Init ()
		{

			//TPM FirstTPM = new TPM (CountOfHiddenNeirons, , Range);
			//TPM SecondTPM = new TPM (CountOfHiddenNeirons, /*CountOfInputNeirons, Range);
			int TetaA, TetaB; 
			int i = 0;
			while (true) {
				Rand.GenerateRandomInput (ref InputData, CountOfInputNeirons);
				//FirstTPM.GetInput (InputData);
				//SecondTPM.GetInput (InputData);
				TetaA = FirstTPM.Teta ();
				TetaB = SecondTPM.Teta ();
				FirstTPM.GetWeights (ref WeightsA);
				SecondTPM.GetWeights (ref WeightsB);
				if (OptionalMethods.IsEqual (WeightsA, WeightsB)) {
					Console.WriteLine (i);
					break;
				}
				//if (TetaA == TetaB) {
				//	FirstTPM.CorrectWeights ();
				//	SecondTPM.CorrectWeights ();
				//} else {
				//	FirstTPM.CorrectWeights (TetaB);
				//	SecondTPM.CorrectWeights (TetaA);
				//}

				//Console.WriteLine (i);
				i++;
			} 

			string foo = string.Empty, bar = string.Empty;
			FirstTPM.GetKey (ref WeightsA, ref foo);
			SecondTPM.GetKey (ref WeightsB, ref bar);
			for (int j = 0; j < WeightsA.Count; j++)
				Console.WriteLine ("Key 1 = " + WeightsA [j]);
			Console.WriteLine ("Hash = " + foo + '\n' + "___________");

			for (int j = 0; j < WeightsA.Count; j++)
				Console.WriteLine ("Key 2 = " + WeightsA [j]);
			Console.WriteLine ("Hash = " + foo + '\n' + "___________");

		}

		public void ReturnKey (ref List<int> Key)
		{

			Key.Clear ();
			HashSet<int> HashSetForTPM = new HashSet<int> (WeightsA);
			Key.AddRange (HashSetForTPM);

		}
	}
}*/
