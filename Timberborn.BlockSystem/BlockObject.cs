using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.ErrorReporting;
using Timberborn.Persistence;
using Timberborn.TemplateSystem;
using Timberborn.TransformControl;
using Timberborn.WorldPersistence;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x0200000B RID: 11
	public class BlockObject : BaseComponent, IAwakableComponent, IPersistentEntity, IDeletableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000026 RID: 38 RVA: 0x000026F8 File Offset: 0x000008F8
		// (remove) Token: 0x06000027 RID: 39 RVA: 0x00002730 File Offset: 0x00000930
		public event EventHandler<bool> OverridableChanged;

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002765 File Offset: 0x00000965
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000276D File Offset: 0x0000096D
		public Vector3Int Coordinates { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002776 File Offset: 0x00000976
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000277E File Offset: 0x0000097E
		public Vector3Int CoordinatesAtBaseZ { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002787 File Offset: 0x00000987
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000278F File Offset: 0x0000098F
		public Orientation Orientation { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002798 File Offset: 0x00000998
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000027A0 File Offset: 0x000009A0
		public FlipMode FlipMode { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027A9 File Offset: 0x000009A9
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000027B1 File Offset: 0x000009B1
		public PositionedBlocks PositionedBlocks { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000027BA File Offset: 0x000009BA
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000027C2 File Offset: 0x000009C2
		public PositionedEntrance PositionedEntrance { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000027CB File Offset: 0x000009CB
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000027D3 File Offset: 0x000009D3
		public bool Solid { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000027DC File Offset: 0x000009DC
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000027E4 File Offset: 0x000009E4
		public bool GroundOnly { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000027ED File Offset: 0x000009ED
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000027F5 File Offset: 0x000009F5
		public bool AboveGround { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000027FE File Offset: 0x000009FE
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002806 File Offset: 0x00000A06
		public bool AddedToService { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000280F File Offset: 0x00000A0F
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002817 File Offset: 0x00000A17
		public bool Overridable { get; private set; }

		// Token: 0x0600003E RID: 62 RVA: 0x00002820 File Offset: 0x00000A20
		public BlockObject(BlockService blockService, BlockValidator blockValidator, OverridenBlockObjectService overridenBlockObjectService, EntityService entityService, BlockObjectValidationService blockObjectValidationService, ILoadingIssueService loadingIssueService)
		{
			this._blockService = blockService;
			this._blockValidator = blockValidator;
			this._overridenBlockObjectService = overridenBlockObjectService;
			this._entityService = entityService;
			this._blockObjectValidationService = blockObjectValidationService;
			this._loadingIssueService = loadingIssueService;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002860 File Offset: 0x00000A60
		public Blocks Blocks
		{
			get
			{
				Blocks result;
				if ((result = this._blocks) == null)
				{
					result = (this._blocks = this._blockObjectSpec.GetBlocks());
				}
				return result;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000040 RID: 64 RVA: 0x0000288B File Offset: 0x00000A8B
		public EntranceBlockSpec Entrance
		{
			get
			{
				return this._blockObjectSpec.Entrance;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002898 File Offset: 0x00000A98
		public int BaseZ
		{
			get
			{
				return this._blockObjectSpec.BaseZ;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000028A5 File Offset: 0x00000AA5
		public Placement Placement
		{
			get
			{
				return new Placement(this.Coordinates, this.Orientation, this.FlipMode);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000028BE File Offset: 0x00000ABE
		public bool HasEntrance
		{
			get
			{
				return this.PositionedEntrance != null;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000028C9 File Offset: 0x00000AC9
		public bool Positioned
		{
			get
			{
				return this.PositionedBlocks != null;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000028D4 File Offset: 0x00000AD4
		public bool IsFinished
		{
			get
			{
				return this._blockObjectState.IsFinished;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000028E1 File Offset: 0x00000AE1
		public bool IsUnfinished
		{
			get
			{
				return this._blockObjectState.IsUnfinished;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000047 RID: 71 RVA: 0x000028EE File Offset: 0x00000AEE
		public bool IsPreview
		{
			get
			{
				return this._blockObjectState.IsPreview;
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000028FC File Offset: 0x00000AFC
		public void Awake()
		{
			this._placementChangeNotifier = base.GetComponent<PlacementChangeNotifier>();
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._blockObjectState = base.GetComponent<BlockObjectState>();
			this._blockObjectSpec = base.GetComponent<BlockObjectSpec>();
			base.GetComponents<IBlockObjectDeletionBlocker>(this._deletionBlockers);
			TransformController component = base.GetComponent<TransformController>();
			this._positionModifier = component.AddPositionModifier();
			this._rotationModifier = component.AddRotationModifier(0);
			this._scaleModifier = component.AddScaleModifier();
			this.Overridable = this._blockObjectSpec.Overridable;
			if (!this.Blocks.GetAllBlocks().Any<Block>())
			{
				throw new Exception(base.Name + " BlockObjectSpec Blocks must contain at least one element");
			}
			this.Solid = this.Blocks.GetOccupiedBlocks().Any((Block block) => block.Stackable > BlockStackable.None);
			this.GroundOnly = this.Blocks.GetAllBlocks().Any((Block block) => block.MatterBelow == MatterBelow.Ground);
			this.AboveGround = this.Blocks.GetAllBlocks().Any((Block block) => block.MatterBelow == MatterBelow.Stackable);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002A49 File Offset: 0x00000C49
		public void DeleteEntity()
		{
			this.RemoveFromService();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002A51 File Offset: 0x00000C51
		public void MarkAsFinished()
		{
			this._blockObjectState.MarkAsFinished();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002A5E File Offset: 0x00000C5E
		public void MarkAsFinishedAndAddToServices()
		{
			this._blockObjectState.MarkAsFinished();
			if (!this.AddedToService)
			{
				this.AddToService();
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002A79 File Offset: 0x00000C79
		public void MarkAsPreview()
		{
			this._blockObjectState.MarkAsPreview();
			if (this.AddedToService)
			{
				this.RemoveFromService();
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002A94 File Offset: 0x00000C94
		public void MarkAsPreviewAndInitialize()
		{
			this._blockObjectState.MarkAsPreview();
			this._blockObjectState.Initialize();
			if (this.AddedToService)
			{
				this.RemoveFromService();
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002ABA File Offset: 0x00000CBA
		public void MakeOverridable()
		{
			this.ToggleOverridable(true);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AC3 File Offset: 0x00000CC3
		public void MakeNonOverridable()
		{
			this.ToggleOverridable(false);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002ACC File Offset: 0x00000CCC
		public void Reposition(Placement placement)
		{
			if (this.Placement != placement || !this.Positioned)
			{
				this._placementChangeNotifier.NotifyPreChangeListeners();
				this.RemoveFromService();
				this.UpdateValues(placement);
				this.AddToService();
				this._placementChangeNotifier.NotifyPostChangeListeners();
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002B18 File Offset: 0x00000D18
		public bool CanDelete()
		{
			return !this._deletionBlockers.FastAny((IBlockObjectDeletionBlocker blocker) => blocker.IsDeletionBlocked);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002B47 File Offset: 0x00000D47
		public Vector2Int TransformTile(Vector2Int tile)
		{
			return this.Blocks.Transform(tile, this.Placement);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002B5B File Offset: 0x00000D5B
		public Vector3Int TransformCoordinates(Vector3Int coordinates)
		{
			return this.Blocks.Transform(coordinates, this.Placement);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B6F File Offset: 0x00000D6F
		public Vector3 TransformCoordinates(Vector3 coordinates)
		{
			return this.Blocks.Transform(coordinates, this.Placement);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B84 File Offset: 0x00000D84
		public Direction2D TransformDirection(Direction2D direction2D)
		{
			return this.Orientation.Transform(this.FlipMode.Transform(direction2D));
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002BAC File Offset: 0x00000DAC
		public Direction3D TransformDirection(Direction3D direction3D)
		{
			return this.FlipMode.Transform(direction3D).RotateHorizontally(this.Orientation);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002BD3 File Offset: 0x00000DD3
		public bool IsIntersecting(Block block)
		{
			return this.PositionedBlocks.HasIntersectingBlock(block);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public Vector3Int CoordinatesBehind()
		{
			return this.TransformCoordinates(new Vector3Int(0, this._blockObjectSpec.Size.y, 0));
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C14 File Offset: 0x00000E14
		public void Save(IEntitySaver entitySaver)
		{
			IObjectSaver component = entitySaver.GetComponent(BlockObject.BlockObjectKey);
			component.Set(BlockObject.CoordinatesKey, this.Coordinates + new Vector3Int(0, 0, this.BaseZ));
			if (this.Orientation != Orientation.Cw0)
			{
				component.Set<Orientation>(BlockObject.OrientationKey, this.Orientation);
			}
			if (this.FlipMode.IsFlipped)
			{
				component.Set(BlockObject.FlippedKey, true);
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002C88 File Offset: 0x00000E88
		public void Load(IEntityLoader entityLoader)
		{
			IObjectLoader objectLoader;
			if (entityLoader.TryGetComponent(BlockObject.BlockObjectKey, out objectLoader))
			{
				this.Coordinates = objectLoader.Get(BlockObject.CoordinatesKey) - new Vector3Int(0, 0, this.BaseZ);
				this.Orientation = (objectLoader.Has<Orientation>(BlockObject.OrientationKey) ? objectLoader.Get<Orientation>(BlockObject.OrientationKey) : Orientation.Cw0);
				this.FlipMode = new FlipMode(this._blockObjectSpec.Flippable && objectLoader.Has<bool>(BlockObject.FlippedKey) && objectLoader.Get(BlockObject.FlippedKey));
			}
			this.UpdateValues(this.Placement);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002D27 File Offset: 0x00000F27
		public bool IsAlmostValid()
		{
			return this._blockValidator.BlocksAlmostValid(this.PositionedBlocks);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002D3A File Offset: 0x00000F3A
		public bool IsValid()
		{
			return this._blockValidator.BlocksValid(this.PositionedBlocks) && this._blockObjectValidationService.IsValid(this);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002D60 File Offset: 0x00000F60
		public void AddToServiceAfterLoad()
		{
			if (!this.AddedToService)
			{
				if (this.IsValid() && base.GetComponent<TemplateSpec>().UsableWithCurrentFeatureToggles)
				{
					this.AddToService();
					return;
				}
				LabeledEntitySpec component = base.GetComponent<LabeledEntitySpec>();
				this._loadingIssueService.AddIssue(string.Format("Can't validate loaded {0} {1} at {2}.", "BlockObject", base.Name, this.Coordinates) + " It's not backward compatible. Deleting it.", BlockObject.BlockObjectLoadingIssueLocKey, component.DisplayNameLocKey, true);
				this._entityService.Delete(this);
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002DE8 File Offset: 0x00000FE8
		public void AddToService()
		{
			if (!this.AddedToService && !this.IsPreview)
			{
				if (!this.IsValid())
				{
					throw new InvalidOperationException(string.Format("Cannot place {0} {1} at {2}.", "BlockObject", base.Name, this.Coordinates));
				}
				this._overridenBlockObjectService.OverrideBlockObjects(this);
				this._blockService.SetObject(this);
				this.AddedToService = true;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002E52 File Offset: 0x00001052
		public void UpdateValues(Placement placement)
		{
			this.Coordinates = placement.Coordinates;
			this.Orientation = placement.Orientation;
			this.FlipMode = placement.FlipMode;
			this.UpdateTransformedBlocks();
			this.UpdateTransform();
			this._blockObjectCenter.UpdateCenter();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002E92 File Offset: 0x00001092
		public void RemoveFromService()
		{
			if (this.AddedToService)
			{
				this._blockService.UnsetObject(this);
				this.AddedToService = false;
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002EAF File Offset: 0x000010AF
		public void ToggleOverridable(bool isOverridable)
		{
			bool addedToService = this.AddedToService;
			this.RemoveFromService();
			this.Overridable = isOverridable;
			if (addedToService)
			{
				this.AddToService();
			}
			EventHandler<bool> overridableChanged = this.OverridableChanged;
			if (overridableChanged == null)
			{
				return;
			}
			overridableChanged(this, isOverridable);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void UpdateTransformedBlocks()
		{
			this.PositionedBlocks = PositionedBlocks.From(this.Blocks, this.Placement);
			this.PositionedEntrance = PositionedEntrance.From(this.Blocks, this.Entrance, this.Placement);
			this.CoordinatesAtBaseZ = this.Coordinates + new Vector3Int(0, 0, this.BaseZ);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002F40 File Offset: 0x00001140
		public void UpdateTransform()
		{
			Vector3 vector = this.Blocks.Pivot(this.Coordinates, this.Orientation) + new Vector3(0f, 0f, (float)this.BaseZ);
			if (this.FlipMode == FlipMode.Unflipped)
			{
				this._positionModifier.Set(CoordinateSystem.GridToWorld(vector));
				this._scaleModifier.Reset();
			}
			else
			{
				Vector3 coordinates = vector + this.Orientation.Transform(new Vector3Int(this.Blocks.Size.x, 0, 0));
				this._positionModifier.Set(CoordinateSystem.GridToWorld(coordinates));
				this._scaleModifier.Set(new Vector3(-1f, 1f, 1f));
			}
			this._rotationModifier.Set(this.Orientation.ToWorldSpaceRotation());
		}

		// Token: 0x04000021 RID: 33
		public static readonly ComponentKey BlockObjectKey = new ComponentKey("BlockObject");

		// Token: 0x04000022 RID: 34
		public static readonly PropertyKey<Vector3Int> CoordinatesKey = new PropertyKey<Vector3Int>("Coordinates");

		// Token: 0x04000023 RID: 35
		public static readonly PropertyKey<Orientation> OrientationKey = new PropertyKey<Orientation>("Orientation");

		// Token: 0x04000024 RID: 36
		public static readonly PropertyKey<bool> FlippedKey = new PropertyKey<bool>("Flipped");

		// Token: 0x04000025 RID: 37
		public static readonly string BlockObjectLoadingIssueLocKey = "LoadingIssue.BlockObjectLoadingIssue";

		// Token: 0x04000032 RID: 50
		public readonly BlockService _blockService;

		// Token: 0x04000033 RID: 51
		public readonly BlockValidator _blockValidator;

		// Token: 0x04000034 RID: 52
		public readonly OverridenBlockObjectService _overridenBlockObjectService;

		// Token: 0x04000035 RID: 53
		public readonly EntityService _entityService;

		// Token: 0x04000036 RID: 54
		public readonly BlockObjectValidationService _blockObjectValidationService;

		// Token: 0x04000037 RID: 55
		public readonly ILoadingIssueService _loadingIssueService;

		// Token: 0x04000038 RID: 56
		public PositionModifier _positionModifier;

		// Token: 0x04000039 RID: 57
		public RotationModifier _rotationModifier;

		// Token: 0x0400003A RID: 58
		public ScaleModifier _scaleModifier;

		// Token: 0x0400003B RID: 59
		public PlacementChangeNotifier _placementChangeNotifier;

		// Token: 0x0400003C RID: 60
		public Blocks _blocks;

		// Token: 0x0400003D RID: 61
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x0400003E RID: 62
		public BlockObjectState _blockObjectState;

		// Token: 0x0400003F RID: 63
		public BlockObjectSpec _blockObjectSpec;

		// Token: 0x04000040 RID: 64
		public readonly List<IBlockObjectDeletionBlocker> _deletionBlockers = new List<IBlockObjectDeletionBlocker>();
	}
}
