using System;
using System.Collections.Generic;
using Timberborn.BlockSystem;
using Timberborn.BuilderPrioritySystem;
using Timberborn.PrioritySystemUI;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using UnityEngine;

namespace Timberborn.BuilderPrioritySystemUI
{
	// Token: 0x0200000A RID: 10
	public class BuilderPrioritizableHighlighter : IPostLoadableSingleton
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002360 File Offset: 0x00000560
		public BuilderPrioritizableHighlighter(EventBus eventBus, Highlighter highlighter, PriorityColors priorityColors)
		{
			this._eventBus = eventBus;
			this._highlighter = highlighter;
			this._priorityColors = priorityColors;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002388 File Offset: 0x00000588
		public void PostLoad()
		{
			this._eventBus.Register(this);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002396 File Offset: 0x00000596
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			if (this._enabled)
			{
				this.HighlightAll();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023A6 File Offset: 0x000005A6
		[OnEvent]
		public void OnToolGroupEntered(ToolGroupEnteredEvent toolGroupEnteredEvent)
		{
			ToolGroupSpec toolGroup = toolGroupEnteredEvent.ToolGroup;
			if (toolGroup != null && toolGroup.HasSpec<BuilderPriorityToolGroupSpec>())
			{
				this._enabled = true;
				this.HighlightAll();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000023C9 File Offset: 0x000005C9
		[OnEvent]
		public void OnToolGroupExited(ToolGroupExitedEvent toolGroupExitedEvent)
		{
			ToolGroupSpec toolGroup = toolGroupExitedEvent.ToolGroup;
			if (toolGroup != null && toolGroup.HasSpec<BuilderPriorityToolGroupSpec>())
			{
				this._enabled = false;
				this._highlighter.UnhighlightAllSecondary();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023F1 File Offset: 0x000005F1
		public void AddBuilderPrioritizable(BuilderPrioritizable builderPrioritizable)
		{
			this._builderPrioritizables.Add(builderPrioritizable);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023FF File Offset: 0x000005FF
		public void RemoveBuilderPrioritizable(BuilderPrioritizable builderPrioritizable)
		{
			this._builderPrioritizables.Remove(builderPrioritizable);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000240E File Offset: 0x0000060E
		public void HighlightIfEnabled(BuilderPrioritizable builderPrioritizable)
		{
			if (this._enabled)
			{
				this.Highlight(builderPrioritizable);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002420 File Offset: 0x00000620
		public void HighlightAll()
		{
			this._highlighter.UnhighlightAllSecondary();
			foreach (BuilderPrioritizable builderPrioritizable in this._builderPrioritizables)
			{
				this.Highlight(builderPrioritizable);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002480 File Offset: 0x00000680
		public void Highlight(BuilderPrioritizable builderPrioritizable)
		{
			Color highlightColor = this._priorityColors.GetHighlightColor(builderPrioritizable.Priority);
			this._highlighter.HighlightSecondary(builderPrioritizable, highlightColor);
		}

		// Token: 0x0400001A RID: 26
		public readonly EventBus _eventBus;

		// Token: 0x0400001B RID: 27
		public readonly Highlighter _highlighter;

		// Token: 0x0400001C RID: 28
		public readonly PriorityColors _priorityColors;

		// Token: 0x0400001D RID: 29
		public readonly List<BuilderPrioritizable> _builderPrioritizables = new List<BuilderPrioritizable>();

		// Token: 0x0400001E RID: 30
		public bool _enabled;
	}
}
