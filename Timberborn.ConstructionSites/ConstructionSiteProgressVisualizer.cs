using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Buildings;
using Timberborn.SlotSystem;
using UnityEngine;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000019 RID: 25
	public class ConstructionSiteProgressVisualizer : BaseComponent, IAwakableComponent, IUnfinishedStateListener, IFinishedStateListener
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000A3 RID: 163 RVA: 0x00003908 File Offset: 0x00001B08
		// (remove) Token: 0x060000A4 RID: 164 RVA: 0x00003940 File Offset: 0x00001B40
		public event EventHandler StageChanged;

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00003975 File Offset: 0x00001B75
		public bool ShouldShowProgress
		{
			get
			{
				return this._constructionSite.WasStarted;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003984 File Offset: 0x00001B84
		public void Awake()
		{
			this._constructionSite = base.GetComponent<ConstructionSite>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._slotManager = base.GetComponent<SlotManager>();
			this._buildingModel = base.GetComponent<BuildingModel>();
			this._constructionSiteProgressVisualizerSpec = base.GetComponent<ConstructionSiteProgressVisualizerSpec>();
			this.InitializeStages();
			this.UpdateVisualization();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000039D9 File Offset: 0x00001BD9
		public void OnEnterUnfinishedState()
		{
			this._constructionSite.OnConstructionSiteProgressed += this.OnConstructionSiteProgressed;
			this.UpdateVisualization();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000039F8 File Offset: 0x00001BF8
		public void OnExitUnfinishedState()
		{
			this._constructionSite.OnConstructionSiteProgressed -= this.OnConstructionSiteProgressed;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003A11 File Offset: 0x00001C11
		public void OnEnterFinishedState()
		{
			this.UpdateVisualization();
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003A19 File Offset: 0x00001C19
		public void OnExitFinishedState()
		{
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00003A1B File Offset: 0x00001C1B
		public ImmutableArray<float> ProgressThresholds
		{
			get
			{
				return this._constructionSiteProgressVisualizerSpec.ProgressThresholds;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A28 File Offset: 0x00001C28
		public void InitializeStages()
		{
			if (!this._buildingModel.UnfinishedModel)
			{
				throw new Exception("Unfinished model not found in BuildingModel of " + base.Name);
			}
			foreach (object obj in this._buildingModel.UnfinishedModel.transform)
			{
				Transform transform = (Transform)obj;
				this._stages.Add(transform.gameObject);
			}
			if (this.ProgressThresholds.Length != this._stages.Count - 1)
			{
				throw new Exception(string.Format("Number of thresholds ({0}) is not equal to number of ", this.ProgressThresholds.Length) + string.Format("stages minus ConstructionBase ({0}) in BuildingModel ", this._stages.Count - 1) + "of " + base.Name);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003A11 File Offset: 0x00001C11
		public void OnConstructionSiteProgressed(object sender, EventArgs e)
		{
			this.UpdateVisualization();
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003B2C File Offset: 0x00001D2C
		public void UpdateVisualization()
		{
			this.HideAll();
			if (!this._blockObject.IsFinished && this._blockObject.Positioned)
			{
				int stageIndex = this.GetStageIndex();
				this._stages[stageIndex].SetActive(true);
				EventHandler stageChanged = this.StageChanged;
				if (stageChanged != null)
				{
					stageChanged(this, EventArgs.Empty);
				}
				this.ReassignAllSlots(stageIndex);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003B90 File Offset: 0x00001D90
		public void HideAll()
		{
			foreach (GameObject gameObject in this._stages)
			{
				gameObject.SetActive(false);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public int GetStageIndex()
		{
			if (this.ShouldShowProgress)
			{
				for (int i = this._stages.Count - 1; i >= 1; i--)
				{
					if (this._constructionSite.BuildTimeProgress >= this.ProgressThresholds[i - 1])
					{
						return i;
					}
				}
			}
			return 0;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003C32 File Offset: 0x00001E32
		public void ReassignAllSlots(int stageIndex)
		{
			if (this._slotsIndex != stageIndex && this._slotManager)
			{
				this._slotsIndex = stageIndex;
				this._slotManager.ReassignAllSlots();
			}
		}

		// Token: 0x04000056 RID: 86
		public ConstructionSite _constructionSite;

		// Token: 0x04000057 RID: 87
		public BlockObject _blockObject;

		// Token: 0x04000058 RID: 88
		public SlotManager _slotManager;

		// Token: 0x04000059 RID: 89
		public BuildingModel _buildingModel;

		// Token: 0x0400005A RID: 90
		public ConstructionSiteProgressVisualizerSpec _constructionSiteProgressVisualizerSpec;

		// Token: 0x0400005B RID: 91
		public readonly List<GameObject> _stages = new List<GameObject>();

		// Token: 0x0400005C RID: 92
		public int _slotsIndex = -1;
	}
}
