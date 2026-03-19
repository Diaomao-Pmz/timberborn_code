using System;
using System.Text;
using Timberborn.Persistence;

namespace Timberborn.MapIndexSystem
{
	// Token: 0x0200000B RID: 11
	public abstract class PackedListSerializer<T> : IValueSerializer<PackedList<T>>
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000028E8 File Offset: 0x00000AE8
		public void Serialize(PackedList<T> packedList, IValueSaver valueSaver)
		{
			this._reusableStringBuilder.Clear();
			T[] array = packedList.Array;
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				if (i > 0)
				{
					this._reusableStringBuilder.Append(PackedListSerializer<T>.Separator);
				}
				this.Serialize(array[i], this._reusableStringBuilder);
			}
			valueSaver.AsObject().Set(PackedListSerializer<T>.ArrayKey, this._reusableStringBuilder.ToString());
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000295C File Offset: 0x00000B5C
		public Obsoletable<PackedList<T>> Deserialize(IValueLoader valueLoader)
		{
			string[] array = valueLoader.AsObject().Get(PackedListSerializer<T>.ArrayKey).Split(PackedListSerializer<T>.Separator, StringSplitOptions.None);
			int num = array.Length;
			T[] array2 = new T[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = this.Deserialize(array[i]);
			}
			return new PackedList<T>(array2);
		}

		// Token: 0x06000033 RID: 51
		public abstract void Serialize(T value, StringBuilder stringBuilder);

		// Token: 0x06000034 RID: 52
		public abstract T Deserialize(string value);

		// Token: 0x06000035 RID: 53 RVA: 0x000029B7 File Offset: 0x00000BB7
		public PackedListSerializer()
		{
		}

		// Token: 0x04000014 RID: 20
		public static readonly PropertyKey<string> ArrayKey = new PropertyKey<string>("Array");

		// Token: 0x04000015 RID: 21
		public static readonly char Separator = ' ';

		// Token: 0x04000016 RID: 22
		public readonly StringBuilder _reusableStringBuilder = new StringBuilder();
	}
}
