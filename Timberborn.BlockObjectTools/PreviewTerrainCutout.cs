using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.BlockObjectTools
{
	// Token: 0x0200002C RID: 44
	public class PreviewTerrainCutout : BaseComponent, IAwakableComponent, IStartableComponent, IPrePlacementChangeListener, IPostPlacementChangeListener, IPreviewSelectionListener
	{
		// Token: 0x060000F9 RID: 249 RVA: 0x00004765 File Offset: 0x00002965
		public PreviewTerrainCutout(ITerrainService terrainService, PreviewTerrainCutoutService previewTerrainCutoutService, StackableBlockService stackableBlockService)
		{
			this._terrainService = terrainService;
			this._previewTerrainCutoutService = previewTerrainCutoutService;
			this._stackableBlockService = stackableBlockService;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004782 File Offset: 0x00002982
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._cutoutTilesProvider = base.GetComponent<ICutoutTilesProvider>();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000479C File Offset: 0x0000299C
		public void Start()
		{
			if (this._blockObject.IsPreview)
			{
				this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000047C2 File Offset: 0x000029C2
		public void OnPrePlacementChanged()
		{
			this.UnsetTerrainCutout();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000047CA File Offset: 0x000029CA
		public void OnPostPlacementChanged()
		{
			this.SetTerrainCutout();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000047CA File Offset: 0x000029CA
		public void OnPreviewSelect()
		{
			this.SetTerrainCutout();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000047C2 File Offset: 0x000029C2
		public void OnPreviewUnselect()
		{
			this.UnsetTerrainCutout();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000047D2 File Offset: 0x000029D2
		public void SetTerrainCutout()
		{
			if (this._blockObject.IsPreview && !this._isTerrainCutoutSet)
			{
				this._previewTerrainCutoutService.SetCutout(this.GetCutoutTiles());
				this._isTerrainCutoutSet = true;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004801 File Offset: 0x00002A01
		public void UnsetTerrainCutout()
		{
			if (this._blockObject.IsPreview && this._isTerrainCutoutSet)
			{
				this._previewTerrainCutoutService.UnsetCutout(this.GetCutoutTiles());
				this._isTerrainCutoutSet = false;
			}
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004830 File Offset: 0x00002A30
		public IEnumerable<Vector3Int> GetCutoutTiles()
		{
			int baseHeight = this._blockObject.CoordinatesAtBaseZ.z;
			foreach (Vector3Int vector3Int in this._cutoutTilesProvider.GetPositionedCutoutTiles())
			{
				if (vector3Int.z == baseHeight || this._stackableBlockService.IsUnfinishedGroundBlockAt(vector3Int.Below()))
				{
					yield return vector3Int;
				}
			}
			IEnumerator<Vector3Int> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004840 File Offset: 0x00002A40
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainHeightChangeEventArgs)
		{
			TerrainHeightChange change = terrainHeightChangeEventArgs.Change;
			if (this._isTerrainCutoutSet)
			{
				Vector3Int vector3Int = change.Coordinates.ToVector3Int(change.To);
				if (this.CutoutContainsCoordinates(vector3Int))
				{
					if (this._blockObject.CoordinatesAtBaseZ.z == change.To)
					{
						this._previewTerrainCutoutService.SetCutout(vector3Int);
						return;
					}
					this._previewTerrainCutoutService.UnsetCutout(vector3Int);
				}
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000048B0 File Offset: 0x00002AB0
		public bool CutoutContainsCoordinates(Vector3Int coordinates)
		{
			using (IEnumerator<Vector3Int> enumerator = this._cutoutTilesProvider.GetPositionedCutoutTiles().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == coordinates)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000091 RID: 145
		public readonly ITerrainService _terrainService;

		// Token: 0x04000092 RID: 146
		public readonly PreviewTerrainCutoutService _previewTerrainCutoutService;

		// Token: 0x04000093 RID: 147
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x04000094 RID: 148
		public BlockObject _blockObject;

		// Token: 0x04000095 RID: 149
		public ICutoutTilesProvider _cutoutTilesProvider;

		// Token: 0x04000096 RID: 150
		public bool _isTerrainCutoutSet;
	}
}
