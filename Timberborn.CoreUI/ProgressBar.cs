using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000040 RID: 64
	[UxmlElement]
	public class ProgressBar : VisualElement
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004EA0 File Offset: 0x000030A0
		public override VisualElement contentContainer { get; }

		// Token: 0x06000111 RID: 273 RVA: 0x00004EA8 File Offset: 0x000030A8
		public ProgressBar()
		{
			Resources.Load<VisualTreeAsset>("UI/Views/Core/ProgressBar").CloneTree(this);
			this._simpleProgressBar = UQueryExtensions.Q<SimpleProgressBar>(this, "Progress", null);
			this.contentContainer = UQueryExtensions.Q<VisualElement>(this, "ContentContainer", null);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004EE4 File Offset: 0x000030E4
		public void SetProgress(float progress)
		{
			this._simpleProgressBar.SetProgress(progress);
		}

		// Token: 0x0400008F RID: 143
		public readonly SimpleProgressBar _simpleProgressBar;

		// Token: 0x02000041 RID: 65
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x06000113 RID: 275 RVA: 0x00004EF2 File Offset: 0x000030F2
			public override object CreateInstance()
			{
				return new ProgressBar();
			}
		}
	}
}
