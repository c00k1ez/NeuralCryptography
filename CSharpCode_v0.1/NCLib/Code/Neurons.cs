using System;
using System.Collections.Generic;
using System.Threading;

namespace NCLib
{
	public class HiddenNeuron
	{

		/// <summary>
		/// количество входных неиронов
		/// </summary>
		private int CountOfInputs;
		/// <summary>
		/// список с весами к каждому входу
		/// </summary>
		private List<int> Weights = new List<int> ();
		/// <summary>
		/// вектор с входными данными
		/// </summary>
		private List<int> InputVector = new List<int> ();
		/// <summary>
		/// диапозон для генерации весов
		/// </summary>
		private int Distance;

		/// <summary>
		/// Взвешенная сумма, привязанная к нейрону
		/// </summary>
		private int WeightedTotal;

		/// <summary>
		/// Конструктор класса <see cref="NeiroNetTest.HiddenNeiron"/>. Принимает два агрумента
		/// </summary>
		/// <param name="countOfInputs">Кол-во входных нейронов</param>
		/// <param name="distance">Диапозон для генерации весов</param>
		public HiddenNeuron (int CountOfInputs, int Distance)
		{

			this.CountOfInputs = CountOfInputs;
			this.Distance = Distance;
			Init ();

		}

		public void GetWeightedTotal (int WeightedTotal)
		{

			this.WeightedTotal = WeightedTotal;

		}

		public void ReturnWeights (ref List<int> Data)
		{

			HashSet<int> tmp = new HashSet<int> (Weights);
			List<int> TmpWeights = new List<int> (tmp);
			Data.AddRange (TmpWeights);
			tmp.Clear ();
			TmpWeights.Clear ();

		}

		public void GetWeights (ref List<int> Data)
		{

			Data.Clear ();
			Data.AddRange (Weights);

		}

		/// <summary>
		/// Метод для генерации весов
		/// </summary>
		public void GetRandomWeights ()
		{

			for (int i = 0; i < CountOfInputs; i++) { 
				Weights.Add (Rand.HelpToGeneration (Rand.GetThreadRandom (), Distance));
			}

		}

		/// <summary>
		/// Корректировка весов по правилу Хэбба
		/// </summary>
		/// <param name="tetaA">Выход первой TPM</param>
		/// <param name="tetaB">Выход второй TPM</param>
		public void CorrectWeights (int FirstOut, int SecondOut)
		{

			Func <int, int> g = fn => (fn > Distance) ? Distance : (fn < (-1) * Distance) ? (-1) * Distance : fn;
			Func <int, int, int> comp = (foo, bar) => (foo == bar) ? 1 : -1;
			for (int i = 0; i < Weights.Count; i++)
				Weights [i] = g (Weights [i] - InputVector [i] * comp (WeightedTotal, FirstOut) * comp (WeightedTotal, SecondOut));

		}

		/// <summary>
		/// DEBUG Метод для вывода  весов
		/// </summary>
		public void DEBUG_METHOD ()
		{

			for (int i = 0; i < Weights.Count; i++)
				Console.WriteLine ("weight {0} = {1}", i, Weights [i]);

		}

		/// <summary>
		/// Передача входного векора
		/// </summary>
		/// <param name="_inputVector">Вектор с входными данными</param>
		public void GetInput (List<int> _InputVector)
		{

			InputVector.Clear ();
			InputVector.AddRange (_InputVector); 

		}

		/// <summary>
		/// Нахождение выхода НЕЙРОНА
		/// </summary>
		public int Summary ()
		{

			Func<int, int> sgn = x => (x > 0) ? 1 : (x == 0) ? 0 : -1;
			int res = 0;
			for (int i = 0; i < CountOfInputs; i++) {
				res += InputVector [i] * Weights [i];
			}
			return sgn (res);

		}

		/// <summary>
		/// Начальная инициализация 
		/// </summary>
		private void Init ()
		{

			GetRandomWeights ();

		}



	}
}

