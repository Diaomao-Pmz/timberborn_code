using System;

namespace Timberborn.Common
{
	// Token: 0x0200002A RID: 42
	public readonly struct ReadOnlyJaggedArray<T>
	{
		// Token: 0x0600009B RID: 155 RVA: 0x000035E8 File Offset: 0x000017E8
		public ReadOnlyJaggedArray(T[][] array)
		{
			this._array = array;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000035F1 File Offset: 0x000017F1
		public ref readonly T Get(int row, int column)
		{
			return ref this._array[row][column];
		}

		// Token: 0x04000043 RID: 67
		public readonly T[][] _array;
	}
}
