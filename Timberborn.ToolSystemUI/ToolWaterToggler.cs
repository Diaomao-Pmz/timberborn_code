using System;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using Timberborn.WaterSystemRendering;

namespace Timberborn.ToolSystemUI
{
	// Token: 0x02000010 RID: 16
	public class ToolWaterToggler : ILoadableSingleton
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000273F File Offset: 0x0000093F
		public ToolWaterToggler(EventBus eventBus, ToolService toolService, WaterOpacityService waterOpacityService)
		{
			this._eventBus = eventBus;
			this._toolService = toolService;
			this._waterOpacityService = waterOpacityService;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x0000275C File Offset: 0x0000095C
		public void Load()
		{
			this._waterOpacityToggle = this._waterOpacityService.GetWaterOpacityToggle();
			this._eventBus.Register(this);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000277B File Offset: 0x0000097B
		[OnEvent]
		public void OnToolGroupEntered(ToolGroupEnteredEvent toolGroupEnteredEvent)
		{
			if (toolGroupEnteredEvent.ToolGroup != null)
			{
				this._waterOpacityToggle.HideWater();
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002796 File Offset: 0x00000996
		[OnEvent]
		public void OnToolGroupExited(ToolGroupExitedEvent toolGroupExitedEvent)
		{
			this._waterOpacityToggle.ShowWater();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000027A3 File Offset: 0x000009A3
		[OnEvent]
		public void OnToolEntered(ToolEnteredEvent toolEnteredEvent)
		{
			if (!this._toolService.IsDefaultToolActive && !(toolEnteredEvent.Tool is IWaterIgnoringTool))
			{
				this._waterOpacityToggle.HideWater();
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002796 File Offset: 0x00000996
		[OnEvent]
		public void OnToolExited(ToolExitedEvent toolExitedEvent)
		{
			this._waterOpacityToggle.ShowWater();
		}

		// Token: 0x0400001E RID: 30
		public readonly EventBus _eventBus;

		// Token: 0x0400001F RID: 31
		public readonly ToolService _toolService;

		// Token: 0x04000020 RID: 32
		public readonly WaterOpacityService _waterOpacityService;

		// Token: 0x04000021 RID: 33
		public WaterOpacityToggle _waterOpacityToggle;
	}
}
