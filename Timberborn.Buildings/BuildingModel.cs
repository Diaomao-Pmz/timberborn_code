using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.Buildings
{
	// Token: 0x0200000D RID: 13
	public class BuildingModel : BaseComponent, IAwakableComponent, IBlockObjectModel, IFinishedStateListener, IUnfinishedStateListener, IPreviewStateListener
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002779 File Offset: 0x00000979
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002781 File Offset: 0x00000981
		public GameObject FinishedModel { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000278A File Offset: 0x0000098A
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002792 File Offset: 0x00000992
		public GameObject UnfinishedModel { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000049 RID: 73 RVA: 0x0000279B File Offset: 0x0000099B
		// (set) Token: 0x0600004A RID: 74 RVA: 0x000027A3 File Offset: 0x000009A3
		public GameObject FinishedUncoveredModel { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600004B RID: 75 RVA: 0x000027AC File Offset: 0x000009AC
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000027B4 File Offset: 0x000009B4
		public int UndergroundModelDepth { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000027BD File Offset: 0x000009BD
		public bool HasUnfinishedModel
		{
			get
			{
				return this.UnfinishedModel;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000027CA File Offset: 0x000009CA
		public bool HasUncoveredModel
		{
			get
			{
				return this.FinishedUncoveredModel;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000027D7 File Offset: 0x000009D7
		public bool HasUndergroundModel
		{
			get
			{
				return this._undergroundModel;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000027E4 File Offset: 0x000009E4
		public bool UnfinishedConstructionModeModel
		{
			get
			{
				return this._buildingModelSpec.UnfinishedConstructionModeModel;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000027F1 File Offset: 0x000009F1
		public bool IsUnfinishedModelBlocked
		{
			get
			{
				return this._blockUnfinishedModel;
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000027FC File Offset: 0x000009FC
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._blockObjectModelController = base.GetComponent<BlockObjectModelController>();
			this._buildingModelSpec = base.GetComponent<BuildingModelSpec>();
			this.UndergroundModelDepth = this._buildingModelSpec.UndergroundModelDepth;
			this.FinishedModel = base.GameObject.FindChildIfNameNotEmpty(this._buildingModelSpec.FinishedModelName);
			this.UnfinishedModel = base.GameObject.FindChildIfNameNotEmpty(this._buildingModelSpec.UnfinishedModelName);
			this.FinishedUncoveredModel = base.GameObject.FindChildIfNameNotEmpty(this._buildingModelSpec.FinishedUncoveredModelName);
			this._undergroundModel = base.GameObject.FindChildIfNameNotEmpty(this._buildingModelSpec.UndergroundModelName);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000028AE File Offset: 0x00000AAE
		public void OnEnterFinishedState()
		{
			this.ShowFinishedModel();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000021B9 File Offset: 0x000003B9
		public void OnExitFinishedState()
		{
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000028B6 File Offset: 0x00000AB6
		public void OnEnterUnfinishedState()
		{
			this.ShowUnfinishedModel();
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000021B9 File Offset: 0x000003B9
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000028AE File Offset: 0x00000AAE
		public void OnEnterPreviewState()
		{
			this.ShowFinishedModel();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000028BE File Offset: 0x00000ABE
		public void ShowFinishedModel()
		{
			this._showFinishedModel = true;
			this.UpdateModelVisibility();
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000028CD File Offset: 0x00000ACD
		public void ShowUnfinishedModel()
		{
			this._showFinishedModel = false;
			this.UpdateModelVisibility();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000028DC File Offset: 0x00000ADC
		public void BlockUnfinishedModel()
		{
			this._blockUnfinishedModel = true;
			this.UpdateModelVisibility();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000028EB File Offset: 0x00000AEB
		public void UnblockUnfinishedModel()
		{
			this._blockUnfinishedModel = false;
			this.UpdateModelVisibility();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000028FC File Offset: 0x00000AFC
		public void UpdateModelVisibility()
		{
			if (this._blockObjectModelController.ModelBlocked)
			{
				this.ToggleModelVisibility(false, !this._blockObject.IsPreview && this._showFinishedModel, false, false, false);
				return;
			}
			if (this.ForceUnfinishedModel)
			{
				this.ToggleModelVisibility(false, false, false, true, false);
				return;
			}
			if (this._showFinishedModel)
			{
				bool shouldShowUncoveredModel = this._blockObjectModelController.ShouldShowUncoveredModel;
				bool shouldShowUndergroundModel = this._blockObjectModelController.ShouldShowUndergroundModel;
				this.ToggleModelVisibility(!shouldShowUncoveredModel && !shouldShowUndergroundModel, true, shouldShowUncoveredModel, false, shouldShowUndergroundModel);
				return;
			}
			bool shouldShowUndergroundModel2 = this._blockObjectModelController.ShouldShowUndergroundModel;
			this.ToggleModelVisibility(false, false, false, !this._blockUnfinishedModel && !shouldShowUndergroundModel2, shouldShowUndergroundModel2);
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000029A1 File Offset: 0x00000BA1
		public bool ForceUnfinishedModel
		{
			get
			{
				return this.UnfinishedConstructionModeModel && this._blockObject.IsUnfinished && !this._blockUnfinishedModel;
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000029C4 File Offset: 0x00000BC4
		public void ToggleModelVisibility(bool showFinished, bool showFinishedShadows, bool showFinishedUncovered, bool showUnfinished, bool showUndergroundModel)
		{
			this.FinishedModel.ToggleModelVisibility(showFinished, showFinishedShadows);
			this.FinishedUncoveredModel.ToggleModelVisibility(showFinishedUncovered, false);
			this.UnfinishedModel.ToggleModelVisibility(showUnfinished, showUnfinished);
			this._undergroundModel.ToggleModelVisibility(showUndergroundModel, false);
			if (showUndergroundModel)
			{
				Vector3 localPosition = this._undergroundModel.transform.localPosition;
				this._undergroundModel.transform.localPosition = new Vector3(localPosition.x, (float)this._blockObjectModelController.UndergroundModelZOffset, localPosition.z);
			}
			this._blockObjectModelController.SetModelState(showFinished || showUnfinished, showFinished || showFinishedUncovered, showFinishedUncovered);
		}

		// Token: 0x04000019 RID: 25
		public BlockObject _blockObject;

		// Token: 0x0400001A RID: 26
		public BlockObjectModelController _blockObjectModelController;

		// Token: 0x0400001B RID: 27
		public BuildingModelSpec _buildingModelSpec;

		// Token: 0x0400001C RID: 28
		public bool _showFinishedModel;

		// Token: 0x0400001D RID: 29
		public bool _blockUnfinishedModel;

		// Token: 0x0400001E RID: 30
		public GameObject _undergroundModel;
	}
}
