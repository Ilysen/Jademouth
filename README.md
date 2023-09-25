# Jademouth

This is a mod for Caves of Qud that adds a new town, called Jademouth. Feel free to use the code here for learning or reference; everything is documented pretty well. For more a thorough description including screenshots, check the [Steam Workshop page](https://steamcommunity.com/sharedfiles/filedetails/?id=2926820352)!

Jademouth is licensed under the [GNU General Public License v3](http://www.gnu.org/licenses/agpl.html), which can be found in full in [LICENSE.md](LICENSE.md). Code prior to commit `a234e336a9234842b05156b88800a2627d7ea8f2` is licensed under the [MIT License](https://opensource.org/license/mit/) instead.

## Credits

* **Ilysen** - Code, mapping, and most of the writing
* **Lamb** - All of the sprites, and some help with the writing

## Changelog

### 25 September, 2023
#### Version 1.2
**This version won't be publicly released until a save-breaking update has been pushed to the stable branch, to avoid bricking existing saves.**

* Prospectors now sell 3-5 rough jade, up from 1-3.
* Did a polish and writing pass on all the descriptions and dialogue. Most things are unchanged, but certain options are slightly different, and some new ones have been added.
* Overhauled the way that rewards are selected for Chaos Theory. It now uses a "select multiple" popup like giving books to Sheba or tossing artifacts into the sacred well.
* Internally changed almost everything in the mod to use a consistent naming scheme. __This is incompatible with saves__ but should serve to fully future-proof any potential ID conflicts.

### 19 September, 2023

#### Version 1.1.5
* Prospector detail color now appears as bright green when they have jade to sell, hopefully making shopping easier. Existing saves won't break, but a new save is required for the functionality to work.
* Prospectors no longer keep stuff traded to them forever, and instead have bartered items disappear upon restock like other merchants. (thanks books!)

### 16 September, 2023

#### Version 1.1.4
* Hotfix for an API change in the latest update - should cause no trouble with saves.

### 12 September, 2023

#### Version 1.1.3
* Fixed some display issues in ASCII mode.
* Fixed a long-standing issue with jade glittermensches having a white detail color - now it shows as dark green.
* Removed an old unused debug map, to trim unnecessary file size.

### 31 August, 2023

#### Version 1.1.2
* Made prospectors sell rough jade again. What can I say, people liked it!
* Fixed some grammar regressions in the descriptions for prospectors and the Jademouth overmap tile.

#### Version 1.1.1
* Fixed a syntax error that would cause the mod to fail to load on the stable branch. Oops.

#### Version 1.1
Note: 1.1 onwards is licensed under GNU General Public License v3. Version 1.0 remains licensed under the MIT License.

* Chaos Theory now awards a lump sum of XP at the end of the quest, instead of giving some after each step.
* Merged the "find a dram of warm static" and "return the warm static to Bright" stages of Chaos Theory; the quest now has two steps instead of three.
* Jademouth Gravy has been revamped with a new effect: `10-15% max HP, +300 reputation with mollusks`. The previous effect was `+2 AV, +4 DV`.
* Jademouth Gravy no longer requires congealed skulk to cook.
* Prospectors now sell polished jade instead of rough jade.
* Jademouth's rock tumbler now spawns empty.
* The pumping station now draws fresh water from a nearby spring, instead of from the pool of salt water.
* Revamped a bunch of the writing, include a near-complete overhaul of Bright's dialogue to be more aligned with the base game's tone.
* General code cleanup and modernization pass.
* Added a readme.
* Fixed numerous map bugs.

### 7 February, 2023 (version 1.0)
* Initial release.
