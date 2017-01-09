using System;
using System.Collections.Generic;
using System.Threading;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace NCLib
{
	public class TPM
	{

		private int CountOfHiddenNeirons;
		private int CountOfInputNeirons;
		private int Distance;
		private List<HiddenNeuron> HiddenNeirons = new List<HiddenNeuron> ();
		private List< List<int> > Input = new List< List<int> > ();
		private int ThisTeta;
		private int CountOfInputsInOneNeuron;
		protected List<int> Key = new List<int> ();
		protected HashSet<int> _Key = new HashSet<int> ();

		public TPM (int CountOfHiddenNeirons, int CountOfInputsInOneNeuron, int Distance)
		{

			this.CountOfHiddenNeirons = CountOfHiddenNeirons;
			this.CountOfInputNeirons = CountOfHiddenNeirons * CountOfInputsInOneNeuron;
			this.CountOfInputsInOneNeuron = CountOfInputsInOneNeuron;
			this.Distance = Distance;
			Init ();


		}

		public TPM ()
		{

			CountOfHiddenNeirons = 0;
			CountOfInputNeirons = 0;
			Distance = 0;

		}

		public void GetInitData (int CountOfHiddenNeirons, int CountOfInputsInOneNeuron, int Distance)
		{

			this.CountOfHiddenNeirons = CountOfHiddenNeirons;
			this.CountOfInputsInOneNeuron = CountOfInputsInOneNeuron;
			this.CountOfInputNeirons = CountOfHiddenNeirons * CountOfInputsInOneNeuron;
			this.Distance = Distance;
			Init ();

		}

		private void Init ()
		{

			for (int i = 0; i < CountOfHiddenNeirons; i++) {
				HiddenNeirons.Add (new HiddenNeuron (CountOfInputsInOneNeuron, Distance));
			}

		}

		public int Teta ()
		{

			int tmp = 1;
			int summs;
			for (int i = 0; i < CountOfHiddenNeirons; i++) {
				HiddenNeirons [i].GetInput (Input [i]);
				summs = HiddenNeirons [i].Summary ();
				HiddenNeirons [i].GetWeightedTotal (summs);
				tmp *= summs;
				//Console.WriteLine ("hidden neiron {0} - {1}", i, summs);
			}
			summs = 0;
			ThisTeta = tmp;
			return tmp;

		}

		public void GetWeights (ref List<int> Weights)
		{

			Weights.Clear ();
			//Key.Clear ();

			for (int i = 0; i < CountOfHiddenNeirons; i++) {
				HiddenNeirons [i].ReturnWeights (ref Weights);
				//_Key.Add (Weights [i]);
			}

			Key.AddRange (_Key);
			//_Key.Clear ();

		}

		public void GetInput (List< List<int> > Data)
		{

			Input.Clear ();
			Input.AddRange (Data);
			//Console.WriteLine (Data.Count);

		}

		public void DEBUG ()
		{

			for (int i = 0; i < HiddenNeirons.Count; i++) {
				Console.WriteLine ("Neiron {0}", i);
				HiddenNeirons [i].DEBUG_METHOD ();
				Console.WriteLine ("______________");
			}

		}

		private void SetKey ()
		{

			_Key.Clear ();
			Key.Clear ();
			List<int> foo = new List<int> ();
			for (int i = 0; i < HiddenNeirons.Count; i++) {
				HiddenNeirons [i].GetWeights (ref foo);
				for (int j = 0; j < foo.Count; j++) {
					_Key.Add (foo [j]);
				}
				foo.Clear ();
			}
			Key.AddRange (_Key);

		}

		private string SetHash ()
		{

			MD5 Hash = MD5.Create ();
			int[] Arr = Key.ToArray ();
			byte[] Bytes = Arr.SelectMany(BitConverter.GetBytes).ToArray(); 
			Bytes = Hash.ComputeHash (Bytes);
			StringBuilder sBuilder = new StringBuilder();
			for (int i = 0; i < Bytes.Length; i++) {
				sBuilder.Append(Bytes [i].ToString("x2"));
			}
			return sBuilder.ToString ();

		}

		public void GetKey (ref List<int> __Key, ref string HashOfKey)
		{

			SetKey ();
			__Key.Clear ();
			__Key.AddRange (Key);
			HashOfKey = SetHash ();

		}

		public void CorrectWeights (int OtherTeta)
		{

			for (int i = 0; i < CountOfHiddenNeirons; i++) {
				HiddenNeirons [i].CorrectWeights (ThisTeta, OtherTeta);
			}

		}
	}

}