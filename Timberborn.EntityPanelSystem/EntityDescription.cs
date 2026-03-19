using System;
using UnityEngine.UIElements;

namespace Timberborn.EntityPanelSystem
{
	// Token: 0x0200000B RID: 11
	public class EntityDescription
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002704 File Offset: 0x00000904
		public string Content { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002D RID: 45 RVA: 0x0000270C File Offset: 0x0000090C
		public VisualElement Section { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002714 File Offset: 0x00000914
		public int Order { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600002F RID: 47 RVA: 0x0000271C File Offset: 0x0000091C
		public bool FlavorSection { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002724 File Offset: 0x00000924
		public bool Input { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000031 RID: 49 RVA: 0x0000272C File Offset: 0x0000092C
		public bool Output { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002734 File Offset: 0x00000934
		public string Time { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000273C File Offset: 0x0000093C
		public bool BottomSection { get; }

		// Token: 0x06000034 RID: 52 RVA: 0x00002744 File Offset: 0x00000944
		public EntityDescription(string content, VisualElement section, int order, bool flavorSection = false, bool input = false, bool output = false, string time = null, bool bottomSection = false)
		{
			this.Content = content;
			this.Section = section;
			this.Order = order;
			this.FlavorSection = flavorSection;
			this.Input = input;
			this.Output = output;
			this.Time = time;
			this.BottomSection = bottomSection;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002794 File Offset: 0x00000994
		public static EntityDescription CreateTextSection(string content, int order)
		{
			return new EntityDescription(content, null, order, false, false, false, null, false);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000027A3 File Offset: 0x000009A3
		public static EntityDescription CreateFlavorSection(string content, int order)
		{
			return new EntityDescription(content, null, order, true, false, false, null, false);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027B2 File Offset: 0x000009B2
		public static EntityDescription CreateMiddleSection(VisualElement content, int order)
		{
			return new EntityDescription(null, content, order, false, false, false, null, false);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000027C1 File Offset: 0x000009C1
		public static EntityDescription CreateBottomSection(VisualElement content, int order)
		{
			return new EntityDescription(null, content, order, false, false, false, null, true);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000027D0 File Offset: 0x000009D0
		public static EntityDescription CreateInputOutputSection(VisualElement content, int order)
		{
			return new EntityDescription(null, content, order, false, true, true, null, false);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027DF File Offset: 0x000009DF
		public static EntityDescription CreateInputSectionWithTime(VisualElement content, int order, string time)
		{
			return new EntityDescription(null, content, order, false, true, false, time, false);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027EE File Offset: 0x000009EE
		public static EntityDescription CreateOutputSection(VisualElement content, int order)
		{
			return new EntityDescription(null, content, order, false, false, true, null, false);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000027FD File Offset: 0x000009FD
		public bool TextSection
		{
			get
			{
				return this.Content != null && !this.FlavorSection;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002812 File Offset: 0x00000A12
		public bool MiddleSection
		{
			get
			{
				return this.Section != null && !this.ProductionSection && !this.BottomSection;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003E RID: 62 RVA: 0x0000282F File Offset: 0x00000A2F
		public bool ProductionSection
		{
			get
			{
				return this.Input || this.Output;
			}
		}
	}
}
