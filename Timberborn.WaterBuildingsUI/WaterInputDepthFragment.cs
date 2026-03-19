using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;
using Timberborn.UIFormatters;
using Timberborn.WaterBuildings;
using UnityEngine.UIElements;

namespace Timberborn.WaterBuildingsUI
{
	// Token: 0x0200001B RID: 27
	public class WaterInputDepthFragment : IEntityPanelFragment
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004B88 File Offset: 0x00002D88
		public WaterInputDepthFragment(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00004BEC File Offset: 0x00002DEC
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/WaterInputDepthFragment";
			this._root = this._visualElementLoader.LoadVisualElement(elementName);
			this._root.ToggleDisplayStyle(false);
			this._depth = UQueryExtensions.Q<Label>(this._root, "Depth", null);
			this._limit = UQueryExtensions.Q<Label>(this._root, "Limit", null);
			this._increaseDepth = UQueryExtensions.Q<Button>(this._root, "IncreaseDepth", null);
			this._increaseDepth.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.IncreaseDepth), 0);
			this._decreaseDepth = UQueryExtensions.Q<Button>(this._root, "DecreaseDepth", null);
			this._decreaseDepth.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.DecreaseDepth), 0);
			this._useDepthLimit = UQueryExtensions.Q<Toggle>(this._root, "UseDepthLimit", null);
			this._useDepthLimit.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.ToggleDepthLimit), 0);
			return this._root;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004CDE File Offset: 0x00002EDE
		public void ShowFragment(BaseComponent entity)
		{
			this._waterInputSpec = entity.GetComponent<WaterInputSpec>();
			if (this._waterInputSpec != null)
			{
				this._waterInputCoordinates = entity.GetComponent<WaterInputCoordinates>();
				this._root.ToggleDisplayStyle(true);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004D12 File Offset: 0x00002F12
		public void ClearFragment()
		{
			this._waterInputCoordinates = null;
			this._waterInputSpec = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004D30 File Offset: 0x00002F30
		public void UpdateFragment()
		{
			if (this._waterInputSpec != null)
			{
				bool useDepthLimit = this._waterInputCoordinates.UseDepthLimit;
				int depthLimit = this._waterInputCoordinates.DepthLimit;
				this._increaseDepth.SetEnabled(useDepthLimit && depthLimit < this._waterInputSpec.MaxDepth);
				this._decreaseDepth.SetEnabled(useDepthLimit && depthLimit > 0);
				this._limit.SetEnabled(useDepthLimit);
				this._limit.text = this._loc.T<int>(this._limitPhrase, this._waterInputCoordinates.DepthLimit);
				this._depth.text = this._loc.T<int>(this._depthPhrase, this._waterInputCoordinates.Depth);
				this._useDepthLimit.SetValueWithoutNotify(useDepthLimit);
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004E00 File Offset: 0x00003000
		public void IncreaseDepth(ClickEvent evt)
		{
			int depthLimit = Math.Min(this._waterInputSpec.MaxDepth, this._waterInputCoordinates.DepthLimit + 1);
			this._waterInputCoordinates.SetDepthLimit(depthLimit);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004E38 File Offset: 0x00003038
		public void DecreaseDepth(ClickEvent evt)
		{
			int depthLimit = Math.Max(0, this._waterInputCoordinates.DepthLimit - 1);
			this._waterInputCoordinates.SetDepthLimit(depthLimit);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004E65 File Offset: 0x00003065
		public void ToggleDepthLimit(ClickEvent evt)
		{
			if (this._waterInputCoordinates.UseDepthLimit)
			{
				this._waterInputCoordinates.DisableDepthLimit();
				return;
			}
			this._waterInputCoordinates.SetDepthLimit(this._waterInputCoordinates.Depth);
		}

		// Token: 0x040000B9 RID: 185
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x040000BA RID: 186
		public readonly ILoc _loc;

		// Token: 0x040000BB RID: 187
		public WaterInputCoordinates _waterInputCoordinates;

		// Token: 0x040000BC RID: 188
		public WaterInputSpec _waterInputSpec;

		// Token: 0x040000BD RID: 189
		public VisualElement _root;

		// Token: 0x040000BE RID: 190
		public Label _depth;

		// Token: 0x040000BF RID: 191
		public Label _limit;

		// Token: 0x040000C0 RID: 192
		public Button _increaseDepth;

		// Token: 0x040000C1 RID: 193
		public Button _decreaseDepth;

		// Token: 0x040000C2 RID: 194
		public Toggle _useDepthLimit;

		// Token: 0x040000C3 RID: 195
		public readonly Phrase _limitPhrase = Phrase.New("WaterInputCoordinates.Depth").Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatDistance));

		// Token: 0x040000C4 RID: 196
		public readonly Phrase _depthPhrase = Phrase.New("WaterInputCoordinates.Limit").Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatDistance));
	}
}
