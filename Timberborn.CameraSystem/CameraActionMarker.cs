using System;
using Timberborn.CoreUI;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CameraSystem
{
	// Token: 0x02000007 RID: 7
	public class CameraActionMarker : ILoadableSingleton
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public CameraActionMarker(VisualElementLoader visualElementLoader, UILayout uiLayout)
		{
			this._visualElementLoader = visualElementLoader;
			this._uiLayout = uiLayout;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002116 File Offset: 0x00000316
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/CameraActionMarker");
			this._uiLayout.AddAbsoluteItem(this._root);
			this.Hide();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002148 File Offset: 0x00000348
		public void ShowMarker(Vector2 positionNdc)
		{
			this._root.ToggleDisplayStyle(true);
			float width = this._root.parent.resolvedStyle.width;
			float height = this._root.parent.resolvedStyle.height;
			this._root.style.left = positionNdc.x * width - (float)CameraActionMarker.MarkerHalfSize;
			this._root.style.top = (1f - positionNdc.y) * height - (float)CameraActionMarker.MarkerHalfSize;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021DB File Offset: 0x000003DB
		public void Hide()
		{
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x04000008 RID: 8
		public static readonly int MarkerHalfSize = 16;

		// Token: 0x04000009 RID: 9
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400000A RID: 10
		public readonly UILayout _uiLayout;

		// Token: 0x0400000B RID: 11
		public VisualElement _root;
	}
}
