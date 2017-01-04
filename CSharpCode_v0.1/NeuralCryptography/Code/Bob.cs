using System;
using System.Collections.Generic;

namespace NeuroNetTest
{
	public class Bob
	{

		private int CountOfInputNeirons;
		private int CountOfHiddenNeirons;
		private int Range;
		private List< List<int> > InputVector = new List< List<int> > ();
		private TPM BobTPM = new TPM ();
		private List<int> Key = new List<int> ();
		private string AliceHash = string.Empty;
		private string BobHash = string.Empty;
		private int AliceTeta;
		private int BobTeta;

		private bool IsDebugOn = false;

		public Bob (int CountOfInputsInOneNeuron, int CountOfHiddenNeirons, int Range)
		{

			this.CountOfInputNeirons = CountOfHiddenNeirons * CountOfInputsInOneNeuron;
			this.CountOfHiddenNeirons = CountOfHiddenNeirons;
			this.Range = Range;
			BobTPM.GetInitData (this.CountOfHiddenNeirons, CountOfInputsInOneNeuron, this.Range);

		}

		public void GetInputVector (List< List<int> > _InputVector)
		{

			InputVector.Clear ();
			InputVector.AddRange (_InputVector);

		}

		public void BobOutput ()
		{

			BobTPM.GetInput (InputVector);
			BobTeta = BobTPM.Teta ();
			GetKeyFromTPM ();

		}

		public void OutputData ()
		{

			Console.WriteLine ("Bob hash = " + BobHash);
			for (int i = 0; i < Key.Count; i++)
				Console.WriteLine ("Bob key = " + Key [i]);

		}

		public void GetPackage (ref Package Pack)
		{

			AliceTeta = Pack.Output;
			AliceHash = Pack.MDKey;

		}

		public void CorrectBobWeights (ref bool IsHasesEqual)
		{

			if (AliceHash == BobHash) {
				IsHasesEqual = true;
				if (IsDebugOn) {
					Console.WriteLine ("Bob hash = " + AliceHash);
					for (int i = 0; i < Key.Count; i++)
						Console.WriteLine ("Bob key = " + Key [i]); 
				}
				return;
			} else {
				IsHasesEqual = false;
				if (AliceTeta == BobTeta)
					//	BobTPM.CorrectWeights ();
					//else
					BobTPM.CorrectWeights (AliceTeta);
			}

		}

		private void GetKeyFromTPM ()
		{

			BobTPM.GetKey (ref Key, ref BobHash);

		}

		public void SetPackage (ref Package Pack)
		{

			Package Tmp = new Package (BobHash, BobTeta);
			Pack = Tmp;

		}

	}
}

