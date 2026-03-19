using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Rendering;
using Timberborn.StatusSystem;
using UnityEngine;

namespace Timberborn.Demolishing
{
	// Token: 0x02000014 RID: 20
	public class DemolishableStatusIconOffsetter : BaseComponent, IAwakableComponent, IStatusIconOffsetter, IPreInitializableEntity, IDeletableEntity
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002EAD File Offset: 0x000010AD
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00002EB5 File Offset: 0x000010B5
		public float TopBound { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002EBE File Offset: 0x000010BE
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002EC6 File Offset: 0x000010C6
		public Vector3 Position { get; private set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002ECF File Offset: 0x000010CF
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002ED7 File Offset: 0x000010D7
		public Vector2Int Key { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002EE0 File Offset: 0x000010E0
		// (set) Token: 0x06000083 RID: 131 RVA: 0x00002EE8 File Offset: 0x000010E8
		public BlockObject BlockObject { get; private set; }

		// Token: 0x06000084 RID: 132 RVA: 0x00002EF1 File Offset: 0x000010F1
		public DemolishableStatusIconOffsetter(IStatusIconOffsetService statusIconOffsetService, BoundsCalculator boundsCalculator)
		{
			this._statusIconOffsetService = statusIconOffsetService;
			this._boundsCalculator = boundsCalculator;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002F07 File Offset: 0x00001107
		public float FinishedTopBound
		{
			get
			{
				return this.TopBound;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00002F0F File Offset: 0x0000110F
		public bool StatusActive
		{
			get
			{
				return this._demolishable.IsMarked;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002F1C File Offset: 0x0000111C
		public void Awake()
		{
			this.BlockObject = base.GetComponent<BlockObject>();
			this._demolishable = base.GetComponent<Demolishable>();
			this._markerPosition = base.GetComponent<MarkerPosition>();
			this._demolishable.Marked += delegate(object _, EventArgs _)
			{
				this.UpdateIcon();
			};
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002F5C File Offset: 0x0000115C
		public void PreInitializeEntity()
		{
			this.Position = base.GetComponent<BlockObjectCenter>().GridCenter;
			this.Key = new Vector2Int(Mathf.RoundToInt(this.Position.x * 2f), Mathf.RoundToInt(this.Position.y * 2f));
			this._statusIconOffsetService.AddOffsetter(this);
			this.TopBound = this._boundsCalculator.GetRendererYMaxBound(base.Transform) + DemolishableStatusIconOffsetter.Offset;
			this._statusIconOffsetService.UpdateIcons(this);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002FE6 File Offset: 0x000011E6
		public void DeleteEntity()
		{
			this._statusIconOffsetService.RemoveOffsetter(this);
			this._statusIconOffsetService.UpdateIcons(this);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003000 File Offset: 0x00001200
		public void UpdateIcon()
		{
			float num = this._statusIconOffsetService.CalculateVerticalPosition(this) - this.Position.z - DemolishableStatusIconOffsetter.Offset;
			this._markerPosition.UpdatePosition(new Vector3(0f, 0f, num));
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002F07 File Offset: 0x00001107
		public float GetUnfinishedTopBound()
		{
			return this.TopBound;
		}

		// Token: 0x04000028 RID: 40
		public static readonly float Offset = 1f;

		// Token: 0x0400002D RID: 45
		public readonly IStatusIconOffsetService _statusIconOffsetService;

		// Token: 0x0400002E RID: 46
		public readonly BoundsCalculator _boundsCalculator;

		// Token: 0x0400002F RID: 47
		public Demolishable _demolishable;

		// Token: 0x04000030 RID: 48
		public MarkerPosition _markerPosition;
	}
}
