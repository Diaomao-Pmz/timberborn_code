using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.ConstructionSites;
using Timberborn.EntitySystem;

namespace Timberborn.UnderstructureSystem
{
	// Token: 0x0200000C RID: 12
	public class UnderstructureConstructionSiteValidator : BaseComponent, IAwakableComponent, IUnfinishedStateListener, IConstructionSiteValidator
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000033 RID: 51 RVA: 0x00002638 File Offset: 0x00000838
		// (remove) Token: 0x06000034 RID: 52 RVA: 0x00002670 File Offset: 0x00000870
		public event EventHandler ValidationStateChanged;

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026A5 File Offset: 0x000008A5
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000026AD File Offset: 0x000008AD
		public bool IsValid { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000037 RID: 55 RVA: 0x000026B6 File Offset: 0x000008B6
		public bool IsModelValid
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026B9 File Offset: 0x000008B9
		public void Awake()
		{
			this._understructureConstraint = base.GetComponent<UnderstructureConstraint>();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026C7 File Offset: 0x000008C7
		public void OnEnterUnfinishedState()
		{
			this.Validate();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000220D File Offset: 0x0000040D
		public void OnExitUnfinishedState()
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000026D0 File Offset: 0x000008D0
		public void Validate()
		{
			bool isValid = this.IsValid;
			EntityComponent understructureEntity = this._understructureConstraint.UnderstructureEntity;
			this.IsValid = (understructureEntity != null && understructureEntity.GetComponent<BlockObject>().IsFinished);
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

		// Token: 0x0400001A RID: 26
		public UnderstructureConstraint _understructureConstraint;
	}
}
