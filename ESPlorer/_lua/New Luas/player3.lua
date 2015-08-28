local player={}
player.id=3
player.answer=""
player.socket=""
function player.connect(skt)
    player.socket=skt
    --Ideally, this is what would be sent: 1,player.id,prefs.diff,prefs.time
    skt:send("132300")
   	print("Player 3 Connected")
	require("lights").on("yellow")
end
function player.disconnect()
	print("Player 3 has Disconnected")
    require("prefs").removeplayer(player.id)
	require("lights").off("yellow")
end
function player.addanswer(answer)
	require("prefs").addanswer()
end
return player
