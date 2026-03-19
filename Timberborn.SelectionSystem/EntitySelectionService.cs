using System;
using Timberborn.BaseComponentSystem;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.EntitySystem;
using Timberborn.EntityUndoSystem;
using Timberborn.LevelVisibilitySystem;
using Timberborn.SingletonSystem;
using UnityEngine;

namespace Timberborn.SelectionSystem
{
	// Token: 0x0200000B RID: 11
	public class EntitySelectionService : ILoadableSingleton, IUpdatableSingleton
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026BE File Offset: 0x000008BE
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000026C6 File Offset: 0x000008C6
		public bool IsAnythingSelected { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000026CF File Offset: 0x000008CF
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000026D7 File Offset: 0x000008D7
		public SelectableObject SelectedObject { get; private set; }

		// Token: 0x06000034 RID: 52 RVA: 0x000026E0 File Offset: 0x000008E0
		public EntitySelectionService(EventBus eventBus, Highlighter highlighter, SelectableObjectRetriever selectableObjectRetriever, CameraTargeter cameraTargeter, ILevelVisibilityService levelVisibilityService, ISpecService specService)
		{
			this._eventBus = eventBus;
			this._highlighter = highlighter;
			this._selectableObjectRetriever = selectableObjectRetriever;
			this._cameraTargeter = cameraTargeter;
			this._levelVisibilityService = levelVisibilityService;
			this._specService = specService;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002715 File Offset: 0x00000915
		public void Load()
		{
			this._eventBus.Register(this);
			this._entitySelectionColor = this._specService.GetSingleSpec<SelectionColorsSpec>().EntitySelection;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002739 File Offset: 0x00000939
		public void UpdateSingleton()
		{
			if (this.IsAnythingSelected && !this.SelectedObjectIsDestroyed)
			{
				this.HighlightSelectedObject();
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002751 File Offset: 0x00000951
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			this.Unselect(entityDeletedEvent.Entity.GetComponent<SelectableObject>());
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002764 File Offset: 0x00000964
		[OnEvent]
		public void OnUndoableEntityChanged(UndoableEntityChangedEvent undoableEntityChangedEvent)
		{
			this.Select(undoableEntityChangedEvent.Entity);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002774 File Offset: 0x00000974
		public void Select(BaseComponent target)
		{
			if (EntitySelectionService.IsSelectable(target))
			{
				SelectableObject selectableObject = this._selectableObjectRetriever.GetSelectableObject(target);
				this.SelectSelectable(selectableObject);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027A0 File Offset: 0x000009A0
		public void SelectAndFollow(BaseComponent target)
		{
			if (EntitySelectionService.IsSelectable(target))
			{
				SelectableObject selectableObject = this._selectableObjectRetriever.GetSelectableObject(target);
				this.UpdateVisibleLayer(selectableObject);
				this.SelectSelectable(selectableObject);
				this.FollowSelectable(selectableObject);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027D8 File Offset: 0x000009D8
		public void SelectAndFocusOn(BaseComponent target)
		{
			if (EntitySelectionService.IsSelectable(target))
			{
				SelectableObject selectableObject = this._selectableObjectRetriever.GetSelectableObject(target);
				this.UpdateVisibleLayer(selectableObject);
				this.SelectSelectable(selectableObject);
				this.FocusOnSelectable(selectableObject);
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002810 File Offset: 0x00000A10
		public void UnselectAndFollow(BaseComponent target)
		{
			this.Unselect();
			if (EntitySelectionService.IsSelectable(target))
			{
				SelectableObject selectableObject = this._selectableObjectRetriever.GetSelectableObject(target);
				this.FollowSelectable(selectableObject);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002840 File Offset: 0x00000A40
		public void Unselect()
		{
			if (this.IsAnythingSelected)
			{
				if (!this.SelectedObjectIsDestroyed)
				{
					this._highlighter.UnhighlightAllPrimary();
					this.SelectedObject.OnUnselect();
				}
				SelectableObject selectedObject = this.SelectedObject;
				this.SelectedObject = null;
				this.IsAnythingSelected = false;
				this._eventBus.Post(new SelectableObjectUnselectedEvent(selectedObject));
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002899 File Offset: 0x00000A99
		public void Replace(SelectableObject oldTarget, SelectableObject newTarget)
		{
			if (this.IsSelected(oldTarget))
			{
				if (this.IsFollowed(oldTarget))
				{
					this.FollowSelectable(newTarget);
				}
				this.SelectSelectable(newTarget);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028BB File Offset: 0x00000ABB
		public bool IsSelected(SelectableObject target)
		{
			return this.IsAnythingSelected && this.SelectedObject == target;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000028D0 File Offset: 0x00000AD0
		public void UnhighlightUntilNextUpdate()
		{
			if (this.IsAnythingSelected)
			{
				this._highlighter.UnhighlightAllPrimary();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000028E5 File Offset: 0x00000AE5
		public bool SelectedObjectIsDestroyed
		{
			get
			{
				return !this.SelectedObject;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028F5 File Offset: 0x00000AF5
		public static bool IsSelectable(BaseComponent target)
		{
			return target && target.HasComponent<EntityComponent>() && !target.GetComponent<EntityComponent>().Deleted;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002917 File Offset: 0x00000B17
		public void Unselect(SelectableObject target)
		{
			if (this.IsSelected(target))
			{
				this.Unselect();
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002928 File Offset: 0x00000B28
		public void SelectSelectable(SelectableObject target)
		{
			if (this.SelectedObject != target)
			{
				this.Unselect();
				this.SelectedObject = target;
				this.IsAnythingSelected = true;
				this.HighlightSelectedObject();
				this._eventBus.Post(new SelectableObjectSelectedEvent(this.SelectedObject));
				target.OnSelect();
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002974 File Offset: 0x00000B74
		public void UpdateVisibleLayer(SelectableObject target)
		{
			int z = CoordinateSystem.WorldToGridInt(target.Transform.position).z;
			if (this._levelVisibilityService.MaxVisibleLevel < z)
			{
				this._levelVisibilityService.SetMaxVisibleLevel(z);
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000029B4 File Offset: 0x00000BB4
		public void FollowSelectable(SelectableObject selectableObject)
		{
			this._cameraTargeter.Follow(selectableObject);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000029C2 File Offset: 0x00000BC2
		public void FocusOnSelectable(SelectableObject selectableObject)
		{
			this._cameraTargeter.CenterCameraOn(selectableObject);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029D0 File Offset: 0x00000BD0
		public bool IsFollowed(SelectableObject target)
		{
			SelectableObject followedTarget = this._cameraTargeter.FollowedTarget;
			return followedTarget && followedTarget == target;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029F7 File Offset: 0x00000BF7
		public void HighlightSelectedObject()
		{
			this._highlighter.HighlightPrimary(this.SelectedObject, this._entitySelectionColor);
		}

		// Token: 0x0400001B RID: 27
		public readonly EventBus _eventBus;

		// Token: 0x0400001C RID: 28
		public readonly Highlighter _highlighter;

		// Token: 0x0400001D RID: 29
		public readonly SelectableObjectRetriever _selectableObjectRetriever;

		// Token: 0x0400001E RID: 30
		public readonly CameraTargeter _cameraTargeter;

		// Token: 0x0400001F RID: 31
		public readonly ILevelVisibilityService _levelVisibilityService;

		// Token: 0x04000020 RID: 32
		public readonly ISpecService _specService;

		// Token: 0x04000021 RID: 33
		public Color _entitySelectionColor;
	}
}
