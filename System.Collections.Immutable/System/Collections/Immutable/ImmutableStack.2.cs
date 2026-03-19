using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000048 RID: 72
	[NullableContext(1)]
	[Nullable(0)]
	[CollectionBuilder(typeof(ImmutableStack), "Create")]
	[DebuggerDisplay("IsEmpty = {IsEmpty}, Top = {_head}")]
	[DebuggerTypeProxy(typeof(ImmutableEnumerableDebuggerProxy<>))]
	public sealed class ImmutableStack<[Nullable(2)] T> : IImmutableStack<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06000395 RID: 917 RVA: 0x0000988D File Offset: 0x00007A8D
		private ImmutableStack()
		{
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00009895 File Offset: 0x00007A95
		private ImmutableStack(T head, ImmutableStack<T> tail)
		{
			this._head = head;
			this._tail = tail;
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000397 RID: 919 RVA: 0x000098AB File Offset: 0x00007AAB
		public static ImmutableStack<T> Empty
		{
			get
			{
				return ImmutableStack<T>.s_EmptyField;
			}
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000098B2 File Offset: 0x00007AB2
		public ImmutableStack<T> Clear()
		{
			return ImmutableStack<T>.Empty;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000098B9 File Offset: 0x00007AB9
		IImmutableStack<T> IImmutableStack<!0>.Clear()
		{
			return this.Clear();
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600039A RID: 922 RVA: 0x000098C1 File Offset: 0x00007AC1
		public bool IsEmpty
		{
			get
			{
				return this._tail == null;
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000098CC File Offset: 0x00007ACC
		public T Peek()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._head;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000098E7 File Offset: 0x00007AE7
		public ref readonly T PeekRef()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return ref this._head;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00009902 File Offset: 0x00007B02
		public ImmutableStack<T> Push(T value)
		{
			return new ImmutableStack<T>(value, this);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0000990B File Offset: 0x00007B0B
		IImmutableStack<T> IImmutableStack<!0>.Push(T value)
		{
			return this.Push(value);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00009914 File Offset: 0x00007B14
		public ImmutableStack<T> Pop()
		{
			if (this.IsEmpty)
			{
				throw new InvalidOperationException(SR.InvalidEmptyOperation);
			}
			return this._tail;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000992F File Offset: 0x00007B2F
		public ImmutableStack<T> Pop(out T value)
		{
			value = this.Peek();
			return this.Pop();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00009943 File Offset: 0x00007B43
		IImmutableStack<T> IImmutableStack<!0>.Pop()
		{
			return this.Pop();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0000994B File Offset: 0x00007B4B
		[NullableContext(0)]
		public ImmutableStack<T>.Enumerator GetEnumerator()
		{
			return new ImmutableStack<T>.Enumerator(this);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00009954 File Offset: 0x00007B54
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			if (!this.IsEmpty)
			{
				return new ImmutableStack<T>.EnumeratorObject(this);
			}
			return Enumerable.Empty<T>().GetEnumerator();
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000997C File Offset: 0x00007B7C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ImmutableStack<T>.EnumeratorObject(this);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00009984 File Offset: 0x00007B84
		internal ImmutableStack<T> Reverse()
		{
			ImmutableStack<T> immutableStack = this.Clear();
			ImmutableStack<T> immutableStack2 = this;
			while (!immutableStack2.IsEmpty)
			{
				immutableStack = immutableStack.Push(immutableStack2.Peek());
				immutableStack2 = immutableStack2.Pop();
			}
			return immutableStack;
		}

		// Token: 0x04000048 RID: 72
		private static readonly ImmutableStack<T> s_EmptyField = new ImmutableStack<T>();

		// Token: 0x04000049 RID: 73
		private readonly T _head;

		// Token: 0x0400004A RID: 74
		private readonly ImmutableStack<T> _tail;

		// Token: 0x020000BD RID: 189
		[Nullable(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator
		{
			// Token: 0x0600083B RID: 2107 RVA: 0x00015C50 File Offset: 0x00013E50
			internal Enumerator(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
				this._remainingStack = null;
			}

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x0600083C RID: 2108 RVA: 0x00015C6B File Offset: 0x00013E6B
			public T Current
			{
				get
				{
					if (this._remainingStack == null || this._remainingStack.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._remainingStack.Peek();
				}
			}

			// Token: 0x0600083D RID: 2109 RVA: 0x00015C94 File Offset: 0x00013E94
			public bool MoveNext()
			{
				if (this._remainingStack == null)
				{
					this._remainingStack = this._originalStack;
				}
				else if (!this._remainingStack.IsEmpty)
				{
					this._remainingStack = this._remainingStack.Pop();
				}
				return !this._remainingStack.IsEmpty;
			}

			// Token: 0x04000149 RID: 329
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x0400014A RID: 330
			private ImmutableStack<T> _remainingStack;
		}

		// Token: 0x020000BE RID: 190
		private sealed class EnumeratorObject : IEnumerator<!0>, IEnumerator, IDisposable
		{
			// Token: 0x0600083E RID: 2110 RVA: 0x00015CE3 File Offset: 0x00013EE3
			internal EnumeratorObject(ImmutableStack<T> stack)
			{
				Requires.NotNull<ImmutableStack<T>>(stack, "stack");
				this._originalStack = stack;
			}

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x0600083F RID: 2111 RVA: 0x00015CFD File Offset: 0x00013EFD
			public T Current
			{
				get
				{
					this.ThrowIfDisposed();
					if (this._remainingStack == null || this._remainingStack.IsEmpty)
					{
						throw new InvalidOperationException();
					}
					return this._remainingStack.Peek();
				}
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06000840 RID: 2112 RVA: 0x00015D2B File Offset: 0x00013F2B
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000841 RID: 2113 RVA: 0x00015D38 File Offset: 0x00013F38
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._remainingStack == null)
				{
					this._remainingStack = this._originalStack;
				}
				else if (!this._remainingStack.IsEmpty)
				{
					this._remainingStack = this._remainingStack.Pop();
				}
				return !this._remainingStack.IsEmpty;
			}

			// Token: 0x06000842 RID: 2114 RVA: 0x00015D8D File Offset: 0x00013F8D
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._remainingStack = null;
			}

			// Token: 0x06000843 RID: 2115 RVA: 0x00015D9C File Offset: 0x00013F9C
			public void Dispose()
			{
				this._disposed = true;
			}

			// Token: 0x06000844 RID: 2116 RVA: 0x00015DA5 File Offset: 0x00013FA5
			private void ThrowIfDisposed()
			{
				if (this._disposed)
				{
					Requires.FailObjectDisposed<ImmutableStack<T>.EnumeratorObject>(this);
				}
			}

			// Token: 0x0400014B RID: 331
			private readonly ImmutableStack<T> _originalStack;

			// Token: 0x0400014C RID: 332
			private ImmutableStack<T> _remainingStack;

			// Token: 0x0400014D RID: 333
			private bool _disposed;
		}
	}
}
