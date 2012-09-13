using System;
using System.Text;

namespace Aqueduct.Utils
{
	/// <summary>
	/// Convert between bases
	/// </summary>
	public class BaseConverter
	{
		/// <summary>
		///		Converts a number from any base between 2 and 36 to any base between 2 and 36.
		/// </summary>
		/// <param name="numberAsString">The number to be converted.</param>
		/// <param name="fromBase">The base from which to convert. Has to be between 2 and 36.</param>
		/// <param name="toBase">The base to which to convert. Has to be between 2 and 36.</param>
		/// <returns>The number converted from fromBase to toBase.</returns>
		public static string ToBase(string numberAsString, ushort fromBase, ushort toBase)
		{
			ulong base10 = BaseConverter.ToBase10(numberAsString, fromBase);
			return BaseConverter.FromBase10(base10, toBase);
		}

		/// <summary>
		///		Converts a number from any base between 2 and 36 to base 10.
		/// </summary>
		/// <param name="encodedNumber">The number to be converted.</param>
		/// <param name="fromBase">The base from which to convert. Has to be between 2 and 36.</param>
		/// <returns>The number converted from fromBase to base 10.</returns>
		public static ulong ToBase10(string encodedNumber, ushort fromBase)
		{
			// If the from base is 10, simply parse the string and return it
			if (fromBase == 10)
			{
				return UInt64.Parse(encodedNumber);
			}

			// Ensure that the string only contains upper case characters
			encodedNumber = encodedNumber.ToUpper();

			// Go through each character and decode its value.
			int length = encodedNumber.Length;
			ulong runningTotal = 0;

			for (int index = 0; index < length; index++)
			{
				char currentChar = encodedNumber[index];

				// Anything above base 10 uses letters as well as numbers, so A will be 10, B will be 11, etc.
				uint currentValue = Char.IsDigit(currentChar) ? (uint)(currentChar - '0') :
																(uint)(currentChar - 'A' + 10);

				// The value which of the character represents depends on its position and it is calculated
				// by multiplying its value with the power of the base to the position of the character, from
				// right to left.
				runningTotal += currentValue * (ulong)Math.Pow(fromBase, length - index - 1);
			}

			return runningTotal;
		}

		/// <summary>
		///		Converts a number from base 10 to any base between 2 and 36.
		/// </summary>
		/// <param name="number">The number to be converted.</param>
		/// <param name="toBase">The base to which to convert. Has to be between 2 and 36.</param>
		/// <returns>The number converted from base 10 to toBase.</returns>
		public static string FromBase10(ulong number, ushort toBase)
		{
			//	If the to base is 10, simply return the number as a string
			if (toBase == 10)
			{
				return number.ToString();
			}

			// The number has to be divided by the base it needs to be converted to
			// until the result of the division is 0. The modulus of the division 
			// is used to calculate the character that represents it
			StringBuilder runningResult = new StringBuilder();

			while (number > 0)
			{
				ulong modulus = number % toBase;

				if (modulus < 10)
				{
					runningResult.Insert(0, modulus);
				}
				else
				{
					runningResult.Insert(0, (char)('A' + modulus - 10));
				}

				number = (number - modulus) / toBase;
			}

			return runningResult.ToString();
		}
	}
}
