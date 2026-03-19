using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Timberborn.Automation;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.AutomationUI
{
	// Token: 0x0200001A RID: 26
	[UxmlElement]
	public class TransmitterSelector : VisualElement, ILocalizableElement
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003624 File Offset: 0x00001824
		public TransmitterSelector()
		{
			Resources.Load<VisualTreeAsset>("UI/Views/Game/TransmitterSelector").CloneTree(this);
			this._dropdown = UQueryExtensions.Q<Dropdown>(this, "TransmitterDropdown", null);
			this._dropdown.Showed += this.OnDropdownShowed;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003670 File Offset: 0x00001870
		public bool IsSet
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003673 File Offset: 0x00001873
		public void Initialize(DropdownItemsSetter dropdownItemsSetter, EventBus eventBus, TransmitterPickerTool transmitterPickerTool, TransmitterDropdownProvider transmitterDropdownProvider, AutomationStateIcon automationStateIcon, Action<Automator> setter)
		{
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._eventBus = eventBus;
			this._transmitterPickerTool = transmitterPickerTool;
			this._transmitterDropdownProvider = transmitterDropdownProvider;
			this._automationStateIcon = automationStateIcon;
			this._setter = setter;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000036A2 File Offset: 0x000018A2
		public void Localize(ILoc loc)
		{
			this._dropdown.OverrideLabelLocKey(this._labelLocKey);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000036B5 File Offset: 0x000018B5
		public void Show(BaseComponent owner)
		{
			this._owner = owner;
			this._dropdownItemsSetter.SetItems(this._dropdown, this._transmitterDropdownProvider);
			this._eventBus.Register(this);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000036E1 File Offset: 0x000018E1
		public void UpdateStateIcon()
		{
			this._automationStateIcon.Update();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000036EE File Offset: 0x000018EE
		public void UpdateSelectedValue()
		{
			this._dropdown.UpdateSelectedValue();
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000036FB File Offset: 0x000018FB
		public void ClearItems()
		{
			this._owner = null;
			this._dropdown.ClearItems();
			this._eventBus.Unregister(this);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000371B File Offset: 0x0000191B
		[OnEvent]
		public void OnEntityInitialized(EntityInitializedEvent entityInitializedEvent)
		{
			this.UpdateItems(entityInitializedEvent.Entity);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003729 File Offset: 0x00001929
		[OnEvent]
		public void OnEntityDeleted(EntityDeletedEvent entityDeletedEvent)
		{
			this.UpdateItems(entityDeletedEvent.Entity);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003737 File Offset: 0x00001937
		public void OnDropdownShowed(object sender, EventArgs args)
		{
			this._transmitterPickerTool.SwitchTo(this._owner, this._dropdown, this._setter);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003756 File Offset: 0x00001956
		public void UpdateItems(BaseComponent entity)
		{
			if (this._owner && entity.HasComponent<Automator>())
			{
				this._dropdown.ClearItems();
				this._dropdownItemsSetter.SetItems(this._dropdown, this._transmitterDropdownProvider);
			}
		}

		// Token: 0x04000069 RID: 105
		public DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400006A RID: 106
		public EventBus _eventBus;

		// Token: 0x0400006B RID: 107
		public TransmitterPickerTool _transmitterPickerTool;

		// Token: 0x0400006C RID: 108
		public readonly Dropdown _dropdown;

		// Token: 0x0400006D RID: 109
		public TransmitterDropdownProvider _transmitterDropdownProvider;

		// Token: 0x0400006E RID: 110
		public AutomationStateIcon _automationStateIcon;

		// Token: 0x0400006F RID: 111
		[UxmlAttribute("label-loc-key")]
		public string _labelLocKey;

		// Token: 0x04000070 RID: 112
		public BaseComponent _owner;

		// Token: 0x04000071 RID: 113
		public Action<Automator> _setter;

		// Token: 0x0200001B RID: 27
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x06000085 RID: 133 RVA: 0x0000378F File Offset: 0x0000198F
			[RegisterUxmlCache]
			[Conditional("UNITY_EDITOR")]
			public static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(TransmitterSelector.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("_labelLocKey", "label-loc-key", null, Array.Empty<string>())
				}, false);
			}

			// Token: 0x06000086 RID: 134 RVA: 0x000037C3 File Offset: 0x000019C3
			public override object CreateInstance()
			{
				return new TransmitterSelector();
			}

			// Token: 0x06000087 RID: 135 RVA: 0x000037CC File Offset: 0x000019CC
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				TransmitterSelector transmitterSelector = (TransmitterSelector)obj;
				if (UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this._labelLocKey_UxmlAttributeFlags))
				{
					transmitterSelector._labelLocKey = this._labelLocKey;
				}
			}

			// Token: 0x04000072 RID: 114
			[UxmlAttribute("label-loc-key")]
			[SerializeField]
			private string _labelLocKey;

			// Token: 0x04000073 RID: 115
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags _labelLocKey_UxmlAttributeFlags;
		}
	}
}
