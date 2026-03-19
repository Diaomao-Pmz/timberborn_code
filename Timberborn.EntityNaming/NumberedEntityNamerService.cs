using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.Common;
using Timberborn.Persistence;
using Timberborn.SingletonSystem;
using Timberborn.WorldPersistence;

namespace Timberborn.EntityNaming
{
	// Token: 0x02000011 RID: 17
	public class NumberedEntityNamerService : ILoadableSingleton, ISaveableSingleton
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002A70 File Offset: 0x00000C70
		public NumberedEntityNamerService(ISingletonLoader singletonLoader, SerializedEntityNameNumberSerializer serializedEntityNameNumberSerializer)
		{
			this._singletonLoader = singletonLoader;
			this._serializedEntityNameNumberSerializer = serializedEntityNameNumberSerializer;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A91 File Offset: 0x00000C91
		public void Save(ISingletonSaver singletonSaver)
		{
			singletonSaver.GetSingleton(NumberedEntityNamerService.SingletonKey).Set<SerializedEntityNameNumber>(NumberedEntityNamerService.NextNumbersKey, this.SerializeNextNumbers(), this._serializedEntityNameNumberSerializer);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002ABC File Offset: 0x00000CBC
		public void Load()
		{
			IObjectLoader objectLoader;
			if (this._singletonLoader.TryGetSingleton(NumberedEntityNamerService.SingletonKey, out objectLoader))
			{
				this._nextNumbers.AddRange(NumberedEntityNamerService.DeserializeNextNumbers(objectLoader.Get<SerializedEntityNameNumber>(NumberedEntityNamerService.NextNumbersKey, this._serializedEntityNameNumberSerializer)));
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002B00 File Offset: 0x00000D00
		public int GenerateNumber(string group)
		{
			int num2;
			int num = this._nextNumbers.TryGetValue(group, out num2) ? (num2 + 1) : 1;
			this._nextNumbers[group] = num;
			return num;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002B32 File Offset: 0x00000D32
		public ImmutableArray<SerializedEntityNameNumber> SerializeNextNumbers()
		{
			return (from nextNumber in this._nextNumbers
			select new SerializedEntityNameNumber(nextNumber.Key, nextNumber.Value)).ToImmutableArray<SerializedEntityNameNumber>();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002B63 File Offset: 0x00000D63
		public static IEnumerable<KeyValuePair<string, int>> DeserializeNextNumbers(List<SerializedEntityNameNumber> nextPersistentNameNumbers)
		{
			return from nextNumber in nextPersistentNameNumbers
			select new KeyValuePair<string, int>(nextNumber.Group, nextNumber.NextNumber);
		}

		// Token: 0x04000025 RID: 37
		public static readonly SingletonKey SingletonKey = new SingletonKey("NumberedEntityNamerService");

		// Token: 0x04000026 RID: 38
		public static readonly ListKey<SerializedEntityNameNumber> NextNumbersKey = new ListKey<SerializedEntityNameNumber>("NextNumbers");

		// Token: 0x04000027 RID: 39
		public readonly ISingletonLoader _singletonLoader;

		// Token: 0x04000028 RID: 40
		public readonly SerializedEntityNameNumberSerializer _serializedEntityNameNumberSerializer;

		// Token: 0x04000029 RID: 41
		public readonly Dictionary<string, int> _nextNumbers = new Dictionary<string, int>();
	}
}
