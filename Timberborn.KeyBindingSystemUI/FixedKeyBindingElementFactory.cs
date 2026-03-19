using System;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.KeyBindingSystemUI
{
	// Token: 0x02000004 RID: 4
	public class FixedKeyBindingElementFactory
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public FixedKeyBindingElementFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D0 File Offset: 0x000002D0
		public VisualElement Create(string keyBindingId)
		{
			string[] array = keyBindingId.Split('|', StringSplitOptions.None);
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/FixedKeyBinding");
			visualElement.AddToClassList(FixedKeyBindingElementFactory.MouseButtonToClass(array[0]));
			visualElement.AddToClassList(FixedKeyBindingElementFactory.DirectionToClass(array[1]));
			return visualElement;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002114 File Offset: 0x00000314
		public static string MouseButtonToClass(string mouseButton)
		{
			string result;
			if (!(mouseButton == "MouseLeft"))
			{
				if (!(mouseButton == "MouseRight"))
				{
					if (!(mouseButton == "MouseMiddle"))
					{
						if (!(mouseButton == "MouseZoom"))
						{
							throw new ArgumentOutOfRangeException("mouseButton", mouseButton, null);
						}
						result = "mouse-zoom";
					}
					else
					{
						result = "mouse-middle";
					}
				}
				else
				{
					result = "mouse-right";
				}
			}
			else
			{
				result = "mouse-left";
			}
			return result;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002188 File Offset: 0x00000388
		public static string DirectionToClass(string direction)
		{
			string result;
			if (!(direction == "Up"))
			{
				if (!(direction == "Down"))
				{
					if (!(direction == "Left"))
					{
						if (!(direction == "Right"))
						{
							if (!(direction == "ScrollUp"))
							{
								if (!(direction == "ScrollDown"))
								{
									throw new ArgumentOutOfRangeException("direction", direction, null);
								}
								result = "scroll-down";
							}
							else
							{
								result = "scroll-up";
							}
						}
						else
						{
							result = "right";
						}
					}
					else
					{
						result = "left";
					}
				}
				else
				{
					result = "down";
				}
			}
			else
			{
				result = "up";
			}
			return result;
		}

		// Token: 0x04000006 RID: 6
		public readonly VisualElementLoader _visualElementLoader;
	}
}
