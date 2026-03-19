using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Localization;
using UnityEngine;

namespace Timberborn.EntitySystem
{
	// Token: 0x02000018 RID: 24
	public class LabeledEntity : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00002894 File Offset: 0x00000A94
		public LabeledEntity(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000028A4 File Offset: 0x00000AA4
		public string DisplayName
		{
			get
			{
				string result;
				if ((result = this._displayName) == null)
				{
					result = (this._displayName = this._loc.T(this._labeledEntitySpec.DisplayNameLocKey));
				}
				return result;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000028DA File Offset: 0x00000ADA
		public Sprite Image
		{
			get
			{
				return this._labeledEntitySpec.Icon.Asset;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000028EC File Offset: 0x00000AEC
		public void Awake()
		{
			this._labeledEntitySpec = base.GetComponent<LabeledEntitySpec>();
		}

		// Token: 0x04000022 RID: 34
		public readonly ILoc _loc;

		// Token: 0x04000023 RID: 35
		public LabeledEntitySpec _labeledEntitySpec;

		// Token: 0x04000024 RID: 36
		public string _displayName;

		// Token: 0x04000025 RID: 37
		public Sprite _image;
	}
}
