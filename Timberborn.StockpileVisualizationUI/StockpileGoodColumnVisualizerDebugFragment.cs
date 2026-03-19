using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.InventorySystem;
using Timberborn.StockpileVisualization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.StockpileVisualizationUI
{
	// Token: 0x02000004 RID: 4
	public class StockpileGoodColumnVisualizerDebugFragment : IEntityPanelFragment
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public StockpileGoodColumnVisualizerDebugFragment(DebugFragmentFactory debugFragmentFactory, VisualElementLoader visualElementLoader)
		{
			this._debugFragmentFactory = debugFragmentFactory;
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public VisualElement InitializeFragment()
		{
			string elementName = "Game/EntityPanel/StockpileGoodColumnVisualizerDebugFragment";
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement(elementName);
			this._color = UQueryExtensions.Q<TextField>(visualElement, "Color", null);
			UQueryExtensions.Q<Button>(visualElement, "SetColor", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.SetColor), 0);
			this._root = this._debugFragmentFactory.Create("Warehouse box color");
			UQueryExtensions.Q<VisualElement>(this._root, "Content", null).Add(visualElement);
			UQueryExtensions.Q<Label>(this._root, "Text", null).text = "Put hex color below";
			return this._root;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002172 File Offset: 0x00000372
		public void ShowFragment(BaseComponent entity)
		{
			this._stockpileGoodColumnVisualizer = entity.GetComponent<StockpileGoodColumnVisualizer>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002180 File Offset: 0x00000380
		public void ClearFragment()
		{
			this._stockpileGoodColumnVisualizer = null;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002189 File Offset: 0x00000389
		public void UpdateFragment()
		{
			this._root.ToggleDisplayStyle(this._stockpileGoodColumnVisualizer);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021A4 File Offset: 0x000003A4
		public void SetColor(ClickEvent evt)
		{
			string text = "#" + this._color.value.TrimStart('#');
			Color color;
			if (ColorUtility.TryParseHtmlString(text, ref color))
			{
				this._stockpileGoodColumnVisualizer.OverrideColor(color);
				this.LogColorChange(text);
				return;
			}
			Debug.LogWarning("Invalid color: " + text);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021FC File Offset: 0x000003FC
		public void LogColorChange(string color)
		{
			Debug.Log(this._stockpileGoodColumnVisualizer.GetComponent<SingleGoodAllower>().AllowedGood + " color set to: " + color);
		}

		// Token: 0x04000006 RID: 6
		public readonly DebugFragmentFactory _debugFragmentFactory;

		// Token: 0x04000007 RID: 7
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000008 RID: 8
		public StockpileGoodColumnVisualizer _stockpileGoodColumnVisualizer;

		// Token: 0x04000009 RID: 9
		public VisualElement _root;

		// Token: 0x0400000A RID: 10
		public TextField _color;
	}
}
