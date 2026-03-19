using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.GameDistricts;
using Timberborn.SelectionSystem;

namespace Timberborn.BuildingsNavigation
{
	// Token: 0x02000022 RID: 34
	public class PathRangeDrawer : BaseComponent, IAwakableComponent, IUpdatableComponent, ISelectionListener, IPreviewSelectionListener
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00004498 File Offset: 0x00002698
		public PathRangeDrawer(DistrictCenterRegistry districtCenterRegistry)
		{
			this._districtCenterRegistry = districtCenterRegistry;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000044A7 File Offset: 0x000026A7
		public void Awake()
		{
			this._blockObjectCenter = base.GetComponent<BlockObjectCenter>();
			this._blockObject = base.GetComponent<BlockObject>();
			this._preview = base.GetComponent<Preview>();
			this._pathDistrictRetriever = base.GetComponent<PathDistrictRetriever>();
			base.DisableComponent();
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000044DF File Offset: 0x000026DF
		public void Update()
		{
			this.DrawRange(true);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000044E8 File Offset: 0x000026E8
		public void OnSelect()
		{
			this.DrawRange(true);
			base.EnableComponent();
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00002D4B File Offset: 0x00000F4B
		public void OnUnselect()
		{
			base.DisableComponent();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000044F8 File Offset: 0x000026F8
		public void OnPreviewSelect()
		{
			if (this._preview.PreviewState.IsLast)
			{
				this.DrawRange(this._preview.PreviewState.IsSingle);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004533 File Offset: 0x00002733
		public void OnPreviewUnselect()
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004538 File Offset: 0x00002738
		public void DrawRange(bool isSingle = true)
		{
			bool isPreview = this._blockObject.IsPreview;
			bool isFinished = this._blockObject.IsFinished;
			DistrictCenter districtCenter = this.GetDistrictCenter(isPreview, isFinished);
			if (districtCenter)
			{
				DistrictPathNavRangeDrawer component = districtCenter.GetComponent<DistrictPathNavRangeDrawer>();
				DrawingParameters drawingParameters = new DrawingParameters(isPreview || !isFinished, this._blockObjectCenter.WorldCenterGrounded, this._preview.BlockObject.Orientation, isSingle);
				component.DrawRange(drawingParameters);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000045A8 File Offset: 0x000027A8
		public DistrictCenter GetDistrictCenter(bool isPreview, bool isFinished)
		{
			if (isFinished)
			{
				return this._pathDistrictRetriever.GetFinishedDistrictCenter();
			}
			if (!isPreview || this._preview.PreviewState.IsBuildable)
			{
				return this.GetDistrictCenter();
			}
			return null;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000045E4 File Offset: 0x000027E4
		public DistrictCenter GetDistrictCenter()
		{
			foreach (DistrictCenter districtCenter in this._districtCenterRegistry.AllDistrictCenters)
			{
				if (districtCenter.IsOnPreviewDistrictRoad(this._blockObjectCenter.WorldCenterGrounded))
				{
					return districtCenter;
				}
			}
			return null;
		}

		// Token: 0x04000075 RID: 117
		public readonly DistrictCenterRegistry _districtCenterRegistry;

		// Token: 0x04000076 RID: 118
		public BlockObjectCenter _blockObjectCenter;

		// Token: 0x04000077 RID: 119
		public BlockObject _blockObject;

		// Token: 0x04000078 RID: 120
		public Preview _preview;

		// Token: 0x04000079 RID: 121
		public PathDistrictRetriever _pathDistrictRetriever;
	}
}
