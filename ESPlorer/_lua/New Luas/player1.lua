local player={}
player.id=2
player.answer=""
player.socket=""
function player.connect(skt)
	player.socket=skt
	print("11"..require("prefs").getallstart())
	print("Player 1 Connected")
	require("lights").on("blue")
end
function player.disconnect()
	print("Player 1 has Disconnected")
	require("lights").off("blue")
end
function player.addanswer(answer)
	require("prefs").addanswer()
end
return player
