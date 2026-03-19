using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.RelationSystem;
using Timberborn.SelectionSystem;
using UnityEngine;

namespace Timberborn.RelationSystemUI
{
	// Token: 0x02000007 RID: 7
	public class RelationHighlighter : BaseComponent, IAwakableComponent, ISelectionListener
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020FB File Offset: 0x000002FB
		public RelationHighlighter(Highlighter highlighter, ISpecService specService)
		{
			this._highlighter = highlighter;
			this._specService = specService;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211C File Offset: 0x0000031C
		public void Awake()
		{
			base.GetComponents<IRelationOwner>(this._relationOwners);
			foreach (IRelationOwner relationOwner in this._relationOwners)
			{
				relationOwner.RelationsChanged += this.OnRelationsChanged;
			}
			this._relationSelectionColor = this._specService.GetSingleSpec<RelationHighlighterSpec>().RelationSelection;
			base.DisableComponent();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021A0 File Offset: 0x000003A0
		public void OnSelect()
		{
			this.HighlightRelations();
			base.EnableComponent();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021AE File Offset: 0x000003AE
		public void OnUnselect()
		{
			this._highlighter.UnhighlightAllPrimary();
			base.DisableComponent();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021C1 File Offset: 0x000003C1
		public void OnRelationsChanged(object sender, EventArgs e)
		{
			if (base.Enabled)
			{
				this._highlighter.UnhighlightAllPrimary();
				this.HighlightRelations();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021DC File Offset: 0x000003DC
		public void HighlightRelations()
		{
			foreach (IRelationOwner relationOwner in this._relationOwners)
			{
				foreach (BaseComponent target in relationOwner.GetRelations())
				{
					this._highlighter.HighlightPrimary(target, this._relationSelectionColor);
				}
			}
		}

		// Token: 0x04000008 RID: 8
		public readonly Highlighter _highlighter;

		// Token: 0x04000009 RID: 9
		public readonly ISpecService _specService;

		// Token: 0x0400000A RID: 10
		public readonly List<IRelationOwner> _relationOwners = new List<IRelationOwner>();

		// Token: 0x0400000B RID: 11
		public Color _relationSelectionColor;
	}
}
