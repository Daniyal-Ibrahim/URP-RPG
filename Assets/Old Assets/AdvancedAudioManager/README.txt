Advanced Audio Manager:

How to use AdvancedAudioManager component:

1. Add the AdvancedAudioManager script to a GameObject.

2. Set the Sounds array size to however many sounds you want to add.

3. On each of the new objects in the array, assign a name and an audio clip, optionally you can toggle the sounds settings as well.

4. To play a sound from another script, reference the AdvancedAudioManager component in your scene and in your script run this line of code "AdvancedAudioManager.PlaySound("sound name")"

5. To stop a sound from another script, reference the AdvancedAudioManager component in your scene and in your script run this line of code "AdvancedAudioManager.StopSound("sound name")"

How to use the AudioTrigger component:

1. Add the AudioTrigger component and a 2D or 3D collider with the isTrigger option checked to a GameObject.

2. Assign the name of the sound you want to play in the AudioTrigger component.

3. Add as many tags as you want to the array of tags to trigger sounds on the AudioTrigger component.

4. Optionally, you can set a delay on playing the sound using the delay value on the AudioTrigger component.

How to use the UIAudio component:

1. Add the UIAudio component to a UI object.

2. Set the names of the sounds you want to play for each event stated, leave blank for none.

