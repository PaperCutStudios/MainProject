local serverFuncs = {}
function serverFuncs.SendDifficulty() 
    local gameprefs = require("gamePrefs")
    for key,value in pairs(gameprefs.Players) do
        local player = require("player"..value)
        player.Socket:send("5"..GamePrefs.Difficulty);
    end
end
function serverFuncs.SendTime()
    local gameprefs = require("gamePrefs")
    for key,value in pairs(gameprefs.Players) do
        local player = require("player"..value)
        player.Socket:send("6"..GamePrefs.Time);
    end
end
return serverFuncs
