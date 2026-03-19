using System;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000049 RID: 73
	public class RowShader
	{
		// Token: 0x0600012F RID: 303 RVA: 0x000054CC File Offset: 0x000036CC
		public void ShadeRows(VisualElement visualElement)
		{
			for (int i = 0; i < visualElement.childCount; i++)
			{
				if (i % 2 != 0)
				{
					visualElement[i].AddToClassList(RowShader.ShadedClass);
				}
			}
		}

		// Token: 0x0400009D RID: 157
		public static readonly string ShadedClass = "production-item--shaded";
	}
}
