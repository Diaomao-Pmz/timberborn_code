using System;
using Timberborn.CameraSystem;
using Timberborn.CoreUI;
using Timberborn.Localization;
using Timberborn.SingletonSystem;
using Timberborn.UIFormatters;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x02000010 RID: 16
	public class WaterPoweredGeneratorPreviewPanel : ILoadableSingleton
	{
		// Token: 0x06000045 RID: 69 RVA: 0x0000287C File Offset: 0x00000A7C
		public WaterPoweredGeneratorPreviewPanel(VisualElementLoader visualElementLoader, CameraService cameraService, Underlay underlay, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._cameraService = cameraService;
			this._underlay = underlay;
			this._loc = loc;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028C8 File Offset: 0x00000AC8
		public void Load()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/WaterPoweredGeneratorPreviewPanel");
			this._outputPower = UQueryExtensions.Q<Label>(this._root, "Text", null);
			this._underlay.Add(this._root);
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002920 File Offset: 0x00000B20
		public void ShowPreview(int powerOutput, Vector3 position)
		{
			if (!this._isVisible)
			{
				this._root.ToggleDisplayStyle(true);
				this._underlay.Add(this._root);
				this._isVisible = true;
			}
			this._outputPower.text = this._loc.T<int>(this._outputPowerPhrase, powerOutput);
			this._root.EnableInClassList(WaterPoweredGeneratorPreviewPanel.PowerClass, powerOutput > 0);
			this._root.EnableInClassList(WaterPoweredGeneratorPreviewPanel.NoPowerClass, powerOutput == 0);
			this.UpdateRootPosition(position);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000029A5 File Offset: 0x00000BA5
		public void HidePreview()
		{
			if (this._isVisible)
			{
				this._root.ToggleDisplayStyle(false);
				this._underlay.Remove(this._root);
				this._isVisible = false;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000029D4 File Offset: 0x00000BD4
		public void UpdateRootPosition(Vector3 position)
		{
			if (this._root.panel != null)
			{
				bool flag = this._cameraService.IsInFront(position);
				this._root.ToggleDisplayStyle(flag);
				if (flag)
				{
					VisualElement root = this._underlay.Root;
					Vector3 vector = this._cameraService.WorldSpaceToPanelSpace(root, position);
					this._root.style.translate = new Vector2(vector.x - root.layout.width / 2f, vector.y - root.layout.height / 2f);
				}
			}
		}

		// Token: 0x0400001F RID: 31
		public static readonly string PowerClass = "square-large--brown";

		// Token: 0x04000020 RID: 32
		public static readonly string NoPowerClass = "square-large--light-red";

		// Token: 0x04000021 RID: 33
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000022 RID: 34
		public readonly CameraService _cameraService;

		// Token: 0x04000023 RID: 35
		public readonly Underlay _underlay;

		// Token: 0x04000024 RID: 36
		public readonly ILoc _loc;

		// Token: 0x04000025 RID: 37
		public VisualElement _root;

		// Token: 0x04000026 RID: 38
		public Label _outputPower;

		// Token: 0x04000027 RID: 39
		public bool _isVisible;

		// Token: 0x04000028 RID: 40
		public readonly Phrase _outputPowerPhrase = Phrase.New().Format<int>(new Func<int, ILoc, string>(UnitFormatter.FormatPower));
	}
}
