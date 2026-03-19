using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.CoreUI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.SteamWorkshopUI
{
	// Token: 0x0200000A RID: 10
	public class UploadPanelTags
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600004C RID: 76 RVA: 0x00002C18 File Offset: 0x00000E18
		// (remove) Token: 0x0600004D RID: 77 RVA: 0x00002C50 File Offset: 0x00000E50
		public event EventHandler TagsChanged;

		// Token: 0x0600004E RID: 78 RVA: 0x00002C85 File Offset: 0x00000E85
		public UploadPanelTags(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C9F File Offset: 0x00000E9F
		public void Initialize(VisualElement root)
		{
			this._root = root;
			this._tagsScrollView = UQueryExtensions.Q<ScrollView>(root, null, null);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002CB8 File Offset: 0x00000EB8
		public void Open(ISteamWorkshopUploadable steamWorkshopUploadable)
		{
			foreach (IGrouping<WorkshopTagCategory, WorkshopTag> grouping in from tag in steamWorkshopUploadable.AvailableTags
			group tag by tag.Category into @group
			orderby @group.Key.Order
			select @group)
			{
				this.AddCategory(grouping.Key.Name);
				foreach (WorkshopTag workshopTag in grouping.OrderBy((WorkshopTag tag) => tag.Order))
				{
					this.AddTag(workshopTag.Name, steamWorkshopUploadable.ChosenTags.Contains(workshopTag.Name));
				}
			}
			this._root.ToggleDisplayStyle(this._tags.Count > 0);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002DF0 File Offset: 0x00000FF0
		public IEnumerable<string> GetChosenTags()
		{
			return from tagToggle in this._tags
			where tagToggle.value
			select tagToggle.text;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002E4B File Offset: 0x0000104B
		public void Clear()
		{
			this._tags.Clear();
			this._tagsScrollView.Clear();
			this._tagsScrollView.scrollOffset = Vector2.zero;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002E74 File Offset: 0x00001074
		public void AddCategory(string category)
		{
			Label label = (Label)this._visualElementLoader.LoadVisualElement("Common/SteamWorkshop/SteamWorkshopTagCategory");
			label.text = category;
			if (this._tagsScrollView.childCount > 0)
			{
				label.AddToClassList(UploadPanelTags.CategoryMarginClass);
			}
			this._tagsScrollView.Add(label);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002EC4 File Offset: 0x000010C4
		public void AddTag(string tag, bool enabled)
		{
			Toggle toggle = (Toggle)this._visualElementLoader.LoadVisualElement("Common/SteamWorkshop/SteamWorkshopTag");
			toggle.text = tag;
			toggle.value = enabled;
			this._tagsScrollView.Add(toggle);
			this._tags.Add(toggle);
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(toggle, delegate(ChangeEvent<bool> _)
			{
				EventHandler tagsChanged = this.TagsChanged;
				if (tagsChanged == null)
				{
					return;
				}
				tagsChanged(this, EventArgs.Empty);
			});
		}

		// Token: 0x04000032 RID: 50
		public static readonly string CategoryMarginClass = "steam-workshop-tag--margin";

		// Token: 0x04000034 RID: 52
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000035 RID: 53
		public VisualElement _root;

		// Token: 0x04000036 RID: 54
		public ScrollView _tagsScrollView;

		// Token: 0x04000037 RID: 55
		public readonly List<Toggle> _tags = new List<Toggle>();
	}
}
