using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Playables;

public class SubtitleClip : PlayableAsset
{
   public string stringKey;
   public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
   {
      var playable = ScriptPlayable<SubtitleBehaviour>.Create(graph);
      SubtitleBehaviour subtitleBehaviour = playable.GetBehaviour();
      subtitleBehaviour.subtitleText = LocalizationSettings.StringDatabase.GetLocalizedString("Prologue", stringKey);
      return playable;
   }
}
