using System;
using UnityEngine.UIElements;

namespace Timberborn.BottomBarSystem
{
	// Token: 0x02000004 RID: 4
	public readonly struct BottomBarElement
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public VisualElement MainElement { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public VisualElement SubElement { get; }

		// Token: 0x06000005 RID: 5 RVA: 0x000020CE File Offset: 0x000002CE
		public BottomBarElement(VisualElement mainElement, VisualElement subElement)
		{
			this.MainElement = mainElement;
			this.SubElement = subElement;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020DE File Offset: 0x000002DE
		public static BottomBarElement CreateMultiLevel(VisualElement mainElement, VisualElement subElement)
		{
			return new BottomBarElement(mainElement, subElement);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020E7 File Offset: 0x000002E7
		public static BottomBarElement CreateSingleLevel(VisualElement mainElement)
		{
			return new BottomBarElement(mainElement, null);
		}
	}
}
