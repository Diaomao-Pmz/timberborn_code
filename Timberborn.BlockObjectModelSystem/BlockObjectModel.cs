using System;
using Timberborn.BaseComponentSystem;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.BlockObjectModelSystem
{
	// Token: 0x02000007 RID: 7
	public class BlockObjectModel : BaseComponent, IAwakableComponent, IBlockObjectModel
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020FE File Offset: 0x000002FE
		// (set) Token: 0x06000008 RID: 8 RVA: 0x00002106 File Offset: 0x00000306
		public GameObject FullModel { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000210F File Offset: 0x0000030F
		// (set) Token: 0x0600000A RID: 10 RVA: 0x00002117 File Offset: 0x00000317
		public int UndergroundModelDepth { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002120 File Offset: 0x00000320
		public bool HasUndergroundModel
		{
			get
			{
				return this._undergroundModel;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000212D File Offset: 0x0000032D
		public bool HasUncoveredModel
		{
			get
			{
				return this._uncoveredModel;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000213A File Offset: 0x0000033A
		public bool UnfinishedConstructionModeModel
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002140 File Offset: 0x00000340
		public void Awake()
		{
			this._blockObjectModelController = base.GetComponent<BlockObjectModelController>();
			BlockObjectModelSpec component = base.GetComponent<BlockObjectModelSpec>();
			this.UndergroundModelDepth = component.UndergroundModelDepth;
			this.FullModel = base.GameObject.FindChildIfNameNotEmpty(component.FullModelName);
			this._uncoveredModel = base.GameObject.FindChildIfNameNotEmpty(component.UncoveredModelName);
			this._undergroundModel = base.GameObject.FindChildIfNameNotEmpty(component.UndergroundModelName);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021B4 File Offset: 0x000003B4
		public void UpdateModelVisibility()
		{
			bool modelBlocked = this._blockObjectModelController.ModelBlocked;
			bool shouldShowUncoveredModel = this._blockObjectModelController.ShouldShowUncoveredModel;
			bool shouldShowUndergroundModel = this._blockObjectModelController.ShouldShowUndergroundModel;
			bool flag = !modelBlocked && !shouldShowUncoveredModel && !shouldShowUndergroundModel && !this._fullModelPermanentlyHidden;
			bool flag2 = !modelBlocked && shouldShowUncoveredModel;
			bool flag3 = !modelBlocked && shouldShowUndergroundModel;
			if (flag3)
			{
				Vector3 localPosition = this._undergroundModel.transform.localPosition;
				this._undergroundModel.transform.localPosition = new Vector3(localPosition.x, (float)this._blockObjectModelController.UndergroundModelZOffset, localPosition.z);
			}
			this.FullModel.ToggleModelVisibility(flag, true);
			this._uncoveredModel.ToggleModelVisibility(flag2, false);
			this._undergroundModel.ToggleModelVisibility(flag3, false);
			this._blockObjectModelController.SetModelState(flag || flag2, true, flag2);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002285 File Offset: 0x00000485
		public void HideFullModelPermanently()
		{
			this._fullModelPermanentlyHidden = true;
			this.UpdateModelVisibility();
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002294 File Offset: 0x00000494
		public void UnhideFullModelPermanently()
		{
			this._fullModelPermanentlyHidden = false;
			this.UpdateModelVisibility();
		}

		// Token: 0x0400000A RID: 10
		public BlockObjectModelController _blockObjectModelController;

		// Token: 0x0400000B RID: 11
		public GameObject _uncoveredModel;

		// Token: 0x0400000C RID: 12
		public GameObject _undergroundModel;

		// Token: 0x0400000D RID: 13
		public bool _fullModelPermanentlyHidden;
	}
}
