using System;

namespace Timberborn.Common
{
	// Token: 0x0200000D RID: 13
	public class BufferedArray<T>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002754 File Offset: 0x00000954
		public T[] Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000027 RID: 39 RVA: 0x0000275C File Offset: 0x0000095C
		public T[] Buffered
		{
			get
			{
				return this._buffered;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002764 File Offset: 0x00000964
		public void Initialize(int size)
		{
			this._current = new T[size];
			this._buffered = new T[size];
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002780 File Offset: 0x00000980
		public void Swap()
		{
			T[] buffered = this._buffered;
			T[] current = this._current;
			this._current = buffered;
			this._buffered = current;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027A9 File Offset: 0x000009A9
		public void Unify()
		{
			this._current.CopyTo(this._buffered, 0);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000027C0 File Offset: 0x000009C0
		public void Fill(T value)
		{
			for (int i = 0; i < this._current.Length; i++)
			{
				this._current[i] = value;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000027F0 File Offset: 0x000009F0
		public void ResizeAndFill(int newSize, T value)
		{
			int num = this._current.Length;
			Array.Resize<T>(ref this._current, newSize);
			Array.Resize<T>(ref this._buffered, newSize);
			for (int i = num; i < newSize; i++)
			{
				this._current[i] = value;
				this._buffered[i] = value;
			}
		}

		// Token: 0x0400001A RID: 26
		public T[] _current;

		// Token: 0x0400001B RID: 27
		public T[] _buffered;
	}
}
