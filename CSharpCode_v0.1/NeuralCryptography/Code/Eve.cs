using System;
using System.Collections.Generic;

namespace NeuroNetTest
{
	public class Eve
	{

		private int CountOfInputNeirons;
		private int CountOfHiddenNeirons;
		private int Range;
		private List< List<int> > InputVector = new List< List<int> > ();
		private TPM EveTPM = new TPM ();
		private List<int> Key = new List<int> ();
		private string EveHash = string.Empty;
		private string OtherHash = string.Empty;
		private int OtherTeta;
		private int EveTeta;


		private bool IsDebugOn = false;

		public Eve (int CountOfInputsInOneNeuron, int CountOfHiddenNeirons, int Range)
		{

			this.CountOfInputNeirons = CountOfHiddenNeirons * CountOfInputsInOneNeuron;
			this.CountOfHiddenNeirons = CountOfHiddenNeirons;
			this.Range = Range;
			EveTPM.GetInitData (this.CountOfHiddenNeirons, CountOfInputsInOneNeuron, this.Range);

		}

		public void GetInputVector (List< List<int> > _InputVector)
		{

			InputVector.Clear ();
			InputVector.AddRange (_InputVector);

		}

		public void EveOutput ()
		{

			EveTPM.GetInput (InputVector);
			EveTeta = EveTPM.Teta ();
			GetKeyFromTPM ();

		}

		public void GetPackage (ref Package Pack)
		{

			OtherTeta = Pack.Output;
			OtherHash = Pack.MDKey;

		}

		public void OutputData ()
		{

			Console.WriteLine ("Eve hash = " + EveHash);
			for (int i = 0; i < Key.Count; i++)
				Console.WriteLine ("Eve key = " + Key [i]);

		}

		public void CorrectBobWeights (ref bool IsHasesEqual)
		{

			if (OtherHash == EveHash) {
				IsHasesEqual = true;
				if (IsDebugOn) {
					Console.WriteLine ("Eve hash = " + EveHash);
					for (int i = 0; i < Key.Count; i++)
						Console.WriteLine ("Eve key = " + Key [i]); 
				}
				return;
			} else {
				IsHasesEqual = false;
				if (OtherTeta == EveTeta)
					//	EveTPM.CorrectWeights ();
					//else
					EveTPM.CorrectWeights (OtherTeta);
			}

		}

		private void GetKeyFromTPM ()
		{

			EveTPM.GetKey (ref Key, ref EveHash);

		}

		public void SetPackage (ref Package Pack)
		{

			Package Tmp = new Package (EveHash, EveTeta);
			Pack = Tmp;

		}

	}
}

