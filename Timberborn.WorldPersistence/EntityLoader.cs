using System;
using Timberborn.Persistence;
using Timberborn.WorldSerialization;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000009 RID: 9
	public class EntityLoader : IEntityLoader
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000023D0 File Offset: 0x000005D0
		public EntityLoader(SerializedEntity serializedEntity)
		{
			this._serializedEntity = serializedEntity;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023DF File Offset: 0x000005DF
		public IObjectLoader GetComponent(ComponentKey key)
		{
			return new ObjectLoader(this._serializedEntity.GetComponent(key.Name).Value);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023FD File Offset: 0x000005FD
		public IObjectLoader GetComponent(ComponentKey key, string suffix)
		{
			return this.GetComponent(key.AddSuffix(suffix));
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000240D File Offset: 0x0000060D
		public bool TryGetComponent(ComponentKey key, out IObjectLoader objectLoader)
		{
			if (this.HasComponent(key))
			{
				objectLoader = this.GetComponent(key);
				return true;
			}
			objectLoader = null;
			return false;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002427 File Offset: 0x00000627
		public bool TryGetComponent(ComponentKey key, string suffix, out IObjectLoader objectLoader)
		{
			if (this.HasComponent(key, suffix))
			{
				objectLoader = this.GetComponent(key, suffix);
				return true;
			}
			objectLoader = null;
			return false;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002443 File Offset: 0x00000643
		public bool HasComponent(ComponentKey key)
		{
			return this._serializedEntity.HasComponent(key.Name);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002457 File Offset: 0x00000657
		public bool HasComponent(ComponentKey key, string suffix)
		{
			return this.HasComponent(key.AddSuffix(suffix));
		}

		// Token: 0x0400000C RID: 12
		public readonly SerializedEntity _serializedEntity;
	}
}
