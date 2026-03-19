using System;
using UnityEngine;

namespace Timberborn.CameraSystem
{
	// Token: 0x0200001B RID: 27
	public interface ICameraAnchorPicker
	{
		// Token: 0x060000F6 RID: 246
		Vector3? PickAnchorPoint(Ray ray);
	}
}
