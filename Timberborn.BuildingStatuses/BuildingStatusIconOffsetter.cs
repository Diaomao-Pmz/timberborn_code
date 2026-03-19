using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.Common;
using Timberborn.ConstructionMode;
using Timberborn.ConstructionSites;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.StatusSystem;
using UnityEngine;

namespace Timberborn.BuildingStatuses
{
	// Token: 0x02000005 RID: 5
	public class BuildingStatusIconOffsetter : BaseComponent, IAwakableComponent, IDeletableEntity, IStatusIconOffsetter, IPreInitializableEntity
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000211B File Offset: 0x0000031B
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002123 File Offset: 0x00000323
		public Vector3 Position { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x0000212C File Offset: 0x0000032C
		// (set) Token: 0x06000009 RID: 9 RVA: 0x00002134 File Offset: 0x00000334
		public Vector2Int Key { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000213D File Offset: 0x0000033D
		// (set) Token: 0x0600000B RID: 11 RVA: 0x00002145 File Offset: 0x00000345
		public BlockObject BlockObject { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000214E File Offset: 0x0000034E
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002156 File Offset: 0x00000356
		public float FinishedTopBound { get; private set; }

		// Token: 0x0600000E RID: 14 RVA: 0x0000215F File Offset: 0x0000035F
		public BuildingStatusIconOffsetter(IStatusIconOffsetService statusIconOffsetService, BoundsCalculator boundsCalculator, ConstructionModeService constructionModeService)
		{
			this._statusIconOffsetService = statusIconOffsetService;
			this._boundsCalculator = boundsCalculator;
			this._constructionModeService = constructionModeService;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000217C File Offset: 0x0000037C
		public bool StatusActive
		{
			get
			{
				return this._statusIconCycler.VisibleAndActive;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002189 File Offset: 0x00000389
		public float TopBound
		{
			get
			{
				if (!this.ShouldUseFinishedBound)
				{
					return this.GetUnfinishedTopBound();
				}
				return this.FinishedTopBound;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021A0 File Offset: 0x000003A0
		public void Awake()
		{
			this.BlockObject = base.GetComponent<BlockObject>();
			this._buildingModel = base.GetComponent<BuildingModel>();
			this._blockObjectModelController = base.GetComponent<BlockObjectModelController>();
			this._blockObjectModelController.ModelsUpdated += this.OnModelsUpdated;
			this._statusIconCycler = base.GetComponent<StatusIconCycler>();
			this._statusIconCycler.InitializeIcon(base.Transform, 0.5f);
			this._statusIconCycler.ActiveStateChanged += this.OnActiveStateChanged;
			this._statusVisibilityToggle = this._statusIconCycler.GetStatusVisibilityToggle();
			this._constructionSiteProgressVisualizer = base.GetComponent<ConstructionSiteProgressVisualizer>();
			if (this._constructionSiteProgressVisualizer)
			{
				this._constructionSiteProgressVisualizer.StageChanged += this.OnStageChanged;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002264 File Offset: 0x00000464
		public void PreInitializeEntity()
		{
			this.Position = base.GetComponent<BlockObjectCenter>().GridCenter;
			this.Key = new Vector2Int(Mathf.RoundToInt(this.Position.x * 2f), Mathf.RoundToInt(this.Position.y * 2f));
			this._statusIconOffsetService.AddOffsetter(this);
			this._statusIconCycler.Root.transform.position = CoordinateSystem.GridToWorld(this.Position);
			this.FinishedTopBound = this.GetFinishedTopBound();
			float unfinishedTopBound = this.GetUnfinishedTopBound();
			this._shouldAlwaysUseFinishedBound = (unfinishedTopBound > this.FinishedTopBound);
			this._isInitialized = true;
			this._statusIconOffsetService.UpdateIcons(this);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000231A File Offset: 0x0000051A
		public void DeleteEntity()
		{
			this._statusIconOffsetService.RemoveOffsetter(this);
			this._statusIconOffsetService.UpdateIcons(this);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002334 File Offset: 0x00000534
		public void UpdateIcon()
		{
			if (this._isInitialized)
			{
				this.SetIconVisibility();
				this.SetIconPosition();
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000234C File Offset: 0x0000054C
		public float GetUnfinishedTopBound()
		{
			if (this.IsPreviewModelBlocked)
			{
				return (float)this.BlockObject.CoordinatesAtBaseZ.z;
			}
			if (this._constructionSiteProgressVisualizer)
			{
				float num = this._constructionSiteProgressVisualizer.ShouldShowProgress ? BuildingStatusIconOffsetter.FinishedOffset : BuildingStatusIconOffsetter.UnfinishedOffset;
				return this._boundsCalculator.GetEnabledRendererYMaxBound(this._buildingModel.UnfinishedModel.transform) + num;
			}
			return (this._buildingModel.HasUnfinishedModel ? this._boundsCalculator.GetRendererYMaxBound(this._buildingModel.UnfinishedModel.transform) : 0f) + BuildingStatusIconOffsetter.UnfinishedOffset;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000023F0 File Offset: 0x000005F0
		public bool IsPreviewModelBlocked
		{
			get
			{
				return this._buildingModel.IsUnfinishedModelBlocked;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000023FD File Offset: 0x000005FD
		public bool IsShown
		{
			get
			{
				return this._blockObjectModelController.IsAnyModelShown;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000240A File Offset: 0x0000060A
		public bool ShouldUseFinishedBound
		{
			get
			{
				return this._shouldAlwaysUseFinishedBound || (this._constructionModeService.InConstructionMode && !base.GetComponent<BuildingModel>().UnfinishedConstructionModeModel) || this.BlockObject.IsFinished;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000243B File Offset: 0x0000063B
		public float GetFinishedTopBound()
		{
			return this._boundsCalculator.GetRendererYMaxBound(this._buildingModel.FinishedModel.transform) + BuildingStatusIconOffsetter.FinishedOffset;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000245E File Offset: 0x0000065E
		public void OnModelsUpdated(object sender, EventArgs e)
		{
			this.UpdateIcon();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002466 File Offset: 0x00000666
		public void OnActiveStateChanged(object sender, EventArgs eventArgs)
		{
			this._statusIconOffsetService.UpdateIcons(this);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002474 File Offset: 0x00000674
		public void OnStageChanged(object sender, EventArgs e)
		{
			float unfinishedTopBound = this.GetUnfinishedTopBound();
			if (this._previousUnfinishedTopBound != unfinishedTopBound)
			{
				this._statusIconOffsetService.UpdatePositions(this);
			}
			this._previousUnfinishedTopBound = unfinishedTopBound;
			this._shouldAlwaysUseFinishedBound = (unfinishedTopBound > this.FinishedTopBound);
			this.UpdateIcon();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024B9 File Offset: 0x000006B9
		public void SetIconVisibility()
		{
			if (this.IsShown)
			{
				this._statusVisibilityToggle.Show();
				return;
			}
			this._statusVisibilityToggle.Hide();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000024DC File Offset: 0x000006DC
		public void SetIconPosition()
		{
			if (this.StatusActive)
			{
				float num = this._statusIconOffsetService.CalculateVerticalPosition(this) - this.Position.z;
				this._statusIconCycler.SetIconLocalPosition(new Vector3(0f, num, 0f));
			}
		}

		// Token: 0x04000006 RID: 6
		public static readonly float FinishedOffset = 0.7f;

		// Token: 0x04000007 RID: 7
		public static readonly float UnfinishedOffset = 1f;

		// Token: 0x0400000C RID: 12
		public readonly IStatusIconOffsetService _statusIconOffsetService;

		// Token: 0x0400000D RID: 13
		public readonly BoundsCalculator _boundsCalculator;

		// Token: 0x0400000E RID: 14
		public readonly ConstructionModeService _constructionModeService;

		// Token: 0x0400000F RID: 15
		public BuildingModel _buildingModel;

		// Token: 0x04000010 RID: 16
		public StatusIconCycler _statusIconCycler;

		// Token: 0x04000011 RID: 17
		public BlockObjectModelController _blockObjectModelController;

		// Token: 0x04000012 RID: 18
		public StatusVisibilityToggle _statusVisibilityToggle;

		// Token: 0x04000013 RID: 19
		public ConstructionSiteProgressVisualizer _constructionSiteProgressVisualizer;

		// Token: 0x04000014 RID: 20
		public bool _isInitialized;

		// Token: 0x04000015 RID: 21
		public bool _shouldAlwaysUseFinishedBound;

		// Token: 0x04000016 RID: 22
		public float _previousUnfinishedTopBound;
	}
}
