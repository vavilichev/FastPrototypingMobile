using System;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using System.Security.Cryptography;

namespace VavilichevGD.Tools {
	
	public enum NumberOrder {
		Singles = 0,
		Thousands = 1,
		Millions = 2,
		Billions = 3,
		Trillions = 4,
		AA = 5,
		BB = 6,
		CC = 7,
		DD = 8,
		EE = 9,
		FF = 10,
		GG = 11,
		HH = 12,
		II = 13,
		JJ = 14,
		KK = 15,
		LL = 16,
		MM = 17,
		NN = 18,
		OO = 19,
		PP = 20,
		QQ = 21,
		RR = 22,
		SS = 23,
		TT = 24,
		UU = 25,
		VV = 26,
		WW = 27,
		XX = 28,
		YY = 29,
		ZZ = 30
	}
	
	[Serializable]
	public class BigNumber {

		[SerializeField] private float value;
		[SerializeField] private NumberOrder order;

		private BigInteger bigIntegerValue;
		private bool isInitialized { get; set; }

		
		public static BigNumber GetMax() {
			int countOfOrders = Enum.GetNames(typeof(NumberOrder)).Length;
			string finalValueString = "";
			for (int i = 0; i < countOfOrders; i++)
				finalValueString = $"{finalValueString}999";

			BigInteger result = BigInteger.Parse(finalValueString);
			return new BigNumber(result);
		}
		

		public BigNumber(BigInteger bigInteger) {
			bigIntegerValue = bigInteger;
			isInitialized = true;
		}

		public BigNumber(int value) {
			bigIntegerValue = value;
			isInitialized = true;
		}

		public BigNumber(string strValue) {
			bigIntegerValue = BigInteger.Parse(strValue);
			isInitialized = true;
		}

		
		
		private void InitializeIfNeed() {
			if (!isInitialized) {
				int intValue = Mathf.FloorToInt(value);
				int decValue = Mathf.RoundToInt((value - intValue) * 1000);
				int addZeroBlockCount = decValue == 0 ? (int) order : (int) order - 1;
				string strValue = intValue.ToString();
				if (decValue > 0)
					strValue = $"{strValue}{decValue}";

				if ((int) order >= (int) NumberOrder.Thousands) {
					for (int i = 0; i < addZeroBlockCount; i++)
						strValue = $"{strValue}000";
				}

				bigIntegerValue = BigInteger.Parse(strValue);
				Debug.Log($"BigInteger: {bigIntegerValue}");

				isInitialized = true;
			}
		}


		#region Calculations

		
		#region BigNumber && BigNumber


		public static BigNumber operator +(BigNumber num1, BigNumber num2) {
			num1.InitializeIfNeed();
			num2.InitializeIfNeed();
			BigInteger bigSum = num1.bigIntegerValue + num2.bigIntegerValue;
			return Clamp(new BigNumber(bigSum));
		}

		public static BigNumber operator -(BigNumber num1, BigNumber num2) {
			num1.InitializeIfNeed();
			num2.InitializeIfNeed();

			BigInteger result = num1.bigIntegerValue - num2.bigIntegerValue;
			return Clamp(new BigNumber(result));
		}

		public static BigNumber operator /(BigNumber dividedNumb, BigNumber divider) {
			dividedNumb.InitializeIfNeed();
			divider.InitializeIfNeed();
			BigInteger result = dividedNumb.bigIntegerValue / divider.bigIntegerValue;
			return Clamp(new BigNumber(result));
		}

		public static BigNumber operator *(BigNumber num1, BigNumber num2) {
			num1.InitializeIfNeed();
			num2.InitializeIfNeed();
			BigInteger result = num1.bigIntegerValue * num2.bigIntegerValue;
			return Clamp(new BigNumber(result));
		}

		
		#endregion


		#region BigNumber && integer


		public static BigNumber operator +(BigNumber num, int value) {
			num.InitializeIfNeed();
			BigInteger result = num.bigIntegerValue + value;
			return Clamp(new BigNumber(result));
		}
		
		public static BigNumber operator -(BigNumber num, int value) {
			num.InitializeIfNeed();
			BigInteger result = num.bigIntegerValue - value;
			if (result < 0)
				return new BigNumber(0);
			return Clamp(new BigNumber(result));
		}

		public static BigNumber operator *(BigNumber num1, int value) {
			num1.InitializeIfNeed();
			BigInteger result = num1.bigIntegerValue * value;
			return Clamp(new BigNumber(result));
		}
		
		public static BigNumber operator /(BigNumber dividedNumb, int value) {
			dividedNumb.InitializeIfNeed();
			BigInteger result = dividedNumb.bigIntegerValue / value;
			return Clamp(new BigNumber(result));
		}
		
		
		#endregion
		

		#region BigNumber && float


		public static BigNumber operator *(BigNumber num, float mul) {
			if (mul < 0)
				throw new Exception(string.Format("Multiplicator cannot be negative: {0}", mul));

			num.InitializeIfNeed();

			if (num.bigIntegerValue < 100) {
				int intValue = (int) num.bigIntegerValue;
				int result = Mathf.CeilToInt((intValue * mul));
				BigInteger bigIntResult = new BigInteger(result);
				return new BigNumber(bigIntResult);
			}
			else {
				float roundedMul = (float) Math.Round(mul, 2);
				int mul100 = Mathf.RoundToInt(roundedMul * 100);
				BigInteger bitIntResult = (num.bigIntegerValue * mul100) / 100;
				return new BigNumber(bitIntResult);
			}
		}

		public static BigNumber operator /(BigNumber num, float div) {
			num.InitializeIfNeed();
			int div100 = Mathf.RoundToInt((float) Math.Round(div, 2) * 100);
			BigInteger num100 = num.bigIntegerValue * 100;
			BigInteger result = num100 / div100;
			return new BigNumber(result);
		}
		

		#endregion


		private static BigNumber Clamp(BigNumber clampingValue) {
			if (clampingValue.bigIntegerValue < 0)
				return new BigNumber(0);


			int countOfOrders = Enum.GetNames(typeof(NumberOrder)).Length;
			int maxValueLength = countOfOrders * 3;		// Every order contains 3 digits.
			int clampingValueLength = clampingValue.ToStringFull().Length;

			if (clampingValueLength > maxValueLength)
				return GetMax();

			return clampingValue;
		}

		public static double DivideToDouble(BigNumber dividedNumb, BigNumber divider) {
			return Math.Exp(BigInteger.Log(dividedNumb.bigIntegerValue) - BigInteger.Log(divider.bigIntegerValue));
		}

		#endregion



		#region Compairs


		public static bool operator <=(BigNumber num1, BigNumber num2) {
			num1.InitializeIfNeed();
			num2.InitializeIfNeed();
			return num1.bigIntegerValue <= num2.bigIntegerValue;
		}

		public static bool operator >=(BigNumber num1, BigNumber num2) {
			num1.InitializeIfNeed();
			num2.InitializeIfNeed();
			return num1.bigIntegerValue >= num2.bigIntegerValue;
		}



		public static bool operator <(BigNumber num1, BigNumber num2) {
			num1.InitializeIfNeed();
			num2.InitializeIfNeed();
			return num1.bigIntegerValue < num2.bigIntegerValue;
		}

		public static bool operator >(BigNumber num1, BigNumber num2) {
			num1.InitializeIfNeed();
			num2.InitializeIfNeed();
			return num1.bigIntegerValue > num2.bigIntegerValue;
		}



		public static bool operator >=(BigNumber num, int intValue) {
			num.InitializeIfNeed();
			return num.bigIntegerValue >= intValue;
		}

		public static bool operator <=(BigNumber num, int intValue) {
			num.InitializeIfNeed();
			return num.bigIntegerValue >= intValue;
		}



		public static bool operator <(BigNumber num, int intValue) {
			num.InitializeIfNeed();
			return num.bigIntegerValue < intValue;
		}

		public static bool operator >(BigNumber num, int intValue) {
			num.InitializeIfNeed();
			return num.bigIntegerValue > intValue;
		}



		public static bool operator ==(BigNumber num, int intValue) {
			num.InitializeIfNeed();
			return num.bigIntegerValue == intValue;
		}

		public static bool operator !=(BigNumber num, int intValue) {
			num.InitializeIfNeed();
			return num.bigIntegerValue != intValue;
		}


		#endregion


		public static BigNumber RandomRange(BigNumber num1, BigNumber num2) {
			num1.InitializeIfNeed();
			num2.InitializeIfNeed();

			var random = RandomNumberGenerator.Create();
			BigInteger randomInteger = RandomInRange(random, num1.bigIntegerValue, num2.bigIntegerValue);
			return new BigNumber(randomInteger);
		}

		private static BigInteger RandomInRange(RandomNumberGenerator rng, BigInteger min, BigInteger max) {
			if (min > max) {
				var buff = min;
				min = max;
				max = buff;
			}

			// offset to set min = 0
			BigInteger offset = -min;
			min = 0;
			max += offset;

			var value = RandomInRangeFromZeroToPositive(rng, max) - offset;
			return value;
		}

		private static BigInteger RandomInRangeFromZeroToPositive(RandomNumberGenerator rng, BigInteger max) {
			BigInteger value;
			var bytes = max.ToByteArray();

			// count how many bits of the most significant byte are 0
			// NOTE: sign bit is always 0 because `max` must always be positive
			byte zeroBitsMask = 0b00000000;

			var mostSignificantByte = bytes[bytes.Length - 1];

			// we try to set to 0 as many bits as there are in the most significant byte, starting from the left (most significant bits first)
			// NOTE: `i` starts from 7 because the sign bit is always 0
			for (var i = 7; i >= 0; i--) {
				// we keep iterating until we find the most significant non-0 bit
				if ((mostSignificantByte & (0b1 << i)) != 0) {
					var zeroBits = 7 - i;
					zeroBitsMask = (byte) (0b11111111 >> zeroBits);
					break;
				}
			}

			do {
				rng.GetBytes(bytes);

				// set most significant bits to 0 (because `value > max` if any of these bits is 1)
				bytes[bytes.Length - 1] &= zeroBitsMask;

				value = new BigInteger(bytes);

				// `value > max` 50% of the times, in which case the fastest way to keep the distribution uniform is to try again
			} while (value > max);

			return value;
		}


		public override string ToString() {
			InitializeIfNeed();
			string stringValue = bigIntegerValue.ToString();
			string[] blocks = SplitToArrayOf3(stringValue);
			int lastIndex = blocks.Length - 1;
			if (lastIndex >= 0) {
				string finalValue = blocks[lastIndex];
				if (lastIndex > 0) {
					string valueAfterComa = blocks[lastIndex - 1][0].ToString();
					if (valueAfterComa != "0")
						finalValue += "." + valueAfterComa;
				}
				return finalValue;
			}

			return $"0 {(NumberOrder)0}";
		}

		public string ToStringShort() {
			string strValue = ToString();
			NumberOrder numberOrder = GetOrder();
			return $"{strValue}{numberOrder}";
		}

		private string[] SplitToArrayOf3(string text) {
			List<string> splittedList = new List<string>();

			for (int i = text.Length - 1; i >= 0; i -= 3) {
				string block = "";
				block = text[i] + block;
				if (i >= 1)
					block = text[i - 1] + block;
				if (i >= 2)
					block = text[i - 2] + block;
				splittedList.Add(block);
			}

			return splittedList.ToArray();
		}

		public string ToStringFull() {
			InitializeIfNeed();
			return bigIntegerValue.ToString();
		}

		public NumberOrder GetOrder() {
			InitializeIfNeed();
			string fullValueString = this.ToStringFull();
			int numberLength = fullValueString.Length;
			int blocksCount = Mathf.CeilToInt(numberLength / 3f - 1);
			return (NumberOrder) blocksCount;
		}
	}
}