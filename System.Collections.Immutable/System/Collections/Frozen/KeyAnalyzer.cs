using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x0200006B RID: 107
	internal static class KeyAnalyzer
	{
		// Token: 0x060004B7 RID: 1207 RVA: 0x0000D040 File Offset: 0x0000B240
		public static KeyAnalyzer.AnalysisResults Analyze([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<string> uniqueStrings, bool ignoreCase, int minLength, int maxLength)
		{
			bool allUniqueStringsAreConfirmedAscii = ignoreCase && KeyAnalyzer.AreAllAscii(uniqueStrings);
			KeyAnalyzer.AnalysisResults result;
			if (minLength == 0 || !KeyAnalyzer.TryUseSubstring(uniqueStrings, allUniqueStringsAreConfirmedAscii, ignoreCase, minLength, maxLength, out result))
			{
				result = KeyAnalyzer.CreateAnalysisResults(uniqueStrings, allUniqueStringsAreConfirmedAscii, ignoreCase, minLength, maxLength, 0, 0, (string s, int _, int _) => MemoryExtensions.AsSpan(s));
			}
			return result;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0000D098 File Offset: 0x0000B298
		private static bool TryUseSubstring(ReadOnlySpan<string> uniqueStrings, bool allUniqueStringsAreConfirmedAscii, bool ignoreCase, int minLength, int maxLength, out KeyAnalyzer.AnalysisResults results)
		{
			int acceptableNonUniqueCount = uniqueStrings.Length / 20;
			KeyAnalyzer.SubstringComparer substringComparer = (!ignoreCase) ? new KeyAnalyzer.JustifiedSubstringComparer() : (allUniqueStringsAreConfirmedAscii ? new KeyAnalyzer.JustifiedCaseInsensitiveAsciiSubstringComparer() : new KeyAnalyzer.JustifiedCaseInsensitiveSubstringComparer());
			HashSet<string> set = new HashSet<string>(substringComparer);
			int num = Math.Min(minLength, 8);
			for (int i = 1; i <= num; i++)
			{
				substringComparer.IsLeft = true;
				substringComparer.Count = i;
				for (int j = 0; j <= minLength - i; j++)
				{
					substringComparer.Index = j;
					if (KeyAnalyzer.HasSufficientUniquenessFactor(set, uniqueStrings, acceptableNonUniqueCount))
					{
						results = KeyAnalyzer.CreateAnalysisResults(uniqueStrings, allUniqueStringsAreConfirmedAscii, ignoreCase, minLength, maxLength, j, i, (string s, int index, int count) => MemoryExtensions.AsSpan(s, index, count));
						return true;
					}
				}
				if (minLength != maxLength)
				{
					substringComparer.IsLeft = false;
					for (int k = 0; k <= minLength - i; k++)
					{
						substringComparer.Index = -k - i;
						if (KeyAnalyzer.HasSufficientUniquenessFactor(set, uniqueStrings, acceptableNonUniqueCount))
						{
							results = KeyAnalyzer.CreateAnalysisResults(uniqueStrings, allUniqueStringsAreConfirmedAscii, ignoreCase, minLength, maxLength, substringComparer.Index, i, (string s, int index, int count) => MemoryExtensions.AsSpan(s, s.Length + index, count));
							return true;
						}
					}
				}
			}
			results = default(KeyAnalyzer.AnalysisResults);
			return false;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0000D1D4 File Offset: 0x0000B3D4
		private unsafe static KeyAnalyzer.AnalysisResults CreateAnalysisResults(ReadOnlySpan<string> uniqueStrings, bool allUniqueStringsAreConfirmedAscii, bool ignoreCase, int minLength, int maxLength, int index, int count, KeyAnalyzer.GetSpan getHashString)
		{
			bool allAsciiIfIgnoreCase = true;
			if (ignoreCase)
			{
				bool flag = true;
				ReadOnlySpan<string> readOnlySpan = uniqueStrings;
				for (int i = 0; i < readOnlySpan.Length; i++)
				{
					string text = *readOnlySpan[i];
					if (!allUniqueStringsAreConfirmedAscii && !KeyAnalyzer.IsAllAscii(getHashString(text, index, count)))
					{
						allAsciiIfIgnoreCase = false;
						flag = false;
						break;
					}
					if (flag && ((count > 0 && !allUniqueStringsAreConfirmedAscii && !KeyAnalyzer.IsAllAscii(MemoryExtensions.AsSpan(text))) || KeyAnalyzer.ContainsAnyAsciiLetters(MemoryExtensions.AsSpan(text))))
					{
						flag = false;
						if (allUniqueStringsAreConfirmedAscii)
						{
							break;
						}
					}
				}
				if (flag)
				{
					ignoreCase = false;
				}
			}
			return new KeyAnalyzer.AnalysisResults(ignoreCase, allAsciiIfIgnoreCase, index, count, minLength, maxLength);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000D264 File Offset: 0x0000B464
		private unsafe static bool AreAllAscii(ReadOnlySpan<string> strings)
		{
			ReadOnlySpan<string> readOnlySpan = strings;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				if (!KeyAnalyzer.IsAllAscii(MemoryExtensions.AsSpan(*readOnlySpan[i])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000D2A0 File Offset: 0x0000B4A0
		internal unsafe static bool IsAllAscii(ReadOnlySpan<char> s)
		{
			fixed (char* pinnableReference = s.GetPinnableReference())
			{
				uint* ptr = (uint*)pinnableReference;
				int i;
				for (i = s.Length; i >= 4; i -= 4)
				{
					if (!KeyAnalyzer.<IsAllAscii>g__AllCharsInUInt32AreAscii|5_0(*ptr | ptr[1]))
					{
						return false;
					}
					ptr += 2;
				}
				char* ptr2 = (char*)ptr;
				while (i-- > 0)
				{
					if (*(ptr2++) >= '\u0080')
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000D300 File Offset: 0x0000B500
		internal unsafe static bool ContainsAnyAsciiLetters(ReadOnlySpan<char> s)
		{
			ReadOnlySpan<char> readOnlySpan = s;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				if ((*readOnlySpan[i] | 32) - 97 <= 25)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000D338 File Offset: 0x0000B538
		[NullableContext(1)]
		internal unsafe static bool HasSufficientUniquenessFactor(HashSet<string> set, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<string> uniqueStrings, int acceptableNonUniqueCount)
		{
			set.Clear();
			ReadOnlySpan<string> readOnlySpan = uniqueStrings;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				string item = *readOnlySpan[i];
				if (!set.Add(item) && --acceptableNonUniqueCount < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000D37E File Offset: 0x0000B57E
		[CompilerGenerated]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool <IsAllAscii>g__AllCharsInUInt32AreAscii|5_0(uint value)
		{
			return (value & 4286644096U) == 0U;
		}

		// Token: 0x020000CC RID: 204
		// (Invoke) Token: 0x06000888 RID: 2184
		private delegate ReadOnlySpan<char> GetSpan(string s, int index, int count);

		// Token: 0x020000CD RID: 205
		internal readonly struct AnalysisResults
		{
			// Token: 0x0600088B RID: 2187 RVA: 0x0001640C File Offset: 0x0001460C
			public AnalysisResults(bool ignoreCase, bool allAsciiIfIgnoreCase, int hashIndex, int hashCount, int minLength, int maxLength)
			{
				this.IgnoreCase = ignoreCase;
				this.AllAsciiIfIgnoreCase = allAsciiIfIgnoreCase;
				this.HashIndex = hashIndex;
				this.HashCount = hashCount;
				this.MinimumLength = minLength;
				this.MaximumLengthDiff = maxLength - minLength;
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x0600088C RID: 2188 RVA: 0x0001643E File Offset: 0x0001463E
			public bool IgnoreCase { get; }

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x0600088D RID: 2189 RVA: 0x00016446 File Offset: 0x00014646
			public bool AllAsciiIfIgnoreCase { get; }

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x0600088E RID: 2190 RVA: 0x0001644E File Offset: 0x0001464E
			public int HashIndex { get; }

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x0600088F RID: 2191 RVA: 0x00016456 File Offset: 0x00014656
			public int HashCount { get; }

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000890 RID: 2192 RVA: 0x0001645E File Offset: 0x0001465E
			public int MinimumLength { get; }

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06000891 RID: 2193 RVA: 0x00016466 File Offset: 0x00014666
			public int MaximumLengthDiff { get; }

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000892 RID: 2194 RVA: 0x0001646E File Offset: 0x0001466E
			public bool SubstringHashing
			{
				get
				{
					return this.HashCount != 0;
				}
			}

			// Token: 0x170001C6 RID: 454
			// (get) Token: 0x06000893 RID: 2195 RVA: 0x00016479 File Offset: 0x00014679
			public bool RightJustifiedSubstring
			{
				get
				{
					return this.HashIndex < 0;
				}
			}
		}

		// Token: 0x020000CE RID: 206
		private abstract class SubstringComparer : IEqualityComparer<string>
		{
			// Token: 0x06000894 RID: 2196
			public abstract bool Equals(string x, string y);

			// Token: 0x06000895 RID: 2197
			public abstract int GetHashCode(string s);

			// Token: 0x0400016E RID: 366
			public int Index;

			// Token: 0x0400016F RID: 367
			public int Count;

			// Token: 0x04000170 RID: 368
			public bool IsLeft;
		}

		// Token: 0x020000CF RID: 207
		private sealed class JustifiedSubstringComparer : KeyAnalyzer.SubstringComparer
		{
			// Token: 0x06000897 RID: 2199 RVA: 0x0001648C File Offset: 0x0001468C
			public override bool Equals(string x, string y)
			{
				return MemoryExtensions.SequenceEqual<char>(MemoryExtensions.AsSpan(x, this.IsLeft ? this.Index : (x.Length + this.Index), this.Count), MemoryExtensions.AsSpan(y, this.IsLeft ? this.Index : (y.Length + this.Index), this.Count));
			}

			// Token: 0x06000898 RID: 2200 RVA: 0x000164F0 File Offset: 0x000146F0
			public override int GetHashCode(string s)
			{
				return Hashing.GetHashCodeOrdinal(MemoryExtensions.AsSpan(s, this.IsLeft ? this.Index : (s.Length + this.Index), this.Count));
			}
		}

		// Token: 0x020000D0 RID: 208
		private sealed class JustifiedCaseInsensitiveSubstringComparer : KeyAnalyzer.SubstringComparer
		{
			// Token: 0x0600089A RID: 2202 RVA: 0x00016528 File Offset: 0x00014728
			public override bool Equals(string x, string y)
			{
				return MemoryExtensions.Equals(MemoryExtensions.AsSpan(x, this.IsLeft ? this.Index : (x.Length + this.Index), this.Count), MemoryExtensions.AsSpan(y, this.IsLeft ? this.Index : (y.Length + this.Index), this.Count), StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x0600089B RID: 2203 RVA: 0x0001658D File Offset: 0x0001478D
			public override int GetHashCode(string s)
			{
				return Hashing.GetHashCodeOrdinalIgnoreCase(MemoryExtensions.AsSpan(s, this.IsLeft ? this.Index : (s.Length + this.Index), this.Count));
			}
		}

		// Token: 0x020000D1 RID: 209
		private sealed class JustifiedCaseInsensitiveAsciiSubstringComparer : KeyAnalyzer.SubstringComparer
		{
			// Token: 0x0600089D RID: 2205 RVA: 0x000165C8 File Offset: 0x000147C8
			public override bool Equals(string x, string y)
			{
				return MemoryExtensions.Equals(MemoryExtensions.AsSpan(x, this.IsLeft ? this.Index : (x.Length + this.Index), this.Count), MemoryExtensions.AsSpan(y, this.IsLeft ? this.Index : (y.Length + this.Index), this.Count), StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x0600089E RID: 2206 RVA: 0x0001662D File Offset: 0x0001482D
			public override int GetHashCode(string s)
			{
				return Hashing.GetHashCodeOrdinalIgnoreCaseAscii(MemoryExtensions.AsSpan(s, this.IsLeft ? this.Index : (s.Length + this.Index), this.Count));
			}
		}
	}
}
