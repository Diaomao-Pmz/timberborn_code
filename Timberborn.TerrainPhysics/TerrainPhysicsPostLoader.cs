using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.ErrorReporting;
using Timberborn.MapIndexSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x02000012 RID: 18
	public class TerrainPhysicsPostLoader
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00002B84 File Offset: 0x00000D84
		public TerrainPhysicsPostLoader(ITerrainService terrainService, MapIndexService mapIndexService, EntityRegistry entityRegistry, EntityService entityService, IBlockService blockService, MatterBelowValidator matterBelowValidator, TerrainPhysicsValidationEnabler terrainPhysicsValidationEnabler, ILoadingIssueService loadingIssueService)
		{
			this._terrainService = terrainService;
			this._mapIndexService = mapIndexService;
			this._entityRegistry = entityRegistry;
			this._entityService = entityService;
			this._blockService = blockService;
			this._matterBelowValidator = matterBelowValidator;
			this._terrainPhysicsValidationEnabler = terrainPhysicsValidationEnabler;
			this._loadingIssueService = loadingIssueService;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public void ValidateAll()
		{
			bool flag = true;
			while (flag)
			{
				flag = this.Validate();
			}
			this._terrainPhysicsValidationEnabler.Enable();
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BFA File Offset: 0x00000DFA
		public bool Validate()
		{
			this.CreateCollections();
			this.GetInitialCandidates();
			while (this._candidates.Count > 0)
			{
				this.ValidateCandidate();
			}
			bool result = this.RemoveBlockObjects() || this.RemoveTerrain();
			this.ClearCollections();
			return result;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002C35 File Offset: 0x00000E35
		public void CreateCollections()
		{
			this._validBlockObjects = new HashSet<BlockObject>();
			this._validTerrain = new HashSet<Vector3Int>();
			this._candidates = new Queue<TerrainPhysicsPostLoader.Candidate>();
			this._visited = this.CreateVisitedArray();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002C64 File Offset: 0x00000E64
		public byte[] CreateVisitedArray()
		{
			byte[] array = new byte[this._mapIndexService.VerticalStride * this._mapIndexService.TotalSize.z];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = byte.MaxValue;
			}
			return array;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public void GetInitialCandidates()
		{
			for (int i = 0; i < this._mapIndexService.TerrainSize.y; i++)
			{
				for (int j = 0; j < this._mapIndexService.TerrainSize.x; j++)
				{
					this._candidates.Enqueue(new TerrainPhysicsPostLoader.Candidate(new Vector3Int(j, i, 0), 0));
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D14 File Offset: 0x00000F14
		public void ValidateCandidate()
		{
			TerrainPhysicsPostLoader.Candidate candidate = this._candidates.Dequeue();
			Vector3Int coordinates = candidate.Coordinates;
			int num = this._mapIndexService.CoordinatesToIndex3D(coordinates);
			if (candidate.Distance < this._visited[num])
			{
				this._visited[num] = candidate.Distance;
				this.ValidateTerrain(coordinates, candidate);
				this.ValidateBlockObjects(coordinates, candidate);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D74 File Offset: 0x00000F74
		public void ValidateTerrain(Vector3Int coordinates, TerrainPhysicsPostLoader.Candidate candidate)
		{
			if (this._terrainService.Underground(coordinates))
			{
				this._validTerrain.Add(coordinates);
				this.Enqueue(coordinates.Above(), 0);
				if ((int)candidate.Distance < TerrainPhysicsValidator.MaxSupportDistance)
				{
					this.EnqueueNeighbors(coordinates, (int)(candidate.Distance + 1));
				}
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DC8 File Offset: 0x00000FC8
		public void ValidateBlockObjects(Vector3Int coordinates, TerrainPhysicsPostLoader.Candidate candidate)
		{
			foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates))
			{
				if (this.IsValid(blockObject))
				{
					this._validBlockObjects.Add(blockObject);
					foreach (Block block in blockObject.PositionedBlocks.GetAllBlocks())
					{
						this.Enqueue(candidate, block);
					}
				}
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E68 File Offset: 0x00001068
		public bool IsValid(BlockObject blockObject)
		{
			foreach (Block block in blockObject.PositionedBlocks.GetFoundationBlocks())
			{
				if (!this._matterBelowValidator.Validate(block))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002ECC File Offset: 0x000010CC
		public void Enqueue(TerrainPhysicsPostLoader.Candidate candidate, Block block)
		{
			BlockStackable stackable = block.Stackable;
			if (stackable.IsStackable())
			{
				this.Enqueue(block.Coordinates.Above(), 0);
				if (stackable == BlockStackable.UnfinishedGround && (int)candidate.Distance < TerrainPhysicsValidator.MaxSupportDistance)
				{
					this.EnqueueNeighbors(block.Coordinates, (int)(candidate.Distance + 1));
				}
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002F24 File Offset: 0x00001124
		public void EnqueueNeighbors(Vector3Int coordinates, int distance)
		{
			foreach (Vector3Int vector3Int in Deltas.Neighbors4Vector3Int)
			{
				this.Enqueue(coordinates + vector3Int, distance);
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002F5B File Offset: 0x0000115B
		public void Enqueue(Vector3Int coordinates, int distance)
		{
			if (Sizing.SizeContains(this._mapIndexService.TotalSize, coordinates))
			{
				this._candidates.Enqueue(new TerrainPhysicsPostLoader.Candidate(coordinates, distance));
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002F84 File Offset: 0x00001184
		public bool RemoveTerrain()
		{
			bool result = false;
			foreach (Vector3Int vector3Int in this.GetTerrainToUnset())
			{
				this._loadingIssueService.AddIssue(string.Format("Loaded terrain at {0}", vector3Int) + " is not supported by terrain physics. Deleting it.", TerrainPhysicsPostLoader.TerrainHasNoSupportLocKey, null, false);
				this._terrainService.UnsetTerrain(vector3Int, 1);
				result = true;
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00003010 File Offset: 0x00001210
		public List<Vector3Int> GetTerrainToUnset()
		{
			List<Vector3Int> list = new List<Vector3Int>();
			for (int i = 0; i < this._mapIndexService.TerrainSize.y; i++)
			{
				for (int j = 0; j < this._mapIndexService.TerrainSize.x; j++)
				{
					int num = this._mapIndexService.CellToIndex(new Vector2Int(j, i));
					int columnCount = this._terrainService.GetColumnCount(num);
					for (int k = 1; k < columnCount; k++)
					{
						int index3D = num + k * this._mapIndexService.VerticalStride;
						int columnFloor = this._terrainService.GetColumnFloor(index3D);
						int columnCeiling = this._terrainService.GetColumnCeiling(index3D);
						for (int l = columnFloor; l < columnCeiling; l++)
						{
							Vector3Int item;
							item..ctor(j, i, l);
							if (!this._validTerrain.Contains(item))
							{
								list.Add(item);
							}
						}
					}
				}
			}
			return list;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003100 File Offset: 0x00001300
		public bool RemoveBlockObjects()
		{
			bool result = false;
			foreach (BlockObject blockObject2 in (from component in this._entityRegistry.Entities
			select component.GetComponent<BlockObject>() into blockObject
			where blockObject
			select blockObject).ToList<BlockObject>())
			{
				if (!this._validBlockObjects.Contains(blockObject2))
				{
					LabeledEntitySpec component2 = blockObject2.GetComponent<LabeledEntitySpec>();
					this._loadingIssueService.AddIssue("Loaded BlockObject " + blockObject2.Name + " at " + string.Format("{0} is not supported by terrain physics. Deleting it.", blockObject2.Coordinates), TerrainPhysicsPostLoader.BlockObjectLoadingIssueLocKey, component2.DisplayNameLocKey, true);
					this._entityService.Delete(blockObject2);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000320C File Offset: 0x0000140C
		public void ClearCollections()
		{
			this._validBlockObjects = null;
			this._validTerrain = null;
			this._candidates = null;
			this._visited = null;
		}

		// Token: 0x0400001D RID: 29
		public static readonly string TerrainHasNoSupportLocKey = "TerrainPhysicsPostLoader.TerrainHasNoSupport";

		// Token: 0x0400001E RID: 30
		public static readonly string BlockObjectLoadingIssueLocKey = "LoadingIssue.BlockObjectLoadingIssue";

		// Token: 0x0400001F RID: 31
		public readonly ITerrainService _terrainService;

		// Token: 0x04000020 RID: 32
		public readonly MapIndexService _mapIndexService;

		// Token: 0x04000021 RID: 33
		public readonly EntityRegistry _entityRegistry;

		// Token: 0x04000022 RID: 34
		public readonly EntityService _entityService;

		// Token: 0x04000023 RID: 35
		public readonly IBlockService _blockService;

		// Token: 0x04000024 RID: 36
		public readonly MatterBelowValidator _matterBelowValidator;

		// Token: 0x04000025 RID: 37
		public readonly TerrainPhysicsValidationEnabler _terrainPhysicsValidationEnabler;

		// Token: 0x04000026 RID: 38
		public readonly ILoadingIssueService _loadingIssueService;

		// Token: 0x04000027 RID: 39
		public HashSet<BlockObject> _validBlockObjects;

		// Token: 0x04000028 RID: 40
		public HashSet<Vector3Int> _validTerrain;

		// Token: 0x04000029 RID: 41
		public Queue<TerrainPhysicsPostLoader.Candidate> _candidates;

		// Token: 0x0400002A RID: 42
		public byte[] _visited;

		// Token: 0x02000013 RID: 19
		public readonly struct Candidate
		{
			// Token: 0x17000008 RID: 8
			// (get) Token: 0x06000052 RID: 82 RVA: 0x00003240 File Offset: 0x00001440
			public Vector3Int Coordinates { get; }

			// Token: 0x17000009 RID: 9
			// (get) Token: 0x06000053 RID: 83 RVA: 0x00003248 File Offset: 0x00001448
			public byte Distance { get; }

			// Token: 0x06000054 RID: 84 RVA: 0x00003250 File Offset: 0x00001450
			public Candidate(Vector3Int coordinates, int distance)
			{
				this.Coordinates = coordinates;
				this.Distance = (byte)distance;
			}
		}
	}
}
