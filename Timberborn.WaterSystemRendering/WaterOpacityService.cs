using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.WaterSystemRendering
{
	// Token: 0x02000018 RID: 24
	public class WaterOpacityService : ILoadableSingleton
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004CFB File Offset: 0x00002EFB
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00004D03 File Offset: 0x00002F03
		public bool IsWaterTransparent { get; private set; }

		// Token: 0x06000094 RID: 148 RVA: 0x00004D0C File Offset: 0x00002F0C
		public WaterOpacityService(EventBus eventBus)
		{
			this._eventBus = eventBus;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004D28 File Offset: 0x00002F28
		public WaterOpacityToggle GetWaterOpacityToggle()
		{
			WaterOpacityToggle waterOpacityToggle = new WaterOpacityToggle();
			this._toggles.Add(waterOpacityToggle);
			waterOpacityToggle.StateChanged += delegate(object _, EventArgs _)
			{
				this.UpdateOpacity();
			};
			return waterOpacityToggle;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004D5A File Offset: 0x00002F5A
		public void Load()
		{
			this.ToggleOpacity(false);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004D63 File Offset: 0x00002F63
		public void ToggleOpacityOverride()
		{
			this._waterOpacityOverriden = !this._waterOpacityOverriden;
			this.UpdateOpacity();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004D7C File Offset: 0x00002F7C
		public void UpdateOpacity()
		{
			bool flag = this._toggles.FastAny((WaterOpacityToggle toggle) => toggle.Hidden) && !this._waterOpacityOverriden;
			if (flag != this.IsWaterTransparent)
			{
				this.ToggleOpacity(flag);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public void ToggleOpacity(bool setTransparent)
		{
			float num = setTransparent ? 0.4f : 1f;
			Shader.SetGlobalFloat(WaterOpacityService.WaterOpacityProperty, num);
			this.IsWaterTransparent = setTransparent;
			this._eventBus.Post(new WaterOpacityChangedEvent(setTransparent));
		}

		// Token: 0x0400009D RID: 157
		public static readonly int WaterOpacityProperty = Shader.PropertyToID("_WaterOpacity");

		// Token: 0x0400009F RID: 159
		public readonly EventBus _eventBus;

		// Token: 0x040000A0 RID: 160
		public readonly List<WaterOpacityToggle> _toggles = new List<WaterOpacityToggle>();

		// Token: 0x040000A1 RID: 161
		public bool _waterOpacityOverriden;
	}
}
