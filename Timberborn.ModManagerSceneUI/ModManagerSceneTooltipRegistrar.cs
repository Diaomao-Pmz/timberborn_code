using System;
using Timberborn.ModdingUI;
using Timberborn.PlatformUtilities;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.ModManagerSceneUI
{
	// Token: 0x02000008 RID: 8
	public class ModManagerSceneTooltipRegistrar : MonoBehaviour, IModManagerTooltipRegistrar
	{
		// Token: 0x0600001F RID: 31 RVA: 0x000024F0 File Offset: 0x000006F0
		public void Awake()
		{
			UIDocument component = base.GetComponent<UIDocument>();
			this._tooltip = UQueryExtensions.Q<VisualElement>(component.rootVisualElement, "ModManagerTooltip", null);
			this._tooltipLabel = UQueryExtensions.Q<Label>(this._tooltip, "TooltipLabel", null);
			this.ToggleTooltip(false);
			this._cursorOffset = (ApplicationPlatform.IsMacOS() ? new Vector2(12f, 18f) : new Vector2(10f, 10f));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002568 File Offset: 0x00000768
		public void RegisterModWarning(VisualElement element, ModItem modItem)
		{
			element.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this.Show(ModManagerSceneTooltipRegistrar.GetWarningText(modItem));
			}, 0);
			element.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this.Hide();
			}, 0);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000025B0 File Offset: 0x000007B0
		public void RegisterModIcon(VisualElement element, ModItem modItem)
		{
			element.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this.Show(ModManagerSceneTooltipRegistrar.GetModSourceText(modItem));
			}, 0);
			element.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this.Hide();
			}, 0);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025F7 File Offset: 0x000007F7
		public void RegisterIncreaseButton(VisualElement element)
		{
			element.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this.Show("Increase loading priority");
			}, 0);
			element.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this.Hide();
			}, 0);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000261F File Offset: 0x0000081F
		public void RegisterDecreaseButton(VisualElement element)
		{
			element.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this.Show("Decrease loading priority");
			}, 0);
			element.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this.Hide();
			}, 0);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002648 File Offset: 0x00000848
		public void Update()
		{
			float unscaledTime = Time.unscaledTime;
			float? showTime = this._showTime;
			if (unscaledTime > showTime.GetValueOrDefault() & showTime != null)
			{
				this.ToggleTooltip(true);
				this.UpdatePosition();
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002681 File Offset: 0x00000881
		public void ToggleTooltip(bool show)
		{
			this._tooltip.style.display = (show ? 0 : 1);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000026A0 File Offset: 0x000008A0
		public static string GetWarningText(ModItem modItem)
		{
			string result;
			switch (modItem.WarningReason)
			{
			case ModWarningReason.None:
				throw new ArgumentException("GetWarningText called with None warning reason");
			case ModWarningReason.MissingRequiredMod:
				result = "Missing required mod: \"" + modItem.WarningInfo + "\".";
				break;
			case ModWarningReason.RequiredModNotEnabled:
				result = "Required mod \"" + modItem.WarningInfo + "\" is not enabled.";
				break;
			case ModWarningReason.RequiredModInvalidVersion:
				result = "Required mod \"" + modItem.WarningInfo + "\" version is below the minimum required version.";
				break;
			case ModWarningReason.RequiredModInvalidOrder:
				result = "Required mod \"" + modItem.WarningInfo + "\" is below this mod in the load order.";
				break;
			case ModWarningReason.InvalidGameVersion:
				result = "Mod requires higher game version: " + modItem.WarningInfo + ".";
				break;
			default:
				throw new ArgumentOutOfRangeException(string.Format("Unknown {0}: {1}", "ModWarningReason", modItem.WarningReason));
			}
			return result;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002780 File Offset: 0x00000980
		public static string GetModSourceText(ModItem modItem)
		{
			return modItem.Mod.ModDirectory.DisplaySource + " mod";
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027AA File Offset: 0x000009AA
		public void Show(string text)
		{
			this._tooltipLabel.text = text;
			this._showTime = new float?(Time.unscaledTime + 0.3f);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000027CE File Offset: 0x000009CE
		public void Hide()
		{
			this.ToggleTooltip(false);
			this._showTime = null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000027E4 File Offset: 0x000009E4
		public void UpdatePosition()
		{
			Vector2 vector = Mouse.current.position.ReadValue();
			Vector2 vector2;
			vector2..ctor(vector.x / (float)Screen.width, vector.y / (float)Screen.height);
			this._tooltip.style.left = this.CalculateHorizontalPosition(vector2.x);
			float height = this._tooltip.parent.resolvedStyle.height;
			this._tooltip.style.top = (1f - vector2.y) * height + this._cursorOffset.y;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002888 File Offset: 0x00000A88
		public float CalculateHorizontalPosition(float mousePosition)
		{
			float width = this._tooltip.parent.resolvedStyle.width;
			float width2 = this._tooltip.resolvedStyle.width;
			float num = mousePosition * width + this._cursorOffset.x;
			if (num + width2 + this._cursorOffset.x <= width)
			{
				return num;
			}
			return width - width2;
		}

		// Token: 0x04000016 RID: 22
		[HideInInspector]
		public VisualElement _tooltip;

		// Token: 0x04000017 RID: 23
		[HideInInspector]
		public Label _tooltipLabel;

		// Token: 0x04000018 RID: 24
		[HideInInspector]
		public float? _showTime;

		// Token: 0x04000019 RID: 25
		[HideInInspector]
		public Vector2 _cursorOffset;
	}
}
