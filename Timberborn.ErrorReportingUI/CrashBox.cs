using System;
using Timberborn.CoreUI;
using Timberborn.ErrorReporting;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.ErrorReportingUI
{
	// Token: 0x02000004 RID: 4
	public class CrashBox : ILoadableSingleton, IUnloadableSingleton
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public CrashBox(RootObjectProvider rootObjectProvider, RootVisualElementProvider rootVisualElementProvider)
		{
			this._rootObjectProvider = rootObjectProvider;
			this._rootVisualElementProvider = rootVisualElementProvider;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D4 File Offset: 0x000002D4
		public void Load()
		{
			this._root = this._rootObjectProvider.CreateRootObject("CrashBox");
			this._rootVisualElement = this._rootVisualElementProvider.Create(this._root, "Common/CrashBox", 10, null);
			this._rootVisualElement.ToggleDisplayStyle(false);
			ExceptionListener.FirstUncaughtException += this.OnFirstUncaughtException;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002133 File Offset: 0x00000333
		public void Unload()
		{
			ExceptionListener.FirstUncaughtException -= this.OnFirstUncaughtException;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002146 File Offset: 0x00000346
		public void OnFirstUncaughtException(object sender, EventArgs e)
		{
			if (!CrashSceneLoader.Enabled)
			{
				this._rootVisualElement.ToggleDisplayStyle(true);
				this._root.SetActive(true);
			}
		}

		// Token: 0x04000006 RID: 6
		public readonly RootObjectProvider _rootObjectProvider;

		// Token: 0x04000007 RID: 7
		public readonly RootVisualElementProvider _rootVisualElementProvider;

		// Token: 0x04000008 RID: 8
		public GameObject _root;

		// Token: 0x04000009 RID: 9
		public VisualElement _rootVisualElement;
	}
}
