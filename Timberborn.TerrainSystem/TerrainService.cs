using System;
using System.Collections.Generic;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.MapIndexSystem;
using Timberborn.MapStateSystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.TerrainSystem
{
	// Token: 0x02000014 RID: 20
	public class TerrainService : ILoadableSingleton, ITerrainService, IPostLoadableSingleton
	{
		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000086 RID: 134 RVA: 0x00003194 File Offset: 0x00001394
		// (remove) Token: 0x06000087 RID: 135 RVA: 0x000031CC File Offset: 0x000013CC
		public event EventHandler<TerrainHeightChangeEventArgs> PreTerrainHeightChanged;

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x06000088 RID: 136 RVA: 0x00003204 File Offset: 0x00001404
		// (remove) Token: 0x06000089 RID: 137 RVA: 0x0000323C File Offset: 0x0000143C
		public event EventHandler<TerrainHeightChangeEventArgs> TerrainHeightChanged;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x0600008A RID: 138 RVA: 0x00003274 File Offset: 0x00001474
		// (remove) Token: 0x0600008B RID: 139 RVA: 0x000032AC File Offset: 0x000014AC
		public event EventHandler MinMaxTerrainHeightChanged;

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600008C RID: 140 RVA: 0x000032E4 File Offset: 0x000014E4
		// (remove) Token: 0x0600008D RID: 141 RVA: 0x0000331C File Offset: 0x0000151C
		public event EventHandler<Vector3Int> FieldOrCutoutChanged;

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00003351 File Offset: 0x00001551
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00003359 File Offset: 0x00001559
		public int MaxTerrainHeight { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003362 File Offset: 0x00001562
		// (set) Token: 0x06000091 RID: 145 RVA: 0x0000336A File Offset: 0x0000156A
		public int MinTerrainHeight { get; private set; }

		// Token: 0x06000092 RID: 146 RVA: 0x00003373 File Offset: 0x00001573
		public TerrainService(MapSize mapSize, TerrainMap terrainMap, ColumnTerrainMap columnTerrainMap, MapIndexService mapIndexService)
		{
			this._mapSize = mapSize;
			this._terrainMap = terrainMap;
			this._columnTerrainMap = columnTerrainMap;
			this._mapIndexService = mapIndexService;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000033AE File Offset: 0x000015AE
		public Vector3Int Size
		{
			get
			{
				return this._mapSize.TerrainSize;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000033BB File Offset: 0x000015BB
		public void Load()
		{
			this._fieldMap = new TerrainPropertyMap<bool>(this.Size);
			this._cutoutMap = new TerrainPropertyMap<int>(this.Size);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000033DF File Offset: 0x000015DF
		public void PostLoad()
		{
			this.CalculateMinAndMaxHeight();
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000033E8 File Offset: 0x000015E8
		public int GetTerrainHeight(Vector3Int coordinates)
		{
			int num;
			if (this.TryGetRelativeHeight(coordinates, out num))
			{
				return coordinates.z + num;
			}
			return 0;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000340B File Offset: 0x0000160B
		public bool TryGetRelativeHeight(Vector3Int coordinates, out int relativeHeight)
		{
			return this._terrainMap.TryGetRelativeHeight(coordinates, out relativeHeight);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000341A File Offset: 0x0000161A
		public int GetTerrainHeightBelow(Vector3Int coordinates)
		{
			return this._terrainMap.GetTerrainHeightBelow(coordinates);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003428 File Offset: 0x00001628
		public IEnumerable<Vector3Int> GetAllHeightsInCell(Vector2Int cellCoordinates)
		{
			int num;
			for (int z = 0; z < this._mapSize.TerrainSize.z; z = num + 1)
			{
				Vector3Int vector3Int = cellCoordinates.ToVector3Int(z);
				if (!this._terrainMap.IsTerrainVoxel(vector3Int) && this._terrainMap.IsTerrainVoxel(cellCoordinates.ToVector3Int(z - 1)))
				{
					yield return vector3Int;
				}
				num = z;
			}
			yield break;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000343F File Offset: 0x0000163F
		public bool UnsafeCellIsTerrain(int index)
		{
			return this._terrainMap.UnsafeIsTerrainVoxel(index);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000344D File Offset: 0x0000164D
		public bool CellIsField(Vector3Int cellCoordinates)
		{
			return this._fieldMap.Get(cellCoordinates);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000345B File Offset: 0x0000165B
		public bool CellIsCutout(Vector3Int cellCoordinates)
		{
			return this._cutoutMap.Get(cellCoordinates) > 0;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000346C File Offset: 0x0000166C
		public bool Underground(Vector3Int coords)
		{
			return this._terrainMap.IsTerrainVoxel(coords);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000347A File Offset: 0x0000167A
		public bool OnGround(Vector3Int coords)
		{
			return this._terrainMap.IsTerrainVoxel(coords.Below()) && !this._terrainMap.IsTerrainVoxel(coords);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000034A0 File Offset: 0x000016A0
		public void SetTerrain(Vector3Int coordinates, int heightChange = 1)
		{
			Vector2Int vector2Int = coordinates.XY();
			if (coordinates.z + heightChange >= this.Size.z)
			{
				heightChange = this.Size.z - 1 - coordinates.z;
			}
			while (this._terrainMap.IsTerrainVoxel(coordinates))
			{
				coordinates.z++;
				heightChange--;
			}
			if (this.Contains(vector2Int) && heightChange > 0)
			{
				this.PrepareChangeData(vector2Int, coordinates.z, heightChange, true);
				foreach (TerrainHeightChange change in this._terrainHeightChanges)
				{
					EventHandler<TerrainHeightChangeEventArgs> preTerrainHeightChanged = this.PreTerrainHeightChanged;
					if (preTerrainHeightChanged != null)
					{
						preTerrainHeightChanged(this, new TerrainHeightChangeEventArgs(change));
					}
					for (int i = change.From; i <= change.To; i++)
					{
						this._terrainMap.SetTerrain(change.Coordinates.ToVector3Int(i));
					}
					if (change.To > this.MaxTerrainHeight)
					{
						this.MaxTerrainHeight = change.To;
						EventHandler minMaxTerrainHeightChanged = this.MinMaxTerrainHeightChanged;
						if (minMaxTerrainHeightChanged != null)
						{
							minMaxTerrainHeightChanged(this, EventArgs.Empty);
						}
					}
					else if (change.From == this.MinTerrainHeight)
					{
						this.CalculateMinAndMaxHeight();
					}
					EventHandler<TerrainHeightChangeEventArgs> terrainHeightChanged = this.TerrainHeightChanged;
					if (terrainHeightChanged != null)
					{
						terrainHeightChanged(this, new TerrainHeightChangeEventArgs(change));
					}
				}
				this._terrainHeightChanges.Clear();
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003630 File Offset: 0x00001830
		public void UnsetTerrain(Vector3Int coordinates, int heightChange = 1)
		{
			Vector2Int vector2Int = coordinates.XY();
			int num = coordinates.z + 1;
			if (num - heightChange < 0)
			{
				heightChange += num - heightChange;
			}
			while (!this._terrainMap.IsTerrainVoxel(coordinates))
			{
				coordinates.z--;
				heightChange--;
			}
			if (this.Contains(vector2Int) && heightChange > 0)
			{
				int destroyedLayersCount = this.GetDestroyedLayersCount(coordinates, heightChange);
				this.PrepareChangeData(vector2Int, coordinates.z - destroyedLayersCount + 1, destroyedLayersCount, false);
				foreach (TerrainHeightChange change in this._terrainHeightChanges)
				{
					EventHandler<TerrainHeightChangeEventArgs> preTerrainHeightChanged = this.PreTerrainHeightChanged;
					if (preTerrainHeightChanged != null)
					{
						preTerrainHeightChanged(this, new TerrainHeightChangeEventArgs(change));
					}
					for (int i = change.From; i <= change.To; i++)
					{
						this._terrainMap.UnsetTerrain(change.Coordinates.ToVector3Int(i));
					}
					if (change.From < this.MinTerrainHeight)
					{
						this.MinTerrainHeight = change.From;
						EventHandler minMaxTerrainHeightChanged = this.MinMaxTerrainHeightChanged;
						if (minMaxTerrainHeightChanged != null)
						{
							minMaxTerrainHeightChanged(this, EventArgs.Empty);
						}
					}
					else if (change.To + 1 == this.MaxTerrainHeight)
					{
						this.CalculateMinAndMaxHeight();
					}
					EventHandler<TerrainHeightChangeEventArgs> terrainHeightChanged = this.TerrainHeightChanged;
					if (terrainHeightChanged != null)
					{
						terrainHeightChanged(this, new TerrainHeightChangeEventArgs(change));
					}
				}
				this._terrainHeightChanges.Clear();
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000037B4 File Offset: 0x000019B4
		public void UnsetTerrain(HashSet<Vector3Int> coordinatesToDestroy)
		{
			foreach (Vector3Int value in coordinatesToDestroy)
			{
				Vector2Int key = value.XY();
				List<int> list;
				if (!this._tileChanges.TryGetValue(key, out list))
				{
					list = new List<int>();
					this._tileChanges[key] = list;
				}
				list.Add(value.z);
			}
			foreach (KeyValuePair<Vector2Int, List<int>> keyValuePair in this._tileChanges)
			{
				Vector2Int key2 = keyValuePair.Key;
				List<int> value2 = keyValuePair.Value;
				if (!value2.IsEmpty<int>())
				{
					value2.Sort();
					List<int> list2 = value2;
					int num = list2[list2.Count - 1];
					int num2 = num;
					for (int i = value2.Count - 2; i >= 0; i--)
					{
						int num3 = value2[i];
						if (num3 != num2 - 1 || i == 0)
						{
							this.PrepareHeightChanges(key2.ToVector3Int(num), num - num2 + 1);
							num = num3;
						}
						num2 = num3;
					}
					if (num2 == num)
					{
						this.PrepareHeightChanges(key2.ToVector3Int(num), 1);
					}
					value2.Clear();
				}
			}
			foreach (TerrainHeightChange change in this._terrainHeightChanges)
			{
				EventHandler<TerrainHeightChangeEventArgs> preTerrainHeightChanged = this.PreTerrainHeightChanged;
				if (preTerrainHeightChanged != null)
				{
					preTerrainHeightChanged(this, new TerrainHeightChangeEventArgs(change));
				}
				for (int j = change.From; j <= change.To; j++)
				{
					this._terrainMap.UnsetTerrain(change.Coordinates.ToVector3Int(j));
				}
				EventHandler<TerrainHeightChangeEventArgs> terrainHeightChanged = this.TerrainHeightChanged;
				if (terrainHeightChanged != null)
				{
					terrainHeightChanged(this, new TerrainHeightChangeEventArgs(change));
				}
			}
			this.CalculateMinAndMaxHeight();
			this._terrainHeightChanges.Clear();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000039D0 File Offset: 0x00001BD0
		public void SetField(Vector3Int coordinates)
		{
			this.Set(coordinates, this._fieldMap, true);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000039E0 File Offset: 0x00001BE0
		public void UnsetField(Vector3Int coordinates)
		{
			this.Set(coordinates, this._fieldMap, false);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000039F0 File Offset: 0x00001BF0
		public void SetCutout(Vector3Int coordinates)
		{
			this.Set(coordinates, this._cutoutMap, true);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003A00 File Offset: 0x00001C00
		public void UnsetCutout(Vector3Int coordinates)
		{
			this.Set(coordinates, this._cutoutMap, false);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003A10 File Offset: 0x00001C10
		public bool Contains(Vector2Int coordinates)
		{
			return Sizing.SizeContains(this.Size, coordinates);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003A1E File Offset: 0x00001C1E
		public bool Contains(Vector3Int coordinates)
		{
			return Sizing.SizeContains(this.Size, coordinates);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003A2C File Offset: 0x00001C2C
		public Vector3Int Clamp(Vector3Int coordinates)
		{
			return new Vector3Int(Mathf.Clamp(coordinates.x, 0, this.Size.x - 1), Mathf.Clamp(coordinates.y, 0, this.Size.y - 1), Mathf.Clamp(coordinates.z, 0, this.Size.z));
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003A93 File Offset: 0x00001C93
		public int GetColumnCount(int index)
		{
			return (int)this._columnTerrainMap.ColumnCount[index];
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003AA2 File Offset: 0x00001CA2
		public int GetColumnFloor(int index3D)
		{
			return this._columnTerrainMap.GetColumn(index3D).Floor;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003AB5 File Offset: 0x00001CB5
		public int GetColumnCeiling(int index3D)
		{
			return this._columnTerrainMap.GetColumn(index3D).Ceiling;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public bool TryGetDistanceToTerrainAbove(Vector3Int coordinates, out int distance)
		{
			Vector2Int coords2D = coordinates.XY();
			for (int i = coordinates.z; i < this.Size.z; i++)
			{
				if (this._terrainMap.IsTerrainVoxel(coords2D.ToVector3Int(i)))
				{
					distance = i - coordinates.z;
					return true;
				}
			}
			distance = -1;
			return false;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003B20 File Offset: 0x00001D20
		public bool IsVisible(Vector3Int coordinates)
		{
			foreach (Vector3Int vector3Int in Deltas.Neighbors6Vector3Int)
			{
				Vector3Int coordinates2 = coordinates + vector3Int;
				if (!this._terrainMap.IsTerrainVoxel(coordinates2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003B64 File Offset: 0x00001D64
		public void PrepareHeightChanges(Vector3Int coordinates, int heightChange)
		{
			Vector2Int vector2Int = coordinates.XY();
			int num = coordinates.z + 1;
			if (num - heightChange < 0)
			{
				heightChange += num - heightChange;
			}
			while (!this._terrainMap.IsTerrainVoxel(coordinates))
			{
				coordinates.z--;
				heightChange--;
			}
			if (this.Contains(vector2Int) && heightChange > 0)
			{
				int destroyedLayersCount = this.GetDestroyedLayersCount(coordinates, heightChange);
				this.PrepareChangeData(vector2Int, coordinates.z - destroyedLayersCount + 1, destroyedLayersCount, false);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003BE0 File Offset: 0x00001DE0
		public void PrepareChangeData(Vector2Int columnCoordinates, int startZ, int height, bool setTerrain)
		{
			int num = -1;
			for (int i = 0; i < height; i++)
			{
				Vector3Int coordinates = columnCoordinates.ToVector3Int(startZ + i);
				if (num == -1 && setTerrain != this._terrainMap.IsTerrainVoxel(coordinates))
				{
					num = startZ + i;
				}
				if (num != -1 && setTerrain == this._terrainMap.IsTerrainVoxel(coordinates))
				{
					int to = startZ + i - 1;
					TerrainHeightChange item = new TerrainHeightChange(columnCoordinates, num, to, setTerrain);
					this._terrainHeightChanges.Add(item);
					num = -1;
				}
			}
			if (num != -1)
			{
				TerrainHeightChange item2 = new TerrainHeightChange(columnCoordinates, num, startZ + height - 1, setTerrain);
				this._terrainHeightChanges.Add(item2);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003C73 File Offset: 0x00001E73
		public void Set(Vector3Int coordinates, TerrainPropertyMap<bool> map, bool value)
		{
			if (this.Contains(coordinates) && map.Get(coordinates) != value)
			{
				map.Set(coordinates, value);
				EventHandler<Vector3Int> fieldOrCutoutChanged = this.FieldOrCutoutChanged;
				if (fieldOrCutoutChanged == null)
				{
					return;
				}
				fieldOrCutoutChanged(this, coordinates);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public void Set(Vector3Int coordinates, TerrainPropertyMap<int> map, bool increase)
		{
			if (this.Contains(coordinates))
			{
				int num = map.Get(coordinates);
				map.Set(coordinates, increase ? (num + 1) : (num - 1));
				if ((num == 0 && increase) || (num == 1 && !increase))
				{
					EventHandler<Vector3Int> fieldOrCutoutChanged = this.FieldOrCutoutChanged;
					if (fieldOrCutoutChanged == null)
					{
						return;
					}
					fieldOrCutoutChanged(this, coordinates);
				}
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public void CalculateMinAndMaxHeight()
		{
			int minTerrainHeight = this.MinTerrainHeight;
			int maxTerrainHeight = this.MaxTerrainHeight;
			this.MinTerrainHeight = int.MaxValue;
			this.MaxTerrainHeight = 0;
			for (int i = 0; i < this.Size.y; i++)
			{
				for (int j = 0; j < this.Size.x; j++)
				{
					int num = this._mapIndexService.CellToIndex(new Vector2Int(j, i));
					int columnCount = this.GetColumnCount(num);
					int ceiling = this._columnTerrainMap.GetColumn(num).Ceiling;
					if (ceiling < this.MinTerrainHeight)
					{
						this.MinTerrainHeight = ceiling;
					}
					int index3D = num + (columnCount - 1) * this._mapIndexService.VerticalStride;
					int ceiling2 = this._columnTerrainMap.GetColumn(index3D).Ceiling;
					if (ceiling2 > this.MaxTerrainHeight)
					{
						this.MaxTerrainHeight = ceiling2;
					}
				}
			}
			if (minTerrainHeight == this.MinTerrainHeight || maxTerrainHeight != this.MaxTerrainHeight)
			{
				EventHandler minMaxTerrainHeightChanged = this.MinMaxTerrainHeightChanged;
				if (minMaxTerrainHeightChanged == null)
				{
					return;
				}
				minMaxTerrainHeightChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003E0C File Offset: 0x0000200C
		public int GetDestroyedLayersCount(Vector3Int coordinates, int heightChange)
		{
			int num = 0;
			for (int i = 0; i < heightChange; i++)
			{
				if (!this._terrainMap.IsTerrainVoxel(coordinates))
				{
					return num;
				}
				num++;
				coordinates = coordinates.Below();
			}
			return num;
		}

		// Token: 0x04000033 RID: 51
		public readonly MapSize _mapSize;

		// Token: 0x04000034 RID: 52
		public readonly TerrainMap _terrainMap;

		// Token: 0x04000035 RID: 53
		public readonly ColumnTerrainMap _columnTerrainMap;

		// Token: 0x04000036 RID: 54
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000037 RID: 55
		public readonly List<TerrainHeightChange> _terrainHeightChanges = new List<TerrainHeightChange>();

		// Token: 0x04000038 RID: 56
		public readonly Dictionary<Vector2Int, List<int>> _tileChanges = new Dictionary<Vector2Int, List<int>>();

		// Token: 0x04000039 RID: 57
		public TerrainPropertyMap<bool> _fieldMap;

		// Token: 0x0400003A RID: 58
		public TerrainPropertyMap<int> _cutoutMap;
	}
}
