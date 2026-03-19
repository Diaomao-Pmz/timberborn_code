using System;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000058 RID: 88
	public class Underlay : ILoadableSingleton
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00005DDA File Offset: 0x00003FDA
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00005DE2 File Offset: 0x00003FE2
		public VisualElement Root { get; private set; }

		// Token: 0x0600016F RID: 367 RVA: 0x00005DEB File Offset: 0x00003FEB
		public Underlay(RootVisualElementProvider rootVisualElementProvider)
		{
			this._rootVisualElementProvider = rootVisualElementProvider;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005DFC File Offset: 0x00003FFC
		public void Load()
		{
			VisualElement visualElement = this._rootVisualElementProvider.Create("Underlay", "Core/Underlay", 0);
			this.Root = UQueryExtensions.Q<VisualElement>(visualElement, "Underlay", null);
			this.Disable();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005E38 File Offset: 0x00004038
		public void Add(VisualElement element)
		{
			this.Root.Add(element);
			if (this.Root.childCount == 1)
			{
				this.Enable();
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00005E5A File Offset: 0x0000405A
		public void Remove(VisualElement element)
		{
			this.Root.Remove(element);
			if (this.Root.childCount == 0)
			{
				this.Disable();
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00005E7B File Offset: 0x0000407B
		public void Disable()
		{
			this.Root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00005E89 File Offset: 0x00004089
		public void Enable()
		{
			this.Root.ToggleDisplayStyle(true);
		}

		// Token: 0x040000CA RID: 202
		public readonly RootVisualElementProvider _rootVisualElementProvider;
	}
}
