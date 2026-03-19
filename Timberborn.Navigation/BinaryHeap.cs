using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Timberborn.Navigation
{
	// Token: 0x02000009 RID: 9
	public class BinaryHeap<TValue> where TValue : IOrderable<TValue>
	{
		// Token: 0x0600003F RID: 63 RVA: 0x00002A97 File Offset: 0x00000C97
		public BinaryHeap(int initialCapacity = 0)
		{
			if (initialCapacity < 0)
			{
				throw new ArgumentOutOfRangeException(string.Format("{0} can't be negative but is: {1}", "initialCapacity", initialCapacity));
			}
			this._nodes = new List<TValue>(initialCapacity);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002ACC File Offset: 0x00000CCC
		public void Push(TValue value)
		{
			int nextFreeIndex = this._nextFreeIndex;
			this._nextFreeIndex = nextFreeIndex + 1;
			int num = nextFreeIndex;
			this.AddToNodes(value, num);
			this.HeapifyFromIndexToBeginning(num);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002AFA File Offset: 0x00000CFA
		public TValue Peek()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("Can't peek because it's empty");
			}
			return this._nodes[0];
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002B1B File Offset: 0x00000D1B
		public TValue Pop()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("Can't pop because it's empty");
			}
			TValue result = this._nodes[0];
			this.DeleteRoot();
			return result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002B42 File Offset: 0x00000D42
		public void Clear()
		{
			this._nextFreeIndex = 0;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B4B File Offset: 0x00000D4B
		public bool IsEmpty()
		{
			return this._nextFreeIndex == 0;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002B56 File Offset: 0x00000D56
		public void AddToNodes(TValue value, int index)
		{
			if (index == this._nodes.Count)
			{
				this._nodes.Add(value);
				return;
			}
			this._nodes[index] = value;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002B80 File Offset: 0x00000D80
		public void HeapifyFromIndexToBeginning(int startingIndex)
		{
			int num;
			for (int i = startingIndex; i > 0; i = num)
			{
				num = (i - 1) / 2;
				if (this.NodeAtIsSmallerThanNodeAt(num, i))
				{
					return;
				}
				this.SwapNodes(num, i);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public void DeleteRoot()
		{
			int index = this._nextFreeIndex - 1;
			this._nodes[0] = this._nodes[index];
			this._nextFreeIndex--;
			this.HeapifyFromIndexToEnd(0);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002BF4 File Offset: 0x00000DF4
		public void HeapifyFromIndexToEnd(int startingIndex)
		{
			int num = startingIndex;
			if (this.IndexIsInBounds(num))
			{
				for (;;)
				{
					int num2 = num;
					int num3 = 2 * num + 1;
					int num4 = 2 * num + 2;
					if (this.<HeapifyFromIndexToEnd>g__ChildAtIsSmallerThanNodeAt|11_0(num3, num2))
					{
						num2 = num3;
					}
					if (this.<HeapifyFromIndexToEnd>g__ChildAtIsSmallerThanNodeAt|11_0(num4, num2))
					{
						num2 = num4;
					}
					if (num2 == num)
					{
						break;
					}
					this.SwapNodes(num2, num);
					num = num2;
				}
				return;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C43 File Offset: 0x00000E43
		public bool IndexIsInBounds(int index)
		{
			return index < this._nextFreeIndex;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002C50 File Offset: 0x00000E50
		public bool NodeAtIsSmallerThanNodeAt(int index1, int index2)
		{
			TValue tvalue = this._nodes[index1];
			return tvalue.IsLessThan(this._nodes[index2]);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002C84 File Offset: 0x00000E84
		public void SwapNodes(int index1, int index2)
		{
			List<TValue> nodes = this._nodes;
			List<TValue> nodes2 = this._nodes;
			TValue value = this._nodes[index2];
			TValue value2 = this._nodes[index1];
			nodes[index1] = value;
			nodes2[index2] = value2;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002CD3 File Offset: 0x00000ED3
		[CompilerGenerated]
		private bool <HeapifyFromIndexToEnd>g__ChildAtIsSmallerThanNodeAt|11_0(int childIndex, int nodeIndex)
		{
			return this.IndexIsInBounds(childIndex) && this.NodeAtIsSmallerThanNodeAt(childIndex, nodeIndex);
		}

		// Token: 0x0400001B RID: 27
		public readonly List<TValue> _nodes;

		// Token: 0x0400001C RID: 28
		public int _nextFreeIndex;
	}
}
