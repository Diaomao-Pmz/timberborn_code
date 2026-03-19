using System;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200005D RID: 93
	public class VisualElementLocalizer : IVisualElementInitializer
	{
		// Token: 0x06000184 RID: 388 RVA: 0x000060A3 File Offset: 0x000042A3
		public VisualElementLocalizer(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000060B4 File Offset: 0x000042B4
		public void InitializeVisualElement(VisualElement visualElement)
		{
			ILocalizableElement localizableElement = visualElement as ILocalizableElement;
			if (localizableElement == null)
			{
				return;
			}
			if (localizableElement.IsSet)
			{
				localizableElement.Localize(this._loc);
				return;
			}
			string str = VisualElementLocalizer.ElementPath(visualElement);
			throw new InvalidOperationException("text-loc-key is not set for ILocalizableElement: " + str);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000060F8 File Offset: 0x000042F8
		public static string ElementPath(VisualElement visualElement)
		{
			return ((visualElement.parent != null) ? VisualElementLocalizer.ElementPath(visualElement.parent) : string.Empty) + "/" + (string.IsNullOrEmpty(visualElement.name) ? visualElement.GetType().Name : visualElement.name);
		}

		// Token: 0x040000D2 RID: 210
		public readonly ILoc _loc;
	}
}
