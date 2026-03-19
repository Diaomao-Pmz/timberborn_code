using System;
using Timberborn.CoreUI;
using Timberborn.DecalSystem;
using UnityEngine.UIElements;

namespace Timberborn.DecalSystemUI
{
	// Token: 0x02000004 RID: 4
	public class DecalButton
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public DecalButton(IDecalService decalService, VisualElement root, Decal decal)
		{
			this._decalService = decalService;
			this.Root = root;
			this._decal = decal;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E4 File Offset: 0x000002E4
		public void Initialize()
		{
			this._button = UQueryExtensions.Q<Button>(this.Root, "TextureButton", null);
			this._button.RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this._decalSupplier.SetActiveDecal(this._decal);
			}, 0);
			this._button.RegisterCallback<MouseEnterEvent>(delegate(MouseEnterEvent _)
			{
				this.SetHover(true);
			}, 0);
			this._button.RegisterCallback<MouseOutEvent>(delegate(MouseOutEvent _)
			{
				this.SetHover(false);
			}, 0);
			this._button.style.backgroundImage = new StyleBackground(this._decalService.GetDecalTexture(this._decal));
			this._frame = UQueryExtensions.Q<VisualElement>(this.Root, "Frame", null);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000218D File Offset: 0x0000038D
		public void Show(DecalSupplier supplier)
		{
			this._decalSupplier = supplier;
			this._decalSupplier.ActiveDecalChanged += this.OnActiveDecalChanged;
			this._isHovered = false;
			this.UpdateContent();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021BA File Offset: 0x000003BA
		public void Clear()
		{
			if (this._decalSupplier)
			{
				this._decalSupplier.ActiveDecalChanged -= this.OnActiveDecalChanged;
				this._decalSupplier = null;
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021E7 File Offset: 0x000003E7
		public void OnActiveDecalChanged(object sender, EventArgs e)
		{
			this.UpdateContent();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021EF File Offset: 0x000003EF
		public void SetHover(bool hover)
		{
			this._isHovered = hover;
			this.UpdateContent();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002200 File Offset: 0x00000400
		public void UpdateContent()
		{
			bool flag = this._decalSupplier && this._decal.Equals(this._decalSupplier.ActiveDecal);
			this._frame.ToggleDisplayStyle(this._isHovered || flag);
			this._frame.EnableInClassList(DecalButton.FrameFadeClass, this._isHovered && !flag);
		}

		// Token: 0x04000006 RID: 6
		public static readonly string FrameFadeClass = "decal-button__frame--fade";

		// Token: 0x04000008 RID: 8
		public readonly IDecalService _decalService;

		// Token: 0x04000009 RID: 9
		public readonly Decal _decal;

		// Token: 0x0400000A RID: 10
		public Button _button;

		// Token: 0x0400000B RID: 11
		public VisualElement _frame;

		// Token: 0x0400000C RID: 12
		public DecalSupplier _decalSupplier;

		// Token: 0x0400000D RID: 13
		public bool _isHovered;
	}
}
