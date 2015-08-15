local ServerFuncs1 = {}

function ServerFuncs1.InitialJoin (SocketPass)
    local gamePrefs = require("gamePrefs")
    if(gamePrefs.NumPlayers <4) then
        local player = require("player"..gamePrefs.NumPlayers+1)
        player.SetSocket(SocketPass)
        player.Socket:send("1"..player.ID..gamePrefs.Difficulty..gamePrefs.Time)
        gamePrefs.NumPlayers = gamePrefs.NumPlayers + 1;
    else
        SocketPass:send("0")
    end
end



return ServerFuncs1
