using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000041 RID: 65
	[NullableContext(1)]
	[Nullable(0)]
	[CollectionBuilder(typeof(ImmutableQueue), "Create")]
	[DebuggerDisplay("IsEmpty = {IsEmpty}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableQueue<[Nullable(2)] T> : IImmutableQueue<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060002CD RID: 717 RVA: 0x00007EE0 File Offset: 0x000060E0
		internal ImmutableQueue(ImmutableStack<T> forwards, ImmutableStack<T> backwards)
		{
			this._forwards = forwards;
			this._backwards = backwards;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00007EF6 File Offset: 0x000060F6
		public ImmutableQueue<T> Clear()
		{
			return ImmutableQueue<T>.Empty;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002CF RID: 719 RVA: 0x00007EFD File Offset: 0x000060FD
		public bool IsEmpty
		{
			get
			{
				return this._forwards.IsEmpty;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00007F0A File Offset: 0x0000610A
		public static ImmutableQueue<T> Empty
		{
			get
			{
				return ImmutableQueue<T>.s_EmptyField;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00007F11 File Offset: 0x00006111
		IImmutableQueue<T> IImmutableQueue<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00007F19 File Offset: 0x00006119
		private ImmutableStack<T> BackwardsReversed
		{
			get
			{
				if (this._backwardsReversed == null)
				{
					this._backwardsReversed = this._backwards.Reverse();
				}
				return this._backwardsReversed;
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00007F3A File Offset: 0x0000613A
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._forwards.Peek();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00007F5A File Offset: 0x0000615A
		public ref readonly T PeekRef()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._forwards.PeekRef();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00007F7A File Offset: 0x0000617A
		public ImmutableQueue<T> Enqueue(T value)
		{
			if (this.IsEmpty)
			{
				return new ImmutableQueue<T>(ImmutableStack.Create<T>(value), ImmutableStack<T>.Empty);
			}
			return new ImmutableQueue<T>(this._forwards, this._backwards.Push(value));
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00007FAC File Offset: 0x000061AC
		IImmutableQueue<T> IImmutableQueue<!0>.Enqueue(T value)
		{
			return this.Enqueue(value);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00007FB8 File Offset: 0x000061B8
		public ImmutableQueue<T> Dequeue()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			ImmutableStack<T> immutableStack = this._forwards.Pop();
			if (!immutableStack.IsEmpty)
			{
				return new ImmutableQueue<T>(immutableStack, this._backwards);
			}
			if (this._backwards.IsEmpty)
			{
				return ImmutableQueue<T>.Empty;
			}
			return new ImmutableQueue<T>(this.BackwardsReversed, ImmutableStack<T>.Empty);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000801C File Offset: 0x0000621C
		public ImmutableQueue<T> Dequeue(out T value)
		{
			value = this.Peek();
			return this.Dequeue();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008030 File Offset: 0x00006230
		IImmutableQueue<T> IImmutableQueue<!0>.Dequeue()
		{
			return this.Dequeue();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00008038 File Offset: 0x00006238
		[NullableContext(0)]
		public ImmutableQueue<T>.Enumerator GetEnumerator()
		{
			return new ImmutableQueue<T>.Enumerator(this);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00008040 File Offset: 0x00006240
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return new ImmutableQueue<T>.EnumeratorObject(this);
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00008068 File Offset: 0x00006268
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableQueue<T>.EnumeratorObject(this);
		}

		// Token: 0x0400003A RID: 58
		private static readonly ImmutableQueue<T> s_EmptyField = new ImmutableQueue<T>(ImmutableStack<T>.Empty, ImmutableStack<T>.Empty);

		// Token: 0x0400003B RID: 59
		private readonly ImmutableStack<T> _backwards;

		// Token: 0x0400003C RID: 60
		private readonly ImmutableStack<T> _forwards;

		// Token: 0x0400003D RID: 61
		private ImmutableStack<T> _backwardsReversed;

		// Token: 0x020000B3 RID: 179
		[Nullable(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator
		{
			// Token: 0x0600076A RID: 1898 RVA: 0x000136B6 File Offset: 0x000118B6
			internal Enumerator(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
				this._remainingForwardsStack = null;
				this._remainingBackwardsStack = null;
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x0600076B RID: 1899 RVA: 0x000136D0 File Offset: 0x000118D0
			public T Current
			{
				get
				{
					if (this._remainingForwardsStack == null)
					{
						throw new InvalidOperationException();
					}
					if (!this._remainingForwardsStack.IsEmpty)
					{
						return this._remainingForwardsStack.Peek();
					}
					if (!this._remainingBackwardsStack.IsEmpty)
					{
						return this._remainingBackwardsStack.Peek();
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x0600076C RID: 1900 RVA: 0x00013724 File Offset: 0x00011924
			public bool MoveNext()
			{
				if (this._remainingForwardsStack == null)
				{
					this._remainingForwardsStack = this._originalQueue._forwards;
					this._remainingBackwardsStack = this._originalQueue.BackwardsReversed;
				}
				else if (!this._remainingForwardsStack.IsEmpty)
				{
					this._remainingForwardsStack = this._remainingForwardsStack.Pop();
				}
				else if (!this._remainingBackwardsStack.IsEmpty)
				{
					this._remainingBackwardsStack = this._remainingBackwardsStack.Pop();
				}
				return !this._remainingForwardsStack.IsEmpty || !this._remainingBackwardsStack.IsEmpty;
			}

			// Token: 0x04000118 RID: 280
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x04000119 RID: 281
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x0400011A RID: 282
			private ImmutableStack<T> _remainingBackwardsStack;
		}

		// Token: 0x020000B4 RID: 180
		private sealed class EnumeratorObject : IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x0600076D RID: 1901 RVA: 0x000137B8 File Offset: 0x000119B8
			internal EnumeratorObject(ImmutableQueue<T> queue)
			{
				this._originalQueue = queue;
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x0600076E RID: 1902 RVA: 0x000137C8 File Offset: 0x000119C8
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._remainingForwardsStack == null)
					{
						throw new InvalidOperationException();
					}
					if (!this._remainingForwardsStack.IsEmpty)
					{
						return this._remainingForwardsStack.Peek();
					}
					if (!this._remainingBackwardsStack.IsEmpty)
					{
						return this._remainingBackwardsStack.Peek();
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x0600076F RID: 1903 RVA: 0x00013820 File Offset: 0x00011A20
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000770 RID: 1904 RVA: 0x00013830 File Offset: 0x00011A30
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._remainingForwardsStack == null)
				{
					this._remainingForwardsStack = this._originalQueue._forwards;
					this._remainingBackwardsStack = this._originalQueue.BackwardsReversed;
				}
				else if (!this._remainingForwardsStack.IsEmpty)
				{
					this._remainingForwardsStack = this._remainingForwardsStack.Pop();
				}
				else if (!this._remainingBackwardsStack.IsEmpty)
				{
					this._remainingBackwardsStack = this._remainingBackwardsStack.Pop();
				}
				return !this._remainingForwardsStack.IsEmpty || !this._remainingBackwardsStack.IsEmpty;
			}

			// Token: 0x06000771 RID: 1905 RVA: 0x000138CA File Offset: 0x00011ACA
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingBackwardsStack = null;
				this._remainingForwardsStack = null;
			}

			// Token: 0x06000772 RID: 1906 RVA: 0x000138E0 File Offset: 0x00011AE0
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x06000773 RID: 1907 RVA: 0x000138E9 File Offset: 0x00011AE9
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableQueue<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x0400011B RID: 283
			private readonly ImmutableQueue<T> _originalQueue;

			// Token: 0x0400011C RID: 284
			private ImmutableStack<T> _remainingForwardsStack;

			// Token: 0x0400011D RID: 285
			private ImmutableStack<T> _remainingBackwardsStack;

			// Token: 0x0400011E RID: 286
			private bool _disposed;
		}
	}
}
