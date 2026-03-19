using System;
using Timberborn.AutomationBuildings;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.DropdownSystem;
using Timberborn.EntityPanelSystem;
using Timberborn.PlatformUtilities;
using UnityEngine.UIElements;

namespace Timberborn.AutomationBuildingsUI
{
	// Token: 0x0200002B RID: 43
	public class SpeakerFragment : IEntityPanelFragment
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00006134 File Offset: 0x00004334
		public SpeakerFragment(VisualElementLoader visualElementLoader, RadioToggleFactory radioToggleFactory, SpeakerSoundDropdownProvider speakerSoundDropdownProvider, DropdownItemsSetter dropdownItemsSetter, SpeakerSoundService speakerSoundService, IExplorerOpener explorerOpener)
		{
			this._visualElementLoader = visualElementLoader;
			this._radioToggleFactory = radioToggleFactory;
			this._speakerSoundDropdownProvider = speakerSoundDropdownProvider;
			this._dropdownItemsSetter = dropdownItemsSetter;
			this._speakerSoundService = speakerSoundService;
			this._explorerOpener = explorerOpener;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000616C File Offset: 0x0000436C
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/SpeakerFragment");
			this._playbackModeRadioToggle = this._radioToggleFactory.CreateLocalizable<SpeakerPlaybackMode>(SpeakerFragment.ModeLocKeyPrefix, UQueryExtensions.Q<VisualElement>(this._root, "PlaybackModeWrapper", null));
			this._playbackModeRadioToggle.RadioButtonSelected += this.OnPlaybackModeChanged;
			this._spatialModeRadioToggle = this._radioToggleFactory.CreateLocalizable<SpeakerSpatialMode>(SpeakerFragment.SpatialModeLocKeyPrefix, UQueryExtensions.Q<VisualElement>(this._root, "SpatialModeWrapper", null));
			this._spatialModeRadioToggle.RadioButtonSelected += this.OnSpatialModeChanged;
			this._soundId = UQueryExtensions.Q<Dropdown>(this._root, "SoundId", null);
			UQueryExtensions.Q<Button>(this._root, "BrowseButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnBrowseButtonClicked), 0);
			UQueryExtensions.Q<Button>(this._root, "RefreshButton", null).RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnRefreshButtonClicked), 0);
			this._root.ToggleDisplayStyle(false);
			return this._root;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000627A File Offset: 0x0000447A
		public void ShowFragment(BaseComponent entity)
		{
			if (entity.TryGetComponent<Speaker>(out this._speaker))
			{
				this._speakerSoundDropdownProvider.SetSpeaker(this._speaker);
				this._root.ToggleDisplayStyle(true);
				this.UpdateDropdown();
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000062AD File Offset: 0x000044AD
		public void UpdateFragment()
		{
			if (this._speaker)
			{
				this._playbackModeRadioToggle.Update((int)this._speaker.PlaybackMode);
				this._spatialModeRadioToggle.Update((int)this._speaker.SpatialMode);
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000062E8 File Offset: 0x000044E8
		public void ClearFragment()
		{
			this._speakerSoundDropdownProvider.ClearSpeaker();
			this._root.ToggleDisplayStyle(false);
			this._speaker = null;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00006308 File Offset: 0x00004508
		public void OnPlaybackModeChanged(object sender, int index)
		{
			this._speaker.SetPlaybackMode((SpeakerPlaybackMode)index);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00006316 File Offset: 0x00004516
		public void OnSpatialModeChanged(object sender, int index)
		{
			this._speaker.SetSpatialMode((SpeakerSpatialMode)index);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006324 File Offset: 0x00004524
		public void UpdateDropdown()
		{
			this._speakerSoundDropdownProvider.UpdateSounds();
			this._dropdownItemsSetter.SetItems(this._soundId, this._speakerSoundDropdownProvider);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006348 File Offset: 0x00004548
		public void OnBrowseButtonClicked(ClickEvent evt)
		{
			this._explorerOpener.OpenDirectory(this._speakerSoundService.GetCustomSoundDirectory());
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006360 File Offset: 0x00004560
		public void OnRefreshButtonClicked(ClickEvent evt)
		{
			this._speakerSoundService.ReloadCustomSounds();
			this.UpdateDropdown();
		}

		// Token: 0x04000137 RID: 311
		public static readonly string ModeLocKeyPrefix = "Building.Speaker.PlaybackMode.";

		// Token: 0x04000138 RID: 312
		public static readonly string SpatialModeLocKeyPrefix = "Building.Speaker.SpatialMode.";

		// Token: 0x04000139 RID: 313
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x0400013A RID: 314
		public readonly RadioToggleFactory _radioToggleFactory;

		// Token: 0x0400013B RID: 315
		public readonly SpeakerSoundDropdownProvider _speakerSoundDropdownProvider;

		// Token: 0x0400013C RID: 316
		public readonly DropdownItemsSetter _dropdownItemsSetter;

		// Token: 0x0400013D RID: 317
		public readonly SpeakerSoundService _speakerSoundService;

		// Token: 0x0400013E RID: 318
		public readonly IExplorerOpener _explorerOpener;

		// Token: 0x0400013F RID: 319
		public VisualElement _root;

		// Token: 0x04000140 RID: 320
		public RadioToggle _playbackModeRadioToggle;

		// Token: 0x04000141 RID: 321
		public RadioToggle _spatialModeRadioToggle;

		// Token: 0x04000142 RID: 322
		public Dropdown _soundId;

		// Token: 0x04000143 RID: 323
		public Speaker _speaker;
	}
}
