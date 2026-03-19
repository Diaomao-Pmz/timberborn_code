using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200001F RID: 31
	public class StatusSlotUpdateService
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00003D1C File Offset: 0x00001F1C
		public StatusSlotUpdateService(IBlockService blockService, ITerrainService terrainService, StatusIconSlotFactory statusIconSlotFactory)
		{
			this._blockService = blockService;
			this._terrainService = terrainService;
			this._statusIconSlotFactory = statusIconSlotFactory;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003D5C File Offset: 0x00001F5C
		public ReadOnlyList<StatusSlot> GetStatusSlots(Vector2Int key)
		{
			List<StatusSlot> list;
			if (this._slots.TryGetValue(key, out list) && list.Count > 0)
			{
				return list.AsReadOnlyList<StatusSlot>();
			}
			this.UpdateStatusSlots(key);
			return this._slots[key].AsReadOnlyList<StatusSlot>();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public void UpdateStatusSlots(Vector2Int key)
		{
			List<StatusSlot> orAdd = this._slots.GetOrAdd(key);
			orAdd.Clear();
			this.CalculateSlotPositions(key, orAdd);
			this._topBoundCache.Clear();
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public void ClearStatusSlots(Vector2Int key)
		{
			List<StatusSlot> list;
			if (this._slots.TryGetValue(key, out list))
			{
				list.Clear();
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003DFB File Offset: 0x00001FFB
		public IEnumerable<ValueTuple<StatusSlot, Vector2>> GetAllStatusSlots()
		{
			foreach (KeyValuePair<Vector2Int, List<StatusSlot>> keyValuePair in this._slots)
			{
				Vector2Int vector2Int;
				List<StatusSlot> list;
				keyValuePair.Deconstruct(ref vector2Int, ref list);
				Vector2Int key = vector2Int;
				List<StatusSlot> list2 = list;
				foreach (StatusSlot item in list2)
				{
					yield return new ValueTuple<StatusSlot, Vector2>(item, new Vector2((float)key.x / 2f, (float)key.y / 2f));
				}
				List<StatusSlot>.Enumerator enumerator2 = default(List<StatusSlot>.Enumerator);
				key = default(Vector2Int);
			}
			Dictionary<Vector2Int, List<StatusSlot>>.Enumerator enumerator = default(Dictionary<Vector2Int, List<StatusSlot>>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003E0C File Offset: 0x0000200C
		public void CalculateSlotPositions(in Vector2Int key, IList<StatusSlot> statusSlots)
		{
			byte b = byte.MaxValue;
			for (int i = 0; i < this._blockService.Size.z * 2; i++)
			{
				float num = StatusSlotUpdateService.SlotZCoordinateOffset * (float)(i + 1);
				SlotConstraints constraints = this.GetConstraints(key, i, num);
				byte minBaseZ = (byte)Mathf.Min((int)b, (int)constraints.BaseZ);
				StatusSlot item = this.CreateStatusIconSlot(constraints, i / 2, num, minBaseZ);
				statusSlots.Add(item);
				b = constraints.BaseZ;
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003E8C File Offset: 0x0000208C
		public SlotConstraints GetConstraints(Vector2Int key, int z, float slotZCoordinate)
		{
			int num = z / 2;
			if (z % 2 == 0)
			{
				BlockOccupations occupation = SlotBlockOccupation.GetOccupation(key, false);
				return this.GetConstraints(slotZCoordinate, new StatusSlotUpdateService.AffectedCoordinates(key, z / 2), occupation);
			}
			BlockOccupations occupation2 = SlotBlockOccupation.GetOccupation(key, true);
			SlotConstraints constraints = this.GetConstraints(slotZCoordinate, new StatusSlotUpdateService.AffectedCoordinates(key, num), occupation2);
			SlotConstraints constraints2 = this.GetConstraints(slotZCoordinate, new StatusSlotUpdateService.AffectedCoordinates(key, num + 1), SlotBlockOccupation.Default);
			return constraints.Merge(constraints2);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003EF4 File Offset: 0x000020F4
		public StatusSlot CreateStatusIconSlot(SlotConstraints slotConstraints, int gridZ, float statusZCoordinate, byte minBaseZ)
		{
			int key = gridZ - 1;
			TopBoundForLayer topBoundForLayer;
			if (this._topBoundCache.TryGetValue(key, out topBoundForLayer))
			{
				return this._statusIconSlotFactory.CreateBounded(slotConstraints, topBoundForLayer, statusZCoordinate, minBaseZ);
			}
			return this._statusIconSlotFactory.CreateUnbounded(slotConstraints, statusZCoordinate, minBaseZ);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003F38 File Offset: 0x00002138
		public SlotConstraints GetConstraints(float slotZCoordinate, StatusSlotUpdateService.AffectedCoordinates affectedCoordinates, BlockOccupations blockingOccupations)
		{
			int z = affectedCoordinates.Z;
			if (z < this._terrainService.Size.z && this.IsBlockedByTerrain(affectedCoordinates))
			{
				this._topBoundCache[z] = new TopBoundForLayer((float)z + StatusSlotUpdateService.TopBoundForTerrain);
				return SlotConstraints.GetOccupied((byte)z, false, false);
			}
			return this.GetConstraintsFromBlockObject(slotZCoordinate, affectedCoordinates, blockingOccupations);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003F98 File Offset: 0x00002198
		public bool IsBlockedByTerrain(StatusSlotUpdateService.AffectedCoordinates affectedCoordinates)
		{
			for (int i = affectedCoordinates.XMin; i <= affectedCoordinates.XMax; i++)
			{
				for (int j = affectedCoordinates.YMin; j <= affectedCoordinates.YMax; j++)
				{
					if (this._terrainService.Underground(new Vector3Int(i, j, affectedCoordinates.Z)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003FF4 File Offset: 0x000021F4
		public SlotConstraints GetConstraintsFromBlockObject(float slotZCoordinate, StatusSlotUpdateService.AffectedCoordinates affectedCoordinates, BlockOccupations occupations)
		{
			this.GetOccupiersAtAffectedCoordinates(affectedCoordinates);
			byte b = byte.MaxValue;
			bool flag = false;
			bool invalidInConstructionMode = true;
			bool flag2 = true;
			foreach (StatusSlotUpdateService.PositionedOccupier positionedOccupier in this._positionedOccupierCache)
			{
				StatusSlotOccupier occupier = positionedOccupier.Occupier;
				Vector3Int coordinates = positionedOccupier.Coordinates;
				if (occupier.IntersectsAt(coordinates, occupations))
				{
					flag = true;
					if (occupier.BaseZ < b)
					{
						b = occupier.BaseZ;
					}
					if (!occupier.IsUnfinished || slotZCoordinate < occupier.GetNormalModeTopBound())
					{
						invalidInConstructionMode = false;
					}
					flag2 = (flag2 && occupier.UseUnfinishedConstructionModeModel);
				}
				this.UpdateTopBoundForLayer(occupier, coordinates);
			}
			this._positionedOccupierCache.Clear();
			if (!flag)
			{
				return SlotConstraints.GetUnoccupied(b);
			}
			return SlotConstraints.GetOccupied(b, invalidInConstructionMode, flag2);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000040D4 File Offset: 0x000022D4
		public void GetOccupiersAtAffectedCoordinates(StatusSlotUpdateService.AffectedCoordinates affectedCoordinates)
		{
			for (int i = affectedCoordinates.XMin; i <= affectedCoordinates.XMax; i++)
			{
				for (int j = affectedCoordinates.YMin; j <= affectedCoordinates.YMax; j++)
				{
					Vector3Int coordinates;
					coordinates..ctor(i, j, affectedCoordinates.Z);
					foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates))
					{
						StatusSlotOccupier component = blockObject.GetComponent<StatusSlotOccupier>();
						if (component != null)
						{
							this._positionedOccupierCache.Add(new StatusSlotUpdateService.PositionedOccupier(component, coordinates));
						}
					}
				}
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004190 File Offset: 0x00002390
		public void UpdateTopBoundForLayer(StatusSlotOccupier statusSlotOccupier, Vector3Int coordinates)
		{
			float num = 0f;
			float num2 = 0f;
			TopBoundForLayer topBoundForLayer;
			if (!this._topBoundCache.TryGetValue(coordinates.z, out topBoundForLayer))
			{
				num = topBoundForLayer.ConstructionModeTopBound;
				num2 = topBoundForLayer.NormalModeTopBound;
			}
			TopBoundForLayer topBound = statusSlotOccupier.GetTopBound(coordinates);
			if (topBound.ConstructionModeTopBound > num)
			{
				num = topBound.ConstructionModeTopBound;
			}
			if (topBound.NormalModeTopBound > num2)
			{
				num2 = topBound.NormalModeTopBound;
			}
			if (num > 0f)
			{
				this._topBoundCache[coordinates.z] = new TopBoundForLayer(num, num2);
			}
		}

		// Token: 0x04000069 RID: 105
		public static readonly float SlotZCoordinateOffset = 0.5f;

		// Token: 0x0400006A RID: 106
		public static readonly float TopBoundForTerrain = 1.6f;

		// Token: 0x0400006B RID: 107
		public readonly IBlockService _blockService;

		// Token: 0x0400006C RID: 108
		public readonly ITerrainService _terrainService;

		// Token: 0x0400006D RID: 109
		public readonly StatusIconSlotFactory _statusIconSlotFactory;

		// Token: 0x0400006E RID: 110
		public readonly Dictionary<Vector2Int, List<StatusSlot>> _slots = new Dictionary<Vector2Int, List<StatusSlot>>();

		// Token: 0x0400006F RID: 111
		public readonly Dictionary<int, TopBoundForLayer> _topBoundCache = new Dictionary<int, TopBoundForLayer>();

		// Token: 0x04000070 RID: 112
		public readonly List<StatusSlotUpdateService.PositionedOccupier> _positionedOccupierCache = new List<StatusSlotUpdateService.PositionedOccupier>();

		// Token: 0x02000020 RID: 32
		public readonly struct AffectedCoordinates
		{
			// Token: 0x17000027 RID: 39
			// (get) Token: 0x060000CC RID: 204 RVA: 0x00004232 File Offset: 0x00002432
			public int XMin { get; }

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000CD RID: 205 RVA: 0x0000423A File Offset: 0x0000243A
			public int XMax { get; }

			// Token: 0x17000029 RID: 41
			// (get) Token: 0x060000CE RID: 206 RVA: 0x00004242 File Offset: 0x00002442
			public int YMin { get; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x060000CF RID: 207 RVA: 0x0000424A File Offset: 0x0000244A
			public int YMax { get; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004252 File Offset: 0x00002452
			public int Z { get; }

			// Token: 0x060000D1 RID: 209 RVA: 0x0000425C File Offset: 0x0000245C
			public AffectedCoordinates(Vector2Int key, int z)
			{
				this.XMin = Mathf.FloorToInt((float)(key.x - 1) / 2f);
				this.XMax = this.XMin + (key.x - 1) % 2;
				this.YMin = Mathf.FloorToInt((float)(key.y - 1) / 2f);
				this.YMax = this.YMin + (key.y - 1) % 2;
				this.Z = z;
			}
		}

		// Token: 0x02000021 RID: 33
		public readonly struct PositionedOccupier
		{
			// Token: 0x1700002C RID: 44
			// (get) Token: 0x060000D2 RID: 210 RVA: 0x000042D6 File Offset: 0x000024D6
			public StatusSlotOccupier Occupier { get; }

			// Token: 0x1700002D RID: 45
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x000042DE File Offset: 0x000024DE
			public Vector3Int Coordinates { get; }

			// Token: 0x060000D4 RID: 212 RVA: 0x000042E6 File Offset: 0x000024E6
			public PositionedOccupier(StatusSlotOccupier occupier, Vector3Int coordinates)
			{
				this.Occupier = occupier;
				this.Coordinates = coordinates;
			}
		}
	}
}
