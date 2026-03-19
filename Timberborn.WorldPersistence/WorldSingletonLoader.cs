using System;
using Timberborn.Persistence;

namespace Timberborn.WorldPersistence
{
	// Token: 0x0200001E RID: 30
	public class WorldSingletonLoader : ISingletonLoader
	{
		// Token: 0x06000053 RID: 83 RVA: 0x00002B0B File Offset: 0x00000D0B
		public WorldSingletonLoader(ISerializedWorldSupplier serializedWorldSupplier)
		{
			this._serializedWorldSupplier = serializedWorldSupplier;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B1A File Offset: 0x00000D1A
		public IObjectLoader GetSingleton(SingletonKey key)
		{
			return new ObjectLoader(this._serializedWorldSupplier.Get().GetSingleton(key.Name));
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B38 File Offset: 0x00000D38
		public bool TryGetSingleton(SingletonKey key, out IObjectLoader objectLoader)
		{
			if (this.HasSingleton(key))
			{
				objectLoader = this.GetSingleton(key);
				return true;
			}
			objectLoader = null;
			return false;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B52 File Offset: 0x00000D52
		public bool HasSingleton(SingletonKey key)
		{
			return this._serializedWorldSupplier.Get().HasSingleton(key.Name);
		}

		// Token: 0x04000022 RID: 34
		public readonly ISerializedWorldSupplier _serializedWorldSupplier;
	}
}
