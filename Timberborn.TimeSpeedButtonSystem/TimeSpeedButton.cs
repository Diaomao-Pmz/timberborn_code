using System;
using UnityEngine.UIElements;

namespace Timberborn.TimeSpeedButtonSystem
{
	// Token: 0x02000004 RID: 4
	public class TimeSpeedButton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public int TimeSpeed { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public Button Button { get; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public TimeSpeedButton(Button button, int timeSpeed)
		{
			this.Button = button;
			this.TimeSpeed = timeSpeed;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E4 File Offset: 0x000002E4
		public void Highlight()
		{
			this.Button.AddToClassList(TimeSpeedButton.HighlightedClassName);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020F6 File Offset: 0x000002F6
		public void Unhighlight()
		{
			this.Button.RemoveFromClassList(TimeSpeedButton.HighlightedClassName);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string HighlightedClassName = "speed-button--highlighted";

		// Token: 0x04000009 RID: 9
		public readonly bool _devMode;
	}
}
