local player={}
player.id=1
player.answer=""
player.socket=""
function player.connect(skt)
    player.socket=skt
    prefs=require("prefs")
    print("Ideally, this is what would be sent: 1",player.id,prefs.diff,prefs.time)
    skt:send("112300")
   	print("Player 1 Connected")
	require("lights").on("green")
end
function player.disconnect()
	print("Player 1 has Disconnected")
	require("lights").off("green")
end
function player.addanswer(answer)
	require("prefs").addanswer()
end
return player
