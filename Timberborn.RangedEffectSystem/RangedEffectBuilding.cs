using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockingSystem;
using Timberborn.BlockSystem;
using Timberborn.BuildingRange;
using Timberborn.Buildings;
using Timberborn.ConstructionMode;
using Timberborn.MechanicalSystem;
using Timberborn.NeedSpecs;
using Timberborn.TemplateSystem;
using Timberborn.TickSystem;
using UnityEngine;

namespace Timberborn.RangedEffectSystem
{
	// Token: 0x0200000F RID: 15
	public class RangedEffectBuilding : TickableComponent, IAwakableComponent, IFinishedStateListener, IBuildingWithRange
	{
		// Token: 0x06000041 RID: 65 RVA: 0x000026F5 File Offset: 0x000008F5
		public RangedEffectBuilding(ConstructionModeService constructionModeService)
		{
			this._constructionModeService = constructionModeService;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000270F File Offset: 0x0000090F
		public int EffectRadius
		{
			get
			{
				return this._rangedEffectBuildingSpec.EffectRadius;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000271C File Offset: 0x0000091C
		public string RangeName
		{
			get
			{
				return this._templateSpec.TemplateName;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000272C File Offset: 0x0000092C
		public void Awake()
		{
			this._blockableObject = base.GetComponent<BlockableObject>();
			this._blockObjectRange = base.GetComponent<BlockObjectRange>();
			this._buildingSounds = base.GetComponent<BuildingSounds>();
			this._mechanicalBuilding = base.GetComponent<MechanicalBuilding>();
			this._templateSpec = base.GetComponent<TemplateSpec>();
			this._rangedEffectApplier = base.GetComponent<RangedEffectApplier>();
			this._rangedEffectBuildingSpec = base.GetComponent<RangedEffectBuildingSpec>();
			base.DisableComponent();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002793 File Offset: 0x00000993
		public override void Tick()
		{
			if (this._mechanicalBuilding)
			{
				this.ToggleActiveState();
				this._rangedEffectApplier.UpdateEfficiency(this._mechanicalBuilding.Efficiency);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000027BE File Offset: 0x000009BE
		public IEnumerable<BaseComponent> GetObjectsInRange()
		{
			return Enumerable.Empty<BaseComponent>();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000027C8 File Offset: 0x000009C8
		public void OnEnterFinishedState()
		{
			this._blockableObject.ObjectBlocked += this.OnObjectBlocked;
			this._blockableObject.ObjectUnblocked += this.OnObjectUnblocked;
			base.EnableComponent();
			this.UpdateRangedEffectApplierState();
			this.ToggleActiveStateInternal(this.Active);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000281C File Offset: 0x00000A1C
		public void OnExitFinishedState()
		{
			this.ToggleActiveStateInternal(false);
			this._blockableObject.ObjectBlocked -= this.OnObjectBlocked;
			this._blockableObject.ObjectUnblocked -= this.OnObjectUnblocked;
			this._rangedEffectApplier.Disable();
			base.DisableComponent();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002870 File Offset: 0x00000A70
		public IEnumerable<Vector3Int> GetBlocksInRange()
		{
			bool finishedOnly = !this._constructionModeService.InConstructionMode;
			return this._blockObjectRange.GetBlocksOnTerrainOrStackableInRectangularRadius(this.EffectRadius, finishedOnly);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000289E File Offset: 0x00000A9E
		public void AddEffect(ContinuousEffectSpec additionalEffect)
		{
			this._effects.Add(additionalEffect);
			this.UpdateRangedEffectApplierState();
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000028B2 File Offset: 0x00000AB2
		public void RemoveEffect(ContinuousEffectSpec additionalEffect)
		{
			if (this._effects.Remove(additionalEffect))
			{
				this.UpdateRangedEffectApplierState();
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000028C8 File Offset: 0x00000AC8
		public bool Active
		{
			get
			{
				return base.Enabled && this._blockableObject.IsUnblocked && this.MechanicalBuildingActive;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000028E7 File Offset: 0x00000AE7
		public bool MechanicalBuildingActive
		{
			get
			{
				return this._mechanicalBuilding == null || this._mechanicalBuilding.ActiveAndPowered;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000028FE File Offset: 0x00000AFE
		public void OnObjectBlocked(object sender, EventArgs e)
		{
			this.ToggleActiveState();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000028FE File Offset: 0x00000AFE
		public void OnObjectUnblocked(object sender, EventArgs e)
		{
			this.ToggleActiveState();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002908 File Offset: 0x00000B08
		public void UpdateRangedEffectApplierState()
		{
			if (this._rangedEffectApplier.Enabled)
			{
				this._rangedEffectApplier.Disable();
			}
			IEnumerable<Vector2Int> blocksInRectangularRadius = this._blockObjectRange.GetBlocksInRectangularRadius(this.EffectRadius);
			this._rangedEffectApplier.Enable(this._effects, blocksInRectangularRadius, this.Active);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002958 File Offset: 0x00000B58
		public void ToggleActiveState()
		{
			bool active = this.Active;
			if (this._wasActive != active)
			{
				this.ToggleActiveStateInternal(active);
				this._rangedEffectApplier.UpdateActiveState(active);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002988 File Offset: 0x00000B88
		public void ToggleActiveStateInternal(bool state)
		{
			this._buildingSounds.ToggleSound(state);
			this._wasActive = state;
		}

		// Token: 0x0400001D RID: 29
		public readonly ConstructionModeService _constructionModeService;

		// Token: 0x0400001E RID: 30
		public BuildingSounds _buildingSounds;

		// Token: 0x0400001F RID: 31
		public BlockableObject _blockableObject;

		// Token: 0x04000020 RID: 32
		public BlockObjectRange _blockObjectRange;

		// Token: 0x04000021 RID: 33
		public MechanicalBuilding _mechanicalBuilding;

		// Token: 0x04000022 RID: 34
		public TemplateSpec _templateSpec;

		// Token: 0x04000023 RID: 35
		public RangedEffectApplier _rangedEffectApplier;

		// Token: 0x04000024 RID: 36
		public RangedEffectBuildingSpec _rangedEffectBuildingSpec;

		// Token: 0x04000025 RID: 37
		public readonly List<ContinuousEffectSpec> _effects = new List<ContinuousEffectSpec>();

		// Token: 0x04000026 RID: 38
		public bool _wasActive;
	}
}
