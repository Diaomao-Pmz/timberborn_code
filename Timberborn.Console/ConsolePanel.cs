using System;
using System.Collections.Concurrent;
using System.IO;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.PlatformUtilities;
using Timberborn.RootProviders;
using Timberborn.SingletonSystem;
using Timberborn.Versioning;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.Console
{
	// Token: 0x02000006 RID: 6
	public class ConsolePanel : IConsolePanel, IPriorityInputProcessor, ILoadableSingleton, IUnloadableSingleton, ILateUpdatableSingleton
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002296 File Offset: 0x00000496
		public ConsolePanel(InputService inputService, VisualElementLoader visualElementLoader, ILoc loc, IExplorerOpener explorerOpener, RootVisualElementProvider rootVisualElementProvider)
		{
			this._inputService = inputService;
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
			this._explorerOpener = explorerOpener;
			this._rootVisualElementProvider = rootVisualElementProvider;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022D0 File Offset: 0x000004D0
		public void Load()
		{
			this._root = this._rootVisualElementProvider.Create("ConsolePanel", "Common/Console/ConsoleContainer", 10);
			this._consolePanel = this._visualElementLoader.LoadVisualElement("Common/Console/ConsolePanel");
			this._textField = UQueryExtensions.Q<TextField>(this._consolePanel, "TextField", null);
			UQueryExtensions.Q<TextElement>(this._textField, null, null).enableRichText = true;
			this._scroller = UQueryExtensions.Q<Scroller>(this._textField, null, null);
			this._expandButton = UQueryExtensions.Q<Button>(this._consolePanel, "ExpandButton", null);
			this._expandButton.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.ToggleExpand(!this._isExpanded);
			}, 0);
			this.ToggleExpand(false);
			UQueryExtensions.Q<Button>(this._consolePanel, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Hide();
			}, 0);
			UQueryExtensions.Q<Button>(this._consolePanel, "OpenDirectoryButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OpenLogDirectory), 0);
			UQueryExtensions.Q<VisualElement>(this._root, "ConsoleContainer", null).Add(this._consolePanel);
			this._root.ToggleDisplayStyle(false);
			this._inputService.AddInputProcessor(this);
			if (ConsoleLogListener.AnyWarningOrError && ConsolePanel.ShouldAutoOpenOnWarningOrError)
			{
				this.Show();
			}
			ConsoleLogListener.OnFirstWarningOrErrorReceived += this.OnFirstWarningOrErrorReceived;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002422 File Offset: 0x00000622
		public void Unload()
		{
			ConsoleLogListener.OnFirstWarningOrErrorReceived -= this.OnFirstWarningOrErrorReceived;
			ConsoleLogListener.OnLogReceived -= this.OnLogReceived;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002446 File Offset: 0x00000646
		public void ProcessInput()
		{
			if (this._inputService.IsKeyDown(ConsolePanel.ToggleConsoleKey))
			{
				if (!this._isShown)
				{
					this.Show();
					return;
				}
				this.Hide();
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002470 File Offset: 0x00000670
		public void LateUpdateSingleton()
		{
			if (this._resetScroll)
			{
				this.ResetScrollPosition();
				this._resetScroll = false;
			}
			this._resetScroll = (!this._queuedLogs.IsEmpty && this._root.IsDisplayed() && this.IsScrollAtBottom());
			if (!this._justOpened)
			{
				Log log;
				while (this._queuedLogs.TryDequeue(out log))
				{
					this.Add(log);
				}
			}
			else
			{
				this._justOpened = false;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000024E4 File Offset: 0x000006E4
		public void Show()
		{
			if (!this._isShown)
			{
				foreach (Log item in ConsoleLogListener.GetAllLogs())
				{
					this._queuedLogs.Enqueue(item);
				}
				ConsoleLogListener.OnLogReceived += this.OnLogReceived;
				this._justOpened = true;
				this._root.ToggleDisplayStyle(true);
				this._isShown = true;
			}
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002554 File Offset: 0x00000754
		public static bool ShouldAutoOpenOnWarningOrError
		{
			get
			{
				return !Application.isEditor && GameVersions.CurrentVersion.IsDevelopmentVersion;
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002578 File Offset: 0x00000778
		public void ToggleExpand(bool isExpanded)
		{
			this._isExpanded = isExpanded;
			this._expandButton.text = (this._isExpanded ? this._loc.T(ConsolePanel.CollapseLocKey) : this._loc.T(ConsolePanel.ExpandLocKey));
			this._consolePanel.EnableInClassList(ConsolePanel.CollapsedClass, !this._isExpanded);
			this._consolePanel.EnableInClassList(ConsolePanel.ExpandedClass, this._isExpanded);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025F0 File Offset: 0x000007F0
		public void OnLogReceived(object sender, Log log)
		{
			this._queuedLogs.Enqueue(log);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025FE File Offset: 0x000007FE
		public void OnFirstWarningOrErrorReceived(object sender, Log e)
		{
			if (ConsolePanel.ShouldAutoOpenOnWarningOrError)
			{
				this.Show();
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002610 File Offset: 0x00000810
		public void Hide()
		{
			if (this._isShown)
			{
				ConsoleLogListener.OnLogReceived -= this.OnLogReceived;
				this._queuedLogs.Clear();
				this._root.ToggleDisplayStyle(false);
				this._textField.value = "";
				this._isShown = false;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002664 File Offset: 0x00000864
		public bool IsScrollAtBottom()
		{
			return Math.Abs(this._scroller.value - this._scroller.highValue) < 15f;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000268C File Offset: 0x0000088C
		public void Add(Log log)
		{
			string text = ColorUtility.ToHtmlStringRGB(ConsolePanel.GetLogColor(log));
			this._textField.value = ConsolePanel.Trim(string.Concat(new string[]
			{
				this._textField.value,
				"<color=#",
				text,
				">",
				log.Message,
				"</color>",
				Environment.NewLine
			}));
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000026FC File Offset: 0x000008FC
		public static Color GetLogColor(Log log)
		{
			Color result;
			switch (log.LogType)
			{
			case 0:
				result = Color.red;
				break;
			case 1:
				result = Color.red;
				break;
			case 2:
				result = Color.yellow;
				break;
			case 3:
				result = Color.white;
				break;
			case 4:
				result = Color.red;
				break;
			default:
				throw new ArgumentOutOfRangeException("LogType", log.LogType, null);
			}
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002770 File Offset: 0x00000970
		public static string Trim(string text)
		{
			int length = text.Length;
			if (length > ConsolePanel.MaxCharacters)
			{
				text = text.Substring(length - ConsolePanel.MaxCharacters, ConsolePanel.MaxCharacters);
				int num = text.IndexOf('\n', StringComparison.Ordinal);
				string text2 = text;
				int num2 = num + 1;
				text = text2.Substring(num2, text2.Length - num2);
			}
			return text;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027C0 File Offset: 0x000009C0
		public void ResetScrollPosition()
		{
			this._scroller.value = this._scroller.highValue;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027D8 File Offset: 0x000009D8
		public void OpenLogDirectory(ClickEvent evt)
		{
			string directoryName = Path.GetDirectoryName(Application.consoleLogPath);
			this._explorerOpener.OpenDirectory(directoryName);
		}

		// Token: 0x0400000B RID: 11
		public static readonly int MaxCharacters = 20000;

		// Token: 0x0400000C RID: 12
		public static readonly string ToggleConsoleKey = "ToggleConsole";

		// Token: 0x0400000D RID: 13
		public static readonly string CollapsedClass = "console-panel--collapsed";

		// Token: 0x0400000E RID: 14
		public static readonly string ExpandedClass = "console-panel--expanded";

		// Token: 0x0400000F RID: 15
		public static readonly string CollapseLocKey = "Console.Collapse";

		// Token: 0x04000010 RID: 16
		public static readonly string ExpandLocKey = "Console.Expand";

		// Token: 0x04000011 RID: 17
		public readonly InputService _inputService;

		// Token: 0x04000012 RID: 18
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000013 RID: 19
		public readonly ILoc _loc;

		// Token: 0x04000014 RID: 20
		public readonly IExplorerOpener _explorerOpener;

		// Token: 0x04000015 RID: 21
		public readonly RootVisualElementProvider _rootVisualElementProvider;

		// Token: 0x04000016 RID: 22
		public VisualElement _root;

		// Token: 0x04000017 RID: 23
		public VisualElement _consolePanel;

		// Token: 0x04000018 RID: 24
		public Scroller _scroller;

		// Token: 0x04000019 RID: 25
		public TextField _textField;

		// Token: 0x0400001A RID: 26
		public Button _expandButton;

		// Token: 0x0400001B RID: 27
		public bool _isShown;

		// Token: 0x0400001C RID: 28
		public bool _justOpened;

		// Token: 0x0400001D RID: 29
		public bool _isExpanded;

		// Token: 0x0400001E RID: 30
		public bool _resetScroll;

		// Token: 0x0400001F RID: 31
		public readonly ConcurrentQueue<Log> _queuedLogs = new ConcurrentQueue<Log>();
	}
}
