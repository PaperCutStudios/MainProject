local player={}
player.id=4
player.answer=""
player.socket=""
function player.connect(skt)
    player.socket=skt
    prefs=require("prefs")
    print("Ideally, this is what would be sent: 1",player.id,prefs.diff,prefs.time)
    skt:send("142300")
   	print("Player 4 Connected")
	require("lights").on("green")
end
function player.disconnect()
	print("Player 4 has Disconnected")
	require("lights").off("green")
end
function player.addanswer(answer)
	require("prefs").addanswer()
end
return player
