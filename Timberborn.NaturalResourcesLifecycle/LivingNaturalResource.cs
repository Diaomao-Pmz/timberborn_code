using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.Persistence;
using Timberborn.WorldPersistence;

namespace Timberborn.NaturalResourcesLifecycle
{
	// Token: 0x02000007 RID: 7
	public class LivingNaturalResource : BaseComponent, IPersistentEntity, IInitializableEntity
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600001C RID: 28 RVA: 0x00002414 File Offset: 0x00000614
		// (remove) Token: 0x0600001D RID: 29 RVA: 0x0000244C File Offset: 0x0000064C
		public event EventHandler Died;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600001E RID: 30 RVA: 0x00002484 File Offset: 0x00000684
		// (remove) Token: 0x0600001F RID: 31 RVA: 0x000024BC File Offset: 0x000006BC
		public event EventHandler ReversedDeath;

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000024F1 File Offset: 0x000006F1
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000024F9 File Offset: 0x000006F9
		public bool IsDead { get; private set; }

		// Token: 0x06000022 RID: 34 RVA: 0x00002502 File Offset: 0x00000702
		public void InitializeEntity()
		{
			if (this.IsDead)
			{
				this.InternalDie();
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002512 File Offset: 0x00000712
		public void Save(IEntitySaver entitySaver)
		{
			if (this.IsDead)
			{
				entitySaver.GetComponent(LivingNaturalResource.LivingNaturalResourceKey).Set(LivingNaturalResource.IsDeadKey, this.IsDead);
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002538 File Offset: 0x00000738
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(LivingNaturalResource.LivingNaturalResourceKey, out objectLoader))
			{
				this.IsDead = objectLoader.Get(LivingNaturalResource.IsDeadKey);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002565 File Offset: 0x00000765
		public void Die()
		{
			if (!this.IsDead)
			{
				this.InternalDie();
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002575 File Offset: 0x00000775
		public void ReverseDeath()
		{
			if (this.IsDead)
			{
				this.IsDead = false;
				EventHandler reversedDeath = this.ReversedDeath;
				if (reversedDeath == null)
				{
					return;
				}
				reversedDeath(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000259C File Offset: 0x0000079C
		public void InternalDie()
		{
			this.IsDead = true;
			EventHandler died = this.Died;
			if (died == null)
			{
				return;
			}
			died(this, EventArgs.Empty);
		}

		// Token: 0x0400000F RID: 15
		public static readonly ComponentKey LivingNaturalResourceKey = new ComponentKey("LivingNaturalResource");

		// Token: 0x04000010 RID: 16
		public static readonly PropertyKey<bool> IsDeadKey = new PropertyKey<bool>("IsDead");
	}
}
