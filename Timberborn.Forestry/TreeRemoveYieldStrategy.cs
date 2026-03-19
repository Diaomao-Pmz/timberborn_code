using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.ReservableSystem;
using Timberborn.Yielding;

namespace Timberborn.Forestry
{
	// Token: 0x0200001A RID: 26
	public class TreeRemoveYieldStrategy : BaseComponent, IAwakableComponent, IRemoveYieldStrategy
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000096 RID: 150 RVA: 0x00003250 File Offset: 0x00001450
		// (remove) Token: 0x06000097 RID: 151 RVA: 0x00003288 File Offset: 0x00001488
		public event EventHandler<TreeCutter> CuttingStarted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000098 RID: 152 RVA: 0x000032C0 File Offset: 0x000014C0
		// (remove) Token: 0x06000099 RID: 153 RVA: 0x000032F8 File Offset: 0x000014F8
		public event EventHandler CuttingStopped;

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000332D File Offset: 0x0000152D
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003335 File Offset: 0x00001535
		public ReservableReacher Reacher { get; private set; }

		// Token: 0x0600009C RID: 156 RVA: 0x0000333E File Offset: 0x0000153E
		public TreeRemoveYieldStrategy(TreeCuttingArea treeCuttingArea)
		{
			this._treeCuttingArea = treeCuttingArea;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000334D File Offset: 0x0000154D
		public string Id
		{
			get
			{
				return "Cuttable";
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003354 File Offset: 0x00001554
		public bool IsStillRemovable
		{
			get
			{
				return this._treeCuttingArea.IsInCuttingArea(this._blockObject.Coordinates);
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000336C File Offset: 0x0000156C
		public void Awake()
		{
			this.Reacher = base.GetComponent<TreeReacher>();
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003386 File Offset: 0x00001586
		public void StartCutting(TreeCutter treeCutter)
		{
			EventHandler<TreeCutter> cuttingStarted = this.CuttingStarted;
			if (cuttingStarted == null)
			{
				return;
			}
			cuttingStarted(this, treeCutter);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000339A File Offset: 0x0000159A
		public void StopCutting()
		{
			EventHandler cuttingStopped = this.CuttingStopped;
			if (cuttingStopped == null)
			{
				return;
			}
			cuttingStopped(this, EventArgs.Empty);
		}

		// Token: 0x04000038 RID: 56
		public readonly TreeCuttingArea _treeCuttingArea;

		// Token: 0x04000039 RID: 57
		public BlockObject _blockObject;
	}
}
