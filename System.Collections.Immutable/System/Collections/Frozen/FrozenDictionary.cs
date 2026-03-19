using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000058 RID: 88
	[NullableContext(1)]
	[Nullable(0)]
	public static class FrozenDictionary
	{
		// Token: 0x06000403 RID: 1027 RVA: 0x0000A860 File Offset: 0x00008A60
		public static FrozenDictionary<TKey, TValue> ToFrozenDictionary<TKey, [Nullable(2)] TValue>([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] this IEnumerable<KeyValuePair<TKey, TValue>> source, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer = null)
		{
			Dictionary<TKey, TValue> source2;
			return FrozenDictionary.GetExistingFrozenOrNewDictionary<TKey, TValue>(source, comparer, out source2) ?? FrozenDictionary.CreateFromDictionary<TKey, TValue>(source2);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000A880 File Offset: 0x00008A80
		public static FrozenDictionary<TKey, TSource> ToFrozenDictionary<[Nullable(2)] TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer = null)
		{
			return source.ToDictionary(keySelector, comparer).ToFrozenDictionary(comparer);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000A890 File Offset: 0x00008A90
		public static FrozenDictionary<TKey, TElement> ToFrozenDictionary<[Nullable(2)] TSource, TKey, [Nullable(2)] TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<TKey> comparer = null)
		{
			return source.ToDictionary(keySelector, elementSelector, comparer).ToFrozenDictionary(comparer);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		private static FrozenDictionary<TKey, TValue> GetExistingFrozenOrNewDictionary<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> comparer, out Dictionary<TKey, TValue> newDictionary)
		{
			ThrowHelper.ThrowIfNull(source, "source");
			if (comparer == null)
			{
				comparer = EqualityComparer<TKey>.Default;
			}
			FrozenDictionary<TKey, TValue> frozenDictionary = source as FrozenDictionary<TKey, TValue>;
			if (frozenDictionary != null && frozenDictionary.Comparer.Equals(comparer))
			{
				newDictionary = null;
				return frozenDictionary;
			}
			newDictionary = (source as Dictionary<TKey, TValue>);
			if (newDictionary == null || (newDictionary.Count != 0 && !newDictionary.Comparer.Equals(comparer)))
			{
				newDictionary = new Dictionary<TKey, TValue>(comparer);
				foreach (KeyValuePair<TKey, TValue> keyValuePair in source)
				{
					newDictionary[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			if (newDictionary.Count != 0)
			{
				return null;
			}
			if (comparer != FrozenDictionary<TKey, TValue>.Empty.Comparer)
			{
				return new EmptyFrozenDictionary<TKey, TValue>(comparer);
			}
			return FrozenDictionary<TKey, TValue>.Empty;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000A97C File Offset: 0x00008B7C
		private static FrozenDictionary<TKey, TValue> CreateFromDictionary<TKey, TValue>(Dictionary<TKey, TValue> source)
		{
			IEqualityComparer<TKey> comparer = source.Comparer;
			if (typeof(TKey).IsValueType && comparer == EqualityComparer<TKey>.Default)
			{
				if (source.Count <= 10)
				{
					if (Constants.IsKnownComparable<TKey>())
					{
						return (FrozenDictionary<TKey, TValue>)new SmallValueTypeComparableFrozenDictionary<TKey, TValue>(source);
					}
					return (FrozenDictionary<TKey, TValue>)new SmallValueTypeDefaultComparerFrozenDictionary<TKey, TValue>(source);
				}
				else
				{
					if (typeof(TKey) == typeof(int))
					{
						return (FrozenDictionary<TKey, TValue>)new Int32FrozenDictionary<TValue>((Dictionary<int, TValue>)source);
					}
					return new ValueTypeDefaultComparerFrozenDictionary<TKey, TValue>(source);
				}
			}
			else if (typeof(TKey) == typeof(string) && (comparer == EqualityComparer<TKey>.Default || comparer == StringComparer.Ordinal || comparer == StringComparer.OrdinalIgnoreCase))
			{
				IEqualityComparer<string> equalityComparer = (IEqualityComparer<string>)comparer;
				string[] array = (string[])source.Keys.ToArray<TKey>();
				TValue[] values = source.Values.ToArray<TValue>();
				int num = int.MaxValue;
				int num2 = 0;
				ulong num3 = 0UL;
				foreach (string text in array)
				{
					if (text.Length < num)
					{
						num = text.Length;
					}
					if (text.Length > num2)
					{
						num2 = text.Length;
					}
					num3 |= 1UL << text.Length % 64;
				}
				FrozenDictionary<string, TValue> frozenDictionary = LengthBucketsFrozenDictionary<TValue>.CreateLengthBucketsFrozenDictionaryIfAppropriate(array, values, equalityComparer, num, num2);
				if (frozenDictionary != null)
				{
					return (FrozenDictionary<TKey, TValue>)frozenDictionary;
				}
				KeyAnalyzer.AnalysisResults analysisResults = KeyAnalyzer.Analyze(array, equalityComparer == StringComparer.OrdinalIgnoreCase, num, num2);
				if (analysisResults.SubstringHashing)
				{
					if (analysisResults.RightJustifiedSubstring)
					{
						if (analysisResults.IgnoreCase)
						{
							frozenDictionary = (analysisResults.AllAsciiIfIgnoreCase ? new OrdinalStringFrozenDictionary_RightJustifiedCaseInsensitiveAsciiSubstring<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount) : new OrdinalStringFrozenDictionary_RightJustifiedCaseInsensitiveSubstring<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount));
						}
						else
						{
							frozenDictionary = ((analysisResults.HashCount == 1) ? new OrdinalStringFrozenDictionary_RightJustifiedSingleChar<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex) : new OrdinalStringFrozenDictionary_RightJustifiedSubstring<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount));
						}
					}
					else if (analysisResults.IgnoreCase)
					{
						frozenDictionary = (analysisResults.AllAsciiIfIgnoreCase ? new OrdinalStringFrozenDictionary_LeftJustifiedCaseInsensitiveAsciiSubstring<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount) : new OrdinalStringFrozenDictionary_LeftJustifiedCaseInsensitiveSubstring<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount));
					}
					else
					{
						frozenDictionary = ((analysisResults.HashCount == 1) ? new OrdinalStringFrozenDictionary_LeftJustifiedSingleChar<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex) : new OrdinalStringFrozenDictionary_LeftJustifiedSubstring<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount));
					}
				}
				else if (analysisResults.IgnoreCase)
				{
					frozenDictionary = (analysisResults.AllAsciiIfIgnoreCase ? new OrdinalStringFrozenDictionary_FullCaseInsensitiveAscii<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, num3) : new OrdinalStringFrozenDictionary_FullCaseInsensitive<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, num3));
				}
				else
				{
					frozenDictionary = new OrdinalStringFrozenDictionary_Full<TValue>(array, values, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, num3);
				}
				return (FrozenDictionary<TKey, TValue>)frozenDictionary;
			}
			else
			{
				if (source.Count <= 4)
				{
					return new SmallFrozenDictionary<TKey, TValue>(source);
				}
				return new DefaultFrozenDictionary<TKey, TValue>(source);
			}
		}
	}
}
