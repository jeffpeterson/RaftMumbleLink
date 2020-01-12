# Raft Mumble Link

Enables [positional voice audio](https://wiki.mumble.info/wiki/Positional-Audio) for [Raft](https://raft-game.com/) via Mumble.

> Positional Audio is a feature of Mumble that places the people talking to you in a certain position relative to your own depending on their actual position in the game.
> This way you can hear the person like their actual avatar in-game would be talking to you.
>  
> If someone is NOT using Positional Audio, you would hear that audio source always from the same spot, equally loud from each of your stereo speakers.

## Why would I want this mod?

I'm sure you're already using Discord to chat with other players. But, positional voice audio doesn't only solve the problem of crowded voice channels; it makes the game more fun and immersive:

* Navigation: Shout, "Over here!", and it actually means something. Never again lose your friends on a large island because you looked at your inventory for a few seconds.
* Immersion: The characters are talking, not voices in your head.
* Roleplay: Mutter to yourself while tending the crops. Whisper to those close to you. Scream for help. Become your character.
* Story-telling: Split up your group and regale each other with tales of adventure.

Sure, you have to install Mumble and find a server like we're re-living the '90s. It'll be worth it.

## Setup

### Mumble Setup

1. Download [Mumble](https://www.mumble.info/downloads/).
2. Run the installer and complete the audio configuration wizard.
3. Click the Configuration gear icon at the top of the Mumble window.
4. Navigate to "Plugins" in the left nav and check "Link to Game and Transmit Position".
5. (Optional) Navigate to "Audio Output" in the left nav and set the following recommended settings under Positional Audio:
   * [x] Headphones
   * Minimum Distance: `1.0m`
   * Bloom: `150%`
   * Maximum Distance: `75.0m`
   * Minimum Volume: `0%`
6. Save the configuration by clicking "Ok" at the bottom.


### Mod Setup

1. Open RML Launcher.
2. Click "Mods Folder" on the right.
3. Download `MumbleLink.cs` by clicking "Download this mod" in the sidebar.
4. Copy `MumbleLink.cs` to the `mods` folder.
5. Run Raft via RML Launcher.
6. Under "Mod Manager", enable the "Mumble Link" mod.
7. (Optional) Click the `i` button next to "Mumble Link" and check "Load this mod at startup".

Done!
It may take a few seconds to connect to Mumble after entering the world.
Other players in your Mumble room will hear your voice from your character and vice versa.
