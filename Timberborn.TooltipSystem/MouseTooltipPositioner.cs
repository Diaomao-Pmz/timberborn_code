using System;
using Timberborn.InputSystem;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.UIElements;

namespace Timberborn.TooltipSystem
{
	// Token: 0x02000005 RID: 5
	public class MouseTooltipPositioner
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000020BE File Offset: 0x000002BE
		public MouseTooltipPositioner(CursorService cursorService, InputService inputService)
		{
			this._cursorService = cursorService;
			this._inputService = inputService;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020D4 File Offset: 0x000002D4
		public void UpdatePosition(VisualElement visualElement)
		{
			Vector2 mousePositionNdc = this._inputService.MousePositionNdc;
			Vector2 vector = this.CalculateCursorOffset();
			visualElement.style.left = MouseTooltipPositioner.CalculateHorizontalPosition(visualElement, mousePositionNdc.x, vector.x);
			visualElement.style.top = MouseTooltipPositioner.CalculateVerticalPosition(visualElement, mousePositionNdc.y, vector.y);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002138 File Offset: 0x00000338
		public Vector2 CalculateCursorOffset()
		{
			Resolution currentResolution = Screen.currentResolution;
			float num = (float)Screen.width / (float)currentResolution.width;
			float num2 = (float)Screen.height / (float)currentResolution.height;
			Vector2 cursorOffset = this._cursorService.CursorOffset;
			cursorOffset.y += (float)(Application.isEditor ? MouseTooltipPositioner.EditorTooltipOffset : 0);
			cursorOffset.x /= num;
			cursorOffset.y /= num2;
			return cursorOffset;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000021AC File Offset: 0x000003AC
		public static float CalculateHorizontalPosition(VisualElement visualElement, float mousePosition, float horizontalOffset)
		{
			float width = visualElement.parent.resolvedStyle.width;
			float width2 = visualElement.resolvedStyle.width;
			float num = mousePosition * width + horizontalOffset;
			if (num + width2 + horizontalOffset <= width)
			{
				return num;
			}
			return width - width2;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021EC File Offset: 0x000003EC
		public static float CalculateVerticalPosition(VisualElement visualElement, float mousePosition, float verticalOffset)
		{
			float height = visualElement.parent.resolvedStyle.height;
			float height2 = visualElement.resolvedStyle.height;
			return Math.Min((1f - mousePosition) * height + verticalOffset, height - height2);
		}

		// Token: 0x04000006 RID: 6
		public static readonly int EditorTooltipOffset = 10;

		// Token: 0x04000007 RID: 7
		public readonly CursorService _cursorService;

		// Token: 0x04000008 RID: 8
		public readonly InputService _inputService;
	}
}
