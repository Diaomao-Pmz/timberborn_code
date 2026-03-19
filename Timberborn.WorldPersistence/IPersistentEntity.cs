using System;

namespace Timberborn.WorldPersistence
{
	// Token: 0x02000010 RID: 16
	public interface IPersistentEntity
	{
		// Token: 0x0600002B RID: 43
		void Save(IEntitySaver entitySaver);

		// Token: 0x0600002C RID: 44
		void Load(IEntityLoader entityLoader);
	}
}
