using System;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x02000004 RID: 4
	public readonly struct ClickableSubtitle
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public Action ClickAction { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C8 File Offset: 0x000002C8
		public string Subtitle { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020D0 File Offset: 0x000002D0
		public string TooltipText { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020D8 File Offset: 0x000002D8
		public bool HasWarning { get; }

		// Token: 0x06000007 RID: 7 RVA: 0x000020E0 File Offset: 0x000002E0
		public ClickableSubtitle(Action clickAction, string subtitle, string tooltipText, bool hasWarning)
		{
			this.ClickAction = clickAction;
			this.Subtitle = subtitle;
			this.TooltipText = tooltipText;
			this.HasWarning = hasWarning;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020FF File Offset: 0x000002FF
		public static ClickableSubtitle Create(Action clickAction, string subtitle)
		{
			return new ClickableSubtitle(clickAction, subtitle, null, false);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000210A File Offset: 0x0000030A
		public static ClickableSubtitle Create(Action clickAction, string subtitle, string tooltipText, bool isWarning)
		{
			return new ClickableSubtitle(clickAction, subtitle, tooltipText, isWarning);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002115 File Offset: 0x00000315
		public static ClickableSubtitle CreateEmpty()
		{
			return new ClickableSubtitle(null, null, null, false);
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002120 File Offset: 0x00000320
		public bool HasAction
		{
			get
			{
				return this.ClickAction != null;
			}
		}
	}
}
