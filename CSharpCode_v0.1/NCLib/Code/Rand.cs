using System;
using System.Threading;
using System.Collections.Generic;

namespace NCLib
{

	//Адовая магия. Даже не спрашивайте, почему оно работает

	class Rand : Random
	{

		private static int seed = Environment.TickCount; //сид для генерации случайных чисел
		private static ThreadLocal<Random> randomWrapper = new ThreadLocal<Random> 
			(() => new Random (Interlocked.Increment (ref seed))); 

		public static Random GetThreadRandom()
		{
			return randomWrapper.Value;
		} 
		//
		public static int HelpToGeneration (Random rnd, int L)
		{

			return rnd.Next ((-1)*L, L + 1);

		}

		public static int HelpToGeneration (Random rnd)
		{

			return rnd.Next (-1, 2);

		}

		/// <summary>
		/// Генерация случайного входного вектора
		/// </summary>
		/// <param name="Data">Ссылка на вектор</param>
		/// <param name="CountOfInput">Кол-во значений в векторе (кол-во входных нейронов)</param>
		public static void GenerateRandomInput (ref List<int> Data, int CountOfInput)
		{

			Data.Clear ();
			for (int i = 0; i < CountOfInput; i++)
				Data.Add (Rand.HelpToGeneration (Rand.GetThreadRandom ()));

		}


	}
}

