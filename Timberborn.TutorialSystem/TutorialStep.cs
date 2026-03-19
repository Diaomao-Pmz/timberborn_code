using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Timberborn.Common;
using Timberborn.ToolButtonSystem;

namespace Timberborn.TutorialSystem
{
	// Token: 0x0200001B RID: 27
	public class TutorialStep
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003253 File Offset: 0x00001453
		public ITutorialStep Step { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000325B File Offset: 0x0000145B
		public ToolGroupButton ToolGroupButton { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00003263 File Offset: 0x00001463
		public ImmutableArray<ToolButton> ToolButtons { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000326B File Offset: 0x0000146B
		public Action<bool> Highlight { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003273 File Offset: 0x00001473
		public string KeyBinding { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000327B File Offset: 0x0000147B
		public string FixedKeyBinding { get; }

		// Token: 0x0600008D RID: 141 RVA: 0x00003283 File Offset: 0x00001483
		public TutorialStep(ITutorialStep step, ToolGroupButton toolGroupButton, IEnumerable<ToolButton> toolButtons, Action<bool> highlight, string keyBinding, string fixedKeyBinding)
		{
			this.Step = step;
			this.ToolGroupButton = toolGroupButton;
			this.ToolButtons = toolButtons.ToImmutableArray<ToolButton>();
			this.Highlight = highlight;
			this.KeyBinding = keyBinding;
			this.FixedKeyBinding = fixedKeyBinding;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000032BD File Offset: 0x000014BD
		public static TutorialStep Create(ITutorialStep step, Action<bool> highlight = null, string keyBinding = null, string fixedKeyBinding = null)
		{
			return new TutorialStep(step, null, Enumerable.Empty<ToolButton>(), highlight, keyBinding, fixedKeyBinding);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000032CE File Offset: 0x000014CE
		public static TutorialStep Create(ITutorialStep step, ToolGroupButton toolGroupButton, ToolButton toolButton, Action<bool> highlight = null)
		{
			return TutorialStep.Create(step, toolGroupButton, Enumerables.One<ToolButton>(toolButton), highlight);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000032DE File Offset: 0x000014DE
		public static TutorialStep Create(ITutorialStep step, ToolGroupButton toolGroupButton, IEnumerable<ToolButton> toolButtons, Action<bool> highlight = null)
		{
			return new TutorialStep(step, toolGroupButton, toolButtons, highlight, null, null);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000032EB File Offset: 0x000014EB
		public static TutorialStep Create(ITutorialStep step, string keyBinding, string fixedKeyBinding = null)
		{
			return new TutorialStep(step, null, Enumerable.Empty<ToolButton>(), null, keyBinding, fixedKeyBinding);
		}
	}
}
