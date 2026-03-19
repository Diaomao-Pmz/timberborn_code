using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.Coordinates;
using Timberborn.TransformControl;
using UnityEngine;

namespace Timberborn.BlockSystem
{
	// Token: 0x02000060 RID: 96
	public class Preview : BaseComponent, IAwakableComponent, IStartableComponent
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600026F RID: 623 RVA: 0x00007846 File Offset: 0x00005A46
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000784E File Offset: 0x00005A4E
		public BlockObject BlockObject { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000271 RID: 625 RVA: 0x00007857 File Offset: 0x00005A57
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000785F File Offset: 0x00005A5F
		public PreviewState PreviewState { get; private set; }

		// Token: 0x06000273 RID: 627 RVA: 0x00007868 File Offset: 0x00005A68
		public void Awake()
		{
			this.BlockObject = base.GetComponent<BlockObject>();
			base.GetComponents<IPreviewServiceMember>(this._previewServiceMembers);
			base.GetComponents<IPreviewSelectionListener>(this._previewSelectionListeners);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000788E File Offset: 0x00005A8E
		public void Start()
		{
			if (this.BlockObject.IsPreview)
			{
				this.FixGlitchesCausedByPerfectOverlapping();
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x000078A3 File Offset: 0x00005AA3
		public void Reposition(Placement placement)
		{
			this.BlockObject.Reposition(placement);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000078B1 File Offset: 0x00005AB1
		public void Show(PreviewState previewState)
		{
			this.PreviewState = previewState;
			base.GameObject.SetActive(true);
			this.SelectPreviewSelectionListeners();
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000078CC File Offset: 0x00005ACC
		public void Hide()
		{
			base.GameObject.SetActive(false);
			this.UnselectPreviewSelectionListeners();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x000078E0 File Offset: 0x00005AE0
		public void AddToPreviewServices()
		{
			for (int i = 0; i < this._previewServiceMembers.Count; i++)
			{
				this._previewServiceMembers[i].AddToPreviewService();
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00007914 File Offset: 0x00005B14
		public void RemoveFromPreviewServices()
		{
			for (int i = 0; i < this._previewServiceMembers.Count; i++)
			{
				this._previewServiceMembers[i].RemoveFromPreviewService();
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00007948 File Offset: 0x00005B48
		public void SelectPreviewSelectionListeners()
		{
			foreach (IPreviewSelectionListener previewSelectionListener in this._previewSelectionListeners)
			{
				previewSelectionListener.OnPreviewSelect();
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00007998 File Offset: 0x00005B98
		public void UnselectPreviewSelectionListeners()
		{
			foreach (IPreviewSelectionListener previewSelectionListener in this._previewSelectionListeners)
			{
				previewSelectionListener.OnPreviewUnselect();
			}
		}

		// Token: 0x0600027C RID: 636 RVA: 0x000079E8 File Offset: 0x00005BE8
		public void FixGlitchesCausedByPerfectOverlapping()
		{
			base.GetComponent<TransformController>().AddPositionModifier().Set(new Vector3(0f, 0.005f, 0f));
		}

		// Token: 0x0400012C RID: 300
		public readonly List<IPreviewServiceMember> _previewServiceMembers = new List<IPreviewServiceMember>();

		// Token: 0x0400012D RID: 301
		public readonly List<IPreviewSelectionListener> _previewSelectionListeners = new List<IPreviewSelectionListener>();
	}
}
