local ServerFuncs1 = {}

function ServerFuncs1.InitialJoin (SocketPass)
    print("player requesting join")
    local gamePrefs = require("gamePrefs")
    local playernum = gamePrefs.GetAvailablePlayer()
    print("GamePrefs is returning: "..playernum)
    if(playernum == 0) then
        print("Sending 0 as playernum")
        SocketPass:send("0")
    else
        print("Attemping to give player number")
        local player = require("player"..playernum)
        player.connect(SocketPass)
        player.Socket:send("1"..player.ID..gamePrefs.Difficulty..gamePrefs.Time)
        table.insert(gamePrefs.Players, player.ID)
    end
end



return ServerFuncs1
