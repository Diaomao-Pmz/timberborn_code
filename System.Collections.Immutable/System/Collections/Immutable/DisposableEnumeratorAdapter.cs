using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000024 RID: 36
	internal struct DisposableEnumeratorAdapter<[Nullable(2)] T, TEnumerator> : IDisposable where TEnumerator : struct, IEnumerator<T>
	{
		// Token: 0x0600008D RID: 141 RVA: 0x000030AC File Offset: 0x000012AC
		internal DisposableEnumeratorAdapter(TEnumerator enumerator)
		{
			this._enumeratorStruct = enumerator;
			this._enumeratorObject = null;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000030BC File Offset: 0x000012BC
		[NullableContext(1)]
		internal DisposableEnumeratorAdapter(IEnumerator<T> enumerator)
		{
			this._enumeratorStruct = default(TEnumerator);
			this._enumeratorObject = enumerator;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000030D1 File Offset: 0x000012D1
		[Nullable(1)]
		public T Current
		{
			[NullableContext(1)]
			get
			{
				if (this._enumeratorObject == null)
				{
					return this._enumeratorStruct.Current;
				}
				return this._enumeratorObject.Current;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000030F8 File Offset: 0x000012F8
		public bool MoveNext()
		{
			if (this._enumeratorObject == null)
			{
				return this._enumeratorStruct.MoveNext();
			}
			return this._enumeratorObject.MoveNext();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000311F File Offset: 0x0000131F
		public void Dispose()
		{
			if (this._enumeratorObject != null)
			{
				this._enumeratorObject.Dispose();
				return;
			}
			this._enumeratorStruct.Dispose();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003146 File Offset: 0x00001346
		[return: Nullable(new byte[]
		{
			0,
			1,
			0
		})]
		public DisposableEnumeratorAdapter<T, TEnumerator> GetEnumerator()
		{
			return this;
		}

		// Token: 0x04000021 RID: 33
		private readonly IEnumerator<T> _enumeratorObject;

		// Token: 0x04000022 RID: 34
		private TEnumerator _enumeratorStruct;
	}
}
