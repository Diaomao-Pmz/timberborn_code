using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CameraSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.MortalComponents;
using Timberborn.SingletonSystem;
using Timberborn.UILayoutSystem;
using UnityEngine;

namespace Timberborn.StatusSystem
{
	// Token: 0x02000012 RID: 18
	public class StatusIconCycler : BaseComponent, IAwakableComponent, IStartableComponent, IDeadNeededComponent, IDeletableEntity
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000044 RID: 68 RVA: 0x000027AC File Offset: 0x000009AC
		// (remove) Token: 0x06000045 RID: 69 RVA: 0x000027E4 File Offset: 0x000009E4
		public event EventHandler ActiveStateChanged;

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002819 File Offset: 0x00000A19
		// (set) Token: 0x06000047 RID: 71 RVA: 0x00002821 File Offset: 0x00000A21
		public GameObject Root { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000282A File Offset: 0x00000A2A
		// (set) Token: 0x06000049 RID: 73 RVA: 0x00002832 File Offset: 0x00000A32
		public bool VisibleAndActive { get; private set; }

		// Token: 0x0600004A RID: 74 RVA: 0x0000283B File Offset: 0x00000A3B
		public StatusIconCycler(StatusIconMaterials statusIconMaterials, StatusIconCyclerUpdater statusIconCyclerUpdater, StatusIconCyclerFactory statusIconCyclerFactory, UIVisibilityManager uiVisibilityManager, EventBus eventBus)
		{
			this._statusIconMaterials = statusIconMaterials;
			this._statusIconCyclerUpdater = statusIconCyclerUpdater;
			this._statusIconCyclerFactory = statusIconCyclerFactory;
			this._uiVisibilityManager = uiVisibilityManager;
			this._eventBus = eventBus;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x0000287A File Offset: 0x00000A7A
		public void Awake()
		{
			this._eventBus.Register(this);
			this._statusSubject = base.GetComponent<StatusSubject>();
			this._facingCamera = base.GetComponent<FacingCamera>();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000028A0 File Offset: 0x00000AA0
		public void DeleteEntity()
		{
			this._statusIconCyclerUpdater.RemoveStatusIconCycler(this);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000028AE File Offset: 0x00000AAE
		public void Start()
		{
			this._statusSubject.StatusToggled += this.OnStatusToggled;
			this.HideShownIcon();
			this.CheckActiveStatuses();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000028D3 File Offset: 0x00000AD3
		[OnEvent]
		public void OnUIVisibilityChanged(UIVisibilityChangedEvent uiVisibilityChangedEvent)
		{
			this.ToggleRoot(this.VisibleAndActive);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000028E4 File Offset: 0x00000AE4
		public void InitializeIcon(Transform parent, float radius)
		{
			this.Root = this._statusIconCyclerFactory.CreateAsChild(parent);
			this._statusIcon = this.Root.transform.GetChild(0).gameObject;
			this._colliderTransform = this.Root.transform.GetChild(1).transform;
			this._statusIconRenderer = this._statusIcon.GetComponentInChildren<MeshRenderer>();
			this._radius = radius;
			this.UpdateScale();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x0000295C File Offset: 0x00000B5C
		public StatusVisibilityToggle GetStatusVisibilityToggle()
		{
			StatusVisibilityToggle statusVisibilityToggle = new StatusVisibilityToggle();
			this._toggles.Add(statusVisibilityToggle);
			statusVisibilityToggle.StateChanged += delegate(object _, EventArgs _)
			{
				this.UpdateVisibility();
			};
			return statusVisibilityToggle;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x0000298E File Offset: 0x00000B8E
		public void IntervalUpdate()
		{
			if (this._visible)
			{
				this.UpdateIcon();
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000299E File Offset: 0x00000B9E
		public void SetIconLocalPosition(Vector3 position)
		{
			if (this._statusIcon)
			{
				this._statusIcon.transform.localPosition = position;
				this._colliderTransform.localPosition = position;
				this.UpdateScale();
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000029D0 File Offset: 0x00000BD0
		public void UpdateStatusVisibility()
		{
			if (!this._visible || (this._shownIconStatus != null && !this._shownIconStatus.IsVisible()))
			{
				this.HideShownIcon();
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000029F5 File Offset: 0x00000BF5
		public void OnStatusToggled(object sender, EventArgs e)
		{
			if (this._visible)
			{
				this.CheckActiveStatuses();
				this.ToggleRoot(this.VisibleAndActive);
				EventHandler activeStateChanged = this.ActiveStateChanged;
				if (activeStateChanged == null)
				{
					return;
				}
				activeStateChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002A28 File Offset: 0x00000C28
		public void CheckActiveStatuses()
		{
			bool visibleAndActive = this.VisibleAndActive;
			bool visibleAndActive2;
			if (this._visible)
			{
				visibleAndActive2 = this._statusSubject.ActiveStatuses.FastAny((StatusInstance status) => status.ShowFloatingIcon);
			}
			else
			{
				visibleAndActive2 = false;
			}
			this.VisibleAndActive = visibleAndActive2;
			if (this.VisibleAndActive)
			{
				this.UpdateIcon();
				if (!visibleAndActive)
				{
					this._statusIconCyclerUpdater.AddStatusIconCycler(this);
					return;
				}
			}
			else
			{
				this.HideShownIcon();
				if (visibleAndActive)
				{
					this._statusIconCyclerUpdater.RemoveStatusIconCycler(this);
				}
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public void UpdateIcon()
		{
			if (!this._statusSubject.ActiveStatuses.IsEmpty())
			{
				StatusInstance statusInstance = this.MoveToNextStatus();
				if (statusInstance.ShowFloatingIcon && statusInstance.IsVisible())
				{
					this.ShowIcon(statusInstance);
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public StatusInstance MoveToNextStatus()
		{
			ReadOnlyList<StatusInstance> activeStatuses = this._statusSubject.ActiveStatuses;
			int num = this._indexOfStatusCheckedInLastUpdate + 1;
			this._indexOfStatusCheckedInLastUpdate = num;
			if (num >= activeStatuses.Count)
			{
				this._indexOfStatusCheckedInLastUpdate = 0;
			}
			return activeStatuses[this._indexOfStatusCheckedInLastUpdate];
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002B3B File Offset: 0x00000D3B
		public void ShowIcon(StatusInstance status)
		{
			if (this._shownIconStatus != status)
			{
				this._shownIconStatus = status;
				this._statusIconMaterials.SetMaterial(this._statusIconRenderer, status.IconLarge);
				this.ToggleStatusIcon(true);
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B6B File Offset: 0x00000D6B
		public void HideShownIcon()
		{
			this._shownIconStatus = null;
			this.ToggleStatusIcon(false);
			this.IntervalUpdate();
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B84 File Offset: 0x00000D84
		public void UpdateVisibility()
		{
			bool flag = !this._toggles.FastAny((StatusVisibilityToggle toggle) => toggle.Hidden);
			if (flag != this._visible)
			{
				this._visible = flag;
				this.ToggleRoot(flag);
				this.CheckActiveStatuses();
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002BDC File Offset: 0x00000DDC
		public void UpdateScale()
		{
			Vector3 localScale = base.Transform.localScale;
			Vector3 localScale2;
			localScale2..ctor(this._radius / localScale.x, this._radius / localScale.y, this._radius / localScale.z);
			this._statusIcon.transform.localScale = localScale2;
			this._colliderTransform.localScale = localScale2;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002C40 File Offset: 0x00000E40
		public void ToggleRoot(bool visible)
		{
			if (this.Root)
			{
				this.Root.SetActive(visible && this._uiVisibilityManager.GUIVisible);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002C6C File Offset: 0x00000E6C
		public void ToggleStatusIcon(bool visible)
		{
			if (this._statusIcon)
			{
				this._statusIcon.gameObject.SetActive(visible);
				this._colliderTransform.gameObject.SetActive(visible);
				if (visible)
				{
					this._facingCamera.Enable(this._statusIcon.transform);
					return;
				}
				this._facingCamera.Disable();
			}
		}

		// Token: 0x04000024 RID: 36
		public readonly StatusIconMaterials _statusIconMaterials;

		// Token: 0x04000025 RID: 37
		public readonly StatusIconCyclerUpdater _statusIconCyclerUpdater;

		// Token: 0x04000026 RID: 38
		public readonly StatusIconCyclerFactory _statusIconCyclerFactory;

		// Token: 0x04000027 RID: 39
		public readonly UIVisibilityManager _uiVisibilityManager;

		// Token: 0x04000028 RID: 40
		public readonly EventBus _eventBus;

		// Token: 0x04000029 RID: 41
		public MeshRenderer _statusIconRenderer;

		// Token: 0x0400002A RID: 42
		public float _radius;

		// Token: 0x0400002B RID: 43
		public StatusSubject _statusSubject;

		// Token: 0x0400002C RID: 44
		public FacingCamera _facingCamera;

		// Token: 0x0400002D RID: 45
		public StatusInstance _shownIconStatus;

		// Token: 0x0400002E RID: 46
		public GameObject _statusIcon;

		// Token: 0x0400002F RID: 47
		public Transform _colliderTransform;

		// Token: 0x04000030 RID: 48
		public int _indexOfStatusCheckedInLastUpdate;

		// Token: 0x04000031 RID: 49
		public bool _visible = true;

		// Token: 0x04000032 RID: 50
		public readonly List<StatusVisibilityToggle> _toggles = new List<StatusVisibilityToggle>();
	}
}
