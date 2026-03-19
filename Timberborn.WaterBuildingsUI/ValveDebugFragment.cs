using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.WaterBuildings;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x02000012 RID: 18
	public class ValveDebugFragment : IEntityPanelFragment
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public ValveDebugFragment(DebugFragmentFactory debugFragmentFactory, ILoc loc)
		{
			this._debugFragmentFactory = debugFragmentFactory;
			this._loc = loc;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003E04 File Offset: 0x00002004
		public VisualElement InitializeFragment()
		{
			this._root = this._debugFragmentFactory.Create("Valve");
			this._text = UQueryExtensions.Q<Label>(this._root, "Text", null);
			return this._root;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003E39 File Offset: 0x00002039
		public void ShowFragment(BaseComponent entity)
		{
			this._valve = entity.GetComponent<Valve>();
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003E47 File Offset: 0x00002047
		public void ClearFragment()
		{
			this._valve = null;
			this.UpdateFragment();
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003E58 File Offset: 0x00002058
		public void UpdateFragment()
		{
			if (this._valve)
			{
				this._text.text = ((this._valve.CurrentOutflowLimit != null) ? this._loc.T<float>(this._currentOutflowLimitPhrase, this._valve.CurrentOutflowLimit.Value) : "Unlimited");
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000082 RID: 130
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000083 RID: 131
		public readonly ILoc _loc;

		// Token: 0x04000084 RID: 132
		public readonly Phrase _currentOutflowLimitPhrase = Phrase.New().Format<float>((float value) => string.Format("Current outflow limit: {0:F4}cms", value));

		// Token: 0x04000085 RID: 133
		public Valve _valve;

		// Token: 0x04000086 RID: 134
		public VisualElement _root;

		// Token: 0x04000087 RID: 135
		public Label _text;
	}
}
