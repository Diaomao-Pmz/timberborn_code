using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Rendering;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x02000007 RID: 7
	public class AreaHighlightingService : ILoadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AreaHighlightingService(RollingHighlighter rollingHighlighter, MarkerDrawerFactory markerDrawerFactory, ISpecService specService)
		{
			this._rollingHighlighter = rollingHighlighter;
			this._markerDrawerFactory = markerDrawerFactory;
			this._specService = specService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		public void Load()
		{
			this._meshDrawer = this._markerDrawerFactory.CreateTileDrawer();
			this._selectionToolHighlightColor = this._specService.GetSingleSpec<SelectionColorsSpec>().SelectionToolHighlight;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002151 File Offset: 0x00000351
		public void DrawTile(Vector3Int coordinates, Color color)
		{
			this._meshDrawer.DrawAtCoordinates(coordinates, 0.02f, color);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002165 File Offset: 0x00000365
		public void AddForHighlight(BaseComponent target)
		{
			this._objetsToHighlight.Add(target);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002174 File Offset: 0x00000374
		public void Highlight()
		{
			this._rollingHighlighter.HighlightPrimary(this._objetsToHighlight, this._selectionToolHighlightColor);
			this._objetsToHighlight.Clear();
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002198 File Offset: 0x00000398
		public void UnhighlightAll()
		{
			this._rollingHighlighter.UnhighlightAllPrimary();
		}

		// Token: 0x04000008 RID: 8
		public readonly RollingHighlighter _rollingHighlighter;

		// Token: 0x04000009 RID: 9
		public readonly MarkerDrawerFactory _markerDrawerFactory;

		// Token: 0x0400000A RID: 10
		public readonly ISpecService _specService;

		// Token: 0x0400000B RID: 11
		public MeshDrawer _meshDrawer;

		// Token: 0x0400000C RID: 12
		public readonly HashSet<BaseComponent> _objetsToHighlight = new HashSet<BaseComponent>();

		// Token: 0x0400000D RID: 13
		public Color _selectionToolHighlightColor;
	}
}
