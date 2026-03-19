using System;
using Timberborn.ActivatorSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorWaterSourceSystemUI
{
	// Token: 0x02000005 RID: 5
	public class WaterSourceFlowPreview : BaseComponent, IAwakableComponent, IInitializableEntity, IActivableComponent, IWaterStrengthModifier
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000217F File Offset: 0x0000037F
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002187 File Offset: 0x00000387
		public bool IsEnabled { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002190 File Offset: 0x00000390
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002198 File Offset: 0x00000398
		public bool CanEnable { get; private set; }

		// Token: 0x0600000E RID: 14 RVA: 0x000021A1 File Offset: 0x000003A1
		public void Awake()
		{
			this._waterSource = base.GetComponent<WaterSource>();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021B0 File Offset: 0x000003B0
		public void InitializeEntity()
		{
			this._waterSource.AddWaterStrengthModifier(this);
			TimedComponentActivator component = base.GetComponent<TimedComponentActivator>();
			this.CanEnable = (!component || component.IsEnabled);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000021E7 File Offset: 0x000003E7
		public void Deactivate()
		{
			this.CanEnable = true;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021F0 File Offset: 0x000003F0
		public void Activate()
		{
			this.CanEnable = false;
			this.IsEnabled = false;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002200 File Offset: 0x00000400
		public float GetStrengthModifier()
		{
			if (this.CanEnable)
			{
				return (float)(this.IsEnabled ? 1 : 0);
			}
			return 1f;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000221D File Offset: 0x0000041D
		public void EnableFlowPreview()
		{
			this.IsEnabled = true;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002226 File Offset: 0x00000426
		public void DisableFlowPreview()
		{
			this.IsEnabled = false;
		}

		// Token: 0x04000005 RID: 5
		private WaterSource _waterSource;
	}
}
