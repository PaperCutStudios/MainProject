local player={}
player.id=2
player.answer=""
player.socket=""
function player.connect(skt)
    player.socket=skt
    --Ideally, this is what would be sent: 1,player.id,prefs.diff,prefs.time
    skt:send("122300")
   	print("Player 2 Connected")
	require("lights").on("green")
end
function player.disconnect()
	print("Player 2 has Disconnected")
    require("prefs").removeplayer(player.id)
	require("lights").off("green")
end
function player.addanswer(answer)
	require("prefs").addanswer()
end
return player
