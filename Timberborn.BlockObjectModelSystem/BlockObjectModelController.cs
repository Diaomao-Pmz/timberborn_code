using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;

namespace Timberborn.BlockObjectModelSystem
{
	// Token: 0x02000008 RID: 8
	public class BlockObjectModelController : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000013 RID: 19 RVA: 0x000022AC File Offset: 0x000004AC
		// (remove) Token: 0x06000014 RID: 20 RVA: 0x000022E4 File Offset: 0x000004E4
		public event EventHandler ModelsUpdated;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002319 File Offset: 0x00000519
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002321 File Offset: 0x00000521
		public bool IsAnyModelShown { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000232A File Offset: 0x0000052A
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002332 File Offset: 0x00000532
		public bool IsFinishedModelShown { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000233B File Offset: 0x0000053B
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002343 File Offset: 0x00000543
		public bool IsUncoveredModelShown { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000234C File Offset: 0x0000054C
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002354 File Offset: 0x00000554
		public bool ShouldShowUncoveredModel { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000235D File Offset: 0x0000055D
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002365 File Offset: 0x00000565
		public bool ShouldShowUndergroundModel { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000236E File Offset: 0x0000056E
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002376 File Offset: 0x00000576
		public BlockObject BlockObject { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000237F File Offset: 0x0000057F
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002387 File Offset: 0x00000587
		public int UndergroundModelZOffset { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002390 File Offset: 0x00000590
		public bool HasUndergroundModel
		{
			get
			{
				return this._blockObjectModel.HasUndergroundModel;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000239D File Offset: 0x0000059D
		public bool ModelBlocked
		{
			get
			{
				return this._modelBlocked && !this._modelBlockageIgnored;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000023B2 File Offset: 0x000005B2
		public bool HasUncoveredModel
		{
			get
			{
				return this._blockObjectModel.HasUncoveredModel;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000023C0 File Offset: 0x000005C0
		public int UndergroundBaseZ
		{
			get
			{
				if (this._infiniteUndergroundModel == null)
				{
					return Math.Max(0, this.BlockObject.CoordinatesAtBaseZ.z - this._blockObjectModel.UndergroundModelDepth);
				}
				return 0;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000023FC File Offset: 0x000005FC
		public void Awake()
		{
			this.BlockObject = base.GetComponent<BlockObject>();
			this._blockObjectModel = base.GetComponent<IBlockObjectModel>();
			this._infiniteUndergroundModel = base.GetComponent<IInfiniteUndergroundModel>();
			base.GetComponents<IModelUpdater>(this._modelUpdaters);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000242E File Offset: 0x0000062E
		public void Start()
		{
			this.UpdateAll();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002438 File Offset: 0x00000638
		public void UpdateModel()
		{
			foreach (IModelUpdater modelUpdater in this._modelUpdaters)
			{
				modelUpdater.UpdateModel();
			}
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002488 File Offset: 0x00000688
		public void UpdateAll()
		{
			this.UpdateModel();
			this._blockObjectModel.UpdateModelVisibility();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000249B File Offset: 0x0000069B
		public void IgnoreModelBlockade()
		{
			this._modelBlockageIgnored = true;
			this.UpdateModelVisibility();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024AA File Offset: 0x000006AA
		public void UnignoreModelBlockade()
		{
			this._modelBlockageIgnored = false;
			this.UpdateModelVisibility();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000024B9 File Offset: 0x000006B9
		public void BlockModel()
		{
			this._modelBlocked = true;
			this.UpdateModelVisibility();
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000024C8 File Offset: 0x000006C8
		public void UnblockModel()
		{
			this._modelBlocked = false;
			this.UpdateModelVisibility();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000024D7 File Offset: 0x000006D7
		public void ShowUncoveredModel()
		{
			this.ShouldShowUncoveredModel = true;
			this.UpdateModelVisibility();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000024E6 File Offset: 0x000006E6
		public void HideUncoveredModel()
		{
			this.ShouldShowUncoveredModel = false;
			this.UpdateModelVisibility();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000024F5 File Offset: 0x000006F5
		public void ShowUndergroundModel()
		{
			this.ShouldShowUndergroundModel = true;
			this.UpdateModelVisibility();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002504 File Offset: 0x00000704
		public void HideUndergroundModel()
		{
			this.ShouldShowUndergroundModel = false;
			this.SetUndergroundModelZOffset(0);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002514 File Offset: 0x00000714
		public void SetUndergroundModelZOffset(int zOffset)
		{
			this.UndergroundModelZOffset = zOffset;
			this.UpdateModelVisibility();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002523 File Offset: 0x00000723
		public void SetModelState(bool isModelShown, bool isFinishedModelShown, bool isUncoveredModelShown)
		{
			this.IsAnyModelShown = isModelShown;
			this.IsFinishedModelShown = isFinishedModelShown;
			this.IsUncoveredModelShown = isUncoveredModelShown;
			EventHandler modelsUpdated = this.ModelsUpdated;
			if (modelsUpdated == null)
			{
				return;
			}
			modelsUpdated(this, EventArgs.Empty);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002550 File Offset: 0x00000750
		public void UpdateModelVisibility()
		{
			this._blockObjectModel.UpdateModelVisibility();
		}

		// Token: 0x04000016 RID: 22
		public IBlockObjectModel _blockObjectModel;

		// Token: 0x04000017 RID: 23
		public IInfiniteUndergroundModel _infiniteUndergroundModel;

		// Token: 0x04000018 RID: 24
		public readonly List<IModelUpdater> _modelUpdaters = new List<IModelUpdater>();

		// Token: 0x04000019 RID: 25
		public bool _modelBlocked;

		// Token: 0x0400001A RID: 26
		public bool _modelBlockageIgnored;
	}
}
