using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.SingletonSystem;
using Timberborn.TerrainSystem;
using UnityEngine;

namespace Timberborn.TerrainPhysics
{
	// Token: 0x0200000A RID: 10
	public class TerrainAndBlockObjectsToDeleteFinder : ILoadableSingleton
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002140 File Offset: 0x00000340
		public TerrainAndBlockObjectsToDeleteFinder(IBlockService blockService, TerrainPhysicsValidatorFactory terrainPhysicsValidatorFactory, StackableBlockService stackableBlockService, SupportsToBeDeleted supportsToBeDeleted, ITerrainService terrainService, TerrainOnBlockObjectFinder terrainOnBlockObjectFinder)
		{
			this._blockService = blockService;
			this._terrainPhysicsValidatorFactory = terrainPhysicsValidatorFactory;
			this._stackableBlockService = stackableBlockService;
			this._supportsToBeDeleted = supportsToBeDeleted;
			this._terrainService = terrainService;
			this._terrainOnBlockObjectFinder = terrainOnBlockObjectFinder;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021AC File Offset: 0x000003AC
		public void Load()
		{
			this._terrainPhysicsValidator = this._terrainPhysicsValidatorFactory.CreatePreviewValidator();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021BF File Offset: 0x000003BF
		public void FindAll(IEnumerable<Vector3Int> inputTerrain, HashSet<Vector3Int> outputTerrain, HashSet<BlockObject> outputBlockObjects)
		{
			this.ProcessInputTerrain(inputTerrain, false);
			this.GetTerrainAndBlockObjectsToDelete(outputTerrain, outputBlockObjects);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021D1 File Offset: 0x000003D1
		public void FindAllMarkInputAsDeleted(IEnumerable<Vector3Int> inputTerrain, IEnumerable<BlockObject> inputBlockObjects, HashSet<Vector3Int> outputTerrain, HashSet<BlockObject> outputBlockObjects)
		{
			this.ProcessInputTerrain(inputTerrain, true);
			this.ProcessInputBlockObjects(inputBlockObjects);
			this.GetTerrainAndBlockObjectsToDelete(outputTerrain, outputBlockObjects);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021EB File Offset: 0x000003EB
		public void FindAll(IEnumerable<BlockObject> inputBlockObjects, HashSet<Vector3Int> outputTerrain, HashSet<BlockObject> outputBlockObjects)
		{
			this.ProcessInputBlockObjects(inputBlockObjects);
			this.GetTerrainAndBlockObjectsToDelete(outputTerrain, outputBlockObjects);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021FC File Offset: 0x000003FC
		public void ProcessInputTerrain(IEnumerable<Vector3Int> inputTerrain, bool markAsDeleted)
		{
			foreach (Vector3Int vector3Int in inputTerrain)
			{
				this._terrainToCheck.Enqueue(vector3Int);
				Vector3Int vector3Int2 = vector3Int.Above();
				if (this._terrainService.Underground(vector3Int2))
				{
					this._terrainToCheck.Enqueue(vector3Int2);
				}
				if (markAsDeleted)
				{
					this._supportsToBeDeleted.Mark(vector3Int);
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000227C File Offset: 0x0000047C
		public void ProcessInputBlockObjects(IEnumerable<BlockObject> inputBlockObjects)
		{
			foreach (BlockObject blockObject in inputBlockObjects)
			{
				this._terrainOnBlockObjectFinder.Find(blockObject, this._terrainToCheck);
				this.MarkBlockObjectBlocksForDeletion(blockObject);
				this.AddNextBlockObjectToValidate(blockObject);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022E0 File Offset: 0x000004E0
		public void GetTerrainAndBlockObjectsToDelete(HashSet<Vector3Int> outputTerrain, HashSet<BlockObject> outputBlockObjects)
		{
			while (this._terrainToCheck.Count > 0 || this._blockObjectCoordinatesToCheck.Count > 0)
			{
				this._terrainPhysicsValidator.CheckTerrainForDestruction(this._terrainToCheck, this._checkedTerrain);
				this._terrainToCheck.Clear();
				this.AddTerrainAndTerrainBlockToDelete(outputTerrain, outputBlockObjects);
				this.AddBlockObjectStackToDelete(outputBlockObjects);
				foreach (BlockObject blockObject in this._blockObjectsToCheck)
				{
					this._terrainOnBlockObjectFinder.Find(blockObject, this._terrainToCheck);
				}
				this._blockObjectsToCheck.Clear();
			}
			this._supportsToBeDeleted.Clear();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023AC File Offset: 0x000005AC
		public void AddTerrainAndTerrainBlockToDelete(HashSet<Vector3Int> outputTerrain, HashSet<BlockObject> outputBlockObjects)
		{
			foreach (Vector3Int vector3Int in this._checkedTerrain)
			{
				if (this._stackableBlockService.IsUnfinishedGroundBlockAt(vector3Int))
				{
					BlockObject bottomObjectAt = this._blockService.GetBottomObjectAt(vector3Int);
					if (!outputBlockObjects.Contains(bottomObjectAt))
					{
						this._blockObjectsToCheck.Enqueue(bottomObjectAt);
					}
				}
				else if (!outputTerrain.Contains(vector3Int))
				{
					this._terrainToCheck.Enqueue(vector3Int);
					this._supportsToBeDeleted.Mark(vector3Int);
					foreach (BlockObject blockObject in this._blockService.GetObjectsAt(vector3Int))
					{
						if (blockObject.PositionedBlocks.GetBlock(vector3Int).Underground)
						{
							outputBlockObjects.Add(blockObject);
						}
					}
				}
				this._blockObjectCoordinatesToCheck.Enqueue(vector3Int.Above());
			}
			this._checkedTerrain.Clear();
			outputTerrain.AddRange(this._terrainToCheck);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024E4 File Offset: 0x000006E4
		public void AddBlockObjectStackToDelete(HashSet<BlockObject> outputBlockObjects)
		{
			while (this._blockObjectCoordinatesToCheck.Count > 0)
			{
				this.CheckBlockObjectStackToDelete(this._blockObjectCoordinatesToCheck.Dequeue(), outputBlockObjects);
			}
			outputBlockObjects.AddRange(this._blockObjectsToCheck);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002514 File Offset: 0x00000714
		public void CheckBlockObjectStackToDelete(Vector3Int coordinates, HashSet<BlockObject> outputBlockObjects)
		{
			foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates))
			{
				Block block;
				if (this.IsBlockValidForDeletion(coordinates, blockObject, out block) && !outputBlockObjects.Contains(blockObject) && blockObject.GetComponent<INonStackPickable>() == null)
				{
					this._blockObjectsToCheck.Enqueue(blockObject);
					this.MarkBlockObjectBlocksForDeletion(blockObject);
					this.AddNextBlockObjectToValidate(blockObject);
				}
				else if (block.Stackable.IsUnfinishedGround())
				{
					this.AddUnfinishedGroundBlockToCheck(blockObject);
				}
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000025B8 File Offset: 0x000007B8
		public bool IsBlockValidForDeletion(Vector3Int coordinates, BlockObject blockObject, out Block block)
		{
			block = blockObject.PositionedBlocks.GetBlock(coordinates);
			Block block2 = block;
			return (block2.Underground && block2.MatterBelow == MatterBelow.Ground) || (block.IsFoundationBlock && !block.Stackable.IsUnfinishedGround());
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000260C File Offset: 0x0000080C
		public void MarkBlockObjectBlocksForDeletion(BlockObject blockObject)
		{
			foreach (Block block in blockObject.PositionedBlocks.GetOccupiedBlocks())
			{
				if (block.Stackable.IsStackable() || block.Stackable.IsUnfinishedGround())
				{
					this._supportsToBeDeleted.Mark(block.Coordinates);
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002688 File Offset: 0x00000888
		public void AddUnfinishedGroundBlockToCheck(BlockObject blockObject)
		{
			foreach (Block block in blockObject.PositionedBlocks.GetOccupiedBlocks())
			{
				if (block.Stackable.IsUnfinishedGround())
				{
					this._terrainToCheck.Enqueue(block.Coordinates);
				}
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000026F4 File Offset: 0x000008F4
		public void AddNextBlockObjectToValidate(BlockObject blockObject)
		{
			foreach (Block block in blockObject.PositionedBlocks.GetOccupiedBlocks())
			{
				if (block.Stackable.IsStackable())
				{
					Vector3Int vector3Int = block.Coordinates.Above();
					bool flag = this.IsUnderground(vector3Int);
					if (!flag && !this._blockObjectCoordinatesToCheck.Contains(vector3Int))
					{
						this._blockObjectCoordinatesToCheck.Enqueue(vector3Int);
					}
					else if (flag)
					{
						BlockObject undergroundObjectAt = this._blockService.GetUndergroundObjectAt(vector3Int);
						if (undergroundObjectAt && undergroundObjectAt.PositionedBlocks.GetBlock(vector3Int).MatterBelow == MatterBelow.GroundOrStackable)
						{
							this._blockObjectCoordinatesToCheck.Enqueue(vector3Int);
						}
						this._terrainToCheck.Enqueue(vector3Int);
					}
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027D4 File Offset: 0x000009D4
		public bool IsUnderground(Vector3Int coordinates)
		{
			return this._terrainService.Underground(coordinates) || this._stackableBlockService.IsUnfinishedGroundBlockAt(coordinates);
		}

		// Token: 0x04000009 RID: 9
		public readonly IBlockService _blockService;

		// Token: 0x0400000A RID: 10
		public readonly TerrainPhysicsValidatorFactory _terrainPhysicsValidatorFactory;

		// Token: 0x0400000B RID: 11
		public readonly StackableBlockService _stackableBlockService;

		// Token: 0x0400000C RID: 12
		public readonly SupportsToBeDeleted _supportsToBeDeleted;

		// Token: 0x0400000D RID: 13
		public readonly ITerrainService _terrainService;

		// Token: 0x0400000E RID: 14
		public readonly TerrainOnBlockObjectFinder _terrainOnBlockObjectFinder;

		// Token: 0x0400000F RID: 15
		public readonly Queue<Vector3Int> _terrainToCheck = new Queue<Vector3Int>();

		// Token: 0x04000010 RID: 16
		public readonly Queue<BlockObject> _blockObjectsToCheck = new Queue<BlockObject>();

		// Token: 0x04000011 RID: 17
		public readonly HashSet<Vector3Int> _checkedTerrain = new HashSet<Vector3Int>();

		// Token: 0x04000012 RID: 18
		public readonly Queue<Vector3Int> _blockObjectCoordinatesToCheck = new Queue<Vector3Int>();

		// Token: 0x04000013 RID: 19
		public TerrainPhysicsValidator _terrainPhysicsValidator;
	}
}
