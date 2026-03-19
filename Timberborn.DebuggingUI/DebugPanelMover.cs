using System;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.InputSystem;
using Timberborn.SettingsSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000008 RID: 8
	public class DebugPanelMover
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000024CA File Offset: 0x000006CA
		public DebugPanelMover(InputService inputService, ISettings settings)
		{
			this._inputService = inputService;
			this._settings = settings;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024E0 File Offset: 0x000006E0
		public void Initialize(string id, VisualElement root, VisualElement contentContainer)
		{
			Asserts.FieldIsNull<DebugPanelMover>(this, this._root, "_root");
			this._id = id;
			this._root = root;
			this._contentContainer = contentContainer;
			this._headerElement = UQueryExtensions.Q<VisualElement>(this._root, "PanelHeader", null);
			this._headerElement.RegisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), 0);
			this._headerElement.RegisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), 0);
			this._headerElement.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), 0);
			UQueryExtensions.Q<Button>(this._root, "MinimizeIcon", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnClick), 0);
			Vector2 panelPosition;
			bool visible;
			if (this.TryLoadPanelPosition(out panelPosition, out visible))
			{
				this.SetPanelPosition(panelPosition);
				this._contentContainer.ToggleDisplayStyle(visible);
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000025B4 File Offset: 0x000007B4
		public void ResetPanelPosition()
		{
			this._settings.Clear(this.GetKey("x"));
			this._settings.Clear(this.GetKey("y"));
			this._settings.Clear(this.GetKey("visible"));
			this._settings.Clear(this.GetKey(DebugPanelMover.HasSavedPositionKey));
			this._root.style.left = 1;
			this._root.style.top = 1;
			this._contentContainer.ToggleDisplayStyle(true);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002651 File Offset: 0x00000851
		public bool IsVisible
		{
			get
			{
				return this._contentContainer.resolvedStyle.display != 1;
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000266C File Offset: 0x0000086C
		public void OnMouseDown(MouseDownEvent evt)
		{
			if (evt.button == 0)
			{
				float num = this._inputService.MousePositionNdc.x * this._root.parent.resolvedStyle.width - this._root.resolvedStyle.left;
				float num2 = (1f - this._inputService.MousePositionNdc.y) * this._root.parent.resolvedStyle.height - this._root.resolvedStyle.top;
				this._mouseOffset = new Vector2?(new Vector2(num, num2));
				MouseCaptureController.CaptureMouse(this._headerElement);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002718 File Offset: 0x00000918
		public void OnMouseMove(MouseMoveEvent _)
		{
			if (this._mouseOffset != null)
			{
				float num = this._inputService.MousePositionNdc.x * this._root.parent.resolvedStyle.width - this._mouseOffset.Value.x;
				float num2 = (1f - this._inputService.MousePositionNdc.y) * this._root.parent.resolvedStyle.height - this._mouseOffset.Value.y;
				this.SetPanelPosition(new Vector2(num, num2));
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027B8 File Offset: 0x000009B8
		public void OnMouseUp(MouseUpEvent evt)
		{
			if (evt.button == 0)
			{
				this._mouseOffset = null;
				this.SavePanelPosition();
				MouseCaptureController.ReleaseMouse(this._headerElement);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027DF File Offset: 0x000009DF
		public void OnClick(ClickEvent evt)
		{
			this._contentContainer.ToggleDisplayStyle(!this.IsVisible);
			this.SavePanelPosition();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000027FC File Offset: 0x000009FC
		public void SetPanelPosition(Vector2 position)
		{
			float width = this._root.parent.resolvedStyle.width;
			float height = this._root.parent.resolvedStyle.height;
			this._root.style.left = Math.Clamp(position.x, -DebugPanelMover.PanelWidth + (float)DebugPanelMover.SafetyMargin, width - (float)DebugPanelMover.SafetyMargin);
			this._root.style.top = Math.Clamp(position.y, (float)(-(float)DebugPanelMover.SafetyMargin), height - (float)DebugPanelMover.SafetyMargin);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000289C File Offset: 0x00000A9C
		public void SavePanelPosition()
		{
			this._settings.SetFloat(this.GetKey("x"), this._root.style.left.value.value);
			this._settings.SetFloat(this.GetKey("y"), this._root.style.top.value.value);
			this._settings.SetBool(this.GetKey("visible"), this.IsVisible);
			this._settings.SetBool(this.GetKey(DebugPanelMover.HasSavedPositionKey), true);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002948 File Offset: 0x00000B48
		public bool TryLoadPanelPosition(out Vector2 position, out bool visible)
		{
			float safeFloat = this._settings.GetSafeFloat(this.GetKey("x"), 0f);
			float safeFloat2 = this._settings.GetSafeFloat(this.GetKey("y"), 0f);
			position = new Vector2(safeFloat, safeFloat2);
			visible = this._settings.GetSafeBool(this.GetKey("visible"), true);
			return this._settings.GetSafeBool(this.GetKey(DebugPanelMover.HasSavedPositionKey), false);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000029CA File Offset: 0x00000BCA
		public string GetKey(string keyId)
		{
			return string.Format(DebugPanelMover.SettingsKey, this._id, keyId);
		}

		// Token: 0x04000019 RID: 25
		public static readonly string SettingsKey = "DebugPanelMover.{0}.{1}";

		// Token: 0x0400001A RID: 26
		public static readonly string HasSavedPositionKey = "HasSavedPosition";

		// Token: 0x0400001B RID: 27
		public static readonly float PanelWidth = 500f;

		// Token: 0x0400001C RID: 28
		public static readonly int SafetyMargin = 20;

		// Token: 0x0400001D RID: 29
		public readonly InputService _inputService;

		// Token: 0x0400001E RID: 30
		public readonly ISettings _settings;

		// Token: 0x0400001F RID: 31
		public string _id;

		// Token: 0x04000020 RID: 32
		public VisualElement _root;

		// Token: 0x04000021 RID: 33
		public VisualElement _headerElement;

		// Token: 0x04000022 RID: 34
		public VisualElement _contentContainer;

		// Token: 0x04000023 RID: 35
		public Vector2? _mouseOffset;
	}
}
