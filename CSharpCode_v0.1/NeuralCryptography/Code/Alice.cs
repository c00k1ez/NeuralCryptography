using System;
using System.Collections.Generic;

namespace NeuroNetTest
{

	/// <summary>
	/// Выступает в роли "клиента", т.е генерирует все начальные данные
	/// </summary>
	public class Alice
	{

		/// <summary>
		/// Количество скрытых нейронов.
		/// </summary>
		private int CountOfHiddenNeirons;
		/// <summary>
		/// Диапозон значений ключа
		/// </summary>
		private int Range;
		/// <summary>
		/// Входной вектор. Используется для временного хранения входных значений.
		/// </summary>
		private List<int> InputVector = new List<int> ();
		/// <summary>
		/// Полные входные данные на i-том кругу обучения.
		/// </summary>
		private List< List<int> > AllInputData = new List< List<int> > ();
		/// <summary>
		/// Древовидная машина четности абонента.
		/// </summary>
		private TPM AliceTPM = new TPM ();
		/// <summary>
		/// Значения ключа.
		/// </summary>
		private List<int> Key = new List<int> ();
		/// <summary>
		/// Значение хэш-функции этого абонента.
		/// </summary>
		private string AliceHash = string.Empty;
		/// <summary>
		/// Значение хэш-функции воторого абонента.
		/// </summary>
		private string BobHash = string.Empty;
		/// <summary>
		/// Выходное значение древовидной машины четности Алисы.
		/// </summary>
		private int AliceTeta;
		/// <summary>
		/// Выходное значение древовидной машины четности Боба.
		/// </summary>
		private int BobTeta;
		/// <summary>
		/// Кол-во входных нейронов, соответствующее одному выходному.
		/// </summary>
		private int CountOfInputsInOneNeuron;
		/// <summary>
		/// Флаг проверки активации дебаг-мода.
		/// </summary>
		private bool IsDebugOn = false;

		/// <summary>
		/// Конструктор <see cref="NeuroNetTest.Alice"/> класса.
		/// </summary>
		/// <param name="CountOfInputsInOneNeuron">Кол-во входных нейронов, соответствующее одному выходному.</param>
		/// <param name="CountOfHiddenNeirons">Количество скрытых нейронов.</param>
		/// <param name="Range">Диапозон значений ключа.</param>
		public Alice (int CountOfInputsInOneNeuron, int CountOfHiddenNeirons, int Range)
		{


			this.CountOfInputsInOneNeuron = CountOfInputsInOneNeuron;
			this.CountOfHiddenNeirons = CountOfHiddenNeirons;
			this.Range = Range;
			AliceTPM.GetInitData (this.CountOfHiddenNeirons, CountOfInputsInOneNeuron, this.Range);

		}

		/// <summary>
		/// Метод для генерации обучающей выборки. 
		/// </summary>
		/// <param name="_InputVector">Матрица входной выборки.</param>
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

		/// <summary>
		/// Нахождение выходного значения древовидной машины четности на уровне Алисы.
		/// </summary>
		public void AliceOutput ()
		{

			AliceTPM.GetInput (AllInputData);
			AliceTeta = AliceTPM.Teta ();
			GetKeyFromTPM ();

		}

		/// <summary>
		/// Метод для вывода данных.
		/// </summary>
		public void OutputData ()
		{

			Console.WriteLine ("Alice hash = " + AliceHash);
			for (int i = 0; i < Key.Count; i++)
				Console.WriteLine ("Alice key = " + Key [i]);

		}

		/// <summary>
		/// Метод для приема пакета из канала.
		/// </summary>
		/// <param name="Pack">Пакет</param>
		public void GetPackage (ref Package Pack)
		{

			BobTeta = Pack.Output;
			BobHash = Pack.MDKey;

		}

		/// <summary>
		/// Метод для корректировки весов по измененному правилу Хэбба.
		/// </summary>
		/// <param name="IsHasesEqual">Параметр для подтверждения равенства хэшей.</param>
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
					AliceTPM.CorrectWeights (BobTeta);
			}

		}

		/// <summary>
		/// Получение ключа из древовидной машины четности.
		/// </summary>
		private void GetKeyFromTPM ()
		{

			AliceTPM.GetKey (ref Key, ref AliceHash);

		}

		/// <summary>
		/// Метод для отправки пакета.
		/// </summary>
		/// <param name="Pack">Пакет</param>
		public void SetPackage (ref Package Pack)
		{

			Package Tmp = new Package (AliceHash, AliceTeta);
			Pack = Tmp;

		}



	}
}

