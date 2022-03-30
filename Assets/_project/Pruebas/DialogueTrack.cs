using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

[TrackBindingType(typeof(DialogueManager))]
[TrackClipType(typeof(DialogueClip))]
[TrackClipType(typeof(SkipDialogueClip))]
public class DialogueTrack : TrackAsset
{

}
