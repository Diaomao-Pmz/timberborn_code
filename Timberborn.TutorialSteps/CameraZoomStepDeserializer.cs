using System;
using Timberborn.BlueprintSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.PlatformUtilities;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200001B RID: 27
	public class CameraZoomStepDeserializer : IStepDeserializer
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000038EC File Offset: 0x00001AEC
		public CameraZoomStepDeserializer(CameraMovementService cameraMovementService, ILoc loc, InputSettings inputSettings)
		{
			this._cameraMovementService = cameraMovementService;
			this._loc = loc;
			this._inputSettings = inputSettings;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000390C File Offset: 0x00001B0C
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			CameraZoomStepSpec cameraZoomStepSpec = step.Specs[0] as CameraZoomStepSpec;
			if (cameraZoomStepSpec != null)
			{
				tutorialStep = this.Create(cameraZoomStepSpec.Direction, cameraZoomStepSpec.Threshold);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000394B File Offset: 0x00001B4B
		public TutorialStep Create(ZoomDirection direction, float threshold)
		{
			return TutorialStep.Create(new CameraZoomStep(this._cameraMovementService, direction, threshold, this.GetDescription(direction)), CameraZoomStepDeserializer.GetKeyBindingKey(direction), this.GetFixedKeyBindingKey(direction, "MouseZoom"));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003978 File Offset: 0x00001B78
		public string GetDescription(ZoomDirection direction)
		{
			return this._loc.T(CameraZoomStepDeserializer.GetLocKey(direction));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000398C File Offset: 0x00001B8C
		public static string GetLocKey(ZoomDirection direction)
		{
			string result;
			if (direction != ZoomDirection.In)
			{
				if (direction != ZoomDirection.Out)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = "Tutorial.Basics.ZoomCameraOut";
			}
			else
			{
				result = "Tutorial.Basics.ZoomCameraIn";
			}
			return result;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000039BC File Offset: 0x00001BBC
		public static string GetKeyBindingKey(ZoomDirection direction)
		{
			string result;
			if (direction != ZoomDirection.In)
			{
				if (direction != ZoomDirection.Out)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = "ZoomOut";
			}
			else
			{
				result = "ZoomIn";
			}
			return result;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x000039E9 File Offset: 0x00001BE9
		public string GetFixedKeyBindingKey(ZoomDirection direction, string button)
		{
			return button + "|" + this.GetFixedKeyBindingDirectionKey(direction);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003A00 File Offset: 0x00001C00
		public string GetFixedKeyBindingDirectionKey(ZoomDirection direction)
		{
			bool flag = ApplicationPlatform.IsMacOS();
			bool invertZoom = this._inputSettings.InvertZoom;
			bool flag2 = (invertZoom && !flag) || (!invertZoom && flag);
			string result;
			if (direction != ZoomDirection.In)
			{
				if (direction != ZoomDirection.Out)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = (flag2 ? "ScrollUp" : "ScrollDown");
			}
			else
			{
				result = (flag2 ? "ScrollDown" : "ScrollUp");
			}
			return result;
		}

		// Token: 0x04000053 RID: 83
		public readonly CameraMovementService _cameraMovementService;

		// Token: 0x04000054 RID: 84
		public readonly ILoc _loc;

		// Token: 0x04000055 RID: 85
		public readonly InputSettings _inputSettings;
	}
}
