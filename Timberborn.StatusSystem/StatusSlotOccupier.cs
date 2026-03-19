using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockObjectModelSystem;
using Timberborn.BlockSystem;
using Timberborn.EntitySystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x0200001D RID: 29
	public class StatusSlotOccupier : BaseComponent, IAwakableComponent, IInitializableEntity
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00003A5A File Offset: 0x00001C5A
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00003A62 File Offset: 0x00001C62
		public bool UseUnfinishedConstructionModeModel { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00003A6B File Offset: 0x00001C6B
		public bool IsUnfinished
		{
			get
			{
				return this._blockObject.IsUnfinished;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003A78 File Offset: 0x00001C78
		public byte BaseZ
		{
			get
			{
				return (byte)this._blockObject.CoordinatesAtBaseZ.z;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003A99 File Offset: 0x00001C99
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
			this._statusIconOffsetter = base.GetComponent<IStatusIconOffsetter>();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003AB4 File Offset: 0x00001CB4
		public void InitializeEntity()
		{
			IBlockObjectModel component = base.GetComponent<IBlockObjectModel>();
			if (component != null)
			{
				this.UseUnfinishedConstructionModeModel = component.UnfinishedConstructionModeModel;
			}
			Vector3Int size = base.GetComponent<BlockObjectSpec>().Size;
			this._topZCoordinate = this._blockObject.CoordinatesAtBaseZ.z + size.z - 1;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003B06 File Offset: 0x00001D06
		public float GetNormalModeTopBound()
		{
			if (this._blockObject.IsFinished)
			{
				IStatusIconOffsetter statusIconOffsetter = this._statusIconOffsetter;
				if (statusIconOffsetter == null)
				{
					return 0f;
				}
				return statusIconOffsetter.FinishedTopBound;
			}
			else
			{
				IStatusIconOffsetter statusIconOffsetter2 = this._statusIconOffsetter;
				if (statusIconOffsetter2 == null)
				{
					return 0f;
				}
				return statusIconOffsetter2.GetUnfinishedTopBound();
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003B40 File Offset: 0x00001D40
		public TopBoundForLayer GetTopBound(Vector3Int coordinates)
		{
			Block block = this._blockObject.PositionedBlocks.GetBlock(coordinates);
			if ((coordinates.z == this._topZCoordinate || block.Occupation.Intersects(SlotBlockOccupation.Default)) && this._statusIconOffsetter != null)
			{
				return new TopBoundForLayer(this._statusIconOffsetter.FinishedTopBound, this.GetNormalModeTopBound());
			}
			return new TopBoundForLayer(0f, 0f);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public bool IntersectsAt(Vector3Int coordinates, BlockOccupations occupations)
		{
			return this._blockObject.PositionedBlocks.GetBlock(coordinates).Occupation.Intersects(occupations);
		}

		// Token: 0x04000063 RID: 99
		public BlockObject _blockObject;

		// Token: 0x04000064 RID: 100
		public IStatusIconOffsetter _statusIconOffsetter;

		// Token: 0x04000065 RID: 101
		public int _topZCoordinate;
	}
}
