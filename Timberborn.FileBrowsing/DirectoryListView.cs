using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Timberborn.Common;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.FileBrowsing
{
	// Token: 0x02000004 RID: 4
	public class DirectoryListView
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x000020F8 File Offset: 0x000002F8
		public event EventHandler DirectoryChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000005 RID: 5 RVA: 0x00002130 File Offset: 0x00000330
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x00002168 File Offset: 0x00000368
		public event EventHandler<DiskSystemEntry> EntryDoubleClicked;

		// Token: 0x06000007 RID: 7 RVA: 0x0000219D File Offset: 0x0000039D
		public DirectoryListView(DiskSystemEntryElementFactory diskSystemEntryElementFactory, DialogBoxShower dialogBoxShower)
		{
			this._diskSystemEntryElementFactory = diskSystemEntryElementFactory;
			this._dialogBoxShower = dialogBoxShower;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000021BE File Offset: 0x000003BE
		public string CurrentPath
		{
			get
			{
				return this._currentDirectory.Path;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021CC File Offset: 0x000003CC
		public void Initialize(VisualElement root)
		{
			Asserts.FieldIsNull<DirectoryListView>(this, this._diskSystemEntryView, "_diskSystemEntryView");
			this._diskSystemEntryView = UQueryExtensions.Q<ListView>(root, "DiskSystemEntries", null);
			this._diskSystemEntryView.makeItem = (() => this._diskSystemEntryElementFactory.Create(new EventCallback<ClickEvent>(this.OnDiskSystemEntryClicked)));
			this._diskSystemEntryView.bindItem = delegate(VisualElement ve, int i)
			{
				this._diskSystemEntryElementFactory.Bind(ve, this._diskSystemEntries[i], this._fileFilter);
			};
			this._diskSystemEntryView.itemsSource = this._diskSystemEntries;
			this._diskSystemEntryView.virtualizationMethod = 1;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002247 File Offset: 0x00000447
		public void SetFileFilter(FileFilter fileFilter)
		{
			this._fileFilter = fileFilter;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002250 File Offset: 0x00000450
		public void Clear()
		{
			this._diskSystemEntries.Clear();
			this._fileFilter = null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002264 File Offset: 0x00000464
		public bool TryGetSelectedDiskSystemEntry(out DiskSystemEntry diskSystemEntry)
		{
			int selectedIndex = this._diskSystemEntryView.selectedIndex;
			if (selectedIndex >= 0)
			{
				diskSystemEntry = this._diskSystemEntries[selectedIndex];
				return true;
			}
			diskSystemEntry = default(DiskSystemEntry);
			return false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000229D File Offset: 0x0000049D
		public void GoUpward()
		{
			this.TryOpenDirectory(this._currentDirectory.Parent);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022B4 File Offset: 0x000004B4
		public bool TryOpenDirectory(string path)
		{
			try
			{
				DiskSystemEntry diskSystemEntry = DiskSystemEntry.Create(path);
				if (diskSystemEntry.Exists)
				{
					this.OpenDirectory(diskSystemEntry);
					return true;
				}
			}
			catch (UnauthorizedAccessException)
			{
				this.ShowNoPermissionDialog();
			}
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022FC File Offset: 0x000004FC
		public void OnDiskSystemEntryClicked(ClickEvent evt)
		{
			DiskSystemEntry e;
			if (evt.clickCount > 1 && this.TryGetSelectedDiskSystemEntry(out e))
			{
				EventHandler<DiskSystemEntry> entryDoubleClicked = this.EntryDoubleClicked;
				if (entryDoubleClicked == null)
				{
					return;
				}
				entryDoubleClicked(this, e);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002330 File Offset: 0x00000530
		public void OpenDirectory(DiskSystemEntry diskSystemEntry)
		{
			this._diskSystemEntries.Clear();
			this._diskSystemEntries.AddRange(this.GetChildren(diskSystemEntry));
			this._diskSystemEntryView.ClearSelection();
			this._diskSystemEntryView.RefreshItems();
			this._diskSystemEntryView.ScrollToItem(0);
			this._currentDirectory = diskSystemEntry;
			EventHandler directoryChanged = this.DirectoryChanged;
			if (directoryChanged == null)
			{
				return;
			}
			directoryChanged(this, EventArgs.Empty);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000239C File Offset: 0x0000059C
		public IEnumerable<DiskSystemEntry> GetChildren(DiskSystemEntry diskSystemEntry)
		{
			if (string.IsNullOrEmpty(diskSystemEntry.Path))
			{
				return from drive in DriveInfo.GetDrives()
				where drive.IsReady
				select DiskSystemEntry.Create(drive.Name);
			}
			return from info in new DirectoryInfo(diskSystemEntry.Path).GetFileSystemInfos()
			where !info.Attributes.HasFlag(FileAttributes.Hidden | FileAttributes.System)
			where info.Attributes.HasFlag(FileAttributes.Directory) || this._fileFilter.IsValidFile(info)
			select DiskSystemEntry.Create(info.FullName) into entry
			orderby entry.IsDirectory descending
			select entry;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002493 File Offset: 0x00000693
		public void ShowNoPermissionDialog()
		{
			this._dialogBoxShower.Create().SetLocalizedMessage(DirectoryListView.NoPermissionKey).SetConfirmButton(delegate()
			{
				this.TryOpenDirectory(this._currentDirectory.Path);
			}).Show();
		}

		// Token: 0x04000006 RID: 6
		public static readonly string NoPermissionKey = "FileBrowser.NoPermission";

		// Token: 0x04000009 RID: 9
		public readonly DiskSystemEntryElementFactory _diskSystemEntryElementFactory;

		// Token: 0x0400000A RID: 10
		public readonly DialogBoxShower _dialogBoxShower;

		// Token: 0x0400000B RID: 11
		public readonly List<DiskSystemEntry> _diskSystemEntries = new List<DiskSystemEntry>();

		// Token: 0x0400000C RID: 12
		public FileFilter _fileFilter;

		// Token: 0x0400000D RID: 13
		public ListView _diskSystemEntryView;

		// Token: 0x0400000E RID: 14
		public DiskSystemEntry _currentDirectory;
	}
}
