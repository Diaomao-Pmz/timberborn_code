using System;
using System.Collections.Generic;

namespace Timberborn.Persistence
{
	// Token: 0x02000012 RID: 18
	public abstract class SaveConversions
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00002A0C File Offset: 0x00000C0C
		public static object Convert<T>(T value, IValueSerializer<T> serializer)
		{
			ValueSaver valueSaver = new ValueSaver();
			serializer.Serialize(value, valueSaver);
			return valueSaver.Value;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002A30 File Offset: 0x00000C30
		public static object[] ConvertList<T>(IReadOnlyCollection<T> values, IValueSerializer<T> serializer)
		{
			object[] array = new object[values.Count];
			int num = 0;
			foreach (T value in values)
			{
				array[num++] = SaveConversions.Convert<T>(value, serializer);
			}
			return array;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002A90 File Offset: 0x00000C90
		public static bool Deconvert<T>(object inputValue, IValueSerializer<T> serializer, out T value)
		{
			ValueLoader valueLoader = new ValueLoader(inputValue);
			Obsoletable<T> obsoletable = serializer.Deserialize(valueLoader);
			value = (obsoletable.Obsolete ? default(T) : obsoletable.Value);
			return !obsoletable.Obsolete;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public static List<T> DeconvertList<T>(IValueSerializer<T> serializer, IReadOnlyList<object> inputValues)
		{
			List<T> list = new List<T>(inputValues.Count);
			for (int i = 0; i < inputValues.Count; i++)
			{
				T item;
				if (SaveConversions.Deconvert<T>(inputValues[i], serializer, out item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000020B8 File Offset: 0x000002B8
		public SaveConversions()
		{
		}
	}
}
