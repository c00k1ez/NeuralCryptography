using System;
using System.Collections.Generic;

namespace NCLib
{

	/// <summary>
	/// Выступает в роли "клиента", т.е генерирует все начальные данные
	/// </summary>
	public class Alice
	{

		private int CountOfInputNeirons;
		private int CountOfHiddenNeirons;
		private int Range;
		private List<int> InputVector = new List<int> ();
		private List< List<int> > AllInputData = new List< List<int> > ();
		private TPM AliceTPM = new TPM ();
		private List<int> Key = new List<int> ();
		private string AliceHash = string.Empty;
		private string BobHash = string.Empty;
		private int AliceTeta;
		private int BobTeta;
		private int CountOfInputsInOneNeuron;

		private bool IsDebugOn = false;

		public Alice (int CountOfInputsInOneNeuron, int CountOfHiddenNeirons, int Range)
		{

			this.CountOfInputNeirons = CountOfHiddenNeirons * CountOfInputsInOneNeuron;
			this.CountOfInputsInOneNeuron = CountOfInputsInOneNeuron;
			this.CountOfHiddenNeirons = CountOfHiddenNeirons;
			this.Range = Range;
			AliceTPM.GetInitData (this.CountOfHiddenNeirons, CountOfInputsInOneNeuron, this.Range);

		}

		public void GenerateInputVector ( ref List< List<int> > _InputVector)
		{

			_InputVector.Clear ();
			InputVector.Clear ();
			AllInputData.Clear ();
			for (int i = 0; i < CountOfHiddenNeirons; i++) {
				Rand.GenerateRandomInput (ref InputVector, CountOfInputsInOneNeuron); //!!!!!!!!!
				_InputVector.Add (InputVector);
				AllInputData.Add (InputVector);
			}

		}

		public void AliceOutput ()
		{

			AliceTPM.GetInput (AllInputData);
			//Console.WriteLine (AllInputData.Count);
			AliceTeta = AliceTPM.Teta ();
			GetKeyFromTPM ();

		}

		public void OutputData ()
		{

			Console.WriteLine ("Alice hash = " + AliceHash);
			for (int i = 0; i < Key.Count; i++)
				Console.WriteLine ("Alice key = " + Key [i]);

		}

		public void GetPackage (ref Package Pack)
		{

			BobTeta = Pack.Output;
			BobHash = Pack.MDKey;

		}

		public void CorrectAliceWeights (ref bool IsHasesEqual)
		{

			if (AliceHash == BobHash) {
				IsHasesEqual = true;
				if (IsDebugOn) {
					Console.WriteLine ("Hash Alice = " + AliceHash);
					for (int i = 0; i < Key.Count; i++)
						Console.WriteLine ("Alice key = " + Key [i]); 
				}
				return;
			} else {
				IsHasesEqual = false;
				if (AliceTeta == BobTeta)
					//	AliceTPM.CorrectWeights ();
					//else
					AliceTPM.CorrectWeights (BobTeta);
			}

		}

		private void GetKeyFromTPM ()
		{

			AliceTPM.GetKey (ref Key, ref AliceHash);

		}

		public void SetPackage (ref Package Pack)
		{

			Package Tmp = new Package (AliceHash, AliceTeta);
			Pack = Tmp;

		}



	}
}

