using System;
using System.Linq;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using UnityEngine;

namespace Timberborn.ConstructionSites
{
	// Token: 0x02000023 RID: 35
	public class GroundedConstructionSite : BaseComponent, IAwakableComponent, IUnfinishedStateListener, IFinishedStateListener, IPostPlacementChangeListener, IConstructionSiteValidator
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060000E8 RID: 232 RVA: 0x00004358 File Offset: 0x00002558
		// (remove) Token: 0x060000E9 RID: 233 RVA: 0x00004390 File Offset: 0x00002590
		public event EventHandler ValidationStateChanged;

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000043C5 File Offset: 0x000025C5
		// (set) Token: 0x060000EB RID: 235 RVA: 0x000043CD File Offset: 0x000025CD
		public bool IsValid { get; private set; } = true;

		// Token: 0x060000EC RID: 236 RVA: 0x000043D6 File Offset: 0x000025D6
		public GroundedConstructionSite(IBlockService blockService, MatterBelowValidator matterBelowValidator)
		{
			this._blockService = blockService;
			this._matterBelowValidator = matterBelowValidator;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000ED RID: 237 RVA: 0x000043F3 File Offset: 0x000025F3
		public bool IsModelValid
		{
			get
			{
				return this.IsValid;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000043FB File Offset: 0x000025FB
		public void Awake()
		{
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004409 File Offset: 0x00002609
		public void OnEnterFinishedState()
		{
			this.UpdateConstructionSitesAtop();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00003A19 File Offset: 0x00001C19
		public void OnExitFinishedState()
		{
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004411 File Offset: 0x00002611
		public void OnEnterUnfinishedState()
		{
			this.Validate();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003A19 File Offset: 0x00001C19
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00004419 File Offset: 0x00002619
		public void OnPostPlacementChanged()
		{
			if (this._blockObject.IsPreview)
			{
				this.Validate();
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004430 File Offset: 0x00002630
		public void Validate()
		{
			bool isValid = this.IsValid;
			this.IsValid = (from block in this._blockObject.PositionedBlocks.GetOccupiedBlocks()
			where block.MatterBelow.IsSolidMatter()
			where block.Coordinates.z == this._blockObject.CoordinatesAtBaseZ.z
			select block).All((Block block) => this.BlockIsGrounded(block));
			if (this.IsValid != isValid)
			{
				EventHandler validationStateChanged = this.ValidationStateChanged;
				if (validationStateChanged == null)
				{
					return;
				}
				validationStateChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000044C0 File Offset: 0x000026C0
		public void UpdateConstructionSitesAtop()
		{
			foreach (Block block2 in from block in this._blockObject.PositionedBlocks.GetOccupiedBlocks()
			where block.Stackable.IsStackable()
			select block)
			{
				this.UpdateConstructionSitesAtopBlock(block2.Coordinates);
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004544 File Offset: 0x00002744
		public void UpdateConstructionSitesAtopBlock(Vector3Int coordinates)
		{
			Vector3Int coordinates2 = coordinates + new Vector3Int(0, 0, 1);
			foreach (BlockObject blockObject in this._blockService.GetObjectsAt(coordinates2))
			{
				if (blockObject.IsUnfinished)
				{
					GroundedConstructionSite component = blockObject.GetComponent<GroundedConstructionSite>();
					if (component != null)
					{
						component.Validate();
					}
				}
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000045C4 File Offset: 0x000027C4
		public bool BlockIsGrounded(in Block block)
		{
			return this._matterBelowValidator.ValidateIgnoringUnfinishedStackable(block);
		}

		// Token: 0x0400006B RID: 107
		public readonly IBlockService _blockService;

		// Token: 0x0400006C RID: 108
		public readonly MatterBelowValidator _matterBelowValidator;

		// Token: 0x0400006D RID: 109
		public BlockObject _blockObject;
	}
}
