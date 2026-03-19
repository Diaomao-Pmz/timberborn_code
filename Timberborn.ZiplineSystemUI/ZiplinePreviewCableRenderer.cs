using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.BlueprintSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.ZiplineSystem;
using UnityEngine;

namespace Timberborn.ZiplineSystemUI
{
	// Token: 0x0200000E RID: 14
	public class ZiplinePreviewCableRenderer : ILoadableSingleton
	{
		// Token: 0x0600003D RID: 61 RVA: 0x00002ADD File Offset: 0x00000CDD
		public ZiplinePreviewCableRenderer(ZiplineCableRenderer ziplineCableRenderer, ZiplineConnectionService ziplineConnectionService, RollingHighlighter rollingHighlighter, ISpecService specService, ZiplinePreviewTooltip ziplinePreviewTooltip)
		{
			this._ziplineCableRenderer = ziplineCableRenderer;
			this._ziplineConnectionService = ziplineConnectionService;
			this._rollingHighlighter = rollingHighlighter;
			this._specService = specService;
			this._ziplinePreviewTooltip = ziplinePreviewTooltip;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002B15 File Offset: 0x00000D15
		public void Load()
		{
			this._cableModelPreview = this._ziplineCableRenderer.CreateCableModel();
			this._ziplineSystemColorsSpec = this._specService.GetSingleSpec<ZiplineSystemColorsSpec>();
			this.HidePreview();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B3F File Offset: 0x00000D3F
		public void DrawPreview(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, bool isConnectable)
		{
			if (ZiplinePreviewCableRenderer.ShouldDraw(ziplineTower, otherZiplineTower))
			{
				this.DrawCable(ziplineTower, otherZiplineTower, isConnectable);
				this._ziplinePreviewTooltip.ShowTooltip(ziplineTower, otherZiplineTower, isConnectable);
				return;
			}
			this.HidePreview();
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B68 File Offset: 0x00000D68
		public void HidePreview()
		{
			this._cableModelPreview.SetVisibility(false);
			this._rollingHighlighter.UnhighlightAllPrimary();
			this._ziplinePreviewTooltip.HideTooltip();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002B8C File Offset: 0x00000D8C
		public static bool ShouldDraw(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower)
		{
			return otherZiplineTower && ziplineTower != otherZiplineTower && !ziplineTower.IsConnectedTo(otherZiplineTower);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BA6 File Offset: 0x00000DA6
		public void DrawCable(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, bool isConnectable)
		{
			if (isConnectable)
			{
				this.DrawCable(ziplineTower, otherZiplineTower, this._ziplineSystemColorsSpec.ConnectableColor);
				return;
			}
			this.DrawUnconnectableCable(ziplineTower, otherZiplineTower, this._ziplineSystemColorsSpec.NotConnectableColor);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002BD2 File Offset: 0x00000DD2
		public void DrawUnconnectableCable(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, Color highlightColor)
		{
			this.DrawCable(ziplineTower, otherZiplineTower, highlightColor);
			this._ziplineConnectionService.GetBlockingObjects(ziplineTower, otherZiplineTower, this._blockingObjects);
			this._rollingHighlighter.HighlightPrimary(this._blockingObjects, highlightColor);
			this._blockingObjects.Clear();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C0D File Offset: 0x00000E0D
		public void DrawCable(ZiplineTower ziplineTower, ZiplineTower otherZiplineTower, Color highlightColor)
		{
			this._cableModelPreview.SetVisibility(true);
			this._cableModelPreview.Highlight(highlightColor);
			this._cableModelPreview.UpdateModel(ziplineTower, otherZiplineTower);
		}

		// Token: 0x0400003D RID: 61
		public readonly ZiplineCableRenderer _ziplineCableRenderer;

		// Token: 0x0400003E RID: 62
		public readonly ZiplineConnectionService _ziplineConnectionService;

		// Token: 0x0400003F RID: 63
		public readonly RollingHighlighter _rollingHighlighter;

		// Token: 0x04000040 RID: 64
		public readonly ISpecService _specService;

		// Token: 0x04000041 RID: 65
		public readonly ZiplinePreviewTooltip _ziplinePreviewTooltip;

		// Token: 0x04000042 RID: 66
		public ZiplineCableModel _cableModelPreview;

		// Token: 0x04000043 RID: 67
		public readonly List<BlockObject> _blockingObjects = new List<BlockObject>();

		// Token: 0x04000044 RID: 68
		public ZiplineSystemColorsSpec _ziplineSystemColorsSpec;
	}
}
