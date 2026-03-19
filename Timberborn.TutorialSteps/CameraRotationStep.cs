using System;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000017 RID: 23
	public class CameraRotationStep : ITutorialStep
	{
		// Token: 0x06000098 RID: 152 RVA: 0x000034D5 File Offset: 0x000016D5
		public CameraRotationStep(CameraMovementService cameraMovementService, RotationDirection direction, float angle, string description)
		{
			this._cameraMovementService = cameraMovementService;
			this._direction = direction;
			this._angle = angle;
			this._description = description;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000034FA File Offset: 0x000016FA
		public string Description()
		{
			return this._description;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003504 File Offset: 0x00001704
		public bool Achieved()
		{
			RotationDirection direction = this._direction;
			bool result;
			if (direction != RotationDirection.Left)
			{
				if (direction != RotationDirection.Right)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = (this._cameraMovementService.RightRotation > this._angle);
			}
			else
			{
				result = (this._cameraMovementService.LeftRotation > this._angle);
			}
			return result;
		}

		// Token: 0x04000046 RID: 70
		public readonly CameraMovementService _cameraMovementService;

		// Token: 0x04000047 RID: 71
		public readonly RotationDirection _direction;

		// Token: 0x04000048 RID: 72
		public readonly float _angle;

		// Token: 0x04000049 RID: 73
		public readonly string _description;
	}
}
