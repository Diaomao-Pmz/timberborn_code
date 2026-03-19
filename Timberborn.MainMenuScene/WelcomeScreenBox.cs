using System;
using Timberborn.CoreUI;
using Timberborn.ExperimentalModeSystem;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.MainMenuScene
{
	// Token: 0x0200000D RID: 13
	public class WelcomeScreenBox : ILoadableSingleton, IPanelController
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000026EF File Offset: 0x000008EF
		public WelcomeScreenBox(PanelStack panelStack, VisualElementLoader visualElementLoader, ExperimentalMode experimentalMode)
		{
			this._panelStack = panelStack;
			this._visualElementLoader = visualElementLoader;
			this._experimentalMode = experimentalMode;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000270C File Offset: 0x0000090C
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("MainMenu/WelcomeScreenBox");
			UQueryExtensions.Q<Button>(this._root, "Start", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Start();
			}, 0);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002747 File Offset: 0x00000947
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000274F File Offset: 0x0000094F
		public bool OnUIConfirmed()
		{
			this.Start();
			return true;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002758 File Offset: 0x00000958
		public void OnUICancelled()
		{
			this.Start();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002760 File Offset: 0x00000960
		public void Show(Action onStart)
		{
			if (this._experimentalMode.IsExperimental)
			{
				this._onStart = onStart;
				this._panelStack.Push(this);
				return;
			}
			if (onStart != null)
			{
				onStart();
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000278C File Offset: 0x0000098C
		public void Start()
		{
			this._panelStack.Pop(this);
			Action onStart = this._onStart;
			if (onStart == null)
			{
				return;
			}
			onStart();
		}

		// Token: 0x04000029 RID: 41
		public readonly PanelStack _panelStack;

		// Token: 0x0400002A RID: 42
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400002B RID: 43
		public readonly ExperimentalMode _experimentalMode;

		// Token: 0x0400002C RID: 44
		public VisualElement _root;

		// Token: 0x0400002D RID: 45
		public Action _onStart;
	}
}
