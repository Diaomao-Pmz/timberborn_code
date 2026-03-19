using System;
using System.Collections.Generic;
using Timberborn.Common;
using UnityEngine.UIElements;

namespace Timberborn.ToolSystemUI
{
	// Token: 0x0200000B RID: 11
	public class ToolDescription
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002546 File Offset: 0x00000746
		public string Title { get; }

		// Token: 0x06000024 RID: 36 RVA: 0x0000254E File Offset: 0x0000074E
		public ToolDescription(string title, List<ToolDescriptionSection> sections)
		{
			this.Title = title;
			this._sections = sections;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002564 File Offset: 0x00000764
		public ReadOnlyList<ToolDescriptionSection> Sections
		{
			get
			{
				return this._sections.AsReadOnlyList<ToolDescriptionSection>();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002571 File Offset: 0x00000771
		public bool HasTitle
		{
			get
			{
				return this.Title != null;
			}
		}

		// Token: 0x04000015 RID: 21
		public readonly List<ToolDescriptionSection> _sections;

		// Token: 0x0200000C RID: 12
		public class Builder
		{
			// Token: 0x06000027 RID: 39 RVA: 0x0000257C File Offset: 0x0000077C
			public Builder()
			{
			}

			// Token: 0x06000028 RID: 40 RVA: 0x0000258F File Offset: 0x0000078F
			public Builder(string title)
			{
				this._title = title;
			}

			// Token: 0x06000029 RID: 41 RVA: 0x000025A9 File Offset: 0x000007A9
			public ToolDescription.Builder AddSection(string content)
			{
				this._sections.Add(ToolDescriptionSection.CreateInternal(content));
				return this;
			}

			// Token: 0x0600002A RID: 42 RVA: 0x000025BD File Offset: 0x000007BD
			public ToolDescription.Builder AddPrioritizedSection(string content)
			{
				this._sections.Add(ToolDescriptionSection.CreateInternalPrioritized(content));
				return this;
			}

			// Token: 0x0600002B RID: 43 RVA: 0x000025D1 File Offset: 0x000007D1
			public ToolDescription.Builder AddExternalSection(VisualElement content)
			{
				this._sections.Add(ToolDescriptionSection.CreateExternal(content));
				return this;
			}

			// Token: 0x0600002C RID: 44 RVA: 0x000025E5 File Offset: 0x000007E5
			public ToolDescription.Builder AddSection(VisualElement content)
			{
				this._sections.Add(ToolDescriptionSection.CreateInternal(content));
				return this;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x000025F9 File Offset: 0x000007F9
			public ToolDescription.Builder AddUpdatableSection(VisualElement content, Action updateCallback)
			{
				this._sections.Add(ToolDescriptionSection.CreateInternalUpdatable(content, updateCallback));
				return this;
			}

			// Token: 0x0600002E RID: 46 RVA: 0x0000260E File Offset: 0x0000080E
			public ToolDescription Build()
			{
				return new ToolDescription(this._title, this._sections);
			}

			// Token: 0x04000016 RID: 22
			public readonly string _title;

			// Token: 0x04000017 RID: 23
			public readonly List<ToolDescriptionSection> _sections = new List<ToolDescriptionSection>();
		}
	}
}
