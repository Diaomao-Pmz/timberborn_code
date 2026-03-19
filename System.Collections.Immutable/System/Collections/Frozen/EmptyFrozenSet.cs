using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Frozen
{
	// Token: 0x02000057 RID: 87
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	internal sealed class EmptyFrozenSet<[Nullable(2)] T> : FrozenSet<T>
	{
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000A7EB File Offset: 0x000089EB
		internal EmptyFrozenSet(IEqualityComparer<T> comparer) : base(comparer)
		{
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000A7F4 File Offset: 0x000089F4
		private protected override T[] ItemsCore
		{
			get
			{
				return Array.Empty<T>();
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000A7FB File Offset: 0x000089FB
		private protected override int CountCore
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000A7FE File Offset: 0x000089FE
		private protected override int FindItemIndex(T item)
		{
			return -1;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000A801 File Offset: 0x00008A01
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		private protected override FrozenSet<T>.Enumerator GetEnumeratorCore()
		{
			return new FrozenSet<T>.Enumerator(Array.Empty<T>());
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000A80D File Offset: 0x00008A0D
		private protected override bool IsProperSubsetOfCore(IEnumerable<T> other)
		{
			return !EmptyFrozenSet<T>.OtherIsEmpty(other);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000A818 File Offset: 0x00008A18
		private protected override bool IsProperSupersetOfCore(IEnumerable<T> other)
		{
			return false;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000A81B File Offset: 0x00008A1B
		private protected override bool IsSubsetOfCore(IEnumerable<T> other)
		{
			return true;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000A81E File Offset: 0x00008A1E
		private protected override bool IsSupersetOfCore(IEnumerable<T> other)
		{
			return EmptyFrozenSet<T>.OtherIsEmpty(other);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000A826 File Offset: 0x00008A26
		private protected override bool OverlapsCore(IEnumerable<T> other)
		{
			return false;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000A829 File Offset: 0x00008A29
		private protected override bool SetEqualsCore(IEnumerable<T> other)
		{
			return EmptyFrozenSet<T>.OtherIsEmpty(other);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000A834 File Offset: 0x00008A34
		private static bool OtherIsEmpty(IEnumerable<T> other)
		{
			IReadOnlyCollection<T> readOnlyCollection = other as IReadOnlyCollection<!0>;
			if (readOnlyCollection == null)
			{
				return !other.Any<T>();
			}
			return readOnlyCollection.Count == 0;
		}
	}
}
