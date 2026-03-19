using System;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

namespace Timberborn.CoreUI
{
	// Token: 0x02000015 RID: 21
	public class HyperlinkInitializer
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00002EE0 File Offset: 0x000010E0
		public void Initialize(Label label, Action openLinkCallback)
		{
			HyperlinkInitializer.AddUnityRequiredValue(label);
			string originalText = label.text;
			string highlightedText = originalText.Replace("<link=", "<color=#ffff00><link=").Replace("</link>", "</link></color>");
			label.RegisterCallback<PointerDownLinkTagEvent>(delegate(PointerDownLinkTagEvent _)
			{
				openLinkCallback();
			}, 0);
			label.RegisterCallback<PointerOverLinkTagEvent>(delegate(PointerOverLinkTagEvent _)
			{
				label.text = highlightedText;
			}, 0);
			label.RegisterCallback<PointerOutLinkTagEvent>(delegate(PointerOutLinkTagEvent _)
			{
				label.text = originalText;
			}, 0);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002F8F File Offset: 0x0000118F
		public static void AddUnityRequiredValue(Label label)
		{
			label.text = label.text.Replace("<link>", "<link=\"AnyValue\">");
		}
	}
}
