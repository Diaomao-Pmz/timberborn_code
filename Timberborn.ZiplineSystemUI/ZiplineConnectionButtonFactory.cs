using System;
using Timberborn.BlueprintSystem;
using Timberborn.CoreUI;
using Timberborn.EntitySystem;
using Timberborn.Localization;
using Timberborn.SelectionSystem;
using Timberborn.SingletonSystem;
using Timberborn.ToolSystem;
using Timberborn.TooltipSystem;
using Timberborn.ZiplineSystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.ZiplineSystemUI
{
	// Token: 0x02000009 RID: 9
	public class ZiplineConnectionButtonFactory : ILoadableSingleton
	{
		// Token: 0x0600001D RID: 29 RVA: 0x000025B8 File Offset: 0x000007B8
		public ZiplineConnectionButtonFactory(VisualElementLoader visualElementLoader, ZiplineConnectionAddingTool ziplineConnectionAddingTool, ToolService toolService, EntitySelectionService entitySelectionService, ZiplineConnectionService ziplineConnectionService, Highlighter highlighter, ZiplineCableRenderer ziplineCableRenderer, ISpecService specService, ITooltipRegistrar tooltipRegistrar, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._ziplineConnectionAddingTool = ziplineConnectionAddingTool;
			this._toolService = toolService;
			this._entitySelectionService = entitySelectionService;
			this._ziplineConnectionService = ziplineConnectionService;
			this._highlighter = highlighter;
			this._ziplineCableRenderer = ziplineCableRenderer;
			this._specService = specService;
			this._tooltipRegistrar = tooltipRegistrar;
			this._loc = loc;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002618 File Offset: 0x00000818
		public void Load()
		{
			this._ziplineSystemColorsSpec = this._specService.GetSingleSpec<ZiplineSystemColorsSpec>();
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000262B File Offset: 0x0000082B
		public void CreateConnection(VisualElement root, ZiplineTower owner, ZiplineTower otherZiplineTower)
		{
			this.SetForConnection(owner, otherZiplineTower, this.Create(root));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000263C File Offset: 0x0000083C
		public void CreateAddConnection(VisualElement root, ZiplineTower owner)
		{
			Button button = this.Create(root);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.AddConnection(owner);
			}, 0);
			ZiplineConnectionButtonFactory.SetName(button, this._loc.T(ZiplineConnectionButtonFactory.AddConnectionLocKey));
			ZiplineConnectionButtonFactory.SetIcon(button, null, ZiplineConnectionButtonFactory.PlusIconClass);
			this.SetRemoveConnectionButton(button, null);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000026A4 File Offset: 0x000008A4
		public void CreateEmpty(VisualElement root)
		{
			Button button = this.Create(root);
			ZiplineConnectionButtonFactory.SetName(button, null);
			this.SetRemoveConnectionButton(button, null);
			button.SetEnabled(false);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000026D0 File Offset: 0x000008D0
		public Button Create(VisualElement root)
		{
			string elementName = "Game/EntityPanel/ZiplineConnectionButton";
			Button button = UQueryExtensions.Q<Button>(this._visualElementLoader.LoadVisualElement(elementName), null, null);
			root.Add(button);
			return button;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002700 File Offset: 0x00000900
		public void SetForConnection(ZiplineTower owner, ZiplineTower otherZiplineTower, Button button)
		{
			button.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this.Highlight(owner, otherZiplineTower);
			}, 0);
			button.RegisterCallback<MouseLeaveEvent>(delegate(MouseLeaveEvent _)
			{
				this.Unhighlight(owner, otherZiplineTower);
			}, 0);
			button.RegisterCallback<DetachFromPanelEvent>(delegate(DetachFromPanelEvent _)
			{
				this.Unhighlight(owner, otherZiplineTower);
			}, 0);
			button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.Select(otherZiplineTower);
			}, 0);
			LabeledEntity component = otherZiplineTower.GetComponent<LabeledEntity>();
			ZiplineConnectionButtonFactory.SetName(button, component.DisplayName);
			ZiplineConnectionButtonFactory.SetIcon(button, component.Image, null);
			this.SetRemoveConnectionButton(button, delegate
			{
				this.RemoveConnection(owner, otherZiplineTower);
			});
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027AC File Offset: 0x000009AC
		public void Highlight(ZiplineTower owner, ZiplineTower otherZiplineTower)
		{
			this._highlighter.HighlightPrimary(otherZiplineTower, this._ziplineSystemColorsSpec.ConnectableColor);
			this._ziplineCableRenderer.HighlightConnection(owner, otherZiplineTower, this._ziplineSystemColorsSpec.ConnectableColor);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027DD File Offset: 0x000009DD
		public void Unhighlight(ZiplineTower owner, ZiplineTower otherZiplineTower)
		{
			if (owner && otherZiplineTower)
			{
				this._ziplineCableRenderer.UnhighlightConnection(owner, otherZiplineTower);
			}
			this._highlighter.UnhighlightPrimary(otherZiplineTower);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002808 File Offset: 0x00000A08
		public void Select(ZiplineTower otherZiplineTower)
		{
			this._entitySelectionService.SelectAndFocusOn(otherZiplineTower);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002816 File Offset: 0x00000A16
		public void RemoveConnection(ZiplineTower owner, ZiplineTower otherZiplineTower)
		{
			this.Unhighlight(owner, otherZiplineTower);
			this._ziplineConnectionService.Disconnect(owner, otherZiplineTower);
			this._entitySelectionService.Unselect();
			this._entitySelectionService.Select(owner);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002844 File Offset: 0x00000A44
		public void AddConnection(ZiplineTower ziplineTower)
		{
			this._ziplineConnectionAddingTool.SwitchTo(ziplineTower);
			this._toolService.SwitchTool(this._ziplineConnectionAddingTool);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002863 File Offset: 0x00000A63
		public static void SetName(VisualElement root, string text = null)
		{
			Label label = UQueryExtensions.Q<Label>(root, "Name", null);
			label.text = text;
			label.ToggleDisplayStyle(text != null);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002884 File Offset: 0x00000A84
		public static void SetIcon(VisualElement root, Sprite sprite, string className = null)
		{
			Image image = UQueryExtensions.Q<Image>(root, "Icon", null);
			if (sprite != null)
			{
				image.sprite = sprite;
				image.AddToClassList(className);
			}
			if (className != null)
			{
				image.AddToClassList(className);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028C0 File Offset: 0x00000AC0
		public void SetRemoveConnectionButton(VisualElement root, Action actionCallback = null)
		{
			Button button = UQueryExtensions.Q<Button>(root, "RemoveConnection", null);
			if (actionCallback != null)
			{
				button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					actionCallback();
				}, 0);
				this._tooltipRegistrar.RegisterLocalizable(button, ZiplineConnectionButtonFactory.RemoveConnectionTooltipLocKey);
				return;
			}
			button.ToggleDisplayStyle(false);
		}

		// Token: 0x04000022 RID: 34
		public static readonly string PlusIconClass = "icon--plus";

		// Token: 0x04000023 RID: 35
		public static readonly string AddConnectionLocKey = "Zipline.AddConnection";

		// Token: 0x04000024 RID: 36
		public static readonly string RemoveConnectionTooltipLocKey = "Zipline.RemoveConnection";

		// Token: 0x04000025 RID: 37
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000026 RID: 38
		public readonly ZiplineConnectionAddingTool _ziplineConnectionAddingTool;

		// Token: 0x04000027 RID: 39
		public readonly ToolService _toolService;

		// Token: 0x04000028 RID: 40
		public readonly EntitySelectionService _entitySelectionService;

		// Token: 0x04000029 RID: 41
		public readonly ZiplineConnectionService _ziplineConnectionService;

		// Token: 0x0400002A RID: 42
		public readonly Highlighter _highlighter;

		// Token: 0x0400002B RID: 43
		public readonly ZiplineCableRenderer _ziplineCableRenderer;

		// Token: 0x0400002C RID: 44
		public readonly ISpecService _specService;

		// Token: 0x0400002D RID: 45
		public readonly ITooltipRegistrar _tooltipRegistrar;

		// Token: 0x0400002E RID: 46
		public readonly ILoc _loc;

		// Token: 0x0400002F RID: 47
		public ZiplineSystemColorsSpec _ziplineSystemColorsSpec;
	}
}
