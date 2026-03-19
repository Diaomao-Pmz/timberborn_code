using System;
using System.Collections.Generic;
using Timberborn.Attractions;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.CharactersUI;
using Timberborn.CoreUI;
using Timberborn.EnterableSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using UnityEngine.UIElements;

namespace Timberborn.AttractionsUI
{
	// Token: 0x02000008 RID: 8
	public class AttractionFragment : IEntityPanelFragment
	{
		// Token: 0x06000016 RID: 22 RVA: 0x00002363 File Offset: 0x00000563
		public AttractionFragment(VisualElementLoader visualElementLoader, EntitySelectionService entitySelectionService, CharacterButtonFactory characterButtonFactory, EventBus eventBus)
		{
			this._visualElementLoader = visualElementLoader;
			this._entitySelectionService = entitySelectionService;
			this._characterButtonFactory = characterButtonFactory;
			this._eventBus = eventBus;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002394 File Offset: 0x00000594
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/AttractionFragment");
			this._buildingUserButtons = UQueryExtensions.Q<VisualElement>(this._root, "BeaverUserButtons", null);
			this._root.ToggleDisplayStyle(false);
			this._eventBus.Register(this);
			return this._root;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023EC File Offset: 0x000005EC
		[OnEvent]
		public void OnEnteredFinishedState(EnteredFinishedStateEvent enteredFinishedStateEvent)
		{
			if (this._attraction && enteredFinishedStateEvent.BlockObject == this._attraction.GetComponent<BlockObject>())
			{
				this.InitializeButtons();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002414 File Offset: 0x00000614
		public void ShowFragment(BaseComponent entity)
		{
			this._attraction = entity.GetComponent<Attraction>();
			if (this._attraction)
			{
				this._enterable = entity.GetComponent<Enterable>();
				this._enterable.EntererAdded += this.OnEntererAdded;
				this._enterable.EntererRemoved += this.OnEntererRemoved;
				this.InitializeButtons();
				if (this._attraction.Enabled)
				{
					this.UpdateButtons();
				}
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002490 File Offset: 0x00000690
		public void ClearFragment()
		{
			if (this._enterable)
			{
				this._enterable.EntererAdded -= this.OnEntererAdded;
				this._enterable.EntererRemoved -= this.OnEntererRemoved;
			}
			this._enterable = null;
			this._attraction = null;
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024F2 File Offset: 0x000006F2
		public void UpdateFragment()
		{
			if (this._attraction && this._attraction.Enabled)
			{
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002527 File Offset: 0x00000727
		public void OnEntererAdded(object sender, EntererAddedEventArgs e)
		{
			this.UpdateButtons();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002527 File Offset: 0x00000727
		public void OnEntererRemoved(object sender, EntererRemovedEventArgs e)
		{
			this.UpdateButtons();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002530 File Offset: 0x00000730
		public void InitializeButtons()
		{
			this.RemoveAllButtons();
			for (int i = 0; i < this._enterable.Capacity; i++)
			{
				this.AddEmptyButton();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002560 File Offset: 0x00000760
		public void RemoveAllButtons()
		{
			foreach (CharacterButton characterButton in this._userButtons)
			{
				this._buildingUserButtons.Remove(characterButton.Root);
			}
			this._userButtons.Clear();
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025C8 File Offset: 0x000007C8
		public void AddEmptyButton()
		{
			CharacterButton characterButton = this._characterButtonFactory.Create();
			characterButton.ShowEmpty();
			this._userButtons.Add(characterButton);
			this._buildingUserButtons.Add(characterButton.Root);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002604 File Offset: 0x00000804
		public void UpdateButtons()
		{
			int num = 0;
			using (IEnumerator<Enterer> enumerator = this._enterable.EnterersInside.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Enterer enterer = enumerator.Current;
					this._userButtons[num++].ShowFilled(enterer, delegate()
					{
						this._entitySelectionService.SelectAndFollow(enterer);
					});
				}
				goto IL_7E;
			}
			IL_69:
			this._userButtons[num].ShowEmpty();
			num++;
			IL_7E:
			if (num >= this._userButtons.Count)
			{
				return;
			}
			goto IL_69;
		}

		// Token: 0x04000016 RID: 22
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000017 RID: 23
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000018 RID: 24
		public readonly CharacterButtonFactory _characterButtonFactory;

		// Token: 0x04000019 RID: 25
		public readonly EventBus _eventBus;

		// Token: 0x0400001A RID: 26
		public VisualElement _root;

		// Token: 0x0400001B RID: 27
		public VisualElement _buildingUserButtons;

		// Token: 0x0400001C RID: 28
		public Attraction _attraction;

		// Token: 0x0400001D RID: 29
		public Enterable _enterable;

		// Token: 0x0400001E RID: 30
		public readonly List<CharacterButton> _userButtons = new List<CharacterButton>();
	}
}
