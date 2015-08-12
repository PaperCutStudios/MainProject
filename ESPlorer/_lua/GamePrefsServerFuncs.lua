local serverFuncs = {}
function serverFuncs.SendGameStart()
    local GamePrefs = require("gamePrefs")
    for i=1,GamePrefs.NumPlayers do
        local player = require("player"..i)
        player.Socket:send("2"..GamePrefs.Time);
    end
end
function serverFuncs.SendDifficulty() 
    local GamePrefs = require("gamePrefs")
    for i=1,GamePrefs.NumPlayers do
        local player = require("player"..i)
        player.Socket:send("3"..GamePrefs.Difficulty);
    end
end
function serverFuncs.SendTime()
    local GamePrefs = require("gamePrefs")
    for i=1,GamePrefs.NumPlayers do
        local player = require("player"..i)
        player.Socket:send("4"..GamePrefs.Time);
    end
end
return serverFuncs