using System;
using Timberborn.Persistence;
using Timberborn.WorldSerialization;

namespace Timberborn.WorldPersistence
{
	// Token: 0x0200001A RID: 26
	public class SingletonSaver : ISingletonSaver
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000288D File Offset: 0x00000A8D
		public SingletonSaver(SerializedWorld serializedWorld)
		{
			this._serializedWorld = serializedWorld;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000289C File Offset: 0x00000A9C
		public IObjectSaver GetSingleton(SingletonKey key)
		{
			return new ObjectSaver(this._serializedWorld.GetOrAddSingleton(key.Name));
		}

		// Token: 0x04000018 RID: 24
		public readonly SerializedWorld _serializedWorld;
	}
}
