using System;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.PlatformUtilities;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.FileBrowsing
{
	// Token: 0x02000008 RID: 8
	public class FileBrowser : ILoadableSingleton, IPanelController
	{
		// Token: 0x0600002A RID: 42 RVA: 0x0000271D File Offset: 0x0000091D
		public FileBrowser(VisualElementLoader visualElementLoader, PanelStack panelStack, DirectoryListView directoryListView, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._panelStack = panelStack;
			this._directoryListView = directoryListView;
			this._loc = loc;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002744 File Offset: 0x00000944
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Common/FileBrowser");
			this._pathField = UQueryExtensions.Q<TextField>(this._root, "PathField", null);
			this._tipLabel = UQueryExtensions.Q<Label>(this._root, "Tip", null);
			UQueryExtensions.Q<Button>(this._root, "UpwardButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._directoryListView.GoUpward();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "OpenButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.OpenCurrentSelection();
			}, 0);
			UQueryExtensions.Q<Button>(this._root, "CloseButton", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Close();
			}, 0);
			this._directoryListView.Initialize(this._root);
			this._directoryListView.DirectoryChanged += this.OnDirectoryChanged;
			this._directoryListView.EntryDoubleClicked += delegate(object _, DiskSystemEntry diskSystemEntry)
			{
				this.OpenDiskSystemEntry(diskSystemEntry);
			};
			UQueryExtensions.Q<TextElement>(this._pathField, null, null).RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnFocusIn), 0);
			UQueryExtensions.Q<TextElement>(this._pathField, null, null).RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), 0);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000287B File Offset: 0x00000A7B
		public void Open(Action<string> openFileCallback, FileFilter fileFilter, string tipLocKey)
		{
			Asserts.FieldIsNull<FileBrowser>(this, this._openFileCallback, "_openFileCallback");
			this._openFileCallback = openFileCallback;
			this._panelStack.PushDialog(this);
			this._directoryListView.SetFileFilter(fileFilter);
			this.UpdateTip(tipLocKey);
			this.OpenInitialDirectory();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000028BA File Offset: 0x00000ABA
		public VisualElement GetPanel()
		{
			return this._root;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000028C2 File Offset: 0x00000AC2
		public bool OnUIConfirmed()
		{
			this.OpenCurrentSelection();
			return true;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000028CB File Offset: 0x00000ACB
		public void OnUICancelled()
		{
			this.Close();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000028D3 File Offset: 0x00000AD3
		public void OnFocusIn(FocusInEvent evt)
		{
			this._focusInPath = this._pathField.value;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000028E6 File Offset: 0x00000AE6
		public void OnFocusOut(FocusOutEvent evt)
		{
			if (this._focusInPath != this._pathField.value)
			{
				this.ProcessPathFieldPath();
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002908 File Offset: 0x00000B08
		public void OpenCurrentSelection()
		{
			DiskSystemEntry diskSystemEntry;
			if (this._directoryListView.TryGetSelectedDiskSystemEntry(out diskSystemEntry))
			{
				this.OpenDiskSystemEntry(diskSystemEntry);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000292B File Offset: 0x00000B2B
		public void OpenDiskSystemEntry(DiskSystemEntry diskSystemEntry)
		{
			if (diskSystemEntry.IsDirectory)
			{
				this._directoryListView.TryOpenDirectory(diskSystemEntry.Path);
				return;
			}
			Action<string> openFileCallback = this._openFileCallback;
			if (openFileCallback != null)
			{
				openFileCallback(diskSystemEntry.Path);
			}
			this.Close();
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002968 File Offset: 0x00000B68
		public void Close()
		{
			this._panelStack.Pop(this);
			this._openFileCallback = null;
			this._directoryListView.Clear();
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002988 File Offset: 0x00000B88
		public void ProcessPathFieldPath()
		{
			DiskSystemEntry diskSystemEntry = DiskSystemEntry.Create(this._pathField.value);
			if (diskSystemEntry.Exists)
			{
				this.OpenDiskSystemEntry(diskSystemEntry);
				return;
			}
			this._pathField.SetValueWithoutNotify(this._directoryListView.CurrentPath);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000029CD File Offset: 0x00000BCD
		public void OnDirectoryChanged(object sender, EventArgs eventArgs)
		{
			this._pathField.SetValueWithoutNotify(this._directoryListView.CurrentPath);
			PlayerPrefs.SetString(FileBrowser.LastOpenedPathKey, this._directoryListView.CurrentPath);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000029FA File Offset: 0x00000BFA
		public void UpdateTip(string tipLocKey)
		{
			if (string.IsNullOrEmpty(tipLocKey))
			{
				this._tipLabel.ToggleDisplayStyle(false);
				return;
			}
			this._tipLabel.text = this._loc.T(tipLocKey);
			this._tipLabel.ToggleDisplayStyle(true);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002A34 File Offset: 0x00000C34
		public void OpenInitialDirectory()
		{
			if (!this.TryOpenLastDirectory() && !this._directoryListView.TryOpenDirectory(UserDataFolder.Folder))
			{
				throw new InvalidOperationException("Could not open user data folder");
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002A5B File Offset: 0x00000C5B
		public bool TryOpenLastDirectory()
		{
			return PlayerPrefs.HasKey(FileBrowser.LastOpenedPathKey) && this._directoryListView.TryOpenDirectory(PlayerPrefs.GetString(FileBrowser.LastOpenedPathKey));
		}

		// Token: 0x0400001D RID: 29
		public static readonly string LastOpenedPathKey = "FileBrowser.LastOpenedPath";

		// Token: 0x0400001E RID: 30
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400001F RID: 31
		public readonly PanelStack _panelStack;

		// Token: 0x04000020 RID: 32
		public readonly DirectoryListView _directoryListView;

		// Token: 0x04000021 RID: 33
		public readonly ILoc _loc;

		// Token: 0x04000022 RID: 34
		public VisualElement _root;

		// Token: 0x04000023 RID: 35
		public TextField _pathField;

		// Token: 0x04000024 RID: 36
		public Label _tipLabel;

		// Token: 0x04000025 RID: 37
		public Action<string> _openFileCallback;

		// Token: 0x04000026 RID: 38
		public string _focusInPath;
	}
}
