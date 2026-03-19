using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000023 RID: 35
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class DictionaryEnumerator<TKey, [Nullable(2)] TValue> : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00002FD8 File Offset: 0x000011D8
		internal DictionaryEnumerator([Nullable(new byte[]
		{
			1,
			0,
			1,
			1
		})] IEnumerator<KeyValuePair<TKey, TValue>> inner)
		{
			Requires.NotNull<IEnumerator<KeyValuePair<TKey, TValue>>>(inner, "inner");
			this._inner = inner;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002FF4 File Offset: 0x000011F4
		public DictionaryEntry Entry
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				object key = keyValuePair.Key;
				keyValuePair = this._inner.Current;
				return new DictionaryEntry(key, keyValuePair.Value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003038 File Offset: 0x00001238
		public object Key
		{
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Key;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003060 File Offset: 0x00001260
		[Nullable(2)]
		public object Value
		{
			[NullableContext(2)]
			get
			{
				KeyValuePair<TKey, TValue> keyValuePair = this._inner.Current;
				return keyValuePair.Value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00003085 File Offset: 0x00001285
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003092 File Offset: 0x00001292
		public bool MoveNext()
		{
			return this._inner.MoveNext();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000309F File Offset: 0x0000129F
		public void Reset()
		{
			this._inner.Reset();
		}

		// Token: 0x04000020 RID: 32
		private readonly IEnumerator<KeyValuePair<TKey, TValue>> _inner;
	}
}
