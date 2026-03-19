using System;
using Timberborn.BlueprintSystem;
using Timberborn.InputSystem;
using Timberborn.Localization;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000018 RID: 24
	public class CameraRotationStepDeserializer : IStepDeserializer
	{
		// Token: 0x0600009B RID: 155 RVA: 0x00003554 File Offset: 0x00001754
		public CameraRotationStepDeserializer(CameraMovementService cameraMovementService, ILoc loc, InputService inputService)
		{
			this._cameraMovementService = cameraMovementService;
			this._loc = loc;
			this._inputService = inputService;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003574 File Offset: 0x00001774
		public bool TryDeserialize(Blueprint step, out TutorialStep tutorialStep)
		{
			CameraRotationStepSpec cameraRotationStepSpec = step.Specs[0] as CameraRotationStepSpec;
			if (cameraRotationStepSpec != null)
			{
				tutorialStep = this.Create(cameraRotationStepSpec.Direction, cameraRotationStepSpec.Angle);
				return true;
			}
			tutorialStep = null;
			return false;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000035B4 File Offset: 0x000017B4
		public TutorialStep Create(RotationDirection direction, float angle)
		{
			ITutorialStep step = new CameraRotationStep(this._cameraMovementService, direction, angle, this.GetDescription(direction));
			string mouseRotateCameraKey = this._inputService.MouseRotateCameraKey;
			return TutorialStep.Create(step, CameraRotationStepDeserializer.GetKeyBindingKey(direction), CameraRotationStepDeserializer.GetFixedKeyBindingKey(direction, mouseRotateCameraKey));
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000035F3 File Offset: 0x000017F3
		public string GetDescription(RotationDirection direction)
		{
			return this._loc.T(CameraRotationStepDeserializer.GetLocKey(direction));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003608 File Offset: 0x00001808
		public static string GetLocKey(RotationDirection direction)
		{
			string result;
			if (direction != RotationDirection.Left)
			{
				if (direction != RotationDirection.Right)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = "Tutorial.Basics.RotateCameraRight";
			}
			else
			{
				result = "Tutorial.Basics.RotateCameraLeft";
			}
			return result;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003638 File Offset: 0x00001838
		public static string GetKeyBindingKey(RotationDirection direction)
		{
			string result;
			if (direction != RotationDirection.Left)
			{
				if (direction != RotationDirection.Right)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = "RotateCameraRight";
			}
			else
			{
				result = "RotateCameraLeft";
			}
			return result;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003665 File Offset: 0x00001865
		public static string GetFixedKeyBindingKey(RotationDirection direction, string button)
		{
			return button + "|" + CameraRotationStepDeserializer.GetFixedKeyBindingDirectionKey(direction);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003678 File Offset: 0x00001878
		public static string GetFixedKeyBindingDirectionKey(RotationDirection direction)
		{
			string result;
			if (direction != RotationDirection.Left)
			{
				if (direction != RotationDirection.Right)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = "Left";
			}
			else
			{
				result = "Right";
			}
			return result;
		}

		// Token: 0x0400004A RID: 74
		public readonly CameraMovementService _cameraMovementService;

		// Token: 0x0400004B RID: 75
		public readonly ILoc _loc;

		// Token: 0x0400004C RID: 76
		public readonly InputService _inputService;
	}
}
