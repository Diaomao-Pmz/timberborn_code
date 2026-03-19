using System;
using UnityEngine.UIElements;

namespace Timberborn.ToolSystemUI
{
	// Token: 0x0200000D RID: 13
	public class ToolDescriptionSection
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002621 File Offset: 0x00000821
		public string Content { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002629 File Offset: 0x00000829
		public VisualElement Section { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002631 File Offset: 0x00000831
		public bool Prioritized { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002639 File Offset: 0x00000839
		public bool External { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002641 File Offset: 0x00000841
		public Action UpdateCallback { get; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002649 File Offset: 0x00000849
		public ToolDescriptionSection(string content, VisualElement section, bool external = false, bool prioritized = false, Action updateCallback = null)
		{
			this.Content = content;
			this.Section = section;
			this.External = external;
			this.Prioritized = prioritized;
			this.UpdateCallback = updateCallback;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002676 File Offset: 0x00000876
		public static ToolDescriptionSection CreateInternal(string content)
		{
			return new ToolDescriptionSection(content, null, false, false, null);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002682 File Offset: 0x00000882
		public static ToolDescriptionSection CreateInternal(VisualElement content)
		{
			return new ToolDescriptionSection("", content, false, false, null);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002692 File Offset: 0x00000892
		public static ToolDescriptionSection CreateInternalUpdatable(VisualElement content, Action updateCallback)
		{
			return new ToolDescriptionSection("", content, false, false, updateCallback);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000026A2 File Offset: 0x000008A2
		public static ToolDescriptionSection CreateInternalPrioritized(string content)
		{
			return new ToolDescriptionSection(content, null, false, true, null);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000026AE File Offset: 0x000008AE
		public static ToolDescriptionSection CreateExternal(VisualElement content)
		{
			return new ToolDescriptionSection("", content, true, false, null);
		}
	}
}
