using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200005B RID: 91
	[NullableContext(1)]
	[Nullable(0)]
	public static class FrozenSet
	{
		// Token: 0x06000441 RID: 1089 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public static FrozenSet<T> Create<[Nullable(2)] T>([ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> source)
		{
			return FrozenSet.Create<T>(null, source);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0000B6AC File Offset: 0x000098AC
		public unsafe static FrozenSet<T> Create<[Nullable(2)] T>([Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> equalityComparer, [ParamCollection] [ScopedRef] [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> source)
		{
			if (source.Length != 0)
			{
				HashSet<T> hashSet = new HashSet<T>(equalityComparer);
				ReadOnlySpan<T> readOnlySpan = source;
				for (int i = 0; i < readOnlySpan.Length; i++)
				{
					T item = *readOnlySpan[i];
					hashSet.Add(item);
				}
				return hashSet.ToFrozenSet(equalityComparer);
			}
			if (equalityComparer != null && equalityComparer != FrozenSet<T>.Empty.Comparer)
			{
				return new EmptyFrozenSet<T>(equalityComparer);
			}
			return FrozenSet<T>.Empty;
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0000B718 File Offset: 0x00009918
		public static FrozenSet<T> ToFrozenSet<[Nullable(2)] T>(this IEnumerable<T> source, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> comparer = null)
		{
			HashSet<T> source2;
			return FrozenSet.GetExistingFrozenOrNewSet<T>(source, comparer, out source2) ?? FrozenSet.CreateFromSet<T>(source2);
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0000B738 File Offset: 0x00009938
		private static FrozenSet<T> GetExistingFrozenOrNewSet<T>(IEnumerable<T> source, IEqualityComparer<T> comparer, out HashSet<T> newSet)
		{
			ThrowHelper.ThrowIfNull(source, "source");
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			FrozenSet<T> frozenSet = source as FrozenSet<T>;
			if (frozenSet != null && frozenSet.Comparer.Equals(comparer))
			{
				newSet = null;
				return frozenSet;
			}
			newSet = (source as HashSet<T>);
			if (newSet == null || (newSet.Count != 0 && !newSet.Comparer.Equals(comparer)))
			{
				newSet = new HashSet<T>(source, comparer);
			}
			if (newSet.Count != 0)
			{
				return null;
			}
			if (comparer != FrozenSet<T>.Empty.Comparer)
			{
				return new EmptyFrozenSet<T>(comparer);
			}
			return FrozenSet<T>.Empty;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0000B7C8 File Offset: 0x000099C8
		private static FrozenSet<T> CreateFromSet<T>(HashSet<T> source)
		{
			IEqualityComparer<T> comparer = source.Comparer;
			if (typeof(T).IsValueType && comparer == EqualityComparer<T>.Default)
			{
				if (source.Count <= 10)
				{
					if (Constants.IsKnownComparable<T>())
					{
						return (FrozenSet<T>)new SmallValueTypeComparableFrozenSet<T>(source);
					}
					return (FrozenSet<T>)new SmallValueTypeDefaultComparerFrozenSet<T>(source);
				}
				else
				{
					if (typeof(T) == typeof(int))
					{
						return (FrozenSet<T>)new Int32FrozenSet((HashSet<int>)source);
					}
					return new ValueTypeDefaultComparerFrozenSet<T>(source);
				}
			}
			else if (typeof(T) == typeof(string) && !source.Contains(default(T)) && (comparer == EqualityComparer<T>.Default || comparer == StringComparer.Ordinal || comparer == StringComparer.OrdinalIgnoreCase))
			{
				IEqualityComparer<string> equalityComparer = (IEqualityComparer<string>)comparer;
				HashSet<string> hashSet = (HashSet<string>)source;
				string[] array = new string[hashSet.Count];
				hashSet.CopyTo(array);
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
				FrozenSet<string> frozenSet = LengthBucketsFrozenSet.CreateLengthBucketsFrozenSetIfAppropriate(array, equalityComparer, num, num2);
				if (frozenSet != null)
				{
					return (FrozenSet<T>)frozenSet;
				}
				KeyAnalyzer.AnalysisResults analysisResults = KeyAnalyzer.Analyze(array, equalityComparer == StringComparer.OrdinalIgnoreCase, num, num2);
				if (analysisResults.SubstringHashing)
				{
					if (analysisResults.RightJustifiedSubstring)
					{
						if (analysisResults.IgnoreCase)
						{
							frozenSet = (analysisResults.AllAsciiIfIgnoreCase ? new OrdinalStringFrozenSet_RightJustifiedCaseInsensitiveAsciiSubstring(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount) : new OrdinalStringFrozenSet_RightJustifiedCaseInsensitiveSubstring(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount));
						}
						else
						{
							frozenSet = ((analysisResults.HashCount == 1) ? new OrdinalStringFrozenSet_RightJustifiedSingleChar(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex) : new OrdinalStringFrozenSet_RightJustifiedSubstring(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount));
						}
					}
					else if (analysisResults.IgnoreCase)
					{
						frozenSet = (analysisResults.AllAsciiIfIgnoreCase ? new OrdinalStringFrozenSet_LeftJustifiedCaseInsensitiveAsciiSubstring(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount) : new OrdinalStringFrozenSet_LeftJustifiedCaseInsensitiveSubstring(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount));
					}
					else
					{
						frozenSet = ((analysisResults.HashCount == 1) ? new OrdinalStringFrozenSet_LeftJustifiedSingleChar(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex) : new OrdinalStringFrozenSet_LeftJustifiedSubstring(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, analysisResults.HashIndex, analysisResults.HashCount));
					}
				}
				else if (analysisResults.IgnoreCase)
				{
					frozenSet = (analysisResults.AllAsciiIfIgnoreCase ? new OrdinalStringFrozenSet_FullCaseInsensitiveAscii(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, num3) : new OrdinalStringFrozenSet_FullCaseInsensitive(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, num3));
				}
				else
				{
					frozenSet = new OrdinalStringFrozenSet_Full(array, equalityComparer, analysisResults.MinimumLength, analysisResults.MaximumLengthDiff, num3);
				}
				return (FrozenSet<T>)frozenSet;
			}
			else
			{
				if (source.Count <= 4)
				{
					return new SmallFrozenSet<T>(source);
				}
				return new DefaultFrozenSet<T>(source);
			}
		}
	}
}
