using System;
using Timberborn.EntitySystem;
using Timberborn.NaturalResourcesModelSystem;
using Timberborn.SingletonSystem;

namespace Timberborn.MapEditorNaturalResources
{
	// Token: 0x02000006 RID: 6
	public class NaturalResourceLayerService
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000022F0 File Offset: 0x000004F0
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000022F8 File Offset: 0x000004F8
		public bool Enabled { get; private set; } = true;

		// Token: 0x06000012 RID: 18 RVA: 0x00002301 File Offset: 0x00000501
		public NaturalResourceLayerService(EntityComponentRegistry entityComponentRegistry, EventBus eventBus)
		{
			this._entityComponentRegistry = entityComponentRegistry;
			this._eventBus = eventBus;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002320 File Offset: 0x00000520
		public void Enable()
		{
			if (!this.Enabled)
			{
				this.Enabled = true;
				foreach (NaturalResourceModel naturalResourceModel in this._entityComponentRegistry.GetEnabled<NaturalResourceModel>())
				{
					naturalResourceModel.Show();
				}
				this._eventBus.Post(new NaturalResourceLayerChangedEvent());
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002390 File Offset: 0x00000590
		public void Disable()
		{
			if (this.Enabled)
			{
				this.Enabled = false;
				foreach (NaturalResourceModel naturalResourceModel in this._entityComponentRegistry.GetEnabled<NaturalResourceModel>())
				{
					naturalResourceModel.Hide();
				}
				this._eventBus.Post(new NaturalResourceLayerChangedEvent());
			}
		}

		// Token: 0x04000006 RID: 6
		private readonly EntityComponentRegistry _entityComponentRegistry;

		// Token: 0x04000007 RID: 7
		private readonly EventBus _eventBus;
	}
}
