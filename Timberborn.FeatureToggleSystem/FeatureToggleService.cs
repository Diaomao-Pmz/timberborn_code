using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Timberborn.CommandLine;
using UnityEngine;

namespace Timberborn.FeatureToggleSystem
{
	// Token: 0x02000006 RID: 6
	public static class FeatureToggleService
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020CC File Offset: 0x000002CC
		public static void InitializeToggles()
		{
			IEnumerable<FieldInfo> toggleFields = FeatureToggleService.GetToggleFields();
			List<string> list = new List<string>();
			foreach (FieldInfo fieldInfo in toggleFields)
			{
				string name = fieldInfo.Name;
				bool toggleState = FeatureToggleService.GetToggleState(name);
				fieldInfo.SetValue(null, toggleState);
				if (toggleState)
				{
					list.Add(name);
				}
			}
			if (list.Count > 0)
			{
				Debug.LogWarning("Active features: " + string.Join(", ", list));
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002160 File Offset: 0x00000360
		public static IEnumerable<string> GetToggleNames()
		{
			return from fieldInfo in FeatureToggleService.GetToggleFields()
			select fieldInfo.Name;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000218B File Offset: 0x0000038B
		public static bool IsToggleOn(string toggleName)
		{
			bool flag = !string.IsNullOrEmpty(toggleName);
			if (flag && !FeatureToggleService.GetToggleNames().Contains(toggleName))
			{
				throw new ArgumentException("There is no FeatureToggles with name " + toggleName);
			}
			return !flag || FeatureToggleService.GetToggleState(toggleName);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021C1 File Offset: 0x000003C1
		public static IEnumerable<FieldInfo> GetToggleFields()
		{
			return from fieldInfo in typeof(FeatureToggles).GetFields()
			where fieldInfo.IsPublic && fieldInfo.FieldType == typeof(bool)
			select fieldInfo;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021F6 File Offset: 0x000003F6
		public static bool GetToggleState(string toggleName)
		{
			if (!Application.isEditor)
			{
				return FeatureToggleService.GetToggleStateFromCommandLine(toggleName);
			}
			return FeatureToggleService.GetToggleStateFromEditorPrefs(toggleName);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000220C File Offset: 0x0000040C
		public static bool GetToggleStateFromEditorPrefs(string toggleName)
		{
			return EditorFeatureToggler.GetToggleState(toggleName);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002214 File Offset: 0x00000414
		public static bool GetToggleStateFromCommandLine(string toggleName)
		{
			return CommandLineArguments.CreateWithCommandLineArgs().Has(FeatureToggleService.CommandLinePrefix + toggleName);
		}

		// Token: 0x04000007 RID: 7
		public static readonly string CommandLinePrefix = "feature-";
	}
}
