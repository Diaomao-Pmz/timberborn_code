using System;
using Timberborn.CoreUI;
using Timberborn.ToolPanelSystem;
using UnityEngine.UIElements;

namespace Timberborn.MapEditorBrushesUI
{
	// Token: 0x02000012 RID: 18
	public class TerrainIntegrityWarningPanel : IToolFragment
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00004337 File Offset: 0x00002537
		public TerrainIntegrityWarningPanel(VisualElementLoader visualElementLoader, TerrainIntegrityService terrainIntegrityService)
		{
			this._visualElementLoader = visualElementLoader;
			this._terrainIntegrityService = terrainIntegrityService;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004350 File Offset: 0x00002550
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MapEditor/ToolPanel/TerrainIntegrityWarningPanel");
			this._root.ToggleDisplayStyle(false);
			this._terrainIntegrityService.HighlightChanged += this.OnHighlightChanged;
			return this._root;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000439C File Offset: 0x0000259C
		public void OnHighlightChanged(object sender, bool isHighlighted)
		{
			this._root.ToggleDisplayStyle(isHighlighted);
		}

		// Token: 0x04000085 RID: 133
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000086 RID: 134
		public readonly TerrainIntegrityService _terrainIntegrityService;

		// Token: 0x04000087 RID: 135
		public VisualElement _root;
	}
}
