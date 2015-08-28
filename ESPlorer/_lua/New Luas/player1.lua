local player={}
player.id=1
player.answer=""
player.socket=""
function player.connect(skt)
    player.socket=skt
    --Ideally, this is what would be sent: 1,player.id,prefs.diff,prefs.time
    skt:send("112300")
   	print("Player 1 Connected")
	require("lights").on("blue")
end
function player.disconnect()
	print("Player 1 has Disconnected")
    require("prefs").removeplayer(player.id)
	require("lights").off("blue")
end
function player.addanswer(answer)
	require("prefs").addanswer()
end
return player
