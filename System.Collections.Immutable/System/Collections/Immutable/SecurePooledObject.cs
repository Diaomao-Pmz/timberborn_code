using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000050 RID: 80
	internal sealed class SecurePooledObject<[Nullable(2)] T>
	{
		// Token: 0x060003BF RID: 959 RVA: 0x00009C67 File Offset: 0x00007E67
		[NullableContext(1)]
		internal SecurePooledObject(T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			this._value = newValue;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00009C81 File Offset: 0x00007E81
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00009C89 File Offset: 0x00007E89
		internal int Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00009C92 File Offset: 0x00007E92
		[return: Nullable(1)]
		internal T Use<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			if (!this.IsOwned<TCaller>(ref caller))
			{
				Requires.FailObjectDisposed<TCaller>(caller);
			}
			return this._value;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00009CAE File Offset: 0x00007EAE
		internal bool TryUse<TCaller>(ref TCaller caller, [Nullable(1)] [MaybeNullWhen(false)] out T value) where TCaller : struct, ISecurePooledObjectUser
		{
			if (this.IsOwned<TCaller>(ref caller))
			{
				value = this._value;
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00009CCF File Offset: 0x00007ECF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool IsOwned<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			return caller.PoolUserId == this._owner;
		}

		// Token: 0x04000050 RID: 80
		private readonly T _value;

		// Token: 0x04000051 RID: 81
		private int _owner;
	}
}
