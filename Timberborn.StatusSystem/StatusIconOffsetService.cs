using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000018 RID: 24
	public class StatusIconOffsetService : IStatusIconOffsetService, ILoadableSingleton
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003162 File Offset: 0x00001362
		public StatusIconOffsetService(ITerrainService terrainService, StatusSlotUpdateService statusSlotUpdateService, StatusIconOffsetCalculator statusIconOffsetCalculator)
		{
			this._terrainService = terrainService;
			this._statusSlotUpdateService = statusSlotUpdateService;
			this._statusIconOffsetCalculator = statusIconOffsetCalculator;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003195 File Offset: 0x00001395
		public void Load()
		{
			this._terrainService.TerrainHeightChanged += this.OnTerrainHeightChanged;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000031B0 File Offset: 0x000013B0
		public void AddOffsetter(IStatusIconOffsetter offsetter)
		{
			Vector2Int key = offsetter.Key;
			List<IStatusIconOffsetter> list;
			if (this._offsetters.TryGetValue(key, out list))
			{
				list.Add(offsetter);
				return;
			}
			this._offsetters[key] = new List<IStatusIconOffsetter>
			{
				offsetter
			};
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000031F4 File Offset: 0x000013F4
		public void RemoveOffsetter(IStatusIconOffsetter offsetter)
		{
			Vector2Int key = offsetter.Key;
			List<IStatusIconOffsetter> list;
			if (this._offsetters.TryGetValue(key, out list))
			{
				list.Remove(offsetter);
				if (list.Count > 0)
				{
					StatusIconOffsetService.UpdateIcons(list.AsReadOnlyList<IStatusIconOffsetter>());
					return;
				}
				this._statusSlotUpdateService.ClearStatusSlots(key);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003244 File Offset: 0x00001444
		public void UpdateAffectedStatusSlot(Vector2Int coordinates)
		{
			this.GetEffectedKeysForGridPosition(coordinates);
			foreach (Vector2Int key in this._affectedKeysCache)
			{
				ReadOnlyList<IStatusIconOffsetter> offsetters;
				if (this.HasOffsetterAt(key, out offsetters))
				{
					this._statusSlotUpdateService.UpdateStatusSlots(key);
					StatusIconOffsetService.UpdateIcons(offsetters);
				}
			}
			this._affectedKeysCache.Clear();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000032C0 File Offset: 0x000014C0
		public void UpdateIcons(IStatusIconOffsetter offsetter)
		{
			this.GetKeysAffectedByOffsetter(offsetter);
			foreach (Vector2Int key in this._affectedKeysCache)
			{
				ReadOnlyList<IStatusIconOffsetter> offsetters;
				if (this.HasOffsetterAt(key, out offsetters))
				{
					StatusIconOffsetService.UpdateIcons(offsetters);
				}
			}
			this._affectedKeysCache.Clear();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003330 File Offset: 0x00001530
		public void UpdatePositions(IStatusIconOffsetter offsetter)
		{
			this.GetKeysAffectedByOffsetter(offsetter);
			foreach (Vector2Int key in this._affectedKeysCache)
			{
				ReadOnlyList<IStatusIconOffsetter> readOnlyList;
				if (this.HasOffsetterAt(key, out readOnlyList))
				{
					this._statusSlotUpdateService.UpdateStatusSlots(key);
				}
			}
			this._affectedKeysCache.Clear();
			this.UpdateIcons(offsetter);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000033AC File Offset: 0x000015AC
		public float CalculateVerticalPosition(IStatusIconOffsetter offsetter)
		{
			ReadOnlyList<IStatusIconOffsetter> offsetters = this._offsetters[offsetter.Key].AsReadOnlyList<IStatusIconOffsetter>();
			if (!this._previewMode)
			{
				return this._statusIconOffsetCalculator.CalculateVerticalPosition(offsetters, offsetter);
			}
			return this._statusIconOffsetCalculator.CalculatePreviewVerticalPosition(offsetters, offsetter);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000033F4 File Offset: 0x000015F4
		public void RepositionAllIcons()
		{
			foreach (List<IStatusIconOffsetter> list in this._offsetters.Values)
			{
				StatusIconOffsetService.UpdateIcons(list.AsReadOnlyList<IStatusIconOffsetter>());
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003450 File Offset: 0x00001650
		public IEnumerable<ValueTuple<StatusSlot, Vector2>> GetAllStatusSlots()
		{
			return this._statusSlotUpdateService.GetAllStatusSlots();
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000345D File Offset: 0x0000165D
		public void EnablePreviewMode()
		{
			this._previewMode = true;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003466 File Offset: 0x00001666
		public void DisablePreviewMode()
		{
			this._previewMode = false;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003470 File Offset: 0x00001670
		public void OnTerrainHeightChanged(object sender, TerrainHeightChangeEventArgs terrainColumnChangedEventArgs)
		{
			Vector2Int coordinates = terrainColumnChangedEventArgs.Change.Coordinates;
			this.GetEffectedKeysForGridPosition(coordinates);
			foreach (Vector2Int key in this._affectedKeysCache)
			{
				ReadOnlyList<IStatusIconOffsetter> offsetters;
				if (this.HasOffsetterAt(key, out offsetters))
				{
					this._statusSlotUpdateService.UpdateStatusSlots(key);
					StatusIconOffsetService.UpdateIcons(offsetters);
				}
			}
			this._affectedKeysCache.Clear();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000034FC File Offset: 0x000016FC
		public bool HasOffsetterAt(Vector2Int key, out ReadOnlyList<IStatusIconOffsetter> readOnlyOffsetters)
		{
			List<IStatusIconOffsetter> list;
			if (this._offsetters.TryGetValue(key, out list) && list.Count > 0)
			{
				readOnlyOffsetters = list.AsReadOnlyList<IStatusIconOffsetter>();
				return true;
			}
			return false;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003534 File Offset: 0x00001734
		public static void UpdateIcons(ReadOnlyList<IStatusIconOffsetter> offsetters)
		{
			foreach (IStatusIconOffsetter statusIconOffsetter in offsetters)
			{
				statusIconOffsetter.UpdateIcon();
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003580 File Offset: 0x00001780
		public void GetKeysAffectedByOffsetter(IStatusIconOffsetter offsetter)
		{
			foreach (Vector3Int value in offsetter.BlockObject.PositionedBlocks.GetOccupiedCoordinates())
			{
				this.GetEffectedKeysForGridPosition(value.XY());
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000035DC File Offset: 0x000017DC
		public void GetEffectedKeysForGridPosition(Vector2Int position)
		{
			foreach (Vector2 vector in StatusIconOffsetService.Offsets)
			{
				int num = Mathf.RoundToInt(((float)position.x + vector.x) * 2f) + 1;
				int num2 = Mathf.RoundToInt(((float)position.y + vector.y) * 2f) + 1;
				this._affectedKeysCache.Add(new Vector2Int(num, num2));
			}
		}

		// Token: 0x04000045 RID: 69
		public static readonly Vector2[] Offsets = new Vector2[]
		{
			new Vector2(0f, 0f),
			new Vector2(0f, 0.5f),
			new Vector2(0.5f, 0.5f),
			new Vector2(0.5f, 0f),
			new Vector2(0.5f, -0.5f),
			new Vector2(0f, -0.5f),
			new Vector2(-0.5f, -0.5f),
			new Vector2(-0.5f, 0f),
			new Vector2(-0.5f, 0.5f)
		};

		// Token: 0x04000046 RID: 70
		public readonly ITerrainService _terrainService;

		// Token: 0x04000047 RID: 71
		public readonly StatusSlotUpdateService _statusSlotUpdateService;

		// Token: 0x04000048 RID: 72
		public readonly StatusIconOffsetCalculator _statusIconOffsetCalculator;

		// Token: 0x04000049 RID: 73
		public readonly Dictionary<Vector2Int, List<IStatusIconOffsetter>> _offsetters = new Dictionary<Vector2Int, List<IStatusIconOffsetter>>();

		// Token: 0x0400004A RID: 74
		public readonly HashSet<Vector2Int> _affectedKeysCache = new HashSet<Vector2Int>();

		// Token: 0x0400004B RID: 75
		public bool _previewMode;
	}
}
