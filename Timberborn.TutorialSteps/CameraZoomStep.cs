using System;
using Timberborn.TutorialSystem;

namespace Timberborn.TutorialSteps
{
	// Token: 0x0200001A RID: 26
	public class CameraZoomStep : ITutorialStep
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x0000386D File Offset: 0x00001A6D
		public CameraZoomStep(CameraMovementService cameraMovementService, ZoomDirection direction, float threshold, string description)
		{
			this._cameraMovementService = cameraMovementService;
			this._direction = direction;
			this._threshold = threshold;
			this._description = description;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003892 File Offset: 0x00001A92
		public string Description()
		{
			return this._description;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000389C File Offset: 0x00001A9C
		public bool Achieved()
		{
			ZoomDirection direction = this._direction;
			bool result;
			if (direction != ZoomDirection.In)
			{
				if (direction != ZoomDirection.Out)
				{
					throw new ArgumentOutOfRangeException();
				}
				result = (this._cameraMovementService.ZoomOut > this._threshold);
			}
			else
			{
				result = (this._cameraMovementService.ZoomIn > this._threshold);
			}
			return result;
		}

		// Token: 0x0400004F RID: 79
		public readonly CameraMovementService _cameraMovementService;

		// Token: 0x04000050 RID: 80
		public readonly ZoomDirection _direction;

		// Token: 0x04000051 RID: 81
		public readonly float _threshold;

		// Token: 0x04000052 RID: 82
		public readonly string _description;
	}
}
