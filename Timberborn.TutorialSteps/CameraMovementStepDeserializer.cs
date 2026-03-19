using System;
using Timberborn.BlueprintSystem;
using Timberborn.Coordinates;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000015 RID: 21
	public class CameraMovementStepDeserializer : IStepDeserializer
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003113 File Offset: 0x00001313
		public CameraMovementStepDeserializer(CameraMovementService cameraMovementService, ILoc loc, InputService inputService, InputSettings inputSettings)
		{
			this._cameraMovementService = cameraMovementService;
			this._loc = loc;
			this._inputService = inputService;
			this._inputSettings = inputSettings;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003138 File Offset: 0x00001338
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			CameraMovementStepSpec cameraMovementStepSpec = step.Specs[0] as CameraMovementStepSpec;
			if (cameraMovementStepSpec != null)
			{
				tutorialStep = this.Create(cameraMovementStepSpec.Direction, cameraMovementStepSpec.Threshold);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003178 File Offset: 0x00001378
		public TutorialStep Create(Direction2D direction, float threshold)
		{
			ITutorialStep step = new CameraMovementStep(this._cameraMovementService, direction, threshold, this._loc.T(CameraMovementStepDeserializer.GetLocKey(direction)));
			string mouseMoveCameraKey = this._inputService.MouseMoveCameraKey;
			return TutorialStep.Create(step, CameraMovementStepDeserializer.GetKeyBindingKey(direction), this.GetFixedKeyBindingKey(direction, mouseMoveCameraKey));
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000031C4 File Offset: 0x000013C4
		public static string GetLocKey(Direction2D direction)
		{
			string result;
			switch (direction)
			{
			case Direction2D.Down:
				result = "Tutorial.Basics.MoveCameraDown";
				break;
			case Direction2D.Left:
				result = "Tutorial.Basics.MoveCameraLeft";
				break;
			case Direction2D.Up:
				result = "Tutorial.Basics.MoveCameraUp";
				break;
			case Direction2D.Right:
				result = "Tutorial.Basics.MoveCameraRight";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003210 File Offset: 0x00001410
		public static string GetKeyBindingKey(Direction2D direction)
		{
			string result;
			switch (direction)
			{
			case Direction2D.Down:
				result = "MoveCameraDown";
				break;
			case Direction2D.Left:
				result = "MoveCameraLeft";
				break;
			case Direction2D.Up:
				result = "MoveCameraUp";
				break;
			case Direction2D.Right:
				result = "MoveCameraRight";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000325C File Offset: 0x0000145C
		public string GetFixedKeyBindingKey(Direction2D direction, string button)
		{
			return button + "|" + this.GetFixedKeyBindingDirectionKey(direction);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003270 File Offset: 0x00001470
		public string GetFixedKeyBindingDirectionKey(Direction2D direction)
		{
			string result;
			switch (direction)
			{
			case Direction2D.Down:
				result = (this._inputSettings.DragCamera ? "Down" : "Up");
				break;
			case Direction2D.Left:
				result = (this._inputSettings.DragCamera ? "Left" : "Right");
				break;
			case Direction2D.Up:
				result = (this._inputSettings.DragCamera ? "Up" : "Down");
				break;
			case Direction2D.Right:
				result = (this._inputSettings.DragCamera ? "Right" : "Left");
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		// Token: 0x04000040 RID: 64
		public readonly CameraMovementService _cameraMovementService;

		// Token: 0x04000041 RID: 65
		public readonly ILoc _loc;

		// Token: 0x04000042 RID: 66
		public readonly InputService _inputService;

		// Token: 0x04000043 RID: 67
		public readonly InputSettings _inputSettings;
	}
}
