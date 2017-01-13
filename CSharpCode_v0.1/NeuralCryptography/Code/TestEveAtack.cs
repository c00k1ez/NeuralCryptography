using System;
using System.Collections.Generic;

namespace NeuroNetTest
{
	public class TestEveAtack
	{

		//private int CountOfInput;
		private int CountOfHiddenNeirons;
		private int Range;
		private bool IsDebugOn;
		private int CountOfInputsInOneNeuron;
		private bool IsAttackSuccess;

		public TestEveAtack ( int CountOfInputsInOneNeuron, int CountOfHiddenNeirons, int Range, bool IsDebugOn, ref bool IsAttackSuccess)
		{

			this.CountOfInputsInOneNeuron = CountOfInputsInOneNeuron;
			this.CountOfHiddenNeirons = CountOfHiddenNeirons;
			this.Range = Range;
			this.IsDebugOn = IsDebugOn;
			Start ();
			IsAttackSuccess = this.IsAttackSuccess;

		}

		public TestEveAtack ()
		{

			this.CountOfInputsInOneNeuron = 0;
			this.CountOfHiddenNeirons = 0;
			this.Range = 0;
			this.IsDebugOn = true;

		}

		public void GetInitData (int CountOfInputsInOneNeuron, int CountOfHiddenNeirons, int Range, bool IsDebugOn, ref bool IsAttackSuccess)
		{

			this.CountOfInputsInOneNeuron = CountOfInputsInOneNeuron;
			this.CountOfHiddenNeirons = CountOfHiddenNeirons;
			this.Range = Range;
			this.IsDebugOn = IsDebugOn;
			Start ();
			IsAttackSuccess = this.IsAttackSuccess;

		}

		private void Start ()
		{

			Alice alice = new Alice (CountOfInputsInOneNeuron, CountOfHiddenNeirons, Range);
			Bob bob = new Bob (CountOfInputsInOneNeuron, CountOfHiddenNeirons, Range);
			Eve eve = new Eve (CountOfInputsInOneNeuron, CountOfHiddenNeirons, Range);
			Package AlicePack = new Package ();
			Package BobPack = new Package ();
			int i = 0;
			bool AliceCheck = false;
			bool BobCheck = false;
			bool EveCheck = false;
			Package foo = new Package ();
			List< List<int> > InputVector = new List< List<int> > ();
			while (true) {
				alice.GenerateInputVector (ref InputVector);

				bob.GetInputVector (InputVector);
				eve.GetInputVector (InputVector);//!!!

				alice.AliceOutput ();

				bob.BobOutput ();
				eve.EveOutput ();//!!!

				alice.SetPackage (ref AlicePack);

				bob.GetPackage (ref AlicePack);
				eve.GetPackage (ref AlicePack);//!!!

				bob.CorrectBobWeights (ref BobCheck);
				eve.CorrectBobWeights (ref EveCheck);//!!!

				bob.SetPackage (ref BobPack);
				eve.SetPackage (ref foo);
				//eve.GetPackage (ref BobPack);
				alice.GetPackage (ref BobPack);
				alice.CorrectAliceWeights (ref AliceCheck);
				//eve.CorrectBobWeights (ref EveCheck);//!!
				if ((AliceCheck == true) || (BobCheck == true)) {
					//alice.OutputData ();
					//bob.OutputData ();
					//eve.OutputData ();
					//Console.WriteLine (i);
					IsAttackSuccess = EveCheck;
					break;
				}
				if (IsDebugOn) {
					alice.OutputData ();
					bob.OutputData ();
					eve.OutputData ();
					Console.WriteLine (i);
				}
				i++;
			}

		}

	}
}

