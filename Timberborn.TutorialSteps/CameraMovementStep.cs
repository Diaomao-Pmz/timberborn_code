using System;
using Timberborn.Coordinates;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x02000014 RID: 20
	public class CameraMovementStep : ITutorialStep
	{
		// Token: 0x0600007E RID: 126 RVA: 0x0000305A File Offset: 0x0000125A
		public CameraMovementStep(CameraMovementService cameraMovementService, Direction2D direction, float threshold, string description)
		{
			this._cameraMovementService = cameraMovementService;
			this._direction = direction;
			this._threshold = threshold;
			this._description = description;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x0000307F File Offset: 0x0000127F
		public string Description()
		{
			return this._description;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003088 File Offset: 0x00001288
		public bool Achieved()
		{
			bool result;
			switch (this._direction)
			{
			case Direction2D.Down:
				result = (this._cameraMovementService.DownMovement > this._threshold);
				break;
			case Direction2D.Left:
				result = (this._cameraMovementService.LeftMovement > this._threshold);
				break;
			case Direction2D.Up:
				result = (this._cameraMovementService.UpMovement > this._threshold);
				break;
			case Direction2D.Right:
				result = (this._cameraMovementService.RightMovement > this._threshold);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return result;
		}

		// Token: 0x0400003C RID: 60
		public readonly CameraMovementService _cameraMovementService;

		// Token: 0x0400003D RID: 61
		public readonly Direction2D _direction;

		// Token: 0x0400003E RID: 62
		public readonly float _threshold;

		// Token: 0x0400003F RID: 63
		public readonly string _description;
	}
}
