using System;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.Buildings
{
	// Token: 0x02000017 RID: 23
	public class BuildingSoundController : IPersistentEntity
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000035B0 File Offset: 0x000017B0
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000035B8 File Offset: 0x000017B8
		public bool PlaySound { get; private set; } = true;

		// Token: 0x060000AB RID: 171 RVA: 0x000035C1 File Offset: 0x000017C1
		public void Save(IEntitySaver entitySaver)
		{
			if (!this.PlaySound)
			{
				entitySaver.GetComponent(BuildingSoundController.BuildingSoundControllerKey).Set(BuildingSoundController.PlaySoundKey, this.PlaySound);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000035E8 File Offset: 0x000017E8
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(BuildingSoundController.BuildingSoundControllerKey, out objectLoader))
			{
				this.PlaySound = objectLoader.Get(BuildingSoundController.PlaySoundKey);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003615 File Offset: 0x00001815
		public void EnableSound()
		{
			this.PlaySound = true;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000361E File Offset: 0x0000181E
		public void DisableSound()
		{
			this.PlaySound = false;
		}

		// Token: 0x04000035 RID: 53
		public static readonly ComponentKey BuildingSoundControllerKey = new ComponentKey("BuildingSoundController");

		// Token: 0x04000036 RID: 54
		public static readonly PropertyKey<bool> PlaySoundKey = new PropertyKey<bool>("PlaySound");
	}
}
