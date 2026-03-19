using System;
using System.Globalization;

namespace Timberborn.Persistence
{
	// Token: 0x02000004 RID: 4
	public static class CommonNumberSerializer
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020C0 File Offset: 0x000002C0
		public static string SerializeInt(int value)
		{
			string result;
			switch (value)
			{
			case 0:
				result = "0";
				break;
			case 1:
				result = "1";
				break;
			case 2:
				result = "2";
				break;
			case 3:
				result = "3";
				break;
			case 4:
				result = "4";
				break;
			case 5:
				result = "5";
				break;
			case 6:
				result = "6";
				break;
			case 7:
				result = "7";
				break;
			case 8:
				result = "8";
				break;
			case 9:
				result = "9";
				break;
			case 10:
				result = "10";
				break;
			case 11:
				result = "11";
				break;
			case 12:
				result = "12";
				break;
			case 13:
				result = "13";
				break;
			case 14:
				result = "14";
				break;
			case 15:
				result = "15";
				break;
			case 16:
				result = "16";
				break;
			default:
				result = value.ToString(CultureInfo.InvariantCulture);
				break;
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021B8 File Offset: 0x000003B8
		public static string SerializeFloat(float value)
		{
			if (value <= 7f)
			{
				if (value <= 3f)
				{
					if (value <= 1f)
					{
						if (value == 0f)
						{
							return "0";
						}
						if (value == 1f)
						{
							return "1";
						}
					}
					else
					{
						if (value == 2f)
						{
							return "2";
						}
						if (value == 3f)
						{
							return "3";
						}
					}
				}
				else if (value <= 5f)
				{
					if (value == 4f)
					{
						return "4";
					}
					if (value == 5f)
					{
						return "5";
					}
				}
				else
				{
					if (value == 6f)
					{
						return "6";
					}
					if (value == 7f)
					{
						return "7";
					}
				}
			}
			else if (value <= 11f)
			{
				if (value <= 9f)
				{
					if (value == 8f)
					{
						return "8";
					}
					if (value == 9f)
					{
						return "9";
					}
				}
				else
				{
					if (value == 10f)
					{
						return "10";
					}
					if (value == 11f)
					{
						return "11";
					}
				}
			}
			else if (value <= 13f)
			{
				if (value == 12f)
				{
					return "12";
				}
				if (value == 13f)
				{
					return "13";
				}
			}
			else
			{
				if (value == 14f)
				{
					return "14";
				}
				if (value == 15f)
				{
					return "15";
				}
				if (value == 16f)
				{
					return "16";
				}
			}
			return value.ToString(CultureInfo.InvariantCulture);
		}
	}
}
