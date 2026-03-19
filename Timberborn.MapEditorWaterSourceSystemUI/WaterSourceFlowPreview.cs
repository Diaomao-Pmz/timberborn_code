using System;
using Timberborn.ActivatorSystem;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.WaterSourceSystem;

namespace Timberborn.MapEditorWaterSourceSystemUI
{
	// Token: 0x02000007 RID: 7
	public class WaterSourceFlowPreview : BaseComponent, IAwakableComponent, IInitializableEntity, IActivableComponent, IWaterStrengthModifier
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021A8 File Offset: 0x000003A8
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000021B0 File Offset: 0x000003B0
		public bool IsEnabled { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021B9 File Offset: 0x000003B9
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000021C1 File Offset: 0x000003C1
		public bool CanEnable { get; private set; }

		// Token: 0x06000010 RID: 16 RVA: 0x000021CA File Offset: 0x000003CA
		public void Awake()
		{
			this._waterSource = base.GetComponent<WaterSource>();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021D8 File Offset: 0x000003D8
		public void InitializeEntity()
		{
			this._waterSource.AddWaterStrengthModifier(this);
			TimedComponentActivator component = base.GetComponent<TimedComponentActivator>();
			this.CanEnable = (!component || component.IsEnabled);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000220F File Offset: 0x0000040F
		public void Deactivate()
		{
			this.CanEnable = true;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002218 File Offset: 0x00000418
		public void Activate()
		{
			this.CanEnable = false;
			this.IsEnabled = false;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002228 File Offset: 0x00000428
		public float GetStrengthModifier()
		{
			if (this.CanEnable)
			{
				return (float)(this.IsEnabled ? 1 : 0);
			}
			return 1f;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002245 File Offset: 0x00000445
		public void EnableFlowPreview()
		{
			this.IsEnabled = true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000224E File Offset: 0x0000044E
		public void DisableFlowPreview()
		{
			this.IsEnabled = false;
		}

		// Token: 0x0400000B RID: 11
		public WaterSource _waterSource;
	}
}
