Just an extra quick note, the Node we have submitted doesn't have the absolute most recent luas uploaded to it (slight changes were made after leaving the device with Emma to submit, mostly dealing with players disconnecting halfway through a game). I'd suggest trying to play with whats on there first and then maybe uploading to the Node all the files in ESPlorer/_lua/New Luas in case the changes cause issues (should work in theory but without the node cannot test).

If the changes cause the a panic loop, there is a safety timer installed that gives you 3 seconds to send to the device file.remove("init.lua"), which should interrupt the loop on its next iteration.

Also, just to clarify, the LUA scripts that control the node device are stored in ESPlorer/_lua/New luas/. The /OldLuas folder contains files from before we went through and tried to change everything to be slightly less memory intensive (based on recommendations from: http://www.esp8266.com/wiki/doku.php?id=nodemcu-unofficial-faq). The old lua scripts were what we used for the IGDA playtesting session on Tuesday (but wouldn't allow more than 3 players to connect).

---	Prep	---

1. Connect the Node device to a computer via micro-usb
	1a. Preferably, the computer should be running ESPlorer or similar to check debugging messages as the node outputs them

2. Install the Conversity build on to the devices that will be playing (included is both an .apk for Android devices and an .exe for Windows for troubleshooting potential wireless issues)

3. Wait for the blue light to turn off (indicating a successful launch of the Init)

4. Connect each device to the Conversity WiFi (see issue #1)
	ssid = Conversity
	password = Conversi

5. Once you have 4 connected devices, you are ready to play!

---	Play	---

1. One at a time (to avoid potential issues with flooding the device), each player should tap the "connect" button on the main screen

2. Now on the set-up screen, the device should tell the player if they have successfully connected (issue #1), the time limit of the match they are about to play and the difficulty of the rules they are about to use (issue #2)

3. When all players have reached this screen, the "play" button (fig #1: P = Play) on the Node device should be pressed to begin the game (issue #1)

--Fig #1--

__________
| o [] o |
| P [] o |
|	 |
| o o o o|
----------

4. Screen should then switch to the information screen, showing when the player is free, what activities they want to do and the rules they must abide by. Also, a clock is shown to indicate how much longer til the end of the game

5. When the time runs out/the players press the "play" button on the device/the debug-endgame button, an alarm will sound indicating the beginning of the answering phase, in which players must, without talking to one another, enter their answers by tapping the activity they will go to and the time they will be there. 

6. Once all parts are selected, the Submit Answers button will appear, which will send the player's answer to the device for processing (issue #1)

7. The device will calculate how many people have gone to the same place at the same time for each player and send back this number.

8. The players' games will then swtich to the results screen, presenting them with an image showing where they went and with how many people as well as a diary entry summing up the player's day out.
	8a. If Issue #1 occurs, the game cannot switch the the results screen as that depends on being able receive the other players' answers which cannot happen if the device can't communicate with the node

---	Replaying	---

In order to attempt to play the game again, a hard reset must be carried out upon the node the reset some key values. This also then requires the players to ensure they are still connected to Conversity's WiFi.

---	Known Issues	---

#1. Most devices will randomly disconnect themselves from Conversity's wifi, causing many synchronisation issues throughout the game
	1a. Disconnected devices will not respond to the GameStart button press on the device. To troubleshoot this, on the setup screen there is a button on the bottom of the screen that will start the game locally
		1ai. If a device has disconnected before it could receive the random seed from the device, the game becomes fairly unplayble on that device as its no longer sharing the same information
			-This can be tracked through ESPlorer as it outputs a message each time it has sent the seed to a player
	
	1b. Disconnected devices won't be able to send their answer to the device, causing a desync and for the screen to never change to the results screen on any device (as only when all players who initially connected send their 	answer will it perform the answer calculation)

#2. At a certain depth within the LUA code, the ".." operator with numbers causes the node to crash. This means that numbers cannot be concatenated to the strings being sent between the devices. The area this issues most greatly effects is the ability to change the setup settings, which were intended to cycle the difficulty number and add/minus 30seconds from the time limit in the node's settings when certain buttons were pressed on the node device.
	- The code for this change can be found in the LUAs/Old Luas/ButtonFunctions file, from an early time where concatenation would work for numbers at all levels of code

---	Tool	---
The RulesReader takes the easily readable rules excel file and saves its values to an XML for use in the game. To use:

1. Download the rules spreadsheet from Papercut Studios' Google drive
2. Place this along-side the RulesReader.exe
3. Run RulesReader.exe
4. Copy Rules.xml to Assets/Resources
5. Comeplete! the rules.xml has been successfully updated and added to the project.