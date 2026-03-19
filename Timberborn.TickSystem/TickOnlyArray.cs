using System;

namespace Timberborn.TickSystem
{
	// Token: 0x0200001A RID: 26
	public class TickOnlyArray<T>
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00002B48 File Offset: 0x00000D48
		public TickOnlyArray(int size, TickOnlyArrayService tickOnlyArrayService)
		{
			this._array = new T[size];
			this._tickOnlyArrayService = tickOnlyArrayService;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002B63 File Offset: 0x00000D63
		public ReadOnlySpan<T> GetReadOnlySpan()
		{
			return this.GetSpan();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002B70 File Offset: 0x00000D70
		public Span<T> GetSpan()
		{
			if (this._tickOnlyArrayService.AllowEdit)
			{
				return MemoryExtensions.AsSpan<T>(this._array);
			}
			throw new InvalidOperationException("Cannot access array outside of singleton Tick.");
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002B95 File Offset: 0x00000D95
		public T[] GetArray()
		{
			if (this._tickOnlyArrayService.AllowFullAccess)
			{
				return this._array;
			}
			throw new InvalidOperationException("Cannot access array outside of singleton StartParallelTick.");
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002BB5 File Offset: 0x00000DB5
		public void Resize(int newSize)
		{
			if (this._tickOnlyArrayService.AllowEdit)
			{
				Array.Resize<T>(ref this._array, newSize);
				return;
			}
			throw new InvalidOperationException("Cannot resize array outside of singleton Tick.");
		}

		// Token: 0x04000039 RID: 57
		public T[] _array;

		// Token: 0x0400003A RID: 58
		public readonly TickOnlyArrayService _tickOnlyArrayService;
	}
}
