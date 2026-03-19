using System;
using Timberborn.PlatformUtilities;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200004A RID: 74
	public class ScrollBarInitializationService
	{
		// Token: 0x06000132 RID: 306 RVA: 0x0000550C File Offset: 0x0000370C
		public void InitializeVisualElement(VisualElement visualElement)
		{
			ScrollView scrollView = visualElement as ScrollView;
			if (scrollView != null)
			{
				ScrollBarInitializationService.SetScrollView(scrollView);
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000552C File Offset: 0x0000372C
		public static void SetScrollView(ScrollView scrollView)
		{
			ScrollBarInitializationService.AddNineSliceElements(scrollView.verticalScroller.slider, ScrollBarInitializationService.VerticalDraggerClass, ScrollBarInitializationService.VerticalTrackerClass);
			ScrollBarInitializationService.AddNineSliceElements(scrollView.horizontalScroller.slider, ScrollBarInitializationService.HorizontalDraggerClass, ScrollBarInitializationService.HorizontalTrackerClass);
			scrollView.mouseWheelScrollSize = ScrollWheelSpeed.WheelScrollSize.Value;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005580 File Offset: 0x00003780
		public static void AddNineSliceElements(VisualElement root, string draggerClass, string trackerClass)
		{
			VisualElement visualElement = UQueryExtensions.Q<VisualElement>(root, "unity-dragger", null);
			NineSliceVisualElement nineSliceVisualElement = new NineSliceVisualElement();
			nineSliceVisualElement.AddToClassList(draggerClass);
			visualElement.Add(nineSliceVisualElement);
			VisualElement visualElement2 = UQueryExtensions.Q<VisualElement>(root, "unity-tracker", null);
			NineSliceVisualElement nineSliceVisualElement2 = new NineSliceVisualElement();
			nineSliceVisualElement2.AddToClassList(trackerClass);
			visualElement2.Add(nineSliceVisualElement2);
		}

		// Token: 0x0400009E RID: 158
		public static readonly string VerticalDraggerClass = "vertical-scroll-view__nine-slice-dragger";

		// Token: 0x0400009F RID: 159
		public static readonly string VerticalTrackerClass = "vertical-scroll-view__nine-slice-tracker";

		// Token: 0x040000A0 RID: 160
		public static readonly string HorizontalDraggerClass = "horizontal-scroll-view__nine-slice-dragger";

		// Token: 0x040000A1 RID: 161
		public static readonly string HorizontalTrackerClass = "horizontal-scroll-view__nine-slice-tracker";
	}
}
