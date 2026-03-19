using System;
using Timberborn.Persistence;
using Timberborn.WorldSerialization;

namespace Timberborn.WorldPersistence
{
	// Token: 0x0200000A RID: 10
	public class EntitySaver : IEntitySaver
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00002467 File Offset: 0x00000667
		public EntitySaver(SerializedEntity serializedEntity)
		{
			this._serializedEntity = serializedEntity;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002476 File Offset: 0x00000676
		public IObjectSaver GetComponent(ComponentKey componentKey)
		{
			return new ObjectSaver(this._serializedEntity.GetOrAddComponent(componentKey.Name).Value);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002494 File Offset: 0x00000694
		public IObjectSaver GetComponent(ComponentKey componentKey, string suffix)
		{
			return this.GetComponent(componentKey.AddSuffix(suffix));
		}

		// Token: 0x0400000D RID: 13
		public readonly SerializedEntity _serializedEntity;
	}
}
