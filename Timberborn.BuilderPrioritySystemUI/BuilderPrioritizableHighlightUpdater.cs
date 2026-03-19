using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.PrioritySystem;

namespace Timberborn.BuilderPrioritySystemUI
{
	// Token: 0x0200000B RID: 11
	public class BuilderPrioritizableHighlightUpdater : BaseComponent, IAwakableComponent
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000024AC File Offset: 0x000006AC
		public BuilderPrioritizableHighlightUpdater(BuilderPrioritizableHighlighter builderPrioritizableHighlighter)
		{
			this._builderPrioritizableHighlighter = builderPrioritizableHighlighter;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024BC File Offset: 0x000006BC
		public void Awake()
		{
			this._builderPrioritizable = base.GetComponent<BuilderPrioritizable>();
			this._builderPrioritizable.PriorityChanged += this.OnPriorityChanged;
			this._builderPrioritizable.PrioritizableEnabled += this.OnPrioritizableEnabled;
			this._builderPrioritizable.PrioritizableDisabled += this.OnPrioritizableDisabled;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000251A File Offset: 0x0000071A
		public void OnPriorityChanged(object sender, PriorityChangedEventArgs e)
		{
			this._builderPrioritizableHighlighter.HighlightIfEnabled(this._builderPrioritizable);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000252D File Offset: 0x0000072D
		public void OnPrioritizableEnabled(object sender, EventArgs e)
		{
			this._builderPrioritizableHighlighter.AddBuilderPrioritizable(this._builderPrioritizable);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002540 File Offset: 0x00000740
		public void OnPrioritizableDisabled(object sender, EventArgs e)
		{
			this._builderPrioritizableHighlighter.RemoveBuilderPrioritizable(this._builderPrioritizable);
		}

		// Token: 0x0400001F RID: 31
		public readonly BuilderPrioritizableHighlighter _builderPrioritizableHighlighter;

		// Token: 0x04000020 RID: 32
		public BuilderPrioritizable _builderPrioritizable;
	}
}
