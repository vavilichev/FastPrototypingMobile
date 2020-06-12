using System;
using UnityEngine;
using System.Numerics;
using System.Security.Cryptography;

namespace VavilichevGD.Tools.Numerics {
	[Serializable]
	public struct BigNumber {

		#region CONSTANTS

		public const string FORMAT_FULL = "FULL";
		public const string FORMAT_XXX_C = "XXX C";
		public const string FORMAT_XXXC = "XXXC";
		public const string FORMAT_XXX_XX_C = "XXX.XX C";
		public const string FORMAT_XXX_XXC = "XXX.XXC";
		public const string FORMAT_XXX_X_C = "XXX.X C";
		public const string FORMAT_XXX_XC = "XXX.XC";
		public const string FORMAT_DYNAMIC_3_C = "DYNAMIC3 C";
		public const string FORMAT_DYNAMIC_3C = "DYNAMIC3C";
		public const string FORMAT_DYNAMIC_4_C = "DYNAMIC4 C";
		public const string FORMAT_DYNAMIC_4C = "DYNAMIC4C";
		
		#endregion
		
		

		private BigInteger _bigIntegerIntValue;


		
		public static BigNumber maxValue {
			get {
				int countOfOrders = Enum.GetNames(typeof(BigNumberOrder)).Length;
				string finalValueString = "";
				for (int i = 0; i < countOfOrders; i++)
					finalValueString = $"{finalValueString}999";

				BigInteger result = BigInteger.Parse(finalValueString);
				return new BigNumber(result);
			}
		}

		public static BigNumber zero => new BigNumber(0);


		#region CONSTRUCTORS

		public BigNumber(BigInteger bigIntegerInt) {
			this._bigIntegerIntValue = bigIntegerInt;
		}

		public BigNumber(int intValue) {
			this._bigIntegerIntValue = intValue;
		}

		public BigNumber(string strValue) {
			this._bigIntegerIntValue = BigInteger.Parse(strValue);
		}

		public BigNumber(BigNumberOrder order, float cutFloat) {
			var intValue = Mathf.FloorToInt(cutFloat);
			var decValue = Mathf.RoundToInt((cutFloat - intValue) * 1000);
			var addZeroBlockCount = decValue == 0 ? (int) order : (int) order - 1;
			var strValue = intValue.ToString();

			if (decValue > 0)
				strValue = $"{strValue}{decValue}";

			if ((int) order >= 1) {
				for (int i = 0; i < addZeroBlockCount; i++)
					strValue = $"{strValue}000";
			}

			this._bigIntegerIntValue = BigInteger.Parse(strValue);
		}

		#endregion
		

		#region Calculations

		
		#region BigNumber && BigNumber


		public static BigNumber operator +(BigNumber num1, BigNumber num2) {
			BigInteger bigSum = num1._bigIntegerIntValue + num2._bigIntegerIntValue;
			return Clamp(new BigNumber(bigSum));
		}

		public static BigNumber operator -(BigNumber num1, BigNumber num2) {
			BigInteger result = num1._bigIntegerIntValue - num2._bigIntegerIntValue;
			return Clamp(new BigNumber(result));
		}

		public static BigNumber operator /(BigNumber dividedNumb, BigNumber divider) {
			BigInteger result = dividedNumb._bigIntegerIntValue / divider._bigIntegerIntValue;
			return Clamp(new BigNumber(result));
		}

		public static BigNumber operator *(BigNumber num1, BigNumber num2) {
			BigInteger result = num1._bigIntegerIntValue * num2._bigIntegerIntValue;
			return Clamp(new BigNumber(result));
		}

		
		#endregion


		#region BigNumber && integer


		public static BigNumber operator +(BigNumber num, int value) {
			BigInteger result = num._bigIntegerIntValue + value;
			return Clamp(new BigNumber(result));
		}
		
		public static BigNumber operator -(BigNumber num, int value) {
			BigInteger result = num._bigIntegerIntValue - value;
			return Clamp(new BigNumber(result));
		}

		public static BigNumber operator *(BigNumber num1, int value) {
			BigInteger result = num1._bigIntegerIntValue * value;
			return Clamp(new BigNumber(result));
		}
		
		public static BigNumber operator /(BigNumber dividedNumb, int value) {
			BigInteger result = dividedNumb._bigIntegerIntValue / value;
			return Clamp(new BigNumber(result));
		}
		
		
		#endregion
		

		#region BigNumber && float


		public static BigNumber operator *(BigNumber num, float mul) {
			if (mul < 0)
				throw new Exception(string.Format("Multiplicator cannot be negative: {0}", mul));
			
			if (num._bigIntegerIntValue < 100) {
				int intValue = (int) num._bigIntegerIntValue;
				int result = Mathf.CeilToInt((intValue * mul));
				BigInteger bigIntResult = new BigInteger(result);
				return Clamp(new BigNumber(bigIntResult));
			}

			float roundedMul = (float) Math.Round(mul, 2);
			int mul100 = Mathf.RoundToInt(roundedMul * 100);
			BigInteger bitIntResult = (num._bigIntegerIntValue * mul100) / 100;
			return Clamp(new BigNumber(bitIntResult));
		}

		public static BigNumber operator /(BigNumber num, float div) {
			int div100 = Mathf.RoundToInt((float) Math.Round(div, 2) * 100);
			BigInteger num100 = num._bigIntegerIntValue * 100;
			BigInteger result = num100 / div100;
			return Clamp(new BigNumber(result));
		}
		

		#endregion

		
		#region BigNumber && double

		public static BigNumber operator +(BigNumber num, double value) {
			BigInteger result = num._bigIntegerIntValue + new BigInteger(value);
			return Clamp(new BigNumber(result));
		}
		
		public static BigNumber operator -(BigNumber num, double value) {
			BigInteger result = num._bigIntegerIntValue - new BigInteger(value);
			return Clamp(new BigNumber(result));
		}

		public static BigNumber operator *(BigNumber num1, double value) {
			BigInteger result = num1._bigIntegerIntValue * new BigInteger(value);
			return Clamp(new BigNumber(result));
		}
		
		public static BigNumber operator /(BigNumber dividedNumb, double value) {
			BigInteger result = dividedNumb._bigIntegerIntValue / new BigInteger(value);
			return Clamp(new BigNumber(result));
		}

		#endregion
		

		private static BigNumber Clamp(BigNumber clampingValue) {
			if (clampingValue._bigIntegerIntValue < 0)
				return new BigNumber(0);


			var countOfOrders = Enum.GetNames(typeof(BigNumberOrder)).Length;
			var maxValueLength = countOfOrders * 3;		// Every order contains 3 digits.
			var clampingValueLength = clampingValue.ToString().Length;

			if (clampingValueLength > maxValueLength)
				return maxValue;

			return clampingValue;
		}

		public static BigNumber Clamp(BigNumber clampingValue, BigNumber minValue, BigNumber maxValue) {
			BigNumber min = Max(minValue, BigNumber.zero);
			if (clampingValue < min)
				return min;

			BigNumber max = Min(maxValue, BigNumber.maxValue);
			if (clampingValue > max)
				return max;

			return clampingValue;
		}

		public static BigNumber Min(params BigNumber[] numbers) {
			BigNumber min = BigNumber.maxValue;
			foreach (BigNumber number in numbers) {
				if (number < min)
					min = number;
			}

			return min;
		}

		public static BigNumber Max(params BigNumber[] numbers) {
			BigNumber max = BigNumber.zero;
			foreach (BigNumber number in numbers) {
				if (number > max)
					max = number;
			}

			return max;
		}

		public static double DivideToDouble(BigNumber dividedNumb, BigNumber divider) {
			return Math.Exp(BigInteger.Log(dividedNumb._bigIntegerIntValue) - BigInteger.Log(divider._bigIntegerIntValue));
		}

		#endregion


		#region Compairs


		public static bool operator <=(BigNumber num1, BigNumber num2) {
			return num1._bigIntegerIntValue <= num2._bigIntegerIntValue;
		}

		public static bool operator >=(BigNumber num1, BigNumber num2) {
			return num1._bigIntegerIntValue >= num2._bigIntegerIntValue;
		}



		public static bool operator <(BigNumber num1, BigNumber num2) {
			return num1._bigIntegerIntValue < num2._bigIntegerIntValue;
		}

		public static bool operator >(BigNumber num1, BigNumber num2) {
			return num1._bigIntegerIntValue > num2._bigIntegerIntValue;
		}



		public static bool operator >=(BigNumber num, int intValue) {
			return num._bigIntegerIntValue >= intValue;
		}

		public static bool operator <=(BigNumber num, int intValue) {
			return num._bigIntegerIntValue >= intValue;
		}



		public static bool operator <(BigNumber num, int intValue) {
			return num._bigIntegerIntValue < intValue;
		}

		public static bool operator >(BigNumber num, int intValue) {
			return num._bigIntegerIntValue > intValue;
		}



		public static bool operator ==(BigNumber num, int intValue) {
			return num._bigIntegerIntValue == intValue;
		}

		public static bool operator !=(BigNumber num, int intValue) {
			return num._bigIntegerIntValue != intValue;
		}


		#endregion


		#region RandomRange

		public static BigNumber RandomRange(BigNumber num1, BigNumber num2) {
			var random = RandomNumberGenerator.Create();
			BigInteger randomInteger = RandomInRange(random, num1._bigIntegerIntValue, num2._bigIntegerIntValue);
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


		#endregion


		#region ToString

		public override string ToString() {
			return this.ToString("full");
		}

		public string ToString(string format) {
			return this.Formate(format);
		}
		
		public string ToString(string format, IBigNumberDictionary dictionary) {
			return this.Formate(format, dictionary);
		}

		private string Formate(string format, IBigNumberDictionary dictionary = null) {
			
			format = format.ToUpperInvariant();

			if (String.IsNullOrEmpty(format) || (this._bigIntegerIntValue < 1000 && format != FORMAT_FULL))
				format = FORMAT_XXX_C;
				
			var fullNumberToString = this._bigIntegerIntValue.ToString();
			var numberLength = fullNumberToString.Length;
			var orderInt = (numberLength - 1) / 3;
			var order = (BigNumberOrder) orderInt;

			var olderNumbersLength = numberLength % 3 == 0 ? 3 : numberLength % 3;
			var olderNumberString = fullNumberToString.Substring(0, olderNumbersLength);
			var youngerNumberString = "";
			var orderToString = dictionary != null ? dictionary.GetTranslatedOrder(order) : order.ToString();
			if (order == 0)
				orderToString = "";
			
			var finalStringWithoutOrder = "";

			switch (format)
			{
				case FORMAT_XXX_XX_C:
					finalStringWithoutOrder =
						this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, true);
					break;

				case FORMAT_XXX_XXC:
					finalStringWithoutOrder =
						this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, false);
					break;
				
				case FORMAT_XXX_X_C:
					finalStringWithoutOrder =
						this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, true);
					break;
				
				case FORMAT_XXX_XC:
					finalStringWithoutOrder =
						this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, false);
					break;
				
				case FORMAT_XXX_C:
					finalStringWithoutOrder = $"{olderNumberString} ";
					break;
				
				case FORMAT_XXXC:
					finalStringWithoutOrder = $"{olderNumberString}";
					break;
				
				case FORMAT_FULL:
					return this._bigIntegerIntValue.ToString();
					break;
				
				case FORMAT_DYNAMIC_3_C:
					switch (olderNumbersLength) {
						case 1:
							finalStringWithoutOrder =
								this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, true);
							break;
						case 2:
							finalStringWithoutOrder =
								this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, true);
							break;
						case 3:
							finalStringWithoutOrder = $"{olderNumberString} ";
							break;
					}
					break;
				
				case FORMAT_DYNAMIC_3C:
					switch (olderNumbersLength) {
						case 1:
							finalStringWithoutOrder =
								this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, false);
							break;
						case 2:
							finalStringWithoutOrder =
								this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, false);
							break;
						case 3:
							finalStringWithoutOrder = $"{olderNumberString}";
							break;
					}
					break;
				
				case FORMAT_DYNAMIC_4_C:
					if (olderNumbersLength < 3) {
						finalStringWithoutOrder =
							this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, true);
					}
					else {
						finalStringWithoutOrder =
							this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, true);
					}
					break;
				
				case FORMAT_DYNAMIC_4C:
					if (olderNumbersLength < 3) {
						finalStringWithoutOrder =
							this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 2, false);
					}
					else {
						finalStringWithoutOrder =
							this.GetFinalStringWithoutOrder(fullNumberToString, olderNumberString, 1, false);
					}
					break;
				
				default:
					throw new FormatException(String.Format("The '{0}' format string is not supported.", format));
			}
			
			return $"{finalStringWithoutOrder}{orderToString}";
		}

		private string GetFinalStringWithoutOrder(string fullNumberToString, string olderNumberString, int youngerNumbersLength, bool withSpace) {
			int olderNumbersLength = olderNumberString.Length;
			var youngerNumberString = $"{fullNumberToString.Substring(olderNumbersLength, youngerNumbersLength)}";
			if (Convert.ToInt32(youngerNumberString) == 0)
				youngerNumberString = "";
			else
				youngerNumberString = $".{youngerNumberString}";
			return $"{olderNumberString}{youngerNumberString}" + (withSpace ? " " : "");
		}

		#endregion
		
	}
}