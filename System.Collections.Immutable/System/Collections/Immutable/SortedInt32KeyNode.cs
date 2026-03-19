using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000051 RID: 81
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{_key} = {_value}")]
	internal sealed class SortedInt32KeyNode<[Nullable(2)] TValue> : IBinaryTree
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x00009CE5 File Offset: 0x00007EE5
		private SortedInt32KeyNode()
		{
			this._frozen = true;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x00009CF4 File Offset: 0x00007EF4
		private SortedInt32KeyNode(int key, TValue value, SortedInt32KeyNode<TValue> left, SortedInt32KeyNode<TValue> right, bool frozen = false)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(left, "left");
			Requires.NotNull<SortedInt32KeyNode<TValue>>(right, "right");
			this._key = key;
			this._value = value;
			this._left = left;
			this._right = right;
			this._frozen = frozen;
			this._height = checked(1 + Math.Max(left._height, right._height));
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00009D5E File Offset: 0x00007F5E
		public bool IsEmpty
		{
			get
			{
				return this._left == null;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00009D69 File Offset: 0x00007F69
		public int Height
		{
			get
			{
				return (int)this._height;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00009D71 File Offset: 0x00007F71
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public SortedInt32KeyNode<TValue> Left
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._left;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00009D79 File Offset: 0x00007F79
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public SortedInt32KeyNode<TValue> Right
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._right;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00009D81 File Offset: 0x00007F81
		[Nullable(2)]
		IBinaryTree IBinaryTree.Left
		{
			get
			{
				return this._left;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00009D89 File Offset: 0x00007F89
		[Nullable(2)]
		IBinaryTree IBinaryTree.Right
		{
			get
			{
				return this._right;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00009D91 File Offset: 0x00007F91
		int IBinaryTree.Count
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00009D98 File Offset: 0x00007F98
		[Nullable(new byte[]
		{
			0,
			1
		})]
		public KeyValuePair<int, TValue> Value
		{
			[return: Nullable(new byte[]
			{
				0,
				1
			})]
			get
			{
				return new KeyValuePair<int, TValue>(this._key, this._value);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00009DAB File Offset: 0x00007FAB
		internal IEnumerable<TValue> Values
		{
			get
			{
				foreach (KeyValuePair<int, TValue> keyValuePair in this)
				{
					yield return keyValuePair.Value;
				}
				SortedInt32KeyNode<TValue>.Enumerator enumerator = default(SortedInt32KeyNode<TValue>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00009DBB File Offset: 0x00007FBB
		[NullableContext(0)]
		public SortedInt32KeyNode<TValue>.Enumerator GetEnumerator()
		{
			return new SortedInt32KeyNode<TValue>.Enumerator(this);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00009DC3 File Offset: 0x00007FC3
		internal SortedInt32KeyNode<TValue> SetItem(int key, TValue value, IEqualityComparer<TValue> valueComparer, out bool replacedExistingValue, out bool mutated)
		{
			Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, "valueComparer");
			return this.SetOrAdd(key, value, valueComparer, true, out replacedExistingValue, out mutated);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00009DDE File Offset: 0x00007FDE
		internal SortedInt32KeyNode<TValue> Remove(int key, out bool mutated)
		{
			return this.RemoveRecursive(key, out mutated);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00009DE8 File Offset: 0x00007FE8
		[NullableContext(2)]
		internal TValue GetValueOrDefault(int key)
		{
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			while (!sortedInt32KeyNode.IsEmpty)
			{
				if (key == sortedInt32KeyNode._key)
				{
					return sortedInt32KeyNode._value;
				}
				if (key > sortedInt32KeyNode._key)
				{
					sortedInt32KeyNode = sortedInt32KeyNode._right;
				}
				else
				{
					sortedInt32KeyNode = sortedInt32KeyNode._left;
				}
			}
			return default(TValue);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00009E34 File Offset: 0x00008034
		internal bool TryGetValue(int key, [MaybeNullWhen(false)] out TValue value)
		{
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			while (!sortedInt32KeyNode.IsEmpty)
			{
				if (key == sortedInt32KeyNode._key)
				{
					value = sortedInt32KeyNode._value;
					return true;
				}
				if (key > sortedInt32KeyNode._key)
				{
					sortedInt32KeyNode = sortedInt32KeyNode._right;
				}
				else
				{
					sortedInt32KeyNode = sortedInt32KeyNode._left;
				}
			}
			value = default(TValue);
			return false;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00009E88 File Offset: 0x00008088
		internal void Freeze([Nullable(new byte[]
		{
			2,
			0,
			1
		})] Action<KeyValuePair<int, TValue>> freezeAction = null)
		{
			if (!this._frozen)
			{
				if (freezeAction != null)
				{
					freezeAction(new KeyValuePair<int, TValue>(this._key, this._value));
				}
				this._left.Freeze(freezeAction);
				this._right.Freeze(freezeAction);
				this._frozen = true;
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00009ED8 File Offset: 0x000080D8
		private static SortedInt32KeyNode<TValue> RotateLeft(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._right.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> right = tree._right;
			return right.Mutate(tree.Mutate(null, right._left), null);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00009F1C File Offset: 0x0000811C
		private static SortedInt32KeyNode<TValue> RotateRight(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._left.IsEmpty)
			{
				return tree;
			}
			SortedInt32KeyNode<TValue> left = tree._left;
			return left.Mutate(null, tree.Mutate(left._right, null));
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00009F5E File Offset: 0x0000815E
		private static SortedInt32KeyNode<TValue> DoubleLeft(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._right.IsEmpty)
			{
				return tree;
			}
			return SortedInt32KeyNode<TValue>.RotateLeft(tree.Mutate(null, SortedInt32KeyNode<TValue>.RotateRight(tree._right)));
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00009F91 File Offset: 0x00008191
		private static SortedInt32KeyNode<TValue> DoubleRight(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (tree._left.IsEmpty)
			{
				return tree;
			}
			return SortedInt32KeyNode<TValue>.RotateRight(tree.Mutate(SortedInt32KeyNode<TValue>.RotateLeft(tree._left), null));
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00009FC4 File Offset: 0x000081C4
		private static int Balance(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return (int)(tree._right._height - tree._left._height);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00009FE8 File Offset: 0x000081E8
		private static bool IsRightHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) >= 2;
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000A001 File Offset: 0x00008201
		private static bool IsLeftHeavy(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			return SortedInt32KeyNode<TValue>.Balance(tree) <= -2;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000A01C File Offset: 0x0000821C
		private static SortedInt32KeyNode<TValue> MakeBalanced(SortedInt32KeyNode<TValue> tree)
		{
			Requires.NotNull<SortedInt32KeyNode<TValue>>(tree, "tree");
			if (SortedInt32KeyNode<TValue>.IsRightHeavy(tree))
			{
				if (SortedInt32KeyNode<TValue>.Balance(tree._right) >= 0)
				{
					return SortedInt32KeyNode<TValue>.RotateLeft(tree);
				}
				return SortedInt32KeyNode<TValue>.DoubleLeft(tree);
			}
			else
			{
				if (!SortedInt32KeyNode<TValue>.IsLeftHeavy(tree))
				{
					return tree;
				}
				if (SortedInt32KeyNode<TValue>.Balance(tree._left) <= 0)
				{
					return SortedInt32KeyNode<TValue>.RotateRight(tree);
				}
				return SortedInt32KeyNode<TValue>.DoubleRight(tree);
			}
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000A080 File Offset: 0x00008280
		private SortedInt32KeyNode<TValue> SetOrAdd(int key, TValue value, IEqualityComparer<TValue> valueComparer, bool overwriteExistingValue, out bool replacedExistingValue, out bool mutated)
		{
			replacedExistingValue = false;
			if (this.IsEmpty)
			{
				mutated = true;
				return new SortedInt32KeyNode<TValue>(key, value, this, this, false);
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			if (key > this._key)
			{
				SortedInt32KeyNode<TValue> right = this._right.SetOrAdd(key, value, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(null, right);
				}
			}
			else if (key < this._key)
			{
				SortedInt32KeyNode<TValue> left = this._left.SetOrAdd(key, value, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(left, null);
				}
			}
			else
			{
				if (valueComparer.Equals(this._value, value))
				{
					mutated = false;
					return this;
				}
				if (!overwriteExistingValue)
				{
					throw new ArgumentException(SR.Format(SR.DuplicateKey, key));
				}
				mutated = true;
				replacedExistingValue = true;
				sortedInt32KeyNode = new SortedInt32KeyNode<TValue>(key, value, this._left, this._right, false);
			}
			if (!mutated)
			{
				return sortedInt32KeyNode;
			}
			return SortedInt32KeyNode<TValue>.MakeBalanced(sortedInt32KeyNode);
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000A164 File Offset: 0x00008364
		private SortedInt32KeyNode<TValue> RemoveRecursive(int key, out bool mutated)
		{
			if (this.IsEmpty)
			{
				mutated = false;
				return this;
			}
			SortedInt32KeyNode<TValue> sortedInt32KeyNode = this;
			if (key == this._key)
			{
				mutated = true;
				if (this._right.IsEmpty && this._left.IsEmpty)
				{
					sortedInt32KeyNode = SortedInt32KeyNode<TValue>.EmptyNode;
				}
				else if (this._right.IsEmpty && !this._left.IsEmpty)
				{
					sortedInt32KeyNode = this._left;
				}
				else if (!this._right.IsEmpty && this._left.IsEmpty)
				{
					sortedInt32KeyNode = this._right;
				}
				else
				{
					SortedInt32KeyNode<TValue> sortedInt32KeyNode2 = this._right;
					while (!sortedInt32KeyNode2._left.IsEmpty)
					{
						sortedInt32KeyNode2 = sortedInt32KeyNode2._left;
					}
					bool flag;
					SortedInt32KeyNode<TValue> right = this._right.Remove(sortedInt32KeyNode2._key, out flag);
					sortedInt32KeyNode = sortedInt32KeyNode2.Mutate(this._left, right);
				}
			}
			else if (key < this._key)
			{
				SortedInt32KeyNode<TValue> left = this._left.Remove(key, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(left, null);
				}
			}
			else
			{
				SortedInt32KeyNode<TValue> right2 = this._right.Remove(key, out mutated);
				if (mutated)
				{
					sortedInt32KeyNode = this.Mutate(null, right2);
				}
			}
			if (!sortedInt32KeyNode.IsEmpty)
			{
				return SortedInt32KeyNode<TValue>.MakeBalanced(sortedInt32KeyNode);
			}
			return sortedInt32KeyNode;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000A298 File Offset: 0x00008498
		private SortedInt32KeyNode<TValue> Mutate(SortedInt32KeyNode<TValue> left = null, SortedInt32KeyNode<TValue> right = null)
		{
			if (this._frozen)
			{
				return new SortedInt32KeyNode<TValue>(this._key, this._value, left ?? this._left, right ?? this._right, false);
			}
			if (left != null)
			{
				this._left = left;
			}
			if (right != null)
			{
				this._right = right;
			}
			this._height = checked(1 + Math.Max(this._left._height, this._right._height));
			return this;
		}

		// Token: 0x04000052 RID: 82
		internal static readonly SortedInt32KeyNode<TValue> EmptyNode = new SortedInt32KeyNode<TValue>();

		// Token: 0x04000053 RID: 83
		private readonly int _key;

		// Token: 0x04000054 RID: 84
		private readonly TValue _value;

		// Token: 0x04000055 RID: 85
		private bool _frozen;

		// Token: 0x04000056 RID: 86
		private byte _height;

		// Token: 0x04000057 RID: 87
		private SortedInt32KeyNode<TValue> _left;

		// Token: 0x04000058 RID: 88
		private SortedInt32KeyNode<TValue> _right;

		// Token: 0x020000BF RID: 191
		[NullableContext(0)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public struct Enumerator : IEnumerator<KeyValuePair<int, TValue>>, IEnumerator, IDisposable, ISecurePooledObjectUser
		{
			// Token: 0x06000845 RID: 2117 RVA: 0x00015DB8 File Offset: 0x00013FB8
			[NullableContext(1)]
			internal Enumerator(SortedInt32KeyNode<TValue> root)
			{
				Requires.NotNull<SortedInt32KeyNode<TValue>>(root, "root");
				this._root = root;
				this._current = null;
				this._poolUserId = SecureObjectPool.NewId();
				this._stack = null;
				if (!this._root.IsEmpty)
				{
					if (!SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator>.TryTake(this, out this._stack))
					{
						this._stack = SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator>.PrepNew(this, new Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>(root.Height));
					}
					this.PushLeft(this._root);
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x06000846 RID: 2118 RVA: 0x00015E38 File Offset: 0x00014038
			[Nullable(new byte[]
			{
				0,
				1
			})]
			public KeyValuePair<int, TValue> Current
			{
				[return: Nullable(new byte[]
				{
					0,
					1
				})]
				get
				{
					this.ThrowIfDisposed();
					if (this._current != null)
					{
						return this._current.Value;
					}
					throw new InvalidOperationException();
				}
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x06000847 RID: 2119 RVA: 0x00015E59 File Offset: 0x00014059
			int ISecurePooledObjectUser.PoolUserId
			{
				get
				{
					return this._poolUserId;
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000848 RID: 2120 RVA: 0x00015E61 File Offset: 0x00014061
			[Nullable(1)]
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000849 RID: 2121 RVA: 0x00015E70 File Offset: 0x00014070
			public void Dispose()
			{
				this._root = null;
				this._current = null;
				Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack;
				if (this._stack != null && this._stack.TryUse<SortedInt32KeyNode<TValue>.Enumerator>(ref this, out stack))
				{
					stack.ClearFastWhenEmpty<RefAsValueType<SortedInt32KeyNode<TValue>>>();
					SecureObjectPool<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>, SortedInt32KeyNode<TValue>.Enumerator>.TryAdd(this, this._stack);
				}
				this._stack = null;
			}

			// Token: 0x0600084A RID: 2122 RVA: 0x00015EC4 File Offset: 0x000140C4
			public bool MoveNext()
			{
				this.ThrowIfDisposed();
				if (this._stack != null)
				{
					Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
					if (stack.Count > 0)
					{
						SortedInt32KeyNode<TValue> value = stack.Pop().Value;
						this._current = value;
						this.PushLeft(value.Right);
						return true;
					}
				}
				this._current = null;
				return false;
			}

			// Token: 0x0600084B RID: 2123 RVA: 0x00015F1E File Offset: 0x0001411E
			public void Reset()
			{
				this.ThrowIfDisposed();
				this._current = null;
				if (this._stack != null)
				{
					this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this).ClearFastWhenEmpty<RefAsValueType<SortedInt32KeyNode<TValue>>>();
					this.PushLeft(this._root);
				}
			}

			// Token: 0x0600084C RID: 2124 RVA: 0x00015F52 File Offset: 0x00014152
			internal void ThrowIfDisposed()
			{
				if (this._root == null || (this._stack != null && !this._stack.IsOwned<SortedInt32KeyNode<TValue>.Enumerator>(ref this)))
				{
					Requires.FailObjectDisposed<SortedInt32KeyNode<TValue>.Enumerator>(this);
				}
			}

			// Token: 0x0600084D RID: 2125 RVA: 0x00015F80 File Offset: 0x00014180
			private void PushLeft(SortedInt32KeyNode<TValue> node)
			{
				Requires.NotNull<SortedInt32KeyNode<TValue>>(node, "node");
				Stack<RefAsValueType<SortedInt32KeyNode<TValue>>> stack = this._stack.Use<SortedInt32KeyNode<TValue>.Enumerator>(ref this);
				while (!node.IsEmpty)
				{
					stack.Push(new RefAsValueType<SortedInt32KeyNode<TValue>>(node));
					node = node.Left;
				}
			}

			// Token: 0x0400014E RID: 334
			private readonly int _poolUserId;

			// Token: 0x0400014F RID: 335
			private SortedInt32KeyNode<TValue> _root;

			// Token: 0x04000150 RID: 336
			private SecurePooledObject<Stack<RefAsValueType<SortedInt32KeyNode<TValue>>>> _stack;

			// Token: 0x04000151 RID: 337
			private SortedInt32KeyNode<TValue> _current;
		}
	}
}
