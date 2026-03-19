using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.TemplateAttachmentSystem;
using Timberborn.TimbermeshAnimations;

namespace Timberborn.WorkerOutfitSystem
{
	// Token: 0x02000009 RID: 9
	public class WorkerOutfitAnimationAttachmentVisibility : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600001F RID: 31 RVA: 0x00002558 File Offset: 0x00000758
		public void Awake()
		{
			this._templateAttachments = base.GetComponent<TemplateAttachments>();
			this._workerOutfitAnimationAttachmentVisibilitySpec = base.GetComponent<WorkerOutfitAnimationAttachmentVisibilitySpec>();
			this._animator = base.GetComponentInChildren<IAnimator>(false);
			this._animator.AnimationChanged += this.OnAnimationChanged;
			base.GetComponent<WorkerOutfitChangeNotifier>().OutfitChanged += this.OnOutfitChanged;
			this.Initialize();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025BE File Offset: 0x000007BE
		public void OnAnimationChanged(object sender, EventArgs e)
		{
			this.UpdateAttachments();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025C6 File Offset: 0x000007C6
		public void OnOutfitChanged(object sender, WorkerOutfitChangedEventArgs e)
		{
			WorkerOutfitSpec workerOutfitSpec = e.WorkerOutfitSpec;
			this._currentOutfit = ((workerOutfitSpec != null) ? workerOutfitSpec.Id : null);
			this.UpdateAttachments();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025E8 File Offset: 0x000007E8
		public void Initialize()
		{
			foreach (WorkerOutfitAnimationAttachmentSpec spec in this._workerOutfitAnimationAttachmentVisibilitySpec.WorkerOutfitAnimationAttachments)
			{
				this._animationAttachments.Add(new WorkerOutfitAnimationAttachment(spec, this._templateAttachments));
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002634 File Offset: 0x00000834
		public void UpdateAttachments()
		{
			foreach (WorkerOutfitAnimationAttachment workerOutfitAnimationAttachment in this._animationAttachments)
			{
				workerOutfitAnimationAttachment.UpdateState(this._currentOutfit, this._animator.AnimationName);
			}
		}

		// Token: 0x0400000F RID: 15
		public TemplateAttachments _templateAttachments;

		// Token: 0x04000010 RID: 16
		public WorkerOutfitAnimationAttachmentVisibilitySpec _workerOutfitAnimationAttachmentVisibilitySpec;

		// Token: 0x04000011 RID: 17
		public IAnimator _animator;

		// Token: 0x04000012 RID: 18
		public bool _initialized;

		// Token: 0x04000013 RID: 19
		public string _currentOutfit;

		// Token: 0x04000014 RID: 20
		public readonly List<WorkerOutfitAnimationAttachment> _animationAttachments = new List<WorkerOutfitAnimationAttachment>();
	}
}
