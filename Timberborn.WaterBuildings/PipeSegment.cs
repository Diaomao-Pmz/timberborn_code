using System;
using Timberborn.Common;
using UnityEngine;

namespace Timberborn.WaterBuildings
{
	// Token: 0x0200001B RID: 27
	public class PipeSegment
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00003C9A File Offset: 0x00001E9A
		public GameObject Root { get; }

		// Token: 0x060000F4 RID: 244 RVA: 0x00003CA2 File Offset: 0x00001EA2
		public PipeSegment(GameObject root, GameObject middle, GameObject end)
		{
			this.Root = root;
			this._middle = middle;
			this._end = end;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00003CC0 File Offset: 0x00001EC0
		public static PipeSegment Create(GameObject root, float rotationAngle)
		{
			GameObject middle = root.FindChild(PipeSegment.MiddleParentName);
			GameObject end = root.FindChild(PipeSegment.EndParentName);
			root.transform.localRotation = Quaternion.Euler(0f, rotationAngle, 0f);
			root.name = "PipeSegment";
			return new PipeSegment(root, middle, end);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00003D13 File Offset: 0x00001F13
		public void ShowMiddle(Vector3 position)
		{
			this.Show(position, true);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00003D1D File Offset: 0x00001F1D
		public void ShowEnd(Vector3 position)
		{
			this.Show(position, false);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00003D27 File Offset: 0x00001F27
		public void Hide()
		{
			this.Root.SetActive(false);
			this._middle.SetActive(false);
			this._end.SetActive(false);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003D4D File Offset: 0x00001F4D
		public void Show(Vector3 position, bool middle)
		{
			this.Root.transform.position = position;
			this.Root.SetActive(true);
			this._middle.SetActive(middle);
			this._end.SetActive(!middle);
		}

		// Token: 0x0400004C RID: 76
		public static readonly string MiddleParentName = "#Middle";

		// Token: 0x0400004D RID: 77
		public static readonly string EndParentName = "#End";

		// Token: 0x0400004F RID: 79
		public readonly GameObject _middle;

		// Token: 0x04000050 RID: 80
		public readonly GameObject _end;
	}
}
